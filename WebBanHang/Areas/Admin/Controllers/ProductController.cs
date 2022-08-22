using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using WebBanHang.Context;
using WebBanHang.Dao;
using WebBanHang.Models;
using static WebBanHang.Common;

namespace WebBanHang.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        private MyDBContext db = new MyDBContext();
        WebsiteBanHangEntities objWebsiteBanHangEntities = new WebsiteBanHangEntities();
        // GET: Admin/Product
        public ActionResult Index(string currenFilter, string SearchString, int? page)
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
            int pageSize = 7;
            int pageNumber = (page ?? 1);
            listProduct = listProduct.OrderByDescending(n => n.Id).ToList();
            return View(listProduct.ToPagedList(pageNumber, pageSize));
        }
        [ValidateInput(false)]
        public ActionResult Details(int Id)
        {
            var objProduct = objWebsiteBanHangEntities.Products.Where(n => n.Id == Id).FirstOrDefault();
            return View(objProduct);
        }
        [HttpGet]
        public ActionResult Delete(int Id)
        {
            var objProduct = objWebsiteBanHangEntities.Products.Where(n => n.Id == Id).FirstOrDefault();
            return View(objProduct);
        }
        [HttpPost]
        public ActionResult Delete(Product objPro)
        {
            var objProduct = objWebsiteBanHangEntities.Products.Where(n => n.Id == objPro.Id).FirstOrDefault();
            objWebsiteBanHangEntities.Products.Remove(objProduct);
            objWebsiteBanHangEntities.SaveChanges();
            return RedirectToAction("Index");
        }
        [ValidateInput(false)]

        public ActionResult Edit(int Id)
        {

            this.LoadData();

            var objProduct = objWebsiteBanHangEntities.Products.Where(n => n.Id == Id).FirstOrDefault();
            return View(objProduct);

        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product objProduct)
        {
            this.LoadData();
            if (ModelState.IsValid)
            {
                try
                {

                    if (objProduct.ImageUpload != null)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(objProduct.ImageUpload.FileName);
                        string extension = Path.GetExtension(objProduct.ImageUpload.FileName);
                        fileName = fileName + extension;
                        objProduct.Avatar = fileName;
                        objProduct.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/"), fileName));
                    }
                    objWebsiteBanHangEntities.Entry(objProduct).State = EntityState.Modified;
                    objWebsiteBanHangEntities.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }
            return View(objProduct);
        }

        [HttpGet]
        public ActionResult Create()
        {

            this.LoadData();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(Product objProduct)
        {
            this.LoadData();
            if (ModelState.IsValid)
            {
                try
                {
                    if (objProduct.ImageUpload != null)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(objProduct.ImageUpload.FileName);
                        string extension = Path.GetExtension(objProduct.ImageUpload.FileName);
                        fileName = fileName + extension;
                        objProduct.Avatar = fileName;
                        objProduct.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/"), fileName));
                    }
                    objProduct.CreatedOnUser = DateTime.Now;
                    objWebsiteBanHangEntities.Products.Add(objProduct);
                    objWebsiteBanHangEntities.SaveChanges();
                    //TempData["message"] = "Thanh cong";
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }
            return View(objProduct);
        }
        void LoadData()
        {
            Common objCommon = new Common();
            var listCat = objWebsiteBanHangEntities.Categorys.ToList();
            ListtoDataTableConverter converter = new ListtoDataTableConverter();
            DataTable dtCategory = converter.ToDataTable(listCat);
            ViewBag.ListCategory = objCommon.ToSelectList(dtCategory, "Id", "Name");
            var listBrand = objWebsiteBanHangEntities.Brands.ToList();
            DataTable dtBrand = converter.ToDataTable(listBrand);

            ViewBag.ListBrand = objCommon.ToSelectList(dtBrand, "Id", "Name");

            List<ProductType> listProductType = new List<ProductType>();
            ProductType objProductType = new ProductType();
            objProductType.Id = 01;
            objProductType.Name = "Giảm giá sốc";
            listProductType.Add(objProductType);

            objProductType = new ProductType();
            objProductType.Id = 02;
            objProductType.Name = "Đề xuất";
            listProductType.Add(objProductType);

            DataTable dtProductType = converter.ToDataTable(listProductType);

            ViewBag.ProductType = objCommon.ToSelectList(dtProductType, "Id", "Name");
        }

        //[HttpGet]
        public ActionResult Trash( string Status)
        {
            //var listProduct = objWebsiteBanHangEntities.Products.Where(m => m.Status == 0).ToList();
            //return View(listProduct);
            var objProduct = objWebsiteBanHangEntities.Products.Where(n => n.Name== Status).ToList();
            return View(objProduct);
        }
        //[HttpPost]
        //public ActionResult Trash(Product Status)
        //{
            //var objProduct = objWebsiteBanHangEntities.Products.Where(n => n.Name == Status.Name).ToList();
           // objWebsiteBanHangEntities.Products.Remove(Status);
           // objWebsiteBanHangEntities.SaveChanges();
            //return RedirectToAction("Index");
        //}
        public ActionResult Status(int ? id)
        {
            if (id == null)
           {
                 return RedirectToAction("Index", "Product");
            }
            
            
            return RedirectToAction("Index", "Product");
            //return View();
            //Product product = Product.getRow(Id);
            //if(product == null)
            //{
            //   return RedirectToAction("Index", "Product");

            //}
        }
        
        
        //[HttpGet]
        //public ActionResult Brand()
        //{
           // var objBrand = objWebsiteBanHangEntities.Brands.ToList();
            //return View(objBrand);
            
       // }
        //[HttpPost]
        //public ActionResult Brand()
        //{
         //   return View();
        //}
        //[HttpGet]
        //public ActionResult Category()
        //{
           // var objCategory = objWebsiteBanHangEntities.Categorys.ToList();
            //return View(objCategory);
        //}
        //[HttpPost]
        //public ActionResult Category()
        //{
            //return View();
        //}
    }

}

