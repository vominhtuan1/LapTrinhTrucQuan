using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace QuanLyQuanTapHoa
{
    /// <summary>
    /// Interaction logic for CustomMessageBoxWindow.xaml
    /// </summary>
    public partial class CustomMessageBoxWindow : Window
    {

        internal string Message
        {
            get
            {
                return TextMessage.Text;
            }
            set
            {
                TextMessage.Text = value;
            }
        }
        public MessageBoxResult Result { get; set; }

        internal CustomMessageBoxWindow(string message, int type)
        {
            InitializeComponent();
            Message = message;
            DisplayType(type);
        }
        private void DisplayType(int type)
        {
            switch (type)
            {
                case 1:
                    img.Source = new BitmapImage(new Uri(@"/Images/sad.png", UriKind.Relative));
                    OK.Visibility = System.Windows.Visibility.Visible;
                    break;
                case 2:
                    img.Source = new BitmapImage(new Uri(@"/Images/sad.png", UriKind.Relative));
                    Yes.Visibility = System.Windows.Visibility.Visible;
                    No.Visibility = System.Windows.Visibility.Visible;
                    break;
                case 3:
                    img.Source = new BitmapImage(new Uri(@"/Images/smile.png", UriKind.Relative));
                    OK.Visibility = System.Windows.Visibility.Visible;
                    break;
            }
        }

        private void PackIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Result = MessageBoxResult.OK;
            Close();
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            Result = MessageBoxResult.OK;
            Close();
        }

        private void Yes_Click(object sender, RoutedEventArgs e)
        {
            Result = MessageBoxResult.Yes;
            Close();
        }

        private void No_Click(object sender, RoutedEventArgs e)
        {
            Result = MessageBoxResult.No;
            Close();
        }

        private void CustomMessage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
    }
}
