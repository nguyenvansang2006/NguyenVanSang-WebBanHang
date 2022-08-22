using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebBanHang.Context;
using WebBanHang.Models;

namespace WebBanHang.Dao
{
   
    public class ProductDao
    {
        //WebsiteBanHangEntities objWebsiteBanHangEntities = new WebsiteBanHangEntities();
        private MyDBContext db = new MyDBContext();
        public List<Product> getList(string status = "All")
        {
            List<Product> list = null;
            switch (status)
            {
                case "Index":
                    {
                        list = db.Products.Where(m => m.Status != 0).ToList();
                        break;
                    }

                case "Trash":
                    {
                        list = db.Products.Where(m => m.Status == 0).ToList();
                        break;
                    }
                default:
                    {
                        list = db.Products.ToList();
                        break;
                    }

            }
            return list;
        }
    }
}