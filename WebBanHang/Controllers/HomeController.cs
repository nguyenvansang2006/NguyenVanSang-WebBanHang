using Facebook;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Context;
using WebBanHang.Models;

namespace WebBanHang.Controllers
{
    public class HomeController : Controller

    {
        private Uri RedirectUri
        {
            get { 
                var uriBuilder=new UriBuilder(Request.Url);
                uriBuilder.Query = null;
                uriBuilder.Fragment = null;
                uriBuilder.Path = Url.Action("FacebookCallback");
                return uriBuilder.Uri;

                } 
        }
        WebsiteBanHangEntities objWebsiteBanHangEntities = new WebsiteBanHangEntities();
        public ActionResult Index()

        {
            HomeModel objHomeModel = new HomeModel();
            objHomeModel.ListProduct = objWebsiteBanHangEntities.Products.ToList();
            objHomeModel.ListCategory = objWebsiteBanHangEntities.Categorys.ToList();
            return View(objHomeModel);
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(User _user)
        {
            if (ModelState.IsValid)
            {
                var check = objWebsiteBanHangEntities.Users.FirstOrDefault(s => s.Email == _user.Email);
                if (check == null)
                {
                    _user.Password = GetMD5(_user.Password);
                    objWebsiteBanHangEntities.Configuration.ValidateOnSaveEnabled = false;
                    objWebsiteBanHangEntities.Users.Add(_user);
                    objWebsiteBanHangEntities.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "Email already exists";
                    return View();
                }


            }
            return View();

        
        }

        //create a string MD5
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

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string password)
        {
            if (ModelState.IsValid)
            {


                var f_password = GetMD5(password);
                var data = objWebsiteBanHangEntities.Users.Where(s => s.Email.Equals(email) && s.Password.Equals(f_password)).ToList();
                if (data.Count() > 0)
                {
                    //add session
                    Session["FullName"] = data.FirstOrDefault().FistName + " " + data.FirstOrDefault().LastName;
                    Session["Email"] = data.FirstOrDefault().Email;
                    Session["idUser"] = data.FirstOrDefault().Id;
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "Login failed";
                    return RedirectToAction("Login");
                }
            }
            return View();
        }
        public ActionResult LoginFaceBook()
        {
            var fb = new FacebookClient();
            var loginUrl= fb.GetLoginUrl(new
            {
                client_id= ConfigurationManager.AppSettings ["FbAppId"],
                client_secret= ConfigurationManager.AppSettings["FbAppSecret"],
                redirect_uri=RedirectUri.AbsoluteUri,
                response_type="Code",
                scope="email",
            });
            return Redirect(loginUrl.AbsoluteUri);

        }
        public ActionResult FacebookCallback(string code)
        {
            var fb= new FacebookClient();
            dynamic result = fb.Post("auth/access_token", new
            {
                client_id = ConfigurationManager.AppSettings["FbAppId"],
                client_secret = ConfigurationManager.AppSettings["FbAppSecret"],
                redirect_uri = RedirectUri.AbsoluteUri,
                code = code
            }) ; 
            var accessToken = result.access_token;
            if(!string.IsNullOrEmpty(accessToken))
            {
                dynamic me = fb.Get("me?fields=first_name,middle_name,last_name,id,email");
                string email=me.email;
                string username=me.username;
                string firstname=me.firstname;
                string middlename=me.middlename;
                string lastname=me.lastname;

                var user = new User();
                user.Email = email;
                //user.UserName = username;
                //user.Status = true;
                //user.Name = firstname + "" + middlename + "" + lastname;
                //user.CreateDate = DateTime.Now;
                //var resultInsert = new UserDao.InsertForFaceBook(user);
            }
            else
            {

            }
           return Redirect("/");
        }

        //Logout
        public ActionResult Logout()
        {
            Session.Clear();//remove session
            return RedirectToAction("Login");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}