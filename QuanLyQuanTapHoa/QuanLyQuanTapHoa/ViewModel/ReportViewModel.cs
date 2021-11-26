using iTextSharp.text;
using iTextSharp.text.pdf;
using LiveCharts;
using LiveCharts.Wpf;
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
using System.Windows.Media.Imaging;

namespace QuanLyQuanTapHoa.ViewModel
{
    class ReportViewModel : BaseViewModel
    {
        private SeriesCollection _PieSeriesCollection;
        public SeriesCollection PieSeriesCollection { get => _PieSeriesCollection; set { _PieSeriesCollection = value; OnPropertyChanged(); } }

        private SeriesCollection _ColSeriesCollection;
        public SeriesCollection ColSeriesCollection { get => _ColSeriesCollection; set { _ColSeriesCollection = value; OnPropertyChanged(); } }

        private ObservableCollection<HoaDon> _BillList;
        public ObservableCollection<HoaDon> BillList { get => _BillList; set { _BillList = value; OnPropertyChanged(); } }

        private List<int> QuantitySold;

        private string[] labels;
        public string[] Labels { get => labels; set { labels = value; OnPropertyChanged(); } }

        private List<int> _Years;
        public List<int> Years { get => _Years; set { _Years = value; OnPropertyChanged(); } }

        private List<int> _Months = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
        public List<int> Months { get => _Months; }

        private List<int> _Quaters = new List<int>() { 1, 2, 3, 4 };
        public List<int> Quaters { get => _Quaters; }

        private Func<double, string> formatter;
        public Func<double, string> Formatter { get => formatter; set { formatter = value; OnPropertyChanged(); } }

        private int _Month;
        public int Month { get => _Month; set { _Month = value; } }

        private int _Year;
        public int Year { get => _Year; set { _Year = value; } }

        private int _Quater;
        public int Quater { get => _Quater; set { _Quater = value; } }

        private BackgroundWorker worker;

        public ICommand LoadCommand { get; set; }
        public ICommand ReloadPieChartCommand { get; set; }
        public ICommand ReloadColChartCommand { get; set; }
        public ICommand OpenReportDetailWindowCommand { get; set; }
        public ICommand LoadContentForReportDetailCommand { get; set; }
        public ICommand ReloadReportDatilCommand { get; set; }
        public ICommand TakeCaptureCommand { get; set; }
        public ReportViewModel()
        {
            LoadCommand = new RelayCommand<ReportControl>((p) => {
                if(p.IsVisible == true)
                {
                    return true;
                }
                return false;
            }, (p) => { Load(p); });
            ReloadPieChartCommand = new RelayCommand<ComboBox>((p) => { return true; }, (p) => { ReloadPieChart(p); });
            ReloadColChartCommand = new RelayCommand<ComboBox>((p) => { return true; }, (p) => { ReloadColChart(p); });
            OpenReportDetailWindowCommand = new RelayCommand<ComboBox>((p) => { return true; }, (p) => { OpenReportDetailWindow(); });
            LoadContentForReportDetailCommand = new RelayCommand<ReportDetailWindow>((p) => { return true; }, (p) => { LoadContentForReportDetail(11, 2021, p); });
            ReloadReportDatilCommand = new RelayCommand<ReportDetailWindow>((p) => { return true; }, (p) => { LoadContentForReportDetail(Month, Year, p); });
            TakeCaptureCommand = new RelayCommand<ReportDetailWindow>((p) => { return true; }, (p) => { TakeCapture(p); });

        }
        public void Load(ReportControl p)
        {
            worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            worker.RunWorkerAsync(p);

        }
        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) { }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            Month = DateTime.Now.Month;
            Year = DateTime.Now.Year;

            ReportControl p = (ReportControl)e.Argument;
            System.Windows.Threading.Dispatcher homeDispatcher = p.Dispatcher;

