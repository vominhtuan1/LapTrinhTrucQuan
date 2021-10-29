using QuanLyQuanTapHoa.Model;
using QuanLyQuanTapHoa.UserControls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace QuanLyQuanTapHoa.ViewModel
{
    class HomeViewModel : BaseViewModel
    {
        private ObservableCollection<SanPham> _SanPhamList;
        public ObservableCollection<SanPham> SanPhamList { get => _SanPhamList; set { _SanPhamList = value; OnPropertyChanged(); } }

        private ObservableCollection<CartItem> _CartItemList;
        public ObservableCollection<CartItem> CartItemList { get => _CartItemList; set { _CartItemList = value; OnPropertyChanged(); } }

        private BackgroundWorker worker;


        public int TotalAmount = 0;
        public int IdDiscount = 0;
        public string uid;
        public bool isCategorySelecetd = false;

        private string _Sum = "0 VND";
        public string Sum { get => _Sum; set { _Sum = value; OnPropertyChanged(); } }

        private string _Money = "0 VND";
        public string Money { get => _Money; set { _Money = value; OnPropertyChanged(); } }

        private string _DiscountMoney = "0 VND";
        public string DiscountMoney { get => _DiscountMoney; set { _DiscountMoney = value; OnPropertyChanged(); } }

        private List<GiamGia> _DisCountList;
        public List<GiamGia> DisCountList { get => _DisCountList; set { _DisCountList = value; } }
        public ICommand LoadCommand { get; set; }
        public ICommand AddToCartCommand { get; set; }
        public ICommand DeleteCartItemCommand { get; set; }
        public ICommand EditQuantityCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand DiscountCommand { get; set; }
        public ICommand ClearCartCommand { get; set; }
        public ICommand PayCommand { get; set; }
        public ICommand FilterProductCommand { get; set; }
        public ICommand GetUidCommand { get; set; }
        public ICommand ReLoadCommand { get; set; }

        public HomeViewModel()
        {
            CartItemList = new ObservableCollection<CartItem>();
            LoadCommand = new RelayCommand<HomeControl>((p) => { return true; }, (p) => { Load(p); });
            AddToCartCommand = new RelayCommand<ProductControl>((p) => { return true; }, (p) => { AddToCart(p); });
            DeleteCartItemCommand = new RelayCommand<CartItemControl>((p) => { return true; }, (p) => { DeleteCartItem(p); });
            EditQuantityCommand = new RelayCommand<CartItemControl>((p) => { return true; }, (p) => { EditQuantity(p); });
            SearchCommand = new RelayCommand<HomeControl>((p) => { return true; }, (p) => { Search(p); });
            DiscountCommand = new RelayCommand<TextBox>((p) => { return true; }, (p) => { Discount(p); });
            ClearCartCommand = new RelayCommand<TextBox>((p) => { return true; }, (p) => { ClearCart(p); });
            PayCommand = new RelayCommand<TextBox>((p) => { return true; }, (p) => { Pay(p); });
            GetUidCommand = new RelayCommand<Button>((p) => { return true; }, (p) => { uid = p.Uid; isCategorySelecetd = true; });
            FilterProductCommand = new RelayCommand<HomeControl>((p) => { return true; }, (p) => { FilterProduct(p); });
            ReLoadCommand = new RelayCommand<HomeControl>((p) => { return true; }, (p) => { ReLoad(p); });
        }
        public void Load(HomeControl p)
        {
            worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            worker.RunWorkerAsync(p);

        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            HomeControl p = (HomeControl)e.Result;
            p.productList.Visibility = Visibility.Visible;
            p.progressBar.Visibility = Visibility.Hidden;
        }


        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            HomeControl p = (HomeControl)e.Argument;
            System.Windows.Threading.Dispatcher homeDispatcher = p.Dispatcher;
            SanPhamList = new ObservableCollection<SanPham>(DataProvider.Ins.DB.SanPhams.Where(x => x.SLBayBan > 0));
            foreach (SanPham a in SanPhamList)
            {
                UpdateUi update = new UpdateUi(AddProductControlToScreen);
                homeDispatcher.BeginInvoke(update, a, p.productList);
            }
            e.Result = p;
        }

        public delegate void UpdateUi(SanPham a, ItemsControl p);
        public void ReLoad(HomeControl h)
        {
            if (h.IsVisible == true)
            {
                int index;
                h.productList.Items.Clear();
                SanPhamList = new ObservableCollection<SanPham>(DataProvider.Ins.DB.SanPhams.Where(x => x.SLBayBan > 0));
                if (isCategorySelecetd)
                {
                    index = int.Parse(uid);
                }
                else
                {
                    index = 0;
                }
                 
                if (index == 0)
                {
                    foreach (SanPham a in SanPhamList)
                    {
                        AddProductControlToScreen(a, h.productList);
                    }
                    return;
                }
                else
                {
                    foreach (SanPham a in SanPhamList)
                    {
                        if (a.MaLoai == index)
                        {
                            AddProductControlToScreen(a, h.productList);
                        }
                    }
                    return;
                }
            }
            return;
        }
        public void AddToCart(ProductControl p)
        {
            int index = int.Parse(p.id.Text);
            CartItem a = new CartItem();
            foreach (SanPham i in SanPhamList)
            {
                if (i.MaSanPham == index)
                {
                    a.SanPham = i;
                    a.SoLuong = 1;
                    TotalAmount += (int)a.SanPham.GiaBan;
                    Sum = FormatNumber(TotalAmount.ToString()) + " VND";
                    Money = Sum;
                    CartItemList.Add(a);
                    return;
                }
            }
        }
        public void DeleteCartItem(CartItemControl cartItemControl)
        {
            int index = int.Parse(cartItemControl.Id.Text);
            foreach (CartItem i in CartItemList)
            {
                if (i.SanPham.MaSanPham == index)
                {
                    TotalAmount -= (int)(i.SoLuong * i.SanPham.GiaBan);
                    if (TotalAmount == 0)
                    {
                        Sum = "0 VND";
                    }
                    else
                    {
                        Sum = FormatNumber(TotalAmount.ToString()) + " VND";
                    }
                    Money = Sum;
                    CartItemList.Remove(i);
                    return;
                }
            }
        }
        public void EditQuantity(CartItemControl cartItemControl)
        {
            int index = int.Parse(cartItemControl.Id.Text);
            string tempQty = cartItemControl.ProductQuantity.Text;
            int qty = 1;
            if (tempQty != null)
            {
                qty = int.Parse(tempQty);
            }
            foreach (CartItem i in CartItemList)
            {
                if (i.SanPham.MaSanPham == index)
                {
                    i.SoLuong = qty;
                    TotalAmount += (int)((i.SoLuong - 1) * i.SanPham.GiaBan);
                    Sum = FormatNumber(TotalAmount.ToString()) + " VND";
                    Money = Sum;
                    return;
                }
            }
        }
        public void Search(HomeControl home)
        {
            string a = home.SearchBox.Text.ToLower();
            home.productList.Items.Clear();
            foreach (SanPham i in SanPhamList)
            {
                string b = i.TenSanPham.ToLower();
                if (b.Contains(a))
                {
                    AddProductControlToScreen(i, home.productList);
                }
            }
        }
        public void AddProductControlToScreen(SanPham a, ItemsControl p)
        {
            ProductControl product = new ProductControl();
            ImageSourceConverter c = new ImageSourceConverter();
            product.ProductImg.Source = (ImageSource)c.ConvertFrom(a.Image);
            product.ProductName.Text = a.TenSanPham;
            product.ProductPrice.Text = FormatNumber(a.GiaBan.ToString());
            product.id.Text = a.MaSanPham.ToString();
            p.Items.Add(product);
        }
        public string FormatNumber(string a)
        {
            CultureInfo cul = CultureInfo.GetCultureInfo("vi-VN");
            return double.Parse(a).ToString("#,###", cul.NumberFormat);
        }
        public void Discount(TextBox p)
        {
            if (DisCountList == null)
            {
                DisCountList = new List<GiamGia>(DataProvider.Ins.DB.GiamGias);
            }
            string a = p.Text;
            foreach (GiamGia i in DisCountList)
            {
                if (i.Coupoun == a)
                {
                    IdDiscount = i.IdGiamGia;
                    Money = FormatNumber((TotalAmount - i.SoTienGiam).ToString()) + " VND";
                    DiscountMoney = FormatNumber(i.SoTienGiam.ToString()) + " VND";
                    return;
                }
            }
            IdDiscount = 0;
            if (TotalAmount == 0)
            {
                Money = "0 VND";
            }
            else
            {
                Money = FormatNumber(TotalAmount.ToString()) + " VND";
            }
            DiscountMoney = "0 VND";
        }
        public void ClearCart(TextBox p)
        {
            CartItemList.Clear();
            Money = "0 VND";
            Sum = "0 VND";
            DiscountMoney = "0 VND";
            TotalAmount = 0;
            IdDiscount = 0;
            p.Text = "";
        }
        public void Pay(TextBox p)
        {
            HoaDon bill;
            if (IdDiscount == 0)
            {
                bill = new HoaDon() { NgayLapHoaDon = System.DateTime.Now, TongTien = int.Parse(ReturnFormatNumber(Money)) };

            }
            else
            {
                bill = new HoaDon() { NgayLapHoaDon = System.DateTime.Now, TongTien = int.Parse(ReturnFormatNumber(Money)), IdGiamGia = IdDiscount };

            }
            DataProvider.Ins.DB.HoaDons.Add(bill);
            DataProvider.Ins.DB.SaveChanges();
            var b = DataProvider.Ins.DB.HoaDons.ToList().LastOrDefault();

            foreach (CartItem i in CartItemList)
            {
                var billdetail = new ChiTietHoaDon() { MaHoaDon = b.MaHoaDon, MaSanPham = i.SanPham.MaSanPham, SoLuong = i.SoLuong };
                var product = DataProvider.Ins.DB.SanPhams.Where(x => x.MaSanPham == i.SanPham.MaSanPham).SingleOrDefault();
                product.SLBayBan -= i.SoLuong;
                DataProvider.Ins.DB.ChiTietHoaDons.Add(billdetail);
                DataProvider.Ins.DB.SaveChanges();
            }
            ClearCart(p);
            CustomMessageBox.CustomMessageBox.Show("Thanh toán thành công!", 3);
        }
        public string ReturnFormatNumber(string a)
        {
            string b = "";
            foreach (char i in a)
            {
                if (i >= '0' && i <= '9')
                {
                    b += i;
                }
            }
            return b;
        }
        public void FilterProduct(HomeControl home)
        {
            int index = int.Parse(uid);

            home.bdAll.Background = (Brush)new BrushConverter().ConvertFrom("#ffffff");
            home.bdDoUong.Background = (Brush)new BrushConverter().ConvertFrom("#ffffff");
            home.bdSua.Background = (Brush)new BrushConverter().ConvertFrom("#ffffff");
            home.bdMiGoi.Background = (Brush)new BrushConverter().ConvertFrom("#ffffff");
            home.bdDauGiaVi.Background = (Brush)new BrushConverter().ConvertFrom("#ffffff");
            home.bdVeSinh.Background = (Brush)new BrushConverter().ConvertFrom("#ffffff");
            home.bdBanhKeo.Background = (Brush)new BrushConverter().ConvertFrom("#ffffff");

            switch (index)
            {
                case 0:
                    home.bdAll.Background = (Brush)new BrushConverter().ConvertFrom("#FFFFA500");
                    break;
                case 1:
                    home.bdDoUong.Background = (Brush)new BrushConverter().ConvertFrom("#FFFFA500");
                    break;
                case 2:
                    home.bdSua.Background = (Brush)new BrushConverter().ConvertFrom("#FFFFA500");
                    break;
                case 3:
                    home.bdMiGoi.Background = (Brush)new BrushConverter().ConvertFrom("#FFFFA500");
                    break;
                case 4:
                    home.bdDauGiaVi.Background = (Brush)new BrushConverter().ConvertFrom("#FFFFA500");
                    break;
                case 5:
                    home.bdVeSinh.Background = (Brush)new BrushConverter().ConvertFrom("#FFFFA500");
                    break;
                case 6:
                    home.bdBanhKeo.Background = (Brush)new BrushConverter().ConvertFrom("#FFFFA500");
                    break;
            }

            home.productList.Items.Clear();
            if (index == 0)
            {
                foreach (SanPham a in SanPhamList)
                {
                    AddProductControlToScreen(a, home.productList);
                }
                return;
            }
            else
            {
                foreach (SanPham a in SanPhamList)
                {
                    if (a.MaLoai == index)
                    {
                        AddProductControlToScreen(a, home.productList);
                    }
                }
                return;
            }
        }
    }
}
