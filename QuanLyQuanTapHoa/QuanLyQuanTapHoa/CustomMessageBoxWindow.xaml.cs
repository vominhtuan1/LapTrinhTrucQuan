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
                    Type.Background = (Brush)new BrushConverter().ConvertFrom("#FF0000");
                    MessageIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.CloseCircle;
                    MessageIcon.Foreground = (Brush)new BrushConverter().ConvertFrom("#FF0000");
                    break;
                case 2:
                    Type.Background = (Brush)new BrushConverter().ConvertFrom("#FFFF00");
                    MessageIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.AlertCircle;
                    MessageIcon.Foreground = (Brush)new BrushConverter().ConvertFrom("#FFFF00");
                    break;
                case 3:
                    Type.Background = (Brush)new BrushConverter().ConvertFrom("#00DD00");
                    MessageIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.CheckboxMarkedCircle;
                    MessageIcon.Foreground = (Brush)new BrushConverter().ConvertFrom("#00DD00");
                    break;
            }
        }

        private void PackIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Result = MessageBoxResult.OK;
            Close();
        }
    }
}
