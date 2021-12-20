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
using System.Windows.Media.Imaging;

namespace QuanLyQuanTapHoa.ViewModel
{
    class WarehouseViewModel : BaseViewModel
    {

        private ObservableCollection<NhapKho> _NhapKhoList;
        private ObservableCollection<ChiTietNhapKho> _ChiTietNhapKhoList;
        private ObservableCollection<SanPham> _SanPhamKhoList;
        private ObservableCollection<SanPham> _SanPhamHetHanList;
        private ObservableCollection<SanPham> _SanPhamList;
        private List<DonViTinh> _UnitList;
        private List<LoaiSanPham> _CategoryList;
        private BitmapImage _ImageProduct;
        private int _Month = 11;
        private int _Year = 2021;
        private List<int> _Months = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
        private List<int> _Years;



        public ObservableCollection<NhapKho> NhapKhoList { get => _NhapKhoList; set { _NhapKhoList = value; OnPropertyChanged(); } }
        public ObservableCollection<ChiTietNhapKho> ChiTietNhapKhoList { get => _ChiTietNhapKhoList; set { _ChiTietNhapKhoList = value; OnPropertyChanged(); } }
        public ObservableCollection<SanPham> SanPhamKhoList { get => _SanPhamKhoList; set { _SanPhamKhoList = value; OnPropertyChanged(); } }
        public ObservableCollection<SanPham> SanPhamHetHanList { get => _SanPhamHetHanList; set { _SanPhamHetHanList = value; OnPropertyChanged(); } }
        public ObservableCollection<SanPham> SanPhamList { get => _SanPhamList; set { _SanPhamList = value; OnPropertyChanged(); } }
        public List<DonViTinh> UnitList { get => _UnitList; set => _UnitList = value; }
        public List<LoaiSanPham> CategoryList { get => _CategoryList; set => _CategoryList = value; }
        public BitmapImage ImageProduct { get => _ImageProduct; set { _ImageProduct = value; } }
        public int Month { get => _Month; set { _Month = value; } }
        public int Year { get => _Year; set { _Year = value; } }
        public List<int> Months { get => _Months; }
        public List<int> Years { get => _Years; set { _Years = value; OnPropertyChanged(); } }