            UpdateUi update = new UpdateUi(InitPieChart);
            homeDispatcher.BeginInvoke(update, 11, 2021);
            UpdateUi update1 = new UpdateUi(InitColumnChart);
            homeDispatcher.BeginInvoke(update1, 4, 2021);
            UpdateUi update2 = new UpdateUi(InitComboboxYear);
            homeDispatcher.BeginInvoke(update2, 1, 1);
        }
        public delegate void UpdateUi(int a, int b);
        public void InitComboboxYear(int a, int b)
        {
            Years = new List<int>();
            for (int i = Year - 6; i < Year + 6; i++)
            {
                Years.Add(i);
            }
        }
        public void InitPieChart(int month, int year)
        {
            BillList = new ObservableCollection<HoaDon>(DataProvider.Ins.DB.HoaDons.Where(x => x.NgayLapHoaDon.Value.Month == month && x.NgayLapHoaDon.Value.Year == Year));
            QuantitySold = new List<int>() { 0, 0, 0, 0, 0, 0 };
            foreach (HoaDon i in BillList)
            {
                var ListProductOfBill = DataProvider.Ins.DB.ChiTietHoaDons.Where(x => x.MaHoaDon == i.MaHoaDon).ToList();
                foreach (ChiTietHoaDon j in ListProductOfBill)
                {
                    int index = j.SanPham.MaLoai;
                    QuantitySold[index - 1] += (int)j.SoLuong;
                }
            }
            PieSeriesCollection = new SeriesCollection
            {
                new PieSeries
                {
                    Title = "Đồ uống",
                    Values = new ChartValues<int>{ QuantitySold[0]},
                    Fill = (Brush)new BrushConverter().ConvertFrom("#FF0000"),
                },
                new PieSeries
                {
                    Title = "Sữa",
                    Values = new ChartValues<int>{ QuantitySold[1]},
                    Fill = (Brush)new BrushConverter().ConvertFrom("#ffeb3b"),
                },
                new PieSeries
                {
                    Title = "Mì gói",
                    Values = new ChartValues<int>{ QuantitySold[2]},
                    Fill = (Brush)new BrushConverter().ConvertFrom("#51A8DE"),
                },
                new PieSeries
                {
                    Title = "Dầu, gia vị",
                    Values = new ChartValues<int>{ QuantitySold[3]},
                    Fill = (Brush)new BrushConverter().ConvertFrom("#9900ef"),
                },
                new PieSeries
                {
                    Title = "Vệ sinh",
                    Values = new ChartValues<int>{ QuantitySold[4]},
                    Fill = (Brush)new BrushConverter().ConvertFrom("#008b02"),
                },
                new PieSeries
                {
                    Title = "Bánh kẹo",
                    Values = new ChartValues<int>{ QuantitySold[5]},
                    Fill = (Brush)new BrushConverter().ConvertFrom("#ff5722"),
                }
            };
        }
        public void InitColumnChart(int quater, int year)
        {
            switch (quater)
            {
                case 1:
                    BillList = new ObservableCollection<HoaDon>(DataProvider.Ins.DB.HoaDons.Where(x =>
                        (x.NgayLapHoaDon.Value.Month == 1 || x.NgayLapHoaDon.Value.Month == 2 || x.NgayLapHoaDon.Value.Month == 3) && x.NgayLapHoaDon.Value.Year == year));
                    Labels = new[] { "1", "2", "3" };
                    break;
                case 2:
                    BillList = new ObservableCollection<HoaDon>(DataProvider.Ins.DB.HoaDons.Where(x =>
                        (x.NgayLapHoaDon.Value.Month == 4 || x.NgayLapHoaDon.Value.Month == 5 || x.NgayLapHoaDon.Value.Month == 6) && x.NgayLapHoaDon.Value.Year == year));
                    Labels = new[] { "4", "5", "6" };
                    break;
                case 3:
                    BillList = new ObservableCollection<HoaDon>(DataProvider.Ins.DB.HoaDons.Where(x =>
                        (x.NgayLapHoaDon.Value.Month == 7 || x.NgayLapHoaDon.Value.Month == 8 || x.NgayLapHoaDon.Value.Month == 9) && x.NgayLapHoaDon.Value.Year == year));
                    Labels = new[] { "7", "8", "9" };
                    break;
                case 4:
                    BillList = new ObservableCollection<HoaDon>(DataProvider.Ins.DB.HoaDons.Where(x =>
                        (x.NgayLapHoaDon.Value.Month == 10 || x.NgayLapHoaDon.Value.Month == 11 || x.NgayLapHoaDon.Value.Month == 12) && x.NgayLapHoaDon.Value.Year == year));
                    Labels = new[] { "10", "11", "12" };
                    break;
            }
            int[] sum = new int[3] { 0, 0, 0 };
            foreach (HoaDon i in BillList)
            {
                if (i.NgayLapHoaDon.Value.Month == 1 || i.NgayLapHoaDon.Value.Month == 4 || i.NgayLapHoaDon.Value.Month == 7 || i.NgayLapHoaDon.Value.Month == 10)
                {
                    sum[0] += (int)i.TongTien;
                }
                else if (i.NgayLapHoaDon.Value.Month == 2 || i.NgayLapHoaDon.Value.Month == 5 || i.NgayLapHoaDon.Value.Month == 8 || i.NgayLapHoaDon.Value.Month == 11)
                {
                    sum[1] += (int)i.TongTien;
                }
                else
                {
                    sum[2] += (int)i.TongTien;
                }
            }
            ColSeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Doanh thu",
                    Values = new ChartValues<int>{sum[0],sum[1],sum[2]}
                }
            };
            Formatter = value => string.Format("{0:N0}", value);
        }
        public void ReloadPieChart(ComboBox p)
        {
            if (p.Name == "PieMonth")
            {
                Month = int.Parse(p.SelectedItem.ToString());
            }
            if (p.Name == "PieYear")
            {
                Year = int.Parse(p.SelectedItem.ToString());
            }
            InitPieChart(Month, Year);
        }
        public void ReloadColChart(ComboBox p)
        {
            if (p.Name == "ColQuater")
            {
                Quater = int.Parse(p.SelectedItem.ToString());
            }
            if (p.Name == "ColYear")
            {
                Year = int.Parse(p.SelectedItem.ToString());
            }
            InitColumnChart(Quater, Year);
        }
        public void OpenReportDetailWindow()
        {
            ReportDetailWindow reportDetailWindow = new ReportDetailWindow();
            reportDetailWindow.ShowDialog();
        }
        public void LoadContentForReportDetail(int month, int year, ReportDetailWindow p)
        {
            BillList = new ObservableCollection<HoaDon>(DataProvider.Ins.DB.HoaDons.Where(x => x.NgayLapHoaDon.Value.Month == month && x.NgayLapHoaDon.Value.Year == year));
            List<int> MaSP = new List<int>();
            List<int> QTY = new List<int>();
            foreach (HoaDon i in BillList)
            {
                List<ChiTietHoaDon> chiTiets = new List<ChiTietHoaDon>(DataProvider.Ins.DB.ChiTietHoaDons.Where(x => x.MaHoaDon == i.MaHoaDon));
                foreach (ChiTietHoaDon j in chiTiets)
                {
                    if (MaSP.Contains(j.MaSanPham))
                    {
                        int index = MaSP.IndexOf(j.MaSanPham);
                        QTY[index] += (int)j.SoLuong;
                    }
                    else
                    {
                        MaSP.Add(j.MaSanPham);
                        QTY.Add((int)j.SoLuong);
                    }
                }
            }
            int len = MaSP.Count;
            p.WrapContent.Children.Clear();
            int sum = 0;
            for (int i = 0; i < len; i++)
            {
                int myMaSp = MaSP[i];
                var a = DataProvider.Ins.DB.SanPhams.Where(x => x.MaSanPham == myMaSp).ToList().LastOrDefault();
                ReportDetailControl reportDetail = new ReportDetailControl();
                reportDetail.STT.Text = (i + 1).ToString();
                reportDetail.txbProductName.Text = a.TenSanPham;
                reportDetail.txtUnit.Text = a.DonViTinh.TenDonViTinh;
                reportDetail.txbQuantity.Text = QTY[i].ToString();
                reportDetail.txbRevenue.Text = FormatNumber((QTY[i] * a.GiaBan).ToString());
                reportDetail.txbPrice.Text = FormatNumber((QTY[i] * a.GiaNhap).ToString());
                reportDetail.txbProfitLoss.Text = FormatNumber((QTY[i] * (a.GiaBan - a.GiaNhap)).ToString());
                sum += (int)(QTY[i] * (a.GiaBan - a.GiaNhap));
                p.WrapContent.Children.Add(reportDetail);
            }
            p.txbTong.Text = FormatNumber(sum.ToString()) + " VNĐ";
        }
        public string FormatNumber(string a)
        {
            CultureInfo cul = CultureInfo.GetCultureInfo("vi-VN");
            return double.Parse(a).ToString("#,###", cul.NumberFormat);
        }
        public string NameOfPDF(ComboBox m, ComboBox y)
        {
            if (m.SelectedItem == null && y.SelectedItem == null)
            {
                return "Báo cáo doanh thu tháng 11 năm 2021";
            }
            else if (m.SelectedItem != null && y.SelectedItem == null)
            {
                return "Báo cáo doanh thu tháng " + m.SelectedItem.ToString() + " năm 2021";
            }
            else if (m.SelectedItem == null && y.SelectedItem != null)
            {
                return "Báo cáo doanh thu tháng 11 năm " + y.SelectedItem.ToString();
            }
            return "Báo cáo doanh thu tháng " + m.SelectedItem.ToString() + " năm " + y.SelectedItem.ToString();

        }
        public void TakeCapture(ReportDetailWindow p)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "PDF (*.pdf)|*.pdf";
            saveFile.FileName = NameOfPDF(p.cbbMonth, p.cbbYear);

            string ARIALUNI_TFF = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "Arial.TTF");
            BaseFont bf = BaseFont.CreateFont(ARIALUNI_TFF, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);

            Font f = new Font(bf, 10, Font.NORMAL);
            Font f1 = new Font(bf, 18, Font.NORMAL);

            if (saveFile.ShowDialog() == true)
            {
                var columnWidths = new[] { 0.45f, 1.25f, 0.65f, 0.5f, 1f, 1f, 1f };
                var table = new PdfPTable(columnWidths)
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    WidthPercentage = 100,
                    DefaultCell = { MinimumHeight = 22f }
                };
                table.AddCell(new Phrase("STT", f));
                table.AddCell(new Phrase("Tên hàng hóa", f));
                table.AddCell(new Phrase("Đơn vị tính", f));
                table.AddCell(new Phrase("Số lượng", f));
                table.AddCell(new Phrase("Doanh thu", f));
                table.AddCell(new Phrase("Giá vốn", f));
                table.AddCell(new Phrase("Lãi/Lỗ", f));

                foreach (ReportDetailControl i in p.WrapContent.Children)
                {
                    table.AddCell(new Phrase(i.STT.Text, f));
                    table.AddCell(new Phrase(i.txbProductName.Text, f));
                    table.AddCell(new Phrase(i.txtUnit.Text, f));
                    table.AddCell(new Phrase(i.txbQuantity.Text, f));
                    table.AddCell(new Phrase(i.txbRevenue.Text, f));
                    table.AddCell(new Phrase(i.txbPrice.Text, f));
                    table.AddCell(new Phrase(i.txbProfitLoss.Text, f));
                }

                using (FileStream stream = new FileStream(saveFile.FileName, FileMode.Create))
                {
                    Document pdfDoc = new Document(PageSize.A4, 40f, 40f, 60f, 60f);
                    PdfWriter.GetInstance(pdfDoc, stream);

                    pdfDoc.Open();

                    string root = System.IO.Directory.GetCurrentDirectory();
                    root = root.Remove(root.Length - 10);

                    iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(Path.Combine(root, "Images", "AKIKO_free-file.png"));

                    Paragraph para = new Paragraph(NameOfPDF(p.cbbMonth, p.cbbYear), f1);
                    Paragraph para1 = new Paragraph("Tổng lãi/lỗ : " + p.txbTong.Text, f1);

                    para.Alignment = Element.ALIGN_CENTER;
                    para1.Alignment = Element.ALIGN_CENTER;

                    jpg.ScaleToFit(100f, 100f);
                    jpg.SpacingBefore = 5f;
                    jpg.SpacingAfter = 5f;
                    jpg.Alignment = Element.ALIGN_CENTER;

                    pdfDoc.Add(jpg);
                    pdfDoc.Add(para);

                    var spacer = new Paragraph("")
                    {
                        SpacingBefore = 10f,
                        SpacingAfter = 10f,
                    };
                    pdfDoc.Add(spacer);

                    pdfDoc.Add(table);

                    pdfDoc.Add(spacer);

                    pdfDoc.Add(para1);

                    pdfDoc.Close();
                }
                CustomMessageBox.CustomMessageBox.Show("Xuất file PFD thành công.", 3);
            }
        }
    }
}
