using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Context;

namespace WebBanHang.Controllers
{
    public class ProductController : Controller
    {
        WebsiteBanHangEntities objWebsiteBanHangEntities = new WebsiteBanHangEntities();
        // GET: Product

        public ActionResult Details(int Id)

        {
            var objProduct = objWebsiteBanHangEntities.Products.Where(n => n.Id == Id).FirstOrDefault();
            return View(objProduct);
        }
        
        
        public ActionResult Index( int Id,string currenFilter, string SearchString, int? page)
        {
            var listProduct = new List<Product>();
            if (SearchString != null)
            {
                page = 1;
            }
            else
            {
                SearchString = currenFilter;
            }
            if (!string.IsNullOrEmpty(SearchString))
            {
               
                listProduct = objWebsiteBanHangEntities.Products.Where(n => n.Name.Contains(SearchString)).ToList();
                // return View(listProduct);
                
            }
            else
            {
                listProduct = objWebsiteBanHangEntities.Products.ToList();
            }
            ViewBag.CurenFilter = SearchString;
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            
            listProduct = listProduct.OrderByDescending(n => n.Id).ToList();
            return View(listProduct.ToPagedList(pageNumber, pageSize));
        }

    }
}