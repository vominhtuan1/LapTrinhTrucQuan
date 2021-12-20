using QuanLyQuanTapHoa.Model;
using QuanLyQuanTapHoa.UserControls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Windows.Media;
using System.ComponentModel;
using System.Globalization;

namespace QuanLyQuanTapHoa.ViewModel
{
    class SettingViewModel : BaseViewModel
    {
        private ObservableCollection<SanPham> _SanPhamList;
        public ObservableCollection<SanPham> SanPhamList { get => _SanPhamList; set { _SanPhamList = value; OnPropertyChanged(); } }

        private ObservableCollection<SanPham> _SanPhamKhoList;
        public ObservableCollection<SanPham> SanPhamKhoList { get => _SanPhamKhoList; set { _SanPhamKhoList = value; OnPropertyChanged(); } }

        private List<LoaiSanPham> _CategoryList;
        public List<LoaiSanPham> CategoryList { get => _CategoryList; set => _CategoryList = value; }

        private List<DonViTinh> _UnitList;
        public List<DonViTinh> UnitList { get => _UnitList; set => _UnitList = value; }

        public bool isUpdateProductSuccess = true;
        public bool isAddProductSuccess = false;

        private BackgroundWorker worker;


        public ICommand OpenAddProduct { get; set; }
        public ICommand OpenEditProduct { get; set; }
        public ICommand LoadCommand { get; set; }
        public ICommand EditProductCommand { get; set; }
        public ICommand DeleteProductCommand { get; set; }
        public ICommand AddProductFromWarehouse { get; set; }
        public ICommand BringProductBackToWarehouse { get; set; }
        public ICommand SelectionChanged { get; set; }
        public ICommand SubmitAddProductFromWarehouse { get; set; }
        public ICommand SearchCommand { get; set; }

        public SettingViewModel()
        {
            OpenAddProduct = new RelayCommand<ItemsControl>((p) => { return true; }, (p) => { OpenAddProductWD(p); });
            OpenEditProduct = new RelayCommand<ProductDetailControl>((p) => { return true; }, (p) => { OpenEditProductWD(p); });
            LoadCommand = new RelayCommand<SettingControl>((p) => {
                if (p.IsVisible == true)
                {
                    return true;
                }
                else
                    return false; }, (p) => { Load(p); });
            EditProductCommand = new RelayCommand<EditProductWindow>((p) => { return true; }, (p) => { EditProduct(p); if (isUpdateProductSuccess) p.Close(); });
            DeleteProductCommand = new RelayCommand<ProductDetailControl>((p) => { return true; }, (p) => { DeleteProduct(p); });
            AddProductFromWarehouse = new RelayCommand<EditProductWindow>((p) => { return true; }, (p) => { p.txbSoLuongBayBan.IsEnabled = false; });
            BringProductBackToWarehouse = new RelayCommand<EditProductWindow>((p) => { return true; }, (p) => { p.txbThemTuKho.IsEnabled = false; });
            SelectionChanged = new RelayCommand<AddProductWindow>((p) => { return true; }, (p) => { SelectedProduct(p); });
            SubmitAddProductFromWarehouse = new RelayCommand<AddProductWindow>((p) => { return true; }, (p) => { SubmitAddProduct(p); if (isAddProductSuccess) p.Close(); });
            SearchCommand = new RelayCommand<SettingControl>((p) => { return true; }, (p) => { SearchProduct(p); });
        }

        public void OpenAddProductWD(ItemsControl p)
        {
            AddProductWindow addProductWindow = new AddProductWindow();
            addProductWindow.ShowDialog();
            if(isAddProductSuccess)
            {
                int index = SanPhamList.Count;
                SanPham a = new SanPham();
                a = SanPhamList[index - 1];
                AddProductToScreen(a, p);
                isAddProductSuccess = false;
            }
        }

