using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Win32;
using QuanLyQuanTapHoa.Model;
using QuanLyQuanTapHoa.UserControls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;


namespace QuanLyQuanTapHoa.ViewModel
{
    class BillViewModel : BaseViewModel
    {
        private ObservableCollection<HoaDon> _BillList;
        public ObservableCollection<HoaDon> BillList { get => _BillList; set { _BillList = value; OnPropertyChanged(); } }

        private string _Month;
        public string Month { get => _Month; set { _Month = value; } }

        private string _Year;
        public string Year { get => _Year; set { _Year = value; } }

        public ICommand OpenDetailCommand { get; set; }
        public ICommand LoadCommand { get; set; }
        public ICommand ReLoadCommand { get; set; }
        public ICommand LoadBillDetailCommand { get; set; }
        public ICommand LoadMonthCommand { get; set; }
        public ICommand GetMonthCommand { get; set; }
        public ICommand LoadYearCommand { get; set; }
        public ICommand GetYearCommand { get; set; }
        public ICommand FilterCommand { get; set; }
        public ICommand SavePDFCommand { get; set; }

        public BillViewModel()
        {
            OpenDetailCommand = new RelayCommand<MainWindow>((p) => { return true; }, (p) => { OpenDetail(); });
            LoadCommand = new RelayCommand<WrapPanel>((p) => { return true; }, (p) => { Load(p); });
            ReLoadCommand = new RelayCommand<BillControl>((p) => { return true; }, (p) => { ReLoad(p); });
            LoadBillDetailCommand = new RelayCommand<BillDetailControl>((p) => { return true; }, (p) => { LoadBillDetail(p); });
            LoadMonthCommand = new RelayCommand<WrapPanel>((p) => { return true; }, (p) => { LoadMonth(p); });
            GetMonthCommand = new RelayCommand<ButtonMonthControl>((p) => { return true; }, (p) => { GetMonth(p); });
            LoadYearCommand = new RelayCommand<WrapPanel>((p) => { return true; }, (p) => { LoadYear(p); });
            GetYearCommand = new RelayCommand<ButtonYearControl>((p) => { return true; }, (p) => { GetYear(p); });
            FilterCommand = new RelayCommand<BillControl>((p) => { return true; }, (p) => { Filter(p); });
            SavePDFCommand = new RelayCommand<BillDetailWindow>((p) => { return true; }, (p) => { SavePDF(p); });

        }
        public void OpenDetail()
        {
            BillDetailWindow billDetailWindow = new BillDetailWindow();
            billDetailWindow.ShowDialog();
        }
        public void Load(WrapPanel p)
        {
            BillList = new ObservableCollection<HoaDon>(DataProvider.Ins.DB.HoaDons);
            foreach (HoaDon i in BillList)
            {
                BillDetailControl billDetailControl = new BillDetailControl();
                billDetailControl.MaSo.Text = i.MaHoaDon.ToString();
                billDetailControl.TongTien.Text = FormatNumber(i.TongTien.ToString()) + " VND";
                billDetailControl.NgayLap.Text = i.NgayLapHoaDon.Value.ToString("dd/M/yyyy", CultureInfo.InvariantCulture);
                p.Children.Add(billDetailControl);
            }
        }
        public void ReLoad(BillControl p)
        {
            if (p.Visibility == Visibility.Visible)
            {
                BillList = new ObservableCollection<HoaDon>(DataProvider.Ins.DB.HoaDons);
                int count = p.ListBill.Children.Count;
                int l = BillList.Count();
                for (int i = count; i < l; i++)
                {
                    BillDetailControl billDetailControl = new BillDetailControl();
                    billDetailControl.MaSo.Text = BillList[i].MaHoaDon.ToString();
                    billDetailControl.TongTien.Text = FormatNumber(BillList[i].TongTien.ToString()) + " VND";
                    billDetailControl.NgayLap.Text = BillList[i].NgayLapHoaDon.Value.ToString("dd/M/yyyy", CultureInfo.InvariantCulture);
                    p.ListBill.Children.Add(billDetailControl);
                }
            }
        }
        public string FormatNumber(string a)
        {
            CultureInfo cul = CultureInfo.GetCultureInfo("vi-VN");
            return double.Parse(a).ToString("#,###", cul.NumberFormat);
        }
        public void LoadBillDetail(BillDetailControl billDetailControl)
        {
            int maHD = int.Parse(billDetailControl.MaSo.Text);
            int count = 1;
            var bill = DataProvider.Ins.DB.HoaDons.Where(x => x.MaHoaDon == maHD).ToList();
            var ListProductOfBill = DataProvider.Ins.DB.ChiTietHoaDons.Where(x => x.MaHoaDon == maHD).ToList();
            BillDetailWindow billDetailWindow = new BillDetailWindow();
            billDetailWindow.MaHoaDon.Text = billDetailControl.MaSo.Text;
            billDetailWindow.NgayLap.Text = billDetailControl.NgayLap.Text;
            billDetailWindow.ThanhTien.Text = billDetailControl.TongTien.Text;

            if (bill[0].IdGiamGia == null)
            {
                billDetailWindow.Tong.Text = FormatNumber(bill[0].TongTien.ToString()) + " VND";
            }
            else
            {
                billDetailWindow.MaGG.Text = bill[0].GiamGia.Coupoun.ToString();
                billDetailWindow.Giamgia.Text = FormatNumber(bill[0].GiamGia.SoTienGiam.ToString()) + " VND";
                billDetailWindow.Tong.Text = FormatNumber((bill[0].GiamGia.SoTienGiam + bill[0].TongTien).ToString()) + " VND";
            }

            foreach (ChiTietHoaDon i in ListProductOfBill)
            {

                ProductOfBillDetailControl product = new ProductOfBillDetailControl();
                product.Stt.Text = count.ToString();
                product.TenSanPham.Text = i.SanPham.TenSanPham;
                product.DonViTinh.Text = i.SanPham.DonViTinh.TenDonViTinh;
                product.SoLuong.Text = i.SoLuong.ToString();
                product.Loai.Text = i.SanPham.LoaiSanPham.TenLoai;
                product.Tong.Text = FormatNumber((i.SoLuong * i.SanPham.GiaBan).ToString()) + " VND";
                billDetailWindow.ListProductOfBill.Children.Add(product);
                count++;
            }
            billDetailWindow.ShowDialog();
        }
        public void LoadMonth(WrapPanel p)
        {
            for (int i = 1; i <= 12; i++)
            {
                ButtonMonthControl button = new ButtonMonthControl();
                button.Month.Text = i.ToString();
                p.Children.Add(button);
            }
        }
        public void GetMonth(ButtonMonthControl p)
        {
            WrapPanel w = (WrapPanel)p.Parent;
            foreach (ButtonMonthControl i in w.Children)
            {
                i.BorderMonth.Background = (Brush)new BrushConverter().ConvertFrom("#ffffff");
                i.Month.Foreground = (Brush)new BrushConverter().ConvertFrom("#000000");
            }
            p.BorderMonth.Background = (Brush)new BrushConverter().ConvertFrom("#8EA0E0");
            p.Month.Foreground = (Brush)new BrushConverter().ConvertFrom("#ffffff");
            Month = p.Month.Text;
        }
        public void LoadYear(WrapPanel p)
        {
            int year = System.DateTime.Now.Year;
            for (int i = year - 11; i <= year; i++)
            {
                ButtonYearControl button = new ButtonYearControl();
                button.Year.Text = i.ToString();
                p.Children.Add(button);
            }
        }
        public void GetYear(ButtonYearControl p)
        {
            WrapPanel q = (WrapPanel)p.Parent;
            foreach (ButtonYearControl i in q.Children)
            {
                i.BorderYear.Background = (Brush)new BrushConverter().ConvertFrom("#ffffff");
                i.Year.Foreground = (Brush)new BrushConverter().ConvertFrom("#000000");
            }
            p.BorderYear.Background = (Brush)new BrushConverter().ConvertFrom("#8EA0E0");
            p.Year.Foreground = (Brush)new BrushConverter().ConvertFrom("#ffffff");
            Year = p.Year.Text;
        }
        public void Filter(BillControl p)
        {
            if (Month == null || Year == null)
            {
                if (Month == null && Year == null)
                {
                    CustomMessageBox.CustomMessageBox.Show("Vui lòng chọn tháng và năm.", 1);
                }
                else if (Year == null)
                {
                    CustomMessageBox.CustomMessageBox.Show("Vui lòng chọn năm.", 1);
                }
                else
                {
                    CustomMessageBox.CustomMessageBox.Show("Vui lòng chọn tháng.", 1);
                }
            }
            else
            {
                p.ListBill.Children.Clear();
                foreach (HoaDon i in BillList)
                {
                    if (i.NgayLapHoaDon.Value.Month == int.Parse(Month) && i.NgayLapHoaDon.Value.Year == int.Parse(Year))
                    {
                        BillDetailControl billDetailControl = new BillDetailControl();
                        billDetailControl.MaSo.Text = i.MaHoaDon.ToString();
                        billDetailControl.TongTien.Text = FormatNumber(i.TongTien.ToString()) + " VND";
                        billDetailControl.NgayLap.Text = i.NgayLapHoaDon.Value.ToString("dd/M/yyyy", CultureInfo.InvariantCulture);
                        p.ListBill.Children.Add(billDetailControl);
                    }
                }
            }
        }
        public string NameOfPDF(TextBlock p)
        {
            return "Hóa đơn " + p.Text.Replace('/', '-');
        }

