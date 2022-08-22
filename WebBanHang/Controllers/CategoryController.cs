using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Context;

namespace WebBanHang.Controllers
{
    public class CategoryController : Controller
    {
        WebsiteBanHangEntities objWebsiteBanHangEntities = new WebsiteBanHangEntities();
        // GET: Category
        public ActionResult Index()
        {
            var listCategory= objWebsiteBanHangEntities.Categorys.ToList();
            return View(listCategory);
        }
       // public ActionResult ProductCategoty(int Id)
        //{
         //var listProduct= objWebsiteBanHangEntities.Products.Where(n=>n.CategoryId== Id).ToList();    
        //return View(listProduct);
        //}
        public ActionResult ProductCategoty(int Id , int ? page  )
        {
            var listProduct= objWebsiteBanHangEntities.Products.Where(n => n.CategoryId == Id).ToList();
            listProduct = listProduct.OrderByDescending(n => n.Id).ToList();
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            //listProduct = objWebsiteBanHangEntities.Products.ToList();
            return View(listProduct.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult ProductBrand(int Id)
        {
            var listBrand=objWebsiteBanHangEntities.Brands.ToList();
            return View(listBrand);
        }
    }
}