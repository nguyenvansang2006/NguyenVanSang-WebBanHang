using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Context;
using static WebBanHang.Common;

namespace WebBanHang.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        WebsiteBanHangEntities objWebsiteBanHangEntities = new WebsiteBanHangEntities();
        // GET: Admin/User
        public ActionResult Index(string currenFilter, string SearchString, int? page)
        {
            var listUser = new List<User>();
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

                listUser = objWebsiteBanHangEntities.Users.Where(n => n.LastName.Contains(SearchString)).ToList();
                // return View(listProduct);

            }
            else
            {
                listUser = objWebsiteBanHangEntities.Users.ToList();
            }
            ViewBag.CurenFilter = SearchString;
            int pageSize = 10;
            int pageNumber = (page ?? 1);

            listUser = listUser.OrderByDescending(n => n.Id).ToList();
            return View(listUser.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult Details(int Id)
        {
            var objUser= objWebsiteBanHangEntities.Users.Where(n => n.Id == Id).FirstOrDefault();
            return View(objUser);
        }
        [HttpGet]
        public ActionResult Delete(int Id)
        {
            var objUser = objWebsiteBanHangEntities.Users.Where(n => n.Id == Id).FirstOrDefault();
            return View(objUser);
        }
        [HttpPost]
        public ActionResult Delete(User objPro)
        {
            var objUser = objWebsiteBanHangEntities.Users.Where(n => n.Id == objPro.Id).FirstOrDefault();
            objWebsiteBanHangEntities.Users.Remove(objUser);
            objWebsiteBanHangEntities.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int Id)
        {
            var objUser = objWebsiteBanHangEntities.Users.Where(n => n.Id == Id).FirstOrDefault();
            return View(objUser);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(User objUser)
        {
            
            objWebsiteBanHangEntities.Entry(objUser).State = EntityState.Modified;
            objWebsiteBanHangEntities.SaveChanges();
            return View(objUser);
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
        public ActionResult Create(User objUser)
        {
            this.LoadData();
            if (ModelState.IsValid)
            {
                try
                {
                    var check = objWebsiteBanHangEntities.Users.FirstOrDefault(s => s.Email == objUser.Email);
                    if (check == null)
                    {
                        objUser.Password = GetMD5(objUser.Password);
                        objWebsiteBanHangEntities.Configuration.ValidateOnSaveEnabled = false;
                        //objWebsiteBanHangEntities.Users.Add(objUser);
                        //objUser.CreatedOnUser = DateTime.Now;
                        objWebsiteBanHangEntities.Users.Add(objUser);
                        objWebsiteBanHangEntities.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
                catch
                {
                    ViewBag.error = "Email already exists";
                    return View();
                }
            }
            return View(objUser);
        }

        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
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