using QuanLyQuanTapHoa.Model;
using QuanLyQuanTapHoa.UserControls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
namespace QuanLyQuanTapHoa.ViewModel
{
    class WarehouseViewModel : BaseViewModel
    {

        private ObservableCollection<NhapKho> _NhapKhoList;
        private ObservableCollection<ChiTietNhapKho> _ChiTietNhapKhoList;
        private ObservableCollection<SanPham> _SanPhamKhoList;
        private List<DonViTinh> _UnitList;
        private List<LoaiSanPham> _CategoryList;       
        public ObservableCollection<NhapKho> NhapKhoList { get => _NhapKhoList; set { _NhapKhoList = value; OnPropertyChanged(); } }
        public ObservableCollection<ChiTietNhapKho> ChiTietNhapKhoList { get => _ChiTietNhapKhoList; set { _ChiTietNhapKhoList = value; OnPropertyChanged(); } }
        public ObservableCollection<SanPham> SanPhamKhoList { get => _SanPhamKhoList; set { _SanPhamKhoList = value; OnPropertyChanged(); } }
        public List<DonViTinh> UnitList { get => _UnitList; set => _UnitList = value; }
        public List<LoaiSanPham> CategoryList { get => _CategoryList; set => _CategoryList = value; }

        #region commands
        public ICommand OpenImportProduct { get; set; }
        public ICommand LoadCommand { get; set; }      
        public ICommand DeleteProductCommand { get; set; }
        public ICommand SubmitImportProducttoWarehouse { get; set; }
        #endregion

        private BackgroundWorker worker;
        public bool isImportSuccess = false;
        public bool isFistVisible = false;

        public WarehouseViewModel()
        {
            OpenImportProduct = new RelayCommand<ItemsControl>((p) => { return true; }, (p) => { OpenImportProductWD(p); });
            LoadCommand = new RelayCommand<WarehouseControl>((p) => {
                if (p.IsVisible == true && isFistVisible == false)
                {
                    isFistVisible = true;
                    return true;
                }
                else
                    return false;
            }, (p) => { Load(p); });
            DeleteProductCommand = new RelayCommand<ImportProductDetail>((p) => { return true; }, (p) => { DeleteProduct(p); });
            SubmitImportProducttoWarehouse = new RelayCommand<ImportProductWindow>((p) => { return true; }, (p) => { SubmitImportProduct(p); if (isImportSuccess) p.Close(); });
        }


