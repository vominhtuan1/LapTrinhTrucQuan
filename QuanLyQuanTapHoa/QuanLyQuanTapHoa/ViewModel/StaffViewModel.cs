using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using QuanLyQuanTapHoa.ViewModel;
using QuanLyQuanTapHoa.UserControls;
using System.Windows;
using QuanLyQuanTapHoa.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Media;
using System.Security.Cryptography;
using System.Globalization;

namespace QuanLyQuanTapHoa.ViewModel
{

    class StaffViewModel : BaseViewModel
    {
        private ObservableCollection<NhanVien> _StaffList;
        public ObservableCollection<NhanVien> StaffList { get => _StaffList; set { _StaffList = value; OnPropertyChanged(); } }

        private List<TaiKhoan> _AccountList;
        public List<TaiKhoan> AccountList { get => _AccountList; set => _AccountList = value; }

        private List<ChucVu> _RoleList;
        public List<ChucVu> RoleList { get => _RoleList; set => _RoleList = value; }

        public int id_selected = -1;
        public bool isUpdateStaffSuccess = false;
        public bool isAddStaffSuccess = false;
        public bool isFistVisible = false;
        public SHA256 sha256Hash = SHA256.Create();

        private BackgroundWorker worker;


        #region commands
        public ICommand OpenAddStaff { get; set; }
        public ICommand OpenEditStaff { get; set; }
        public ICommand LoadCommand { get; set; }
        public ICommand EditStaffCommand { get; set; }
        public ICommand DeleteStaffCommand { get; set; }
        public ICommand AddStaff { get; set; }
        public ICommand BringProductBackToWarehouse { get; set; }
        public ICommand SelectionChanged { get; set; }
        public ICommand SubmitAddStaff { get; set; }
        public ICommand SearchCommand { get; set; }
        #endregion


        public StaffViewModel()
        {
            OpenAddStaff = new RelayCommand<ItemsControl>((p) => { return true; }, (p) => { OpenAddStaffWD(p); });
            OpenEditStaff = new RelayCommand<StaffDetailControl>((p) => { return true; }, (p) => { OpenEditStaffWD(p); });
            LoadCommand = new RelayCommand<StaffControl>((p) =>
            {
                if (p.IsVisible == true && isFistVisible == false)
                {
                    isFistVisible = true;
                    return true;
                }
                else
                    return false;
            }, (p) => { Load(p); });
            EditStaffCommand = new RelayCommand<EditStaffWindow>((p) => { return true; }, (p) => { EditStaff(p); if (isUpdateStaffSuccess) p.Close(); });
            DeleteStaffCommand = new RelayCommand<StaffDetailControl>((p) => { return true; }, (p) => { DeleteStaff(p); });
            SubmitAddStaff = new RelayCommand<AddStaffWindow>((p) => { return true; }, (p) => { SubmitAdd(p); if (isAddStaffSuccess) p.Close(); });
            SearchCommand = new RelayCommand<StaffControl>((p) => { return true; }, (p) => { SearchStaff(p); });

        }

        public void OpenAddStaffWD(ItemsControl p)
        {
            AddStaffWindow addStaffWindow = new AddStaffWindow();
            addStaffWindow.ShowDialog();
            if (isAddStaffSuccess)
            {
                int index = StaffList.Count;
                NhanVien a = new NhanVien();
                a = StaffList[index - 1];
                AddStaffToScreen(a, p);
                isAddStaffSuccess = false;
            }

        }

