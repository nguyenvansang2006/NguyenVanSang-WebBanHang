using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Context;
using static WebBanHang.Common;

namespace WebBanHang.Areas.Admin.Controllers
{
    
    public class OrderController : Controller
    {
        WebsiteBanHangEntities objWebsiteBanHangEntities = new WebsiteBanHangEntities();
        // GET: Admin/Order
        public ActionResult Index(string currenFilter, string SearchString, int? page)
        {
            var listOrder = new List<Order>();
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

                listOrder = objWebsiteBanHangEntities.Orders.Where(n => n.Name.Contains(SearchString)).ToList();
                // return View(listProduct);

            }
            else
            {
                listOrder = objWebsiteBanHangEntities.Orders.ToList();
            }
            ViewBag.CurenFilter = SearchString;
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            listOrder = listOrder.OrderByDescending(n => n.Id).ToList();
            return View(listOrder.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Details(int Id)
        {
            var objOrder = objWebsiteBanHangEntities.Orders.Where(n => n.Id == Id).FirstOrDefault();
            return View(objOrder);
        }
        [HttpGet]
        public ActionResult Delete(int Id)
        {
            var objOrder = objWebsiteBanHangEntities.Orders.Where(n => n.Id == Id).FirstOrDefault();
            return View(objOrder);
        }
        [HttpPost]
        public ActionResult Delete(Order objPro)
        {
            var objOrder = objWebsiteBanHangEntities.Orders.Where(n => n.Id == objPro.Id).FirstOrDefault();
            objWebsiteBanHangEntities.Orders.Remove(objOrder);
            objWebsiteBanHangEntities.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int Id)
        {
            var objOrder = objWebsiteBanHangEntities.Orders.Where(n => n.Id == Id).FirstOrDefault();
            return View(objOrder);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Order objOrder)
        {
            objWebsiteBanHangEntities.Entry(objOrder).State = EntityState.Modified;
            objWebsiteBanHangEntities.SaveChanges();
            return View(objOrder);
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
        public ActionResult Create(Order objOrder)
        {
            this.LoadData();
            if (ModelState.IsValid)
            {
                try
                {
                    objOrder.CreatedOnUser = DateTime.Now;
                    objWebsiteBanHangEntities.Orders.Add(objOrder);
                    objWebsiteBanHangEntities.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }
            return View(objOrder);
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