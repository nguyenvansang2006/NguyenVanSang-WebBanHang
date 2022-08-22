using Control.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebBanHang.Context;

namespace Control.Dao
{
    public class ProductDao
    {
        private MyDBContext db = new MyDBContext();
        public List<ProductDao> getList(string status = "All")
        {
            List<ProductDao> list = null;
            switch (status)
            {
                case "Index":
                    {
                       //list = db.Products.Where(m => m.Status != 0).ToList();
                        break;
                    }

                case "Bin":
                    {
                        //list = db.Products.Where(m => m.Status == 0).ToList();
                        break;
                    }
                default:
                    {
                        //list = db.Products.ToList();
                        break;
                    }

            }
            return list;
        }
    }
}
    


    