        public void OpenEditProductWD(ProductDetailControl productDetail)
        {
            SanPham s = new SanPham();
            int id = int.Parse(productDetail.txbID.Text);
            int n = SanPhamList.Count;
            int index=0;
            for(int i=0; i < n; i++)
            {
                if(SanPhamList[i].MaSanPham == id)
                {
                    s = SanPhamList[i];
                    index = i;
                    break;
                }
            }
            EditProductWindow editProductWindow = new EditProductWindow();
            editProductWindow.id.Text = s.MaSanPham.ToString();
            editProductWindow.txbProductName.Text = s.TenSanPham;
            editProductWindow.txbSoLuongBayBan.Text = s.SLBayBan.ToString();
            editProductWindow.txbPrice.Text = s.GiaBan.ToString();
            editProductWindow.cbUnit.SelectedIndex = s.MaDonViTinh - 1;
            editProductWindow.cbCategory.SelectedIndex = s.MaLoai - 1;
            editProductWindow.ShowDialog();
            if(SanPhamList[index].SLBayBan > 0)
            {
                productDetail.txbQuantity.Text = SanPhamList[index].SLBayBan.ToString();
                productDetail.txbPrice.Text = FormatNumber(SanPhamList[index].GiaBan.ToString())+ " VND";
            }
        }
        public void AddProductToScreen(SanPham a ,ItemsControl p)
        {
            ProductDetailControl b = new ProductDetailControl();
            b.txbID.Text = a.MaSanPham.ToString();
            b.txbProductName.Text = a.TenSanPham;
            b.txbQuantity.Text = a.SLBayBan.ToString();
            b.txbUnit.Text = a.DonViTinh.TenDonViTinh;
            b.txbCategory.Text = a.LoaiSanPham.TenLoai;
            b.txbPrice.Text = FormatNumber(a.GiaBan.ToString()) + " VND";
            p.Items.Add(b);
        }
        public void Load(SettingControl p)
        {
            worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            worker.RunWorkerAsync(p);
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            SettingControl p = (SettingControl)e.Result;
            p.progressBar.Visibility = Visibility.Hidden;
            p.productList.Visibility = Visibility.Visible;
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            SettingControl p = (SettingControl)e.Argument;
            System.Windows.Threading.Dispatcher settingDispatcher = p.Dispatcher;
            SanPhamList = new ObservableCollection<SanPham>(DataProvider.Ins.DB.SanPhams.Where(x => x.SLBayBan > 0));
            SanPhamKhoList = new ObservableCollection<SanPham>(DataProvider.Ins.DB.SanPhams.Where(x => x.SLBayBan == 0 && x.SLTrongKho > 0));
            CategoryList = new List<LoaiSanPham>(DataProvider.Ins.DB.LoaiSanPhams);
            UnitList = new List<DonViTinh>(DataProvider.Ins.DB.DonViTinhs);

            ClearUI clear = new ClearUI(ClearItemsControl);
            settingDispatcher.BeginInvoke(clear, p.productList);

            foreach (SanPham i in SanPhamList)
            {
                UpdateUi update = new UpdateUi(AddProductToScreen);
                settingDispatcher.BeginInvoke(update, i, p.productList);
            }
            e.Result = p;
        }
        public delegate void UpdateUi(SanPham a, ItemsControl p);

        public delegate void ClearUI(ItemsControl p);

        public void ClearItemsControl(ItemsControl p)
        {
            p.Items.Clear();
        }

