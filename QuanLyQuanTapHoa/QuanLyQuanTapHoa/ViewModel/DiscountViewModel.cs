using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuanLyQuanTapHoa.ViewModel
{
    class DiscountViewModel : BaseViewModel
    {
        public ICommand OpenAddDiscount { get; set; }

        public DiscountViewModel()
        {
            OpenAddDiscount = new RelayCommand<SettingViewModel>((p) => { return true; }, (p) => { OpenAddDiscountWD(); });
        }

        public void OpenAddDiscountWD()
        {
            AddDiscountWindow addDiscountWindow = new AddDiscountWindow();
            addDiscountWindow.ShowDialog();
        }
    }
}
