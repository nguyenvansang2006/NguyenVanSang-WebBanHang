using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebBanHang.Context;

namespace WebBanHang.Models
{
    public class CartModel
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public double PriceDiscount { get; set; }
        public double Price { get; set; }

        public double Total => Quantity * PriceDiscount;

    }
}