        public bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }
        public void EditProduct(EditProductWindow editProductWindow)
        {
            int id = int.Parse(editProductWindow.id.Text);
            var product = DataProvider.Ins.DB.SanPhams.Where(x => x.MaSanPham == id).SingleOrDefault();

            if (editProductWindow.txbSoLuongBayBan.IsEnabled == false)
            {
                int ThemTuKho = 0;
                string tempSLThemTuKho = editProductWindow.txbThemTuKho.Text;
                if (tempSLThemTuKho.Length > 0 && IsDigitsOnly(tempSLThemTuKho))
                {
                    ThemTuKho = int.Parse(tempSLThemTuKho);
                }
                else
                {
                    isUpdateProductSuccess = false;
                    CustomMessageBox.CustomMessageBox.Show("Vui lòng nhập lại. Chỉ nhập các kí tự số vào ô Thêm từ kho", 1);
                    return;
                }

                if (ThemTuKho > product.SLTrongKho)
                {
                    isUpdateProductSuccess = false;
                    CustomMessageBox.CustomMessageBox.Show("Số lượng thêm vượt quá số lượng trong kho!", 1);
                    return;
                }
                else if( ThemTuKho <= 0)
                {
                    isUpdateProductSuccess = false;
                    CustomMessageBox.CustomMessageBox.Show("Vui lòng nhập lại. Số lượng thêm từ kho phải lớn hơn 0!", 1);
                    return;
                }
                else
                {
                    product.SLBayBan += ThemTuKho;
                    product.SLTrongKho -= ThemTuKho;
                }
            }
            else
            {
                int SLBayBan = 0;
                string tempSLBayBan = editProductWindow.txbSoLuongBayBan.Text;
                if(tempSLBayBan == "")
                {
                    isUpdateProductSuccess = false;
                    CustomMessageBox.CustomMessageBox.Show("Vui lòng nhập số lượng bày bán của sản phẩm", 1);
                    return;
                }
                if (tempSLBayBan.Length > 0 && IsDigitsOnly(tempSLBayBan))
                {
                    SLBayBan = int.Parse(tempSLBayBan);
                }
                else
                {
                    isUpdateProductSuccess = false;
                    CustomMessageBox.CustomMessageBox.Show("Vui lòng nhập lại. Chỉ nhập các kí tự số vào ô Số lượng bày bán!", 1);
                    return;
                }
                if (SLBayBan > product.SLBayBan)
                {
                    isUpdateProductSuccess = false;
                    CustomMessageBox.CustomMessageBox.Show("Vui lòng nhập lại. Số lượng bày bán phải nhỏ hơn lúc chưa chỉnh sửa!", 1);
                    return;
                }
                if (SLBayBan <= 0)
                {
                    isUpdateProductSuccess = false;
                    CustomMessageBox.CustomMessageBox.Show("Vui lòng nhập lại. Số lượng bày bán phải lớn hơn 0!", 1);
                    return;
                }
                product.SLTrongKho += product.SLBayBan - SLBayBan;
                product.SLBayBan = SLBayBan;
            }
            if(editProductWindow.txbPrice.Text == "")
            {
                CustomMessageBox.CustomMessageBox.Show("Vui lòng nhập giá bán!", 1);
                isUpdateProductSuccess = false;
                return;
            }
            if (!IsDigitsOnly(editProductWindow.txbPrice.Text))
            {
                CustomMessageBox.CustomMessageBox.Show("Vui lòng nhập lại giá bán. Giá bán chỉ chứa số!", 1);
                isUpdateProductSuccess = false;
                return;
            }

            product.GiaBan = int.Parse(editProductWindow.txbPrice.Text);
            isUpdateProductSuccess = true;
            DataProvider.Ins.DB.SaveChanges();
            CustomMessageBox.CustomMessageBox.Show("Cập nhập sản phẩm thành công!",3);
        }
        public void DeleteProduct(ProductDetailControl productDetail)
        {
            MessageBoxResult result = CustomMessageBox.CustomMessageBox.Show("Bạn muốn xóa sản phẩm khỏi gian hàng không?", 2);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    int index = int.Parse(productDetail.txbID.Text);
                    foreach (SanPham i in SanPhamList)
                    {
                        if (i.MaSanPham == index)
                        {
                            var product = DataProvider.Ins.DB.SanPhams.Where(x => x.MaSanPham == index).SingleOrDefault();
                            product.SLTrongKho += product.SLBayBan;
                            product.SLBayBan = 0;
                            DataProvider.Ins.DB.SaveChanges();
                            SanPhamKhoList.Clear();
                            SanPhamKhoList = new ObservableCollection<SanPham>(DataProvider.Ins.DB.SanPhams.Where(x => x.SLBayBan == 0 && x.SLTrongKho > 0));
                            SanPhamList.Remove(i);
                            ItemsControl p = (ItemsControl)productDetail.Parent;
                            p.Items.Remove(productDetail);
                            break;
                        }
                    }
                    CustomMessageBox.CustomMessageBox.Show("Bạn đã xóa sản phẩm khỏi gian hàng thành công !", 3);
                    break;
                case MessageBoxResult.No:
                    break;
            }
        }
        public void SelectedProduct(AddProductWindow addProductWindow)
        {
            SanPham p = (SanPham)addProductWindow.cbProduct.SelectedItem;
            if (p == null) return;
            ImageSourceConverter c = new ImageSourceConverter();
            addProductWindow.imgProduct.Source = (ImageSource)c.ConvertFrom(p.Image);
            addProductWindow.txbSLTrongKho.Text = p.SLTrongKho.ToString();
            addProductWindow.txbGiaBan.Text = p.GiaBan.ToString();
            addProductWindow.txbGiaNhap.Text = p.GiaNhap.ToString();
            addProductWindow.cbDonViTinh.SelectedIndex = p.MaDonViTinh - 1;
            addProductWindow.cbLoai.SelectedIndex = p.MaLoai - 1;
        }
        public void SubmitAddProduct(AddProductWindow addProductWindow)
        {
            SanPham p = (SanPham)addProductWindow.cbProduct.SelectedItem;
            int SLBayBan = 0;
            string tempSLBayBan = addProductWindow.txbSLBayBan.Text;
            string tempPrice = addProductWindow.txbGiaBan.Text;
            if (p == null)
            {
                isAddProductSuccess = false;
                CustomMessageBox.CustomMessageBox.Show("Vui lòng chọn sản phẩm để thêm vào gian hàng!", 1);
                return;
            }
            if (tempSLBayBan.Length > 0 && IsDigitsOnly(tempSLBayBan))
            {
                SLBayBan = int.Parse(tempSLBayBan);
            }
            else if (tempSLBayBan.Length == 0)
            {
                isAddProductSuccess = false;
                CustomMessageBox.CustomMessageBox.Show("Vui lòng nhập số lượng bày bán!", 1);
                return;
            }
            else
            {
                isAddProductSuccess = false;
                CustomMessageBox.CustomMessageBox.Show("Vui lòng chỉ nhập các chữ số từ 0 - 9 !", 1);
                return;
            }
            if (SLBayBan > p.SLTrongKho)
            {
                isAddProductSuccess = false;
                CustomMessageBox.CustomMessageBox.Show("Số lượng bày bán không được vượt quá số lượng trong kho!", 1);
                return;
            }
            else
            {
                p.SLBayBan = SLBayBan;
                p.SLTrongKho -= SLBayBan;
            }
            if(tempPrice.Length == 0 || tempPrice == "0")
            {
                isAddProductSuccess = false;
                CustomMessageBox.CustomMessageBox.Show("Vui lòng nhập lại giá bán!", 1);
                return;
            }
            if (!IsDigitsOnly(tempPrice))
            {
                isAddProductSuccess = false;
                CustomMessageBox.CustomMessageBox.Show("Vui lòng nhập lại giá bán. Chỉ nhập các chữ số từ 0 - 9 !", 1);
                return;
            }
            var product = DataProvider.Ins.DB.SanPhams.Where(x => x.MaSanPham == p.MaSanPham).SingleOrDefault();
            product.SLBayBan = p.SLBayBan;
            product.SLTrongKho = p.SLTrongKho;
            product.GiaBan = int.Parse(tempPrice);
            DataProvider.Ins.DB.SaveChanges();
            isAddProductSuccess = true;
            SanPhamList.Add(p);
            CustomMessageBox.CustomMessageBox.Show("Thêm sản phẩm từ kho lên gian hàng thành công!",3);
            SanPhamKhoList.Remove(p);
        }
        public void SearchProduct(SettingControl setting)
        {
            string a = setting.SearchBox.Text.ToLower();
            setting.productList.Items.Clear();
            foreach(SanPham i in SanPhamList)
            {
                string b = i.TenSanPham.ToLower();
                if (b.Contains(a))
                {
                    AddProductToScreen(i, setting.productList);
                }
            }
        }
        public string FormatNumber(string a)
        {
            CultureInfo cul = CultureInfo.GetCultureInfo("vi-VN");
            return double.Parse(a).ToString("#,###", cul.NumberFormat);
        }
    }
}