        #region commands
        public ICommand OpenImportProduct { get; set; }
        public ICommand LoadCommand { get; set; }
        public ICommand DeleteProductCommand { get; set; }
        public ICommand SubmitImportProducttoWarehouse { get; set; }
        public ICommand GetImageCommand { get; set; }
        public ICommand OpenImportDetailWDCommand { get; set; }
        public ICommand ReloadImportDetailWDCommand { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand OpendExpiredProductCommand { get; set; }
        public ICommand LoadExpiredProductCommand { get; set; }
        public ICommand DeleteExpiredProductCommand { get; set; }

        #endregion

        private BackgroundWorker worker;
        public bool isImportSuccess = false;
        public bool isFirst = false;

        public WarehouseViewModel()
        {
            OpenImportProduct = new RelayCommand<ItemsControl>((p) => { return true; }, (p) => { OpenImportProductWD(p); });
            LoadCommand = new RelayCommand<WarehouseControl>((p) => {
                if (p.Visibility == Visibility.Visible)
                {
                    return true;
                }
                return false;
            }, (p) => { Load(p); });
            DeleteProductCommand = new RelayCommand<ImportProductDetail>((p) => { return true; }, (p) => { DeleteProduct(p); });
            SubmitImportProducttoWarehouse = new RelayCommand<ImportProductWindow>((p) => { return true; }, (p) => { SubmitImportProduct(p); if (isImportSuccess) p.Close(); });
            GetImageCommand = new RelayCommand<Image>((p) => { return true; }, (p) => { GetImage(p); });
            OpenImportDetailWDCommand = new RelayCommand<Image>((p) => { return true; }, (p) => { OpenImportDetailWindow(); });
            ReloadImportDetailWDCommand = new RelayCommand<ImportDetailWindow>((p) => { return true; }, (p) => { LoadImportDetailWindow(p, Month, Year); });
            SearchCommand = new RelayCommand<WarehouseControl>((p) => { return true; }, (p) => { SearchDetail(p); });
            OpendExpiredProductCommand = new RelayCommand<WarehouseControl>((p) => { return true; }, (p) => { OpenExpiredWD(); });
            LoadExpiredProductCommand = new RelayCommand<ExpiredProductWindow>((p) => { return true; }, (p) => { LoadExpiredProduct(p); });
            DeleteExpiredProductCommand = new RelayCommand<ExpiredProductControl>((p) => { return true; }, (p) => { DeleteExpiredProduct(p); });
        }

        public void OpenExpiredWD()
        {
            ExpiredProductWindow expiredProductWindow = new ExpiredProductWindow();
            SanPhamHetHanList = new ObservableCollection<SanPham>();
            DateTime date = DateTime.Now;
            
            for (int j = 0; j < SanPhamList.Count; j++)
            {
                if ((int)SubtractDay((DateTime)SanPhamList[j].HanSuDung, date) <= 14 && (SanPhamList[j].SLTrongKho > 0 || SanPhamList[j].SLBayBan > 0))
                {
                    SanPhamHetHanList.Add(SanPhamList[j]);
                }
            }
            expiredProductWindow.ShowDialog();
        }
        public void LoadExpiredProduct(ExpiredProductWindow expiredProductWindow)
        {
            int count = 1;
            for (int i = 0; i < SanPhamHetHanList.Count; i++)
            {
                ExpiredProductControl expired = new ExpiredProductControl();
                expired.MaSo.Text = count.ToString();
                expired.id.Text = SanPhamHetHanList[i].MaSanPham.ToString();
                expired.TenSanPham.Text = SanPhamHetHanList[i].TenSanPham;
                expired.DonViTinh.Text = SanPhamHetHanList[i].DonViTinh.TenDonViTinh;
                expired.LoaiSanPham.Text = SanPhamHetHanList[i].LoaiSanPham.TenLoai;
                expired.HanSuDung.Text = SanPhamHetHanList[i].HanSuDung.Value.ToString("dd/MM/yyyy");
                expiredProductWindow.ExpiredProductList.Items.Add(expired);
                count++;
            }
        }
        public double SubtractDay(DateTime d1, DateTime d2)
        {
            TimeSpan time = d1 - d2;
            return time.TotalDays;
        }
        public void DeleteExpiredProduct(ExpiredProductControl expiredProduct)
        {
            MessageBoxResult result = CustomMessageBox.CustomMessageBox.Show("Bạn muốn xóa sản phẩm hết hạn này không?", 2);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    int index = int.Parse(expiredProduct.id.Text);
                    foreach (SanPham i in SanPhamList)
                    {
                        if (i.MaSanPham == index)
                        {
                            var product = DataProvider.Ins.DB.SanPhams.Where(x => x.MaSanPham == index).SingleOrDefault();
                            product.SLTrongKho += product.SLBayBan;
                            product.SLBayBan = 0;
                            product.SLTrongKho = 0;
                            DataProvider.Ins.DB.SaveChanges();
                            //SanPhamKhoList.Clear();
                            //SanPhamKhoList = new ObservableCollection<SanPham>(DataProvider.Ins.DB.SanPhams.Where(x => x.SLBayBan == 0));
                            SanPhamHetHanList.Remove(i);
                            ItemsControl p = (ItemsControl)expiredProduct.Parent;
                            p.Items.Remove(expiredProduct);
                            break;
                        }
                    }
                    CustomMessageBox.CustomMessageBox.Show("Bạn đã xóa sản phẩm khỏi kho và gian hàng thành công !", 3);
                    break;
                case MessageBoxResult.No:
                    break;
            }
        }
        public void OpenImportProductWD(ItemsControl p)
        {
            ImportProductWindow ImportProduct = new ImportProductWindow();
            ImportProduct.ShowDialog();
            if (isImportSuccess)
            {
                ClearProduct(p);

                foreach (SanPham i in SanPhamKhoList)
                {
                    if (i.SLTrongKho > 0)
                    {
                        AddProductToScreen(i, p);
                    }
                }
                isImportSuccess = false;
            }
        }
        public void Load(WarehouseControl p)
        {
            worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            worker.RunWorkerAsync(p);
        }
        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            WarehouseControl p = (WarehouseControl)e.Result;
            p.progressBar.Visibility = Visibility.Hidden;
            p.importList.Visibility = Visibility.Visible;

        }
        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            WarehouseControl p = (WarehouseControl)e.Argument;
            System.Windows.Threading.Dispatcher WarehouseDispatcher = p.Dispatcher;

