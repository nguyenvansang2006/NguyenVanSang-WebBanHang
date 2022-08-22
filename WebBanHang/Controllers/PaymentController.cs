using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebBanHang.Context;
using WebBanHang.Models;

namespace WebBanHang.Controllers
{
    public class PaymentController : Controller
    {
        WebsiteBanHangEntities objWebsiteBanHangEntities = new WebsiteBanHangEntities();
        // GET: Payment
        public ActionResult Index()
        {
            if (Session["idUser"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                var listCart = (List<CartModel>)Session["cart"];
                Order objOrder = new Order();
                objOrder.Name = "DonHang-" + DateTime.Now.ToString("yyyyMMddHHmmss");
                objOrder.UserId= int.Parse(Session["idUser"].ToString());
                objOrder.CreatedOnUser = DateTime.Now;
                objOrder.Status = 1;
                objWebsiteBanHangEntities.Orders.Add(objOrder);
                objWebsiteBanHangEntities.SaveChanges();
                int intOrderId = objOrder.Id;
                List<OrderDetail> listOrderDetails = new List<OrderDetail>();
                foreach(var item in listCart)
                {
                    OrderDetail obj=new OrderDetail();
                    obj.OrderId = intOrderId;
                    obj.Quantity = item.Quantity;
                    obj.ProductId = item.Product.Id;
                    listOrderDetails.Add(obj);
                }
                objWebsiteBanHangEntities.OrderDetails.AddRange(listOrderDetails);
                objWebsiteBanHangEntities.SaveChanges();
            }

            return View();
        }
        public ActionResult ProductPayment()
        {
            //var listPayment=objWebsiteBanHangEntities.Orders.ToString();
            return View();  
            
        }
        [HttpPost]
        public ActionResult ProductPayment(FormCollection filed)
        {
            //string username = filed["username"];
            //var listPayment=objWebsiteBanHangEntities.Orders.ToString();
            return View();

        }
    }
}