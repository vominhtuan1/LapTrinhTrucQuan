using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
namespace QuanLyQuanTapHoa.ViewModel
{
    class WarehouseViewModel:BaseViewModel
    {

        public ICommand OpenImportProduct { get; set; }

        public WarehouseViewModel()
        {
            OpenImportProduct = new RelayCommand<WarehouseViewModel>((p) => { return true; }, (p) => { OpenImportProductWD(); });
        }

        public void OpenImportProductWD()
        {
            ImportProductWindow ImportProduct = new ImportProductWindow();
            ImportProduct.ShowDialog();
        }
    }
}
