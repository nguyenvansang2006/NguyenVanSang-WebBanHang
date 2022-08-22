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
using static WebBanHang.Common;

namespace WebBanHang.Areas.Admin.Controllers
{
    public class BrandController : Controller
    {
        WebsiteBanHangEntities objWebsiteBanHangEntities = new WebsiteBanHangEntities();
        // GET: Admin/Brand
        public ActionResult Index(string currenFilter, string SearchString, int? page)
        {
            var listBrand = new List<Brand>();
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
                listBrand = objWebsiteBanHangEntities.Brands.Where(n => n.Name.Contains(SearchString)).ToList();
                // return View(listProduct);
            }
            else
            {
                listBrand = objWebsiteBanHangEntities.Brands.ToList();
            }
            ViewBag.CurenFilter = SearchString;
            int pageSize = 5;
            int pageNumber = (page ?? 1);
            listBrand = listBrand.OrderByDescending(n => n.Id).ToList();
            return View(listBrand.ToPagedList(pageNumber, pageSize));


        }

        public ActionResult Details(int Id)
        {
            var objBrand = objWebsiteBanHangEntities.Brands.Where(n => n.Id == Id).FirstOrDefault();
            return View(objBrand);
        }
        [HttpGet]
        public ActionResult Delete(int Id)
        {
            var objBrand = objWebsiteBanHangEntities.Brands.Where(n => n.Id == Id).FirstOrDefault();
            return View(objBrand);
        }
        [HttpPost]
        public ActionResult Delete(Brand objPro)
        {
            var objBrand = objWebsiteBanHangEntities.Brands.Where(n => n.Id == objPro.Id).FirstOrDefault();
            objWebsiteBanHangEntities.Brands.Remove(objBrand);
            objWebsiteBanHangEntities.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int Id)
        {
            var objBrand = objWebsiteBanHangEntities.Brands.Where(n => n.Id == Id).FirstOrDefault();
            return View(objBrand);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Brand objBrand)
        {
            if (objBrand.ImageUpload != null)
            {
                string fileName = Path.GetFileNameWithoutExtension(objBrand.ImageUpload.FileName);
                string extension = Path.GetExtension(objBrand.ImageUpload.FileName);
                fileName = fileName + extension;
                objBrand.Avatar = fileName;
                objBrand.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/"), fileName));
            }
            objWebsiteBanHangEntities.Entry(objBrand).State = EntityState.Modified;
            objWebsiteBanHangEntities.SaveChanges();
            return View(objBrand);
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
        public ActionResult Create(Brand objBrand)
        {
            this.LoadData();
            if (ModelState.IsValid)
            {
                try
                {
                    if (objBrand.ImageUpload != null)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(objBrand.ImageUpload.FileName);
                        string extension = Path.GetExtension(objBrand.ImageUpload.FileName);
                        fileName = fileName + extension;
                        objBrand.Avatar = fileName;
                        objBrand.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Content/images/"), fileName));
                    }
                    objBrand.CreatedOnUser = DateTime.Now;
                    objWebsiteBanHangEntities.Brands.Add(objBrand);
                    objWebsiteBanHangEntities.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }
            return View(objBrand);
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
    }
}
    
