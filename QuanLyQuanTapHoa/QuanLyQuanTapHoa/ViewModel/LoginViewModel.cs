using QuanLyQuanTapHoa.Model;
using QuanLyQuanTapHoa.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLyQuanTapHoa.ViewModel
{
    class LoginViewModel : BaseViewModel
    {
        public ICommand LoginCommand { get; set; }
        public ICommand PasswordChangedCommand { get; set; }

        private string _Username = "";
        public string Username { get => _Username; set { _Username = value; OnPropertyChanged(); } }
        private string _Password = "";
        public string Password { get => _Password; set { _Password = value; OnPropertyChanged(); } }

        public LoginViewModel()
        {
            LoginCommand = new RelayCommand<LoginWindow>((p) => { return true; }, (p) => { Login(p); });
            PasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => { Password = p.Password; });

        }
        public void Login(LoginWindow loginWindow)
        {
            if (Username.Length == 0 || Password.Length == 0)
            {
                if (Username.Length == 0)
                {
                    CustomMessageBox.CustomMessageBox.Show("Vui lòng nhập tài khoản.", 1);
                }
                else if (Password.Length == 0)
                {
                    CustomMessageBox.CustomMessageBox.Show("Vui lòng nhập mật khẩu.", 1);
                }
                else
                {
                    CustomMessageBox.CustomMessageBox.Show("Vui lòng nhập tài khoản và mật khẩu.", 1);
                }
            }
            else
            {
                var accCount = DataProvider.Ins.DB.TaiKhoans.Where(x => x.Username == Username && x.Password == Password).Count();
                if (accCount > 0)
                {
                    MainWindow mainWindow = new MainWindow();
                    loginWindow.Hide();
                    mainWindow.ShowDialog();
                    loginWindow.Show();
                }
                else
                {
                    CustomMessageBox.CustomMessageBox.Show("Tài khoản hoặc mật khẩu không đúng.", 1);
                }
            }
        }
    }
}
