using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebBanHang.Context;
using WebBanHang.Models;

namespace WebBanHang.Dao
{
    public class CategoryDao
    {
        private MyDBContext db = new MyDBContext();
        public List<Category> getList(string status = "All")
        {
            List<Category> list = null;
            switch (status)
            {
                case "Index":
                    {
                        list = db.Categoris.Where(m => m.Status != 0).ToList();
                        break;
                    }

                case "Trash":
                    {
                        list = db.Categoris.Where(m => m.Status == 0).ToList();
                        break;
                    }
                default:
                    {
                        list = db.Categoris.ToList();
                        break;
                    }

            }
            return list;
        }
    }
}