        public void SavePDF(BillDetailWindow p)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "PDF (*.pdf)|*.pdf";
            saveFile.FileName = NameOfPDF(p.NgayLap);

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
                table.AddCell(new Phrase("Tên hàng hóa", f));
                table.AddCell(new Phrase("Loại", f));
                table.AddCell(new Phrase("Đơn vị tính", f));
                table.AddCell(new Phrase("Số lượng", f));
                table.AddCell(new Phrase("Tổng", f));

                foreach (ProductOfBillDetailControl i in p.ListProductOfBill.Children)
                {
                    table.AddCell(new Phrase(i.Stt.Text, f));
                    table.AddCell(new Phrase(i.TenSanPham.Text, f));
                    table.AddCell(new Phrase(i.Loai.Text, f));
                    table.AddCell(new Phrase(i.DonViTinh.Text, f));
                    table.AddCell(new Phrase(i.SoLuong.Text, f));
                    table.AddCell(new Phrase(i.Tong.Text, f));
                }

                using (FileStream stream = new FileStream(saveFile.FileName, FileMode.Create))
                {
                    Document pdfDoc = new Document(PageSize.A4, 40f, 40f, 60f, 60f);
                    PdfWriter.GetInstance(pdfDoc, stream);

                    pdfDoc.Open();

                    string root = System.IO.Directory.GetCurrentDirectory();
                    root = root.Remove(root.Length - 10);
                    iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(Path.Combine(root, "Images", "AKIKO_free-file.png"));

                    Paragraph para = new Paragraph(NameOfPDF(p.NgayLap), f1);
                    Paragraph para1 = new Paragraph("Ngày lập : " + p.NgayLap.Text, f);
                    Paragraph para2 = new Paragraph("Mã giảm giá : " + p.MaGG.Text, f);
                    Paragraph para3 = new Paragraph("Tổng  : " + p.Tong.Text, f);
                    Paragraph para4 = new Paragraph("Giảm giá : " + p.Giamgia.Text, f);
                    Paragraph para5 = new Paragraph("Thành tiền: " + p.ThanhTien.Text, f);

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
    }
}
