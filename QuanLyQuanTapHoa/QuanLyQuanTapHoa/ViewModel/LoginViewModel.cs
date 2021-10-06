using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuanLyQuanTapHoa.ViewModel
{
    class LoginViewModel : BaseViewModel
    {
        public ICommand LoginCommand { get; set; }

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand<LoginWindow>((p) => { return true; }, (p) => { Login(p); });
        }
        public void Login(LoginWindow loginWindow)
        {
            MainWindow mainWindow = new MainWindow();
            loginWindow.Hide();
            mainWindow.ShowDialog();
            loginWindow.Show();
        }
    }
}
