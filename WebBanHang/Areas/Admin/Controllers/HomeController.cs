using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Context;
using WebBanHang.Models;

namespace WebBanHang.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        WebsiteBanHangEntities objWebsiteBanHangEntities = new WebsiteBanHangEntities();
         //GET: Admin/Home
        public ActionResult Index()
         {
            //HomeModel objHomeModel = new HomeModel();
            //objHomeModel.ListProduct = objWebsiteBanHangEntities.Products.ToList();
            //objHomeModel.ListCategory = objWebsiteBanHangEntities.Categorys.ToList();
            //objHomeModel.ListBrand = objWebsiteBanHangEntities.Brands.ToList();
            //objHomeModel.ListOrder = objWebsiteBanHangEntities.Orders.ToList();
            //objHomeModel.ListUser = objWebsiteBanHangEntities.Users.ToList();
            //return View(objHomeModel);
            return View();
        }  
        //WebsiteBanHangEntities dbObj=new WebsiteBanHangEntities();  
        //public ActionResult Index()
        //{
          //  ProductCategoryModel objProductCategoryModel = new ProductCategoryModel();
           // var listProduct = dbObj.Products.ToList;
           // var listCategory = dbObj.Categorys.ToList;
           // objProductCategoryModel.listCategoty = listCategory;
           // objProductCategoryModel.listProduct=listProduct;    
           // return View(objProductCategoryModel);
        }
    }

