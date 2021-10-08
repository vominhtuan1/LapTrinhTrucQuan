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
        public List<cartItem> cartItemList { get; set; }
        public class cartItem
        {
            public string name { get; set; }
            public int soluong { get; set; }
        }
        public class product
        {
            public string name { get; set; }
            public int price { get; set; }
        }
        public HomeViewModel()
        {
            cartItemList = new List<cartItem>();
            cartItemList.Add(new cartItem() { name = "Mì 3 miền", soluong = 10 });
            cartItemList.Add(new cartItem() { name = "Dầu gội đầu", soluong = 1 });
            cartItemList.Add(new cartItem() { name = "Bánh gạo", soluong = 2 });
            cartItemList.Add(new cartItem() { name = "Cocacola", soluong = 5 });
            cartItemList.Add(new cartItem() { name = "Dầu ăn", soluong = 1 });
            cartItemList.Add(new cartItem() { name = "Bánh tráng", soluong = 10 });
            cartItemList.Add(new cartItem() { name = "Súc xích", soluong = 10 });
            cartItemList.Add(new cartItem() { name = "Cháo gói", soluong = 1 });
            cartItemList.Add(new cartItem() { name = "Kem đánh răng", soluong = 1 });
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
