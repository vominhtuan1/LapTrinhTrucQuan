using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuanLyQuanTapHoa.ViewModel
{
    class SettingViewModel : BaseViewModel
    {
        public ICommand OpenAddProduct { get; set; }
        public ICommand OpenEditProduct { get; set; }

        public SettingViewModel()
        {
            OpenAddProduct = new RelayCommand<SettingViewModel>((p) => { return true; }, (p) => { OpenAddProductWD(); });
            OpenEditProduct = new RelayCommand<SettingViewModel>((p) => { return true; }, (p) => { OpenEditProductWD(); });
        }

        public void OpenAddProductWD()
        {
            AddProductWindow addProductWindow = new AddProductWindow();
            addProductWindow.ShowDialog();
        }

        public void OpenEditProductWD()
        {
            EditProductWindow editProductWindow = new EditProductWindow();
            editProductWindow.ShowDialog();
        }
    }
}
