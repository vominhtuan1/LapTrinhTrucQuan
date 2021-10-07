using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace QuanLyQuanTapHoa.ViewModel
{
    class HomeViewModel: BaseViewModel
    {
        public List<product> productList { get; set; }
        public class product
        {
            public string name { get; set; }
            public int price { get; set; }
        }
        public HomeViewModel()
        {
            productList = new List<product>();
            productList.Add(new product() { name = "kem", price = 2000 });
            productList.Add(new product() { name = "sua chua", price = 5000 });
            productList.Add(new product() { name = "tra sua", price = 20000 });
            productList.Add(new product() { name = "banh gao", price = 40000 });
            productList.Add(new product() { name = "dau an", price = 10000 });
            productList.Add(new product() { name = "dau goi", price = 20000 });
            productList.Add(new product() { name = "cocacola", price = 5000 });
            productList.Add(new product() { name = "pepsi", price = 5000 });
            productList.Add(new product() { name = "bim bim", price = 10000 });
            productList.Add(new product() { name = "sua bich", price = 7000 });
            productList.Add(new product() { name = "bia tiger", price = 10000 });
            productList.Add(new product() { name = "bia sai gon", price = 10000 });
            productList.Add(new product() { name = "bao tay", price = 2000 });
            productList.Add(new product() { name = "bot giat", price = 2000 });
        }


    }
}