        public void OpenEditStaffWD(StaffDetailControl staffDetail)
        {
            id_selected = int.Parse(staffDetail.txbID.Text);
            //MessageBox.Show(id_selected.ToString());
            int n = StaffList.Count;
            NhanVien s = new NhanVien();
            for (int i = 0; i < n; i++)
            {
                if (StaffList[i].MaNhanVien == id_selected)
                {
                    s = StaffList[i];
                    break;
                }
            }
            EditStaffWindow edit = new EditStaffWindow();
            edit.txbStaffName.Text = s.HoTen;
            edit.cbStaffSex.SelectedIndex = (s.GioiTinh == "Nam") ? 0 : 1;
            edit.txbStaffBY.Text = s.NamSinh.ToString();
            edit.txbStaffPhone.Text = s.SoDienThoai;
            edit.cbStaffRole.SelectedIndex = s.MaChucVu - 1;
            edit.txbStaffSalary.Text = s.Luong.ToString();
            edit.txbStaffUser.Text = s.TaiKhoan.Username;
            //edit.txbStaffPass.Text = s.TaiKhoan.Password;
            edit.ShowDialog();

            // Update screen after updated staff
            if (isUpdateStaffSuccess)
            {
                staffDetail.txbStaffName.Text = edit.txbStaffName.Text;
                staffDetail.txbStaffBY.Text = edit.txbStaffBY.Text;
                staffDetail.txbStaffSex.Text = edit.cbStaffSex.Text;
                staffDetail.txbStaffPhone.Text = edit.txbStaffPhone.Text;
                staffDetail.txbStaffSalary.Text = FormatNumber(edit.txbStaffSalary.Text) + " VND";
                staffDetail.txbStaffRole.Text = edit.cbStaffRole.Text;
                staffDetail.txbStaffAccountName.Text = edit.txbStaffUser.Text;
                isUpdateStaffSuccess = false;
            }

        }
        public void AddStaffToScreen(NhanVien a, ItemsControl p)
        {
            StaffDetailControl b = new StaffDetailControl();
            b.txbID.Text = a.MaNhanVien.ToString();
            b.txbStaffName.Text = a.HoTen;
            b.txbStaffSex.Text = a.GioiTinh;
            b.txbStaffBY.Text = a.NamSinh.ToString();
            b.txbStaffPhone.Text = a.SoDienThoai;
            b.txbStaffRole.Text = a.ChucVu.TenChucVu;
            b.txbStaffAccountName.Text = a.TaiKhoan.Username;
            b.txbStaffSalary.Text = FormatNumber(a.Luong.ToString()) + " VND";
            p.Items.Add(b);
        }

        public void Load(StaffControl p)
        {
            worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            worker.RunWorkerAsync(p);
        }
        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            StaffControl p = (StaffControl)e.Result;
            p.progressBar.Visibility = Visibility.Hidden;
            p.staffList.Visibility = Visibility.Visible;
        }
        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            StaffControl p = (StaffControl)e.Argument;
            System.Windows.Threading.Dispatcher settingDispatcher = p.Dispatcher;
            StaffList = new ObservableCollection<NhanVien>(DataProvider.Ins.DB.NhanViens);
            AccountList = new List<TaiKhoan>(DataProvider.Ins.DB.TaiKhoans);
            RoleList = new List<ChucVu>(DataProvider.Ins.DB.ChucVus);

