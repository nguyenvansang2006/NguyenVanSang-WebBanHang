using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebBanHang.Context;

namespace WebBanHang.Models
{
    public class HomeModel
    {
        public List<Category> ListCategory { get; set; }

        public List<Product> ListProduct {get;set;}
        public List<Brand> ListBrand { get; set; }
        public List<Order> ListOrder { get; set; }
        public List<User> ListUser { get; set; }
        
    }
}