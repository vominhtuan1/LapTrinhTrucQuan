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
using System.Globalization;

namespace QuanLyQuanTapHoa.ViewModel
{
    class DiscountViewModel : BaseViewModel
    {
        private ObservableCollection<GiamGia> _DiscountList;
        public ObservableCollection<GiamGia> DiscountList { get => _DiscountList; set { _DiscountList = value; OnPropertyChanged(); } }


        public bool isUpdateDiscountSuccess = false;
        public bool isAddDiscountSuccess = false;
        public bool isFistVisible = false;

        private BackgroundWorker worker;


        #region commands
        public ICommand OpenAddDiscount { get; set; }
        public ICommand LoadCommand { get; set; }
        public ICommand EditDiscountCommand { get; set; }
        public ICommand DeleteDiscountCommand { get; set; }
        public ICommand AddDiscount { get; set; }
        public ICommand BringProductBackToWarehouse { get; set; }
        public ICommand SelectionChanged { get; set; }
        public ICommand SubmitAddDiscount { get; set; }
        #endregion


        public DiscountViewModel()
        {
            OpenAddDiscount = new RelayCommand<ItemsControl>((p) => { return true; }, (p) => { OpenAddDiscountWD(p); });
            LoadCommand = new RelayCommand<DiscountControl>((p) =>
            {
                if (p.IsVisible == true && isFistVisible == false)
                {
                    isFistVisible = true;
                    return true;
                }
                else
                    return false;
            }, (p) => { Load(p); });
            DeleteDiscountCommand = new RelayCommand<DiscountDetailControl>((p) => { return true; }, (p) => { DeleteDiscount(p); });
            SubmitAddDiscount = new RelayCommand<AddDiscountWindow>((p) => { return true; }, (p) => { SubmitAdd(p); if (isAddDiscountSuccess) p.Close(); });

        }

        public void OpenAddDiscountWD(ItemsControl p)
        {
            AddDiscountWindow addDiscountWindow = new AddDiscountWindow();
            addDiscountWindow.ShowDialog();
            if (isAddDiscountSuccess)
            {
                int index = DiscountList.Count;
                GiamGia a = new GiamGia();
                a = DiscountList[index - 1];
                AddDiscountToScreen(a, p, index);
                isAddDiscountSuccess = false;
            }
        }

        public void AddDiscountToScreen(GiamGia a, ItemsControl p, int count)
        {
            DiscountDetailControl b = new DiscountDetailControl();
            b.tbSTT.Text = count.ToString();
            b.tbCoupoun.Text = a.Coupoun;
            b.tbNgayBatDau.Text = a.NgayBatDau.Value.ToString("dd/M/yyyy");
            b.tbNgayKetThuc.Text = a.NgayKetThuc.Value.ToString("dd/M/yyyy");
            b.tbDonHangTu.Text = a.DonHangTu.ToString();
            b.tbSoTienGiam.Text = a.SoTienGiam.ToString();
            p.Items.Add(b);
        }

        public void Load(DiscountControl p)
        {
            worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            worker.RunWorkerAsync(p);
        }
        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            DiscountControl p = (DiscountControl)e.Result;
            p.progressBar.Visibility = Visibility.Hidden;
            p.discountList.Visibility = Visibility.Visible;
        }
        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            int count = 1;
            DiscountControl p = (DiscountControl)e.Argument;
            System.Windows.Threading.Dispatcher settingDispatcher = p.Dispatcher;
            DiscountList = new ObservableCollection<GiamGia>(DataProvider.Ins.DB.GiamGias);

            int index = 0;
            while (index < DiscountList.Count)
            {
                GiamGia i = DiscountList[index];
                DateTime dateNow = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                //DateTime dateNow = DateTime.Now;
                if (DateTime.Compare(dateNow, Convert.ToDateTime(i.NgayKetThuc)) > 0)
                {
                    DataProvider.Ins.DB.GiamGias.Remove(i);
                    DataProvider.Ins.DB.SaveChanges();
                    DiscountList.Clear();
                    DiscountList = new ObservableCollection<GiamGia>(DataProvider.Ins.DB.GiamGias);
                    string s = "Mã giảm giá " + i.Coupoun + " đã hết hạn !!";
                    ShowMessage showMess = new ShowMessage(ShowMessageBox);
                    settingDispatcher.BeginInvoke(showMess, s);
                    //ShowMessagerBox(s);
                }
                else index++;
            }


