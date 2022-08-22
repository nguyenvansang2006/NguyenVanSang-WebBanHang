using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Context;
using WebBanHang.Dao;
using static WebBanHang.Common;

namespace WebBanHang.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        WebsiteBanHangEntities objWebsiteBanHangEntities = new WebsiteBanHangEntities();
        // GET: Admin/Category
        public ActionResult Index(string currenFilter, string SearchString, int? page)
        {
            var listCategory = new List<Category>();
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

                listCategory = objWebsiteBanHangEntities.Categorys.Where(n => n.Name.Contains(SearchString)).ToList();
                // return View(listProduct);

            }
            else
            {
                listCategory = objWebsiteBanHangEntities.Categorys.ToList();
            }
            ViewBag.CurenFilter = SearchString;
            int pageSize = 5;
            int pageNumber = (page ?? 1);

            listCategory = listCategory.OrderByDescending(n => n.Id).ToList();
            return View(listCategory.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Details(int Id)
        {
            var objCategory = objWebsiteBanHangEntities.Categorys.Where(n => n.Id == Id).FirstOrDefault();
            return View(objCategory);
        }
        [HttpGet]
        public ActionResult Delete(int Id)
        {
            var objCategory = objWebsiteBanHangEntities.Categorys.Where(n => n.Id == Id).FirstOrDefault();
            return View(objCategory);
        }
        [HttpPost]
        public ActionResult Delete(Category objPro)
        {
            var objCategory = objWebsiteBanHangEntities.Categorys.Where(n => n.Id == objPro.Id).FirstOrDefault();
            objWebsiteBanHangEntities.Categorys.Remove(objCategory);
            objWebsiteBanHangEntities.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int Id)
        {
            var objCategory = objWebsiteBanHangEntities.Categorys.Where(n => n.Id == Id).FirstOrDefault();
            return View(objCategory);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Category objCategory)
        {
            if (objCategory.ImageUpload != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(objCategory.ImageUpload.FileName);
                string extension = Path.GetExtension(objCategory.ImageUpload.FileName);
                fileName = fileName + extension;
                objCategory.Avatar = fileName;
                objCategory.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/"), fileName));
            }
            objWebsiteBanHangEntities.Entry(objCategory).State = EntityState.Modified;
            objWebsiteBanHangEntities.SaveChanges();
            return View(objCategory);
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
        public ActionResult Create(Category objCategory)
        {
            this.LoadData();
            if (ModelState.IsValid)
            {
                try
                {
                    if (objCategory.ImageUpload != null)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(objCategory.ImageUpload.FileName);
                        string extension = Path.GetExtension(objCategory.ImageUpload.FileName);
                        fileName = fileName + extension;
                        objCategory.Avatar = fileName;
                        objCategory.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/"), fileName));
                    }
                    objCategory.CreatedOnUser = DateTime.Now;
                    objWebsiteBanHangEntities.Categorys.Add(objCategory);
                    objWebsiteBanHangEntities.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }
            return View(objCategory);
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


        }
        public ActionResult Trash()
        {
            //var objProduct = objWebsiteBanHangEntities.Products.Where(n => n.Name== Status).ToList();
            return View();
        }
        public ActionResult Status(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Category");
            }


            return RedirectToAction("Index", "Category");
        }
        }
}