using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace QuanLyQuanTapHoa.CustomMessageBox
{
    public static class CustomMessageBox
    {
        public static MessageBoxResult Show(string message, int Type)
        {
            CustomMessageBoxWindow msg = new CustomMessageBoxWindow(message, Type);
            msg.ShowDialog();
            return msg.Result;
        }
    }
}