            foreach (NhanVien i in StaffList)
            {
                UpdateUi update = new UpdateUi(AddStaffToScreen);
                settingDispatcher.BeginInvoke(update, i, p.staffList);
            }
            e.Result = p;
        }
        public delegate void UpdateUi(NhanVien a, ItemsControl p);
        public bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }
        public void EditStaff(EditStaffWindow edit)
        {
            if (!IsAccountValid(edit)) return;
            var staff = DataProvider.Ins.DB.NhanViens.Where(x => x.MaNhanVien == id_selected).SingleOrDefault();

            staff.HoTen = edit.txbStaffName.Text;
            staff.GioiTinh = edit.cbStaffSex.Text;
            staff.NamSinh = int.Parse(edit.txbStaffBY.Text);
            staff.SoDienThoai = edit.txbStaffPhone.Text;
            staff.MaChucVu = edit.cbStaffRole.SelectedIndex + 1;

            var acc = DataProvider.Ins.DB.TaiKhoans.Where(x => x.MaTaiKhoan == staff.MaTaiKhoan).SingleOrDefault();
            acc.Username = edit.txbStaffUser.Text;
            acc.Password = GetHash(sha256Hash ,edit.txbStaffPass.Text);

            id_selected = -1;
            isUpdateStaffSuccess = true;
            DataProvider.Ins.DB.SaveChanges();
            AccountList.Clear();
            AccountList = new List<TaiKhoan>(DataProvider.Ins.DB.TaiKhoans);
            StaffList.Clear();
            StaffList = new ObservableCollection<NhanVien>(DataProvider.Ins.DB.NhanViens);
            CustomMessageBox.CustomMessageBox.Show("Cập nhập thông tin nhân viên thành công!", 3);
        }
        public void DeleteStaff(StaffDetailControl staffDetail)
        {
            string s = "Bạn muốn xóa nhân viên có mã số " + staffDetail.txbID.Text + " không?";
            MessageBoxResult result = CustomMessageBox.CustomMessageBox.Show(s, 2);
            if (result == MessageBoxResult.Yes)
            {
                int id = int.Parse(staffDetail.txbID.Text);
                foreach (NhanVien staff in StaffList)
                {
                    if (staff.MaNhanVien == id)
                    {
                        foreach (TaiKhoan acc in AccountList)
                        {
                            if (acc.MaTaiKhoan == staff.MaTaiKhoan)
                            {
                                DataProvider.Ins.DB.TaiKhoans.Remove(acc);
                                break;
                            }
                        }

                        DataProvider.Ins.DB.NhanViens.Remove(staff);
                        DataProvider.Ins.DB.SaveChanges();
                        AccountList.Clear();
                        AccountList = new List<TaiKhoan>(DataProvider.Ins.DB.TaiKhoans);
                        StaffList.Clear();
                        StaffList = new ObservableCollection<NhanVien>(DataProvider.Ins.DB.NhanViens);


                        ItemsControl p = (ItemsControl)staffDetail.Parent;
                        p.Items.Remove(staffDetail);
                        break;
                    }
                }
                CustomMessageBox.CustomMessageBox.Show("Bạn đã xóa nhân viên!", 3);
            }

        }
        public void SubmitAdd(AddStaffWindow add)
        {

            if (!IsAccountValid(add)) return;
            
            TaiKhoan acc = new TaiKhoan() { Username = add.txbStaffUser.Text, Password = GetHash(sha256Hash, add.txbStaffPass.Text) };
            DataProvider.Ins.DB.TaiKhoans.Add(acc);
            DataProvider.Ins.DB.SaveChanges();
            AccountList.Clear();
            AccountList = new List<TaiKhoan>(DataProvider.Ins.DB.TaiKhoans);
            var a = DataProvider.Ins.DB.TaiKhoans.ToList().LastOrDefault();

            var staff = new NhanVien()
            {
                MaTaiKhoan = a.MaTaiKhoan,
                HoTen = add.txbStaffName.Text,
                MaChucVu = add.cbStaffRole.SelectedIndex + 1,
                GioiTinh = add.cbStaffSex.Text,
                NamSinh = int.Parse(add.txbStaffBY.Text),
                SoDienThoai = add.txbStaffPhone.Text,
                Luong = int.Parse(add.txbStaffSalary.Text),

            };

            DataProvider.Ins.DB.NhanViens.Add(staff);
            DataProvider.Ins.DB.SaveChanges();
            StaffList.Clear();
            StaffList = new ObservableCollection<NhanVien>(DataProvider.Ins.DB.NhanViens);

            isAddStaffSuccess = true;
            CustomMessageBox.CustomMessageBox.Show("Thêm nhân viên thành công!", 3);

        }

        // Kiểm tra trường hợp add
        public bool IsAccountValid(AddStaffWindow staff)
        {
            // Kiểm tra trường hợp nhập thiếu
            if (String.IsNullOrEmpty(staff.txbStaffName.Text) || String.IsNullOrEmpty(staff.txbStaffBY.Text) || String.IsNullOrEmpty(staff.txbStaffPhone.Text)
                || String.IsNullOrEmpty(staff.txbStaffSalary.Text) || String.IsNullOrEmpty(staff.txbStaffUser.Text) || String.IsNullOrEmpty(staff.txbStaffPass.Text)
                || staff.cbStaffRole.SelectedIndex == -1 || staff.cbStaffSex.SelectedIndex == -1)
            {
                CustomMessageBox.CustomMessageBox.Show("Vui lòng nhập thông tin đầy đủ", 1);
                return false;
            }
            // Kiểm tra trường hợp nhập năm sinh
            if (IsDigitsOnly(staff.txbStaffBY.Text))
            {
                DateTime now = DateTime.Now;
                if (int.Parse(staff.txbStaffBY.Text) > now.Year)
                {
                    CustomMessageBox.CustomMessageBox.Show("Năm sinh phải bé hơn năm hiện tại", 1);
                    return false;
                }
            }
            else
            {
                CustomMessageBox.CustomMessageBox.Show("Năm sinh phải là số nguyên", 1);
                return false;
            }
            // Kiểm tra trường hợp nhập sdt
            if (!IsDigitsOnly(staff.txbStaffPhone.Text))
            {
                CustomMessageBox.CustomMessageBox.Show("Số điện thoại phải là số nguyên", 1);
                return false;
            }
            // Kiểm tra trường hợp nhập lương
            if (!IsDigitsOnly(staff.txbStaffSalary.Text))
            {
                CustomMessageBox.CustomMessageBox.Show("Lương phải là số nguyên", 1);
                return false;
            }
            // Kiểm tra trường hợp trùng tên tài khoản
            foreach (TaiKhoan acc in AccountList)
            {
                if (acc.Username == staff.txbStaffUser.Text)
                {
                    CustomMessageBox.CustomMessageBox.Show("Tài khoản đã tồn tại", 1);
                    return false;
                }
            }

            return true;
        }

        // Kiểm tra cho trường hợp edit
        public bool IsAccountValid(EditStaffWindow staff)
        {
            // Kiểm tra trường hợp nhập thiếu
            if (String.IsNullOrEmpty(staff.txbStaffName.Text) || String.IsNullOrEmpty(staff.txbStaffBY.Text) || String.IsNullOrEmpty(staff.txbStaffPhone.Text)
                || String.IsNullOrEmpty(staff.txbStaffSalary.Text) || String.IsNullOrEmpty(staff.txbStaffUser.Text) || String.IsNullOrEmpty(staff.txbStaffPass.Text)
                || staff.cbStaffRole.SelectedIndex == -1 || staff.cbStaffSex.SelectedIndex == -1)
            {
                CustomMessageBox.CustomMessageBox.Show("Vui lòng nhập thông tin đầy đủ", 1);
                return false;
            }
            // Kiểm tra trường hợp nhập năm sinh
            if (IsDigitsOnly(staff.txbStaffBY.Text))
            {
                DateTime now = DateTime.Now;
                if (int.Parse(staff.txbStaffBY.Text) > now.Year)
                {
                    CustomMessageBox.CustomMessageBox.Show("Năm sinh phải bé hơn năm hiện tại", 1);
                    return false;
                }
            }
            else
            {
                CustomMessageBox.CustomMessageBox.Show("Năm sinh phải là số nguyên", 1);
                return false;
            }
            // Kiểm tra trường hợp nhập sdt
            if (!IsDigitsOnly(staff.txbStaffPhone.Text))
            {
                CustomMessageBox.CustomMessageBox.Show("Số điện thoại phải là số nguyên", 1);
                return false;
            }
            // Kiểm tra trường hợp nhập lương
            if (!IsDigitsOnly(staff.txbStaffSalary.Text))
            {
                CustomMessageBox.CustomMessageBox.Show("Lương phải là số nguyên", 1);
                return false;
            }
            // Kiểm tra trường hợp trùng tên tài khoản
            var s = DataProvider.Ins.DB.NhanViens.Where(x => x.MaNhanVien == id_selected).SingleOrDefault();
            foreach (TaiKhoan acc in AccountList)
            {
                if (acc.Username == staff.txbStaffUser.Text && acc.MaTaiKhoan != s.MaTaiKhoan)
                {
                    CustomMessageBox.CustomMessageBox.Show("Tài khoản đã tồn tại", 1);
                    return false;
                }
            }

            return true;
        }
        // Search nhân viên theo họ tên
        public void SearchStaff(StaffControl staff)
        {
            string a = staff.txbSearch.Text.ToLower();
            staff.staffList.Items.Clear();
            foreach (NhanVien i in StaffList)
            {
                string b = i.HoTen.ToLower();
                if (b.Contains(a))
                {
                    AddStaffToScreen(i, staff.staffList);
                }
            }
        }

        // Encode Password
        public static string GetHash(HashAlgorithm hashAlgorithm, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            var sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }
        public string FormatNumber(string a)
        {
            CultureInfo cul = CultureInfo.GetCultureInfo("vi-VN");
            return double.Parse(a).ToString("#,###", cul.NumberFormat);
        }
    }
}
