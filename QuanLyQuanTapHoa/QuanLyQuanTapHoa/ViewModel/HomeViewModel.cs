using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Win32;
using QuanLyQuanTapHoa.Model;
using QuanLyQuanTapHoa.UserControls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;

namespace QuanLyQuanTapHoa.ViewModel
{
    class HomeViewModel : BaseViewModel
    {
        private ObservableCollection<SanPham> _SanPhamList;
        public ObservableCollection<SanPham> SanPhamList { get => _SanPhamList; set { _SanPhamList = value; OnPropertyChanged(); } }

        private ObservableCollection<SanPham> _SanPhamKhongBayBanList;
        public ObservableCollection<SanPham> SanPhamKhongBayBanList { get => _SanPhamKhongBayBanList; set { _SanPhamKhongBayBanList = value; OnPropertyChanged(); } }

        private ObservableCollection<CartItem> _CartItemList;
        public ObservableCollection<CartItem> CartItemList { get => _CartItemList; set { _CartItemList = value; OnPropertyChanged(); } }

        private BackgroundWorker worker, worker1;


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

        Notifier notifier = new Notifier(cfg =>
        {
            cfg.PositionProvider = new WindowPositionProvider(
                parentWindow: Application.Current.MainWindow,
                corner: Corner.TopLeft,
                offsetX: 820,
                offsetY: 495);

            cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                notificationLifetime: TimeSpan.FromSeconds(8),
                maximumNotificationCount: MaximumNotificationCount.FromCount(5));

            cfg.Dispatcher = Application.Current.Dispatcher;
        });

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
            foreach (SanPham i in SanPhamList)
            {
                if ((int)SubtractDay((DateTime)i.HanSuDung, DateTime.Now) <= 14)
                {
                    ShowNotification();
                    return;
                }
            }
            foreach (SanPham i in SanPhamKhongBayBanList)
            {
                if ((int)SubtractDay((DateTime)i.HanSuDung, DateTime.Now) <= 14)
                {
                    ShowNotification();
                    return;
                }
            }
        }


        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            HomeControl p = (HomeControl)e.Argument;
            System.Windows.Threading.Dispatcher homeDispatcher = p.Dispatcher;
            SanPhamList = new ObservableCollection<SanPham>(DataProvider.Ins.DB.SanPhams.Where(x => x.SLBayBan > 0));
            SanPhamKhongBayBanList = new ObservableCollection<SanPham>(DataProvider.Ins.DB.SanPhams.Where(x => x.SLBayBan == 0 && x.SLTrongKho > 0));
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
            if(h.IsVisible == true)
            {
                worker1 = new BackgroundWorker();
                worker1.DoWork += Worker_DoWork1;
                worker1.RunWorkerCompleted += Worker_RunWorkerCompleted1;
                worker1.RunWorkerAsync(h);
            }
            return;
        }

        private void Worker_RunWorkerCompleted1(object sender, RunWorkerCompletedEventArgs e)
        {
            HomeControl h = (HomeControl)e.Result;
            h.productList.Visibility = Visibility.Visible;
            h.progressBar.Visibility = Visibility.Hidden;
        }

        private void Worker_DoWork1(object sender, DoWorkEventArgs e)
        {
            HomeControl h = (HomeControl)e.Argument;
            System.Windows.Threading.Dispatcher homeDispatcher = h.Dispatcher;

            ShowProgressBar showPB = new ShowProgressBar(showCircleBar);
            homeDispatcher.BeginInvoke(showPB, h);
            ClearUI clear = new ClearUI(clearItemsControl);
            homeDispatcher.BeginInvoke(clear, h.productList);

            int index = 0;
            SanPhamList = new ObservableCollection<SanPham>(DataProvider.Ins.DB.SanPhams.Where(x => x.SLBayBan > 0));
            if (isCategorySelecetd)
            {
                index = int.Parse(uid);
            }

            if (index == 0)
            {
                foreach (SanPham a in SanPhamList)
                {
                    UpdateUi update = new UpdateUi(AddProductControlToScreen);
                    homeDispatcher.BeginInvoke(update, a, h.productList);
                }
            }
            else
            {
                foreach (SanPham a in SanPhamList)
                {
                    if (a.MaLoai == index)
                    {
                        UpdateUi update = new UpdateUi(AddProductControlToScreen);
                        homeDispatcher.BeginInvoke(update, a, h.productList);
                    }
                }
            }
            e.Result = h;
        }

        public delegate void ShowProgressBar(HomeControl h);
        public delegate void ClearUI(ItemsControl i);

        public void clearItemsControl(ItemsControl i)
        {
            i.Items.Clear();
        }

        public void showCircleBar(HomeControl h)
        {
            h.progressBar.Visibility = Visibility.Visible;
            h.productList.Visibility = Visibility.Hidden;
        }

        public void AddToCart(ProductControl p)
        {
            int index = int.Parse(p.id.Text);
            foreach (SanPham i in SanPhamList)
            {
                if (i.MaSanPham == index)
                {
                    for(int j=0; j< CartItemList.Count; j++)
                    {
                        if (CartItemList[j].SanPham.MaSanPham == i.MaSanPham)
                        {
                            CartItem replace = new CartItem();
                            replace.SanPham = i;
                            replace.SoLuong = CartItemList[j].SoLuong + 1;
                            CartItemList[j] = replace;
                            TotalAmount += (int)CartItemList[j].SanPham.GiaBan ;
                            Sum = FormatNumber(TotalAmount.ToString()) + " VND";
                            Money = FormatNumber((TotalAmount - int.Parse(ReturnFormatNumber(DiscountMoney))).ToString()) + " VND";
                            return;
                        }
                    }
                    CartItem a = new CartItem();
                    a.SanPham = i;
                    a.SoLuong = 1;
                    TotalAmount += (int)a.SanPham.GiaBan;
                    Sum = FormatNumber(TotalAmount.ToString()) + " VND";
                    Money = FormatNumber((TotalAmount - int.Parse(ReturnFormatNumber(DiscountMoney))).ToString()) + " VND";
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
                    Money = FormatNumber((TotalAmount - int.Parse(ReturnFormatNumber(DiscountMoney))).ToString()) + " VND";
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
                DisCountList = new List<GiamGia>(DataProvider.Ins.DB.GiamGias.Where(x => x.DaXoa != 1));
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
        public bool ValidateQuantityProduct()
        {
            foreach (CartItem i in CartItemList)
            {
                var product = DataProvider.Ins.DB.SanPhams.Where(x => x.MaSanPham == i.SanPham.MaSanPham).SingleOrDefault();
                if(product.SLBayBan < i.SoLuong)
                {
                    CustomMessageBox.CustomMessageBox.Show("Không đủ số lượng cho " + product.TenSanPham + "!", 1);
                    return false;
                }
            }
            return true;
        }
        public bool ValidateDiscount(TextBox p)
        {
            var discount = DataProvider.Ins.DB.GiamGias.Where(x => x.IdGiamGia == IdDiscount).SingleOrDefault();

            if (discount.NgayKetThuc < DateTime.Today)
            {
                CustomMessageBox.CustomMessageBox.Show("Mã giảm giá này đã hết hạn!", 1);
                return false;
            }
            if (discount.DonHangTu > int.Parse(ReturnFormatNumber(Sum)))
            {
                CustomMessageBox.CustomMessageBox.Show("Mã giảm giá này chỉ áp dụng cho đơn hàng từ " + FormatNumber(discount.DonHangTu.ToString()) + " VND!", 1);
                return false;
            }

            return true;
        }
        public void Pay(TextBox p)
        {
            if (CartItemList.Count == 0)
            {
                CustomMessageBox.CustomMessageBox.Show("Không có sản phẩm nào trong giỏ hàng!", 1);
                return;
            }
            HoaDon bill = new HoaDon();
            if (IdDiscount == 0)
            {
                if (p.Text.Length == 0)
                {
                    bill = new HoaDon() { NgayLapHoaDon = System.DateTime.Now, TongTien = int.Parse(ReturnFormatNumber(Money)) };
                }
                else
                {
                    CustomMessageBox.CustomMessageBox.Show("Mã giảm giá không tồn tại!", 1);
                    return;
                }

            }
            else
            {
                if (ValidateDiscount(p))
                {
                    bill = new HoaDon() { NgayLapHoaDon = System.DateTime.Now, TongTien = int.Parse(ReturnFormatNumber(Money)), IdGiamGia = IdDiscount };
                }
                else
                {
                    return;
                }
            }
            if (ValidateQuantityProduct())
            {
                // in hóa đơn
                var res = CustomMessageBox.CustomMessageBox.Show("Bạn có muốn in hóa đơn không?", 4);
                if (res == MessageBoxResult.Yes)
                {
                    SaveBillPDF();
                }

                // lưu dữ liệu
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

                // hiển thị thông báo
                CustomMessageBox.CustomMessageBox.Show("Thanh toán thành công!", 3);
            }
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
        public string NamePDF()
        {
            var time = DateTime.Now;
            return "Hóa đơn " + time.ToString("dd-MM-yyyy") + " " + time.Hour.ToString() + " giờ " + time.Minute.ToString() + " phút";
        }
        public void SaveBillPDF()
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "PDF (*.pdf)|*.pdf";
            saveFile.FileName = NamePDF();

            string ARIALUNI_TFF = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "Arial.TTF");
            BaseFont bf = BaseFont.CreateFont(ARIALUNI_TFF, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);

            Font f = new Font(bf, 10, Font.NORMAL);
            Font f1 = new Font(bf, 18, Font.NORMAL);

            if (saveFile.ShowDialog() == true)
            {
                var columnWidths = new[] { 0.5f, 2f, 1f, 1f, 1f, 1f };
                var table = new PdfPTable(columnWidths)
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    WidthPercentage = 100,
                    DefaultCell = { MinimumHeight = 22f }
                };
                table.AddCell(new Phrase("STT", f));
                table.AddCell(new Phrase("Tên sản phẩm", f));
                table.AddCell(new Phrase("Loại", f));
                table.AddCell(new Phrase("Đơn vị tính", f));
                table.AddCell(new Phrase("Số lượng", f));
                table.AddCell(new Phrase("Tổng", f));

                int count = 1;
                foreach (CartItem i in CartItemList)
                {
                    table.AddCell(new Phrase(count.ToString(), f));
                    table.AddCell(new Phrase(i.SanPham.TenSanPham, f));
                    table.AddCell(new Phrase(i.SanPham.LoaiSanPham.TenLoai, f));
                    table.AddCell(new Phrase(i.SanPham.DonViTinh.TenDonViTinh, f));
                    table.AddCell(new Phrase(i.SoLuong.ToString(), f));
                    table.AddCell(new Phrase((i.SoLuong * i.SanPham.GiaBan).ToString(), f));
                    count++;
                }

                using (FileStream stream = new FileStream(saveFile.FileName, FileMode.Create))
                {
                    Document pdfDoc = new Document(PageSize.A4, 40f, 40f, 60f, 60f);
                    PdfWriter.GetInstance(pdfDoc, stream);

                    pdfDoc.Open();

                    Paragraph para = new Paragraph(NamePDF(), f1);

                    string root = System.IO.Directory.GetCurrentDirectory();
                    root = root.Remove(root.Length - 10);

                    iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(Path.Combine( root ,"Images" , "LogoApp.png"));
                    Paragraph para1 = new Paragraph("Ngày tạo : " + DateTime.Now.ToString("dd/MM/yyyy"), f);
                    Paragraph para2;
                    Paragraph para3 = new Paragraph("Tổng tiền : " + Sum, f);
                    Paragraph para4 = new Paragraph("Giảm giá : " + DiscountMoney, f);
                    Paragraph para5 = new Paragraph("Thành tiền : " + Money, f);

                    if (IdDiscount == 0)
                    {
                        para2 = new Paragraph("Mã giảm giá : ", f);
                    }
                    else
                    {
                        var discount = DataProvider.Ins.DB.GiamGias.Where(x => x.IdGiamGia == IdDiscount).SingleOrDefault();
                        para2 = new Paragraph("Mã giảm giá : " + discount.Coupoun, f);

                    }
                    para.Alignment = Element.ALIGN_CENTER;
                    para1.Alignment = Element.ALIGN_CENTER;
                    para2.Alignment = Element.ALIGN_CENTER;
                    para3.Alignment = Element.ALIGN_CENTER;
                    para4.Alignment = Element.ALIGN_CENTER;
                    para5.Alignment = Element.ALIGN_CENTER;

                    jpg.ScaleToFit(100f, 100f);
                    jpg.SpacingBefore = 5f;
                    jpg.SpacingAfter = 5f;
                    jpg.Alignment = Element.ALIGN_CENTER;

                    pdfDoc.Add(jpg);
                    pdfDoc.Add(para);
                    pdfDoc.Add(para1);
                    pdfDoc.Add(para2);

                    var spacer = new Paragraph("")
                    {
                        SpacingBefore = 10f,
                        SpacingAfter = 10f,
                    };
                    pdfDoc.Add(spacer);

                    pdfDoc.Add(table);

                    pdfDoc.Add(spacer);
                    pdfDoc.Add(para3);
                    pdfDoc.Add(para4);
                    pdfDoc.Add(para5);

                    pdfDoc.Close();
                }
                CustomMessageBox.CustomMessageBox.Show("Xuất file PFD thành công.", 3);
            }
        }
        public void ShowNotification()
        {
            notifier.ShowWarning("Có sản phẩm sắp hết hạn. Hãy vô kho xem ngay!!!");
        }
        public double SubtractDay(DateTime d1, DateTime d2)
        {
            TimeSpan time = d1 - d2;
            return time.TotalDays;
        }
    }
}