        public void OpenImportProductWD(ItemsControl p)
        {
            ImportProductWindow ImportProduct = new ImportProductWindow();
            ImportProduct.ShowDialog();
            if (isImportSuccess)
            {

                int index = SanPhamKhoList.Count;
                SanPham a = new SanPham();
                a =SanPhamKhoList[index - 1];
                AddProductToScreen(a, p);
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
            if (isImportSuccess)
            {
                SanPhamKhoList = new ObservableCollection<SanPham>(DataProvider.Ins.DB.SanPhams);
                foreach (SanPham i in SanPhamKhoList)
                {
                    AddUi update = new AddUi(AddProductToScreen);
                    WarehouseDispatcher.BeginInvoke(update, i, p.importList);
                }
            }
                
            NhapKhoList = new ObservableCollection<NhapKho>(DataProvider.Ins.DB.NhapKhoes);
            ChiTietNhapKhoList = new ObservableCollection<ChiTietNhapKho>(DataProvider.Ins.DB.ChiTietNhapKhoes);
            CategoryList = new List<LoaiSanPham>(DataProvider.Ins.DB.LoaiSanPhams);
            UnitList. = new List<DonViTinh>(DataProvider.Ins.DB.DonViTinhs);           
            e.Result = p;

        }

        public delegate void AddUi(SanPham a, ItemsControl p);
       
        public void AddProductToScreen(SanPham i, ItemsControl p)
        {
                ImportProductDetail b = new ImportProductDetail();                     
                b.txbProductName.Text = i.TenSanPham.ToString();
                b.txbID.Text = i.MaSanPham.ToString();
                b.txbQuantity.Text = i.SLTrongKho.ToString();
                b.txbUnit.Text = i.DonViTinh.TenDonViTinh.ToString();
                b.txbCategory.Text = i.LoaiSanPham.TenLoai.ToString();
                b.txbPrice.Text = i.GiaNhap.ToString();
                b.txbDate.Text = DateTime.Today.ToString();
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
                         
                        foreach (ChiTietNhapKho ctnk in ChiTietNhapKhoList)
                        {
                            if (ctnk.MaSanPham == sp.MaSanPham)
                            {
                                foreach (NhapKho nk in NhapKhoList)
                                {
                                    if (nk.MaNhapKho == ctnk.MaNhapKho)
                                    {
                                        DataProvider.Ins.DB.NhapKhoes.Remove(nk);
                                        break;
                                    }
                                }
                                DataProvider.Ins.DB.ChiTietNhapKhoes.Remove(ctnk);
                                break;
                            }
                        }

                        DataProvider.Ins.DB.SanPhams.Remove(sp);
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
                CustomMessageBox.CustomMessageBox.Show("Bạn đã xóa nhân viên!", 3);
            }
        }
        public void SubmitImportProduct(ImportProductWindow import)
        {
            if (!IsProductExists(import)) return;
            SanPham temp = new SanPham();
            SanPham sp = new SanPham();
            sp = DataProvider.Ins.DB.SanPhams.Where(x => x.TenSanPham == import.txbNameProduct.Text).FirstOrDefault();
            sp.TenSanPham = temp.TenSanPham;
            sp.GiaNhap = int.Parse(import.txbCost.Text);
            sp.GiaBan = 0;
            sp.SLBayBan = 0;
            sp.SLTrongKho = int.Parse(import.txbQuantity.Text);
            sp.Image = null;
            sp.MaLoai = temp.MaLoai;
            sp.MaDonViTinh = sp.MaDonViTinh;         
            DataProvider.Ins.DB.SanPhams.Add(sp);
            SanPhamKhoList.Clear();
            SanPhamKhoList = new ObservableCollection<SanPham>(DataProvider.Ins.DB.SanPhams);
            ImportTableKho_CTNK(import, sp);
            DataProvider.Ins.DB.SaveChanges();
            isImportSuccess = true;
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

        public bool IsProductExists(ImportProductWindow i)
        {
            // Kiểm tra trường hợp nhập thiếu
            if (String.IsNullOrEmpty(i.txbNameProduct.Text) || String.IsNullOrEmpty(i.txbCost.Text) || String.IsNullOrEmpty(i.txbQuantity.Text)            
                || i.cbxKindsofGoods.SelectedIndex == -1 || i.cbxUnit.SelectedIndex == -1)
            {
                CustomMessageBox.CustomMessageBox.Show("Vui lòng nhập thông tin đầy đủ", 1);
                return false;
            }         
            // Kiểm tra trường hợp trùng tên sản phẩm
            foreach (SanPham s in SanPhamKhoList)
            {
                if (s.TenSanPham == i.txbNameProduct.Text && s.GiaNhap == int.Parse(i .txbCost.Text))
                {
                    CustomMessageBox.CustomMessageBox.Show("Sản phẩm đã tồn tại và nhập thêm số lượng trong kho" ,1 );
                    DataProvider.Ins.DB.SanPhams.Where(x => x.TenSanPham == i.txbNameProduct.Text && x.GiaNhap == int.Parse(i.txbCost.Text)).FirstOrDefault().SLTrongKho += int.Parse(i.txbQuantity.Text);
                    ImportTableKho_CTNK(i, s);
                    DataProvider.Ins.DB.SaveChanges();
                    return false;
                }
            }


            return true;
        }
    }

}