            foreach (GiamGia i in DiscountList)
            {
                UpdateUi update = new UpdateUi(AddDiscountToScreen);
                settingDispatcher.BeginInvoke(update, i, p.discountList, count);
                count++;
            }
            e.Result = p;
        }
        public void ShowMessageBox(string s)
        {
            CustomMessageBox.CustomMessageBox.Show(s, 3);
        }
        public delegate void ShowMessage(string s);
        public delegate void UpdateUi(GiamGia a, ItemsControl p, int count);
        public bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }
        public void DeleteDiscount(DiscountDetailControl discountDetail)
        {
            string s = "Bạn muốn xóa Giảm giá có mã " + discountDetail.tbCoupoun.Text + " không?";
            MessageBoxResult result = CustomMessageBox.CustomMessageBox.Show(s, 2);
            if (result == MessageBoxResult.Yes)
            {
                string ss = discountDetail.tbCoupoun.Text;
                foreach (GiamGia discount in DiscountList)
                {
                    if (discount.Coupoun == ss)
                    {
                        DataProvider.Ins.DB.GiamGias.Remove(discount);
                        DataProvider.Ins.DB.SaveChanges();
                        DiscountList.Clear();
                        DiscountList = new ObservableCollection<GiamGia>(DataProvider.Ins.DB.GiamGias);

                        ItemsControl p = (ItemsControl)discountDetail.Parent;
                        p.Items.Remove(discountDetail);
                        break;
                    }
                }
                CustomMessageBox.CustomMessageBox.Show("Bạn đã xóa mã giảm giá!", 3);
            }

        }
        public void SubmitAdd(AddDiscountWindow add)
        {

            if (!IsAccountValid(add)) return;
            var a = DataProvider.Ins.DB.GiamGias.ToList().LastOrDefault();
            var Discount = new GiamGia()
            {
                IdGiamGia = 1,
                Coupoun = add.tbCoupoun.Text,
                NgayBatDau = Convert.ToDateTime(add.tbNgayBatDau.Text),
                NgayKetThuc = Convert.ToDateTime(add.tbNgayKetThuc.Text),
                DonHangTu = int.Parse(add.tbDonHangTu.Text),
                SoTienGiam = int.Parse(add.tbSoTienGiam.Text),
            };
            DataProvider.Ins.DB.GiamGias.Add(Discount);
            DataProvider.Ins.DB.SaveChanges();
            DiscountList.Clear();
            DiscountList = new ObservableCollection<GiamGia>(DataProvider.Ins.DB.GiamGias);

            isAddDiscountSuccess = true;
            CustomMessageBox.CustomMessageBox.Show("Thêm mã khuyến mãi thành công!", 3);

        }

        // Kiểm tra trường hợp add
        public bool IsAccountValid(AddDiscountWindow Discount)
        {
            // Kiểm tra trường hợp nhập thiếu
            if (String.IsNullOrEmpty(Discount.tbCoupoun.Text) || String.IsNullOrEmpty(Discount.tbNgayBatDau.Text)
                || String.IsNullOrEmpty(Discount.tbNgayKetThuc.Text) || String.IsNullOrEmpty(Discount.tbDonHangTu.Text) || String.IsNullOrEmpty(Discount.tbSoTienGiam.Text))
            {
                CustomMessageBox.CustomMessageBox.Show("Vui lòng nhập thông tin đầy đủ", 1);
                return false;
            }
            string ss = Discount.tbCoupoun.Text;
            foreach (GiamGia discount in DiscountList)
            {
                if (discount.Coupoun == ss)
                {
                    CustomMessageBox.CustomMessageBox.Show("Mã giảm giá đã tồn tại", 1);
                    return false;
                }
            }
            if (!IsDigitsOnly(Discount.tbDonHangTu.Text) | !IsDigitsOnly(Discount.tbSoTienGiam.Text))
            {
                CustomMessageBox.CustomMessageBox.Show("Nhập tiền là số nguyên", 1);
                return false;
            }
            // Kiểm tra Ngày bắt đầu và kết thúc
            DateTime dateNow = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            DateTime dateFirst = Convert.ToDateTime(Discount.tbNgayBatDau.Text);
            DateTime dateEnd = Convert.ToDateTime(Discount.tbNgayKetThuc.Text);
            if (DateTime.Compare(dateFirst, dateNow) < 0)
            {
                CustomMessageBox.CustomMessageBox.Show("Nhập ngày bắt đầu lớn hơn bằng ngày hiện tại", 1);
                return false;
            }
            if (DateTime.Compare(dateEnd, dateFirst) < 0)
            {
                CustomMessageBox.CustomMessageBox.Show("Nhập ngày bắt đầu bé hơn ngày kết thúc khuyến mãi", 1);
                return false;
            }

            return true;
        }

    }

}
