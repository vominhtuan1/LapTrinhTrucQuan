using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuanLyQuanTapHoa.ViewModel
{
    class BillViewModel : BaseViewModel
    {
        public ICommand OpenDetailCommand { get; set; }
        public BillViewModel()
        {
            OpenDetailCommand = new RelayCommand<MainWindow>((p) => { return true; }, (p) => { OpenDetail(); });
        }
        public void OpenDetail()
        {
            BillDetailWindow billDetailWindow = new BillDetailWindow();
            billDetailWindow.ShowDialog();
        }
    }
}