            ClearUI clearUI = new ClearUI(ClearProduct);
            WarehouseDispatcher.BeginInvoke(clearUI, p.importList);

            SanPhamList = new ObservableCollection<SanPham>(DataProvider.Ins.DB.SanPhams);
            SanPhamKhoList = new ObservableCollection<SanPham>(DataProvider.Ins.DB.SanPhams.Where(x => x.SLTrongKho > 0));
            NhapKhoList = new ObservableCollection<NhapKho>(DataProvider.Ins.DB.NhapKhoes);
            UnitList = new List<DonViTinh>(DataProvider.Ins.DB.DonViTinhs);
            CategoryList = new List<LoaiSanPham>(DataProvider.Ins.DB.LoaiSanPhams);
            ChiTietNhapKhoList = new ObservableCollection<ChiTietNhapKho>(DataProvider.Ins.DB.ChiTietNhapKhoes);

            foreach (SanPham i in SanPhamKhoList)
            {
                if (i.SLTrongKho > 0)
                {
                    AddUi update = new AddUi(AddProductToScreen);
                    WarehouseDispatcher.BeginInvoke(update, i, p.importList);
                }
            }
            e.Result = p;
        }
        public void InitYear(int y)
        {
            Years = new List<int>();
            for (int i = y - 6; i <= y + 6; i++)
            {
                Years.Add(i);
            }
        }

        public delegate void ClearUI(ItemsControl p);
        public delegate void AddUi(SanPham a, ItemsControl p);

        public void ClearProduct(ItemsControl p)
        {
            p.Items.Clear();
        }

        public void AddProductToScreen(SanPham i, ItemsControl p)
        {
            ImportProductDetail b = new ImportProductDetail();
            b.txbProductName.Text = i.TenSanPham;
            b.txbID.Text = i.MaSanPham.ToString();
            b.txbQuantity.Text = i.SLTrongKho.ToString();
            DonViTinh donViTinh = DataProvider.Ins.DB.DonViTinhs.Where(x => x.Id == i.MaDonViTinh).SingleOrDefault();
            b.txbUnit.Text = donViTinh.TenDonViTinh;
            LoaiSanPham loaiSanPham = DataProvider.Ins.DB.LoaiSanPhams.Where(x => x.MaLoai == i.MaLoai).SingleOrDefault();
            b.txbCategory.Text = loaiSanPham.TenLoai;
            b.txbPrice.Text = FormatNumber(i.GiaNhap.ToString()) + " VND";
            p.Items.Add(b);
        }
        public void DeleteProduct(ImportProductDetail ImportProductDT)
        {
            string s = "Bạn muốn xóa sản phẩm có mã số " + ImportProductDT.txbID.Text + " không?";
            MessageBoxResult result = CustomMessageBox.CustomMessageBox.Show(s, 2);
            if (result == MessageBoxResult.Yes)
            {
                int id = int.Parse(ImportProductDT.txbID.Text);
                foreach (SanPham sp in SanPhamKhoList)
                {
                    if (sp.MaSanPham == id)
                    {
                        List<int> maNK = new List<int>();
                        foreach (ChiTietNhapKho ctnk in ChiTietNhapKhoList)
                        {
                            if (ctnk.MaSanPham == sp.MaSanPham)
                            {
                                maNK.Add(ctnk.MaNhapKho);
                                DataProvider.Ins.DB.ChiTietNhapKhoes.Remove(ctnk);
                                DataProvider.Ins.DB.SaveChanges();
                            }
                        }
                        foreach (int i in maNK)
                        {
                            foreach (NhapKho nk in NhapKhoList)
                            {
                                if (nk.MaNhapKho == i)
                                {
                                    DataProvider.Ins.DB.NhapKhoes.Remove(nk);
                                    DataProvider.Ins.DB.SaveChanges();
                                }
                            }
                        }


                        var pr = DataProvider.Ins.DB.SanPhams.Where(x => x.MaSanPham == sp.MaSanPham).SingleOrDefault();
                        pr.SLBayBan = 0;
                        pr.SLTrongKho = 0;
                        DataProvider.Ins.DB.SaveChanges();
                        ChiTietNhapKhoList.Clear();
                        NhapKhoList.Clear();
                        SanPhamKhoList.Clear();
                        SanPhamKhoList = new ObservableCollection<SanPham>(DataProvider.Ins.DB.SanPhams);
                        NhapKhoList = new ObservableCollection<NhapKho>(DataProvider.Ins.DB.NhapKhoes);
                        ChiTietNhapKhoList = new ObservableCollection<ChiTietNhapKho>(DataProvider.Ins.DB.ChiTietNhapKhoes);
                        ItemsControl p = (ItemsControl)ImportProductDT.Parent;
                        p.Items.Remove(ImportProductDT);
                        break;
                    }
                }
                CustomMessageBox.CustomMessageBox.Show("Bạn đã xóa sản phẩm!", 3);
            }
        }
        public void SubmitImportProduct(ImportProductWindow import)
        {
            if (IsProductExists(import))
            {
                return;
            }
            SanPham sp = new SanPham
            {
                TenSanPham = import.txbNameProduct.Text,
                GiaNhap = int.Parse(import.txbCost.Text),
                GiaBan = 0,
                SLBayBan = 0,
                SLTrongKho = int.Parse(import.txbQuantity.Text),
                Image = ConvertBitmapImageToByteData(ImageProduct),
                HanSuDung = import.txbExpired.SelectedDate,
            };

            LoaiSanPham tempL = (LoaiSanPham)import.cbxKindsofGoods.SelectedItem;
            sp.MaLoai = tempL.MaLoai;
            DonViTinh tempD = (DonViTinh)import.cbxUnit.SelectedItem;
            sp.MaDonViTinh = tempD.Id;
            DataProvider.Ins.DB.SanPhams.Add(sp);
            ImportTableKho_CTNK(import, sp);
            DataProvider.Ins.DB.SaveChanges();
            SanPhamKhoList.Clear();
            SanPhamKhoList = new ObservableCollection<SanPham>(DataProvider.Ins.DB.SanPhams);
            isImportSuccess = true;
            CustomMessageBox.CustomMessageBox.Show("Nhập kho thành công", 3);
        }
        public void ImportTableKho_CTNK(ImportProductWindow i, SanPham s)
        {
            NhapKhoList.Clear();
            ChiTietNhapKhoList.Clear();
            ChiTietNhapKhoList = new ObservableCollection<ChiTietNhapKho>(DataProvider.Ins.DB.ChiTietNhapKhoes);
            NhapKhoList = new ObservableCollection<NhapKho>(DataProvider.Ins.DB.NhapKhoes);
            NhapKho nk = new NhapKho() { NgayNhap = DateTime.Today };
            NhapKhoList.Add(nk);
            DataProvider.Ins.DB.NhapKhoes.Add(nk);
            ChiTietNhapKho ctnk = new ChiTietNhapKho() { MaNhapKho = nk.MaNhapKho, SoLuong = int.Parse(i.txbQuantity.Text), MaSanPham = s.MaSanPham };
            ChiTietNhapKhoList.Add(ctnk);
            DataProvider.Ins.DB.ChiTietNhapKhoes.Add(ctnk);
            DataProvider.Ins.DB.SaveChanges();
        }
        public void GetImage(Image p)
        {
            OpenFileDialog openfile = new OpenFileDialog();
            openfile.Title = "Chọn hình";
            openfile.Filter = "All supported graphics| *.jpg; *.jpeg; *.png; *.bmp | " +
                                "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                                "Portable Network Graphic (*.png)|*.png";
            if (openfile.ShowDialog() == true)
            {
                ImageProduct = new BitmapImage(new Uri(openfile.FileName, UriKind.Absolute));
                p.Source = ImageProduct;
            }
        }
        public byte[] ConvertBitmapImageToByteData(BitmapImage bitmap)
        {
            byte[] data;
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(ImageProduct));
            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                data = ms.ToArray();
            }
            return data;
        }
        public bool IsProductExists(ImportProductWindow i)
        {
            // Kiểm tra trường hợp nhập thiếu
            if (string.IsNullOrEmpty(i.txbNameProduct.Text) || string.IsNullOrEmpty(i.txbCost.Text) || string.IsNullOrEmpty(i.txbQuantity.Text)
                || i.cbxKindsofGoods.SelectedIndex == -1 || i.cbxUnit.SelectedIndex == -1)
            {
                CustomMessageBox.CustomMessageBox.Show("Vui lòng nhập thông tin đầy đủ", 1);
                return true;
            }
            if (ImageProduct == null)
            {
                CustomMessageBox.CustomMessageBox.Show("Nhấp vào avatar để chọn hình ảnh", 1);
                return true;
            }
            // Kiểm tra trường hợp trùng tên sản phẩm
            foreach (SanPham s in SanPhamKhoList)
            {
                if (s.TenSanPham == i.txbNameProduct.Text && s.GiaNhap == int.Parse(i.txbCost.Text))
                {
                    CustomMessageBox.CustomMessageBox.Show("Nhập kho thành công", 3);
                    SanPham edit = DataProvider.Ins.DB.SanPhams.Where(x => x.TenSanPham == i.txbNameProduct.Text && x.GiaNhap.ToString() == i.txbCost.Text).FirstOrDefault();
                    edit.SLTrongKho += int.Parse(i.txbQuantity.Text);
                    edit.Image = ConvertBitmapImageToByteData(ImageProduct);
                    ImportTableKho_CTNK(i, s);
                    DataProvider.Ins.DB.SaveChanges();
                    isImportSuccess = true;
                    return true;
                }
            }


            return false;
        }
        public void OpenImportDetailWindow()
        {
            if (isFirst == false)
            {
                InitYear(DateTime.Now.Year);
                isFirst = true;
            }
            ImportDetailWindow importDetailWindow = new ImportDetailWindow();
            LoadImportDetailWindow(importDetailWindow, DateTime.Now.Month, DateTime.Now.Year);
            importDetailWindow.cbbMonth.SelectedItem = DateTime.Now.Month;
            importDetailWindow.ShowDialog();
        }
        public void LoadImportDetailWindow(ImportDetailWindow p, int month, int year)
        {
            p.ListImportProductDetail.Children.Clear();
            int count = 1;

            foreach (NhapKho i in NhapKhoList)
            {
                if (i.NgayNhap.Value.Month == month && i.NgayNhap.Value.Year == year)
                {
                    List<ChiTietNhapKho> CTNKList = new List<ChiTietNhapKho>(DataProvider.Ins.DB.ChiTietNhapKhoes.Where(x => x.MaNhapKho == i.MaNhapKho));
                    foreach (ChiTietNhapKho j in CTNKList)
                    {
                        SanPham product = DataProvider.Ins.DB.SanPhams.Where(x => x.MaSanPham == j.MaSanPham).SingleOrDefault();
                        LoaiSanPham loai = DataProvider.Ins.DB.LoaiSanPhams.Where(x => x.MaLoai == product.MaLoai).SingleOrDefault();
                        DonViTinh donVi = DataProvider.Ins.DB.DonViTinhs.Where(x => x.Id == product.MaDonViTinh).SingleOrDefault();
                        ImportDetailControl importDetailControl = new ImportDetailControl();

                        importDetailControl.txbStt.Text = count.ToString();
                        importDetailControl.txbProductName.Text = product.TenSanPham;
                        importDetailControl.txbCategory.Text = loai.TenLoai;
                        importDetailControl.txbUnit.Text = donVi.TenDonViTinh;
                        importDetailControl.txbQuantity.Text = j.SoLuong.ToString();
                        importDetailControl.txbPrice.Text = FormatNumber(product.GiaNhap.ToString()) + " VND";
                        importDetailControl.txbDate.Text = i.NgayNhap.Value.ToString("dd/MM//yyyy");

                        p.ListImportProductDetail.Children.Add(importDetailControl);
                        count++;
                    }
                }
            }
        }
        public void SearchDetail(WarehouseControl product)
        {
            string a = product.txbSearch.Text.ToLower();
            product.importList.Items.Clear();
            foreach (SanPham i in SanPhamKhoList)
            {
                string b = i.TenSanPham.ToLower();
                if (b.Contains(a))
                {
                    AddProductToScreen(i, product.importList);
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
