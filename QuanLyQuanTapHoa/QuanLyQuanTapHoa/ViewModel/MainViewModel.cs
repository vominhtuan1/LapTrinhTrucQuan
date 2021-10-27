using QuanLyQuanTapHoa.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace QuanLyQuanTapHoa.ViewModel
{
    class MainViewModel : BaseViewModel
    {
        public ICommand SwitchTabCommand { get; set; }
        public ICommand GetUidCommand { get; set; }

        private string uid;
        
        public MainViewModel()
        {
            SwitchTabCommand = new RelayCommand<MainWindow>((p) => { return true; }, (p) => { SwitchTab(p); });
            GetUidCommand = new RelayCommand<Button>((p) => { return true; }, (p) => { uid = p.Uid; });
        }

        public void SwitchTab(MainWindow mainWindow)
        {
            int index = int.Parse(uid);

            mainWindow.grdHome.Visibility = System.Windows.Visibility.Hidden;
            mainWindow.grdReport.Visibility = System.Windows.Visibility.Hidden;
            mainWindow.grdWarehouse.Visibility = System.Windows.Visibility.Hidden;
            mainWindow.grdStaff.Visibility = System.Windows.Visibility.Hidden;
            mainWindow.grdSetting.Visibility = System.Windows.Visibility.Hidden;
            mainWindow.grdBill.Visibility = System.Windows.Visibility.Hidden;
            mainWindow.BillControl.Visibility = System.Windows.Visibility.Hidden;
            mainWindow.grdDiscount.Visibility = System.Windows.Visibility.Hidden;

            mainWindow.bdHome.Background = (Brush)new BrushConverter().ConvertFrom("#ffffff");
            mainWindow.bdReport.Background = (Brush)new BrushConverter().ConvertFrom("#ffffff");
            mainWindow.bdWareHouse.Background = (Brush)new BrushConverter().ConvertFrom("#ffffff");
            mainWindow.bdStaff.Background = (Brush)new BrushConverter().ConvertFrom("#ffffff");
            mainWindow.bdSetting.Background = (Brush)new BrushConverter().ConvertFrom("#ffffff");
            mainWindow.bdBill.Background = (Brush)new BrushConverter().ConvertFrom("#ffffff");
            mainWindow.bdDiscount.Background = (Brush)new BrushConverter().ConvertFrom("#ffffff");

            mainWindow.icHome.Foreground = (Brush)new BrushConverter().ConvertFrom("#000000");
            mainWindow.icReport.Foreground = (Brush)new BrushConverter().ConvertFrom("#000000");
            mainWindow.icWareHouse.Foreground = (Brush)new BrushConverter().ConvertFrom("#000000");
            mainWindow.icStaff.Foreground = (Brush)new BrushConverter().ConvertFrom("#000000");
            mainWindow.icSetting.Foreground = (Brush)new BrushConverter().ConvertFrom("#000000");
            mainWindow.icBill.Foreground = (Brush)new BrushConverter().ConvertFrom("#000000");
            mainWindow.icDiscount.Foreground = (Brush)new BrushConverter().ConvertFrom("#000000");

            mainWindow.txtHome.Foreground = (Brush)new BrushConverter().ConvertFrom("#000000");
            mainWindow.txtReport.Foreground = (Brush)new BrushConverter().ConvertFrom("#000000");
            mainWindow.txtWareHouse.Foreground = (Brush)new BrushConverter().ConvertFrom("#000000");
            mainWindow.txtStaff.Foreground = (Brush)new BrushConverter().ConvertFrom("#000000");
            mainWindow.txtSetting.Foreground = (Brush)new BrushConverter().ConvertFrom("#000000");
            mainWindow.txtBill.Foreground = (Brush)new BrushConverter().ConvertFrom("#000000");
            mainWindow.txtDiscount.Foreground = (Brush)new BrushConverter().ConvertFrom("#000000");

            switch (index)
            {
                case 1:
                    mainWindow.grdHome.Visibility = System.Windows.Visibility.Visible;
                    mainWindow.bdHome.Background = (Brush)new BrushConverter().ConvertFrom("#fe8f8f");
                    mainWindow.icHome.Foreground = (Brush)new BrushConverter().ConvertFrom("#ffffff");
                    mainWindow.txtHome.Foreground = (Brush)new BrushConverter().ConvertFrom("#ffffff");
                    break;
                case 2:
                    mainWindow.grdReport.Visibility = System.Windows.Visibility.Visible;
                    mainWindow.bdReport.Background = (Brush)new BrushConverter().ConvertFrom("#fe8f8f");
                    mainWindow.icReport.Foreground = (Brush)new BrushConverter().ConvertFrom("#ffffff");
                    mainWindow.txtReport.Foreground = (Brush)new BrushConverter().ConvertFrom("#ffffff");
                    break;
                case 3:
                    mainWindow.grdWarehouse.Visibility = System.Windows.Visibility.Visible;
                    mainWindow.bdWareHouse.Background = (Brush)new BrushConverter().ConvertFrom("#fe8f8f");
                    mainWindow.icWareHouse.Foreground = (Brush)new BrushConverter().ConvertFrom("#ffffff");
                    mainWindow.txtWareHouse.Foreground = (Brush)new BrushConverter().ConvertFrom("#ffffff");
                    break;
                case 4:
                    mainWindow.grdStaff.Visibility = System.Windows.Visibility.Visible;
                    mainWindow.bdStaff.Background = (Brush)new BrushConverter().ConvertFrom("#fe8f8f");
                    mainWindow.icStaff.Foreground = (Brush)new BrushConverter().ConvertFrom("#ffffff");
                    mainWindow.txtStaff.Foreground = (Brush)new BrushConverter().ConvertFrom("#ffffff");
                    break;
                case 5:
                    mainWindow.grdSetting.Visibility = System.Windows.Visibility.Visible;
                    mainWindow.bdSetting.Background = (Brush)new BrushConverter().ConvertFrom("#fe8f8f");
                    mainWindow.icSetting.Foreground = (Brush)new BrushConverter().ConvertFrom("#ffffff");
                    mainWindow.txtSetting.Foreground = (Brush)new BrushConverter().ConvertFrom("#ffffff");
                    break;
                case 6:
                    mainWindow.BillControl.Visibility = System.Windows.Visibility.Visible;
                    mainWindow.grdBill.Visibility = System.Windows.Visibility.Visible;
                    mainWindow.bdBill.Background = (Brush)new BrushConverter().ConvertFrom("#fe8f8f");
                    mainWindow.icBill.Foreground = (Brush)new BrushConverter().ConvertFrom("#ffffff");
                    mainWindow.txtBill.Foreground = (Brush)new BrushConverter().ConvertFrom("#ffffff");
                    break;
                case 7:
                    mainWindow.grdDiscount.Visibility = System.Windows.Visibility.Visible;
                    mainWindow.bdDiscount.Background = (Brush)new BrushConverter().ConvertFrom("#fe8f8f");
                    mainWindow.icDiscount.Foreground = (Brush)new BrushConverter().ConvertFrom("#ffffff");
                    mainWindow.txtDiscount.Foreground = (Brush)new BrushConverter().ConvertFrom("#ffffff");
                    break;
            }
        }
    }
}
