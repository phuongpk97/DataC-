using FK.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FK.Controllers
{
    public class OrdersController : Controller
    {
        testEntities db = new testEntities();
        public ActionResult Index()
        {
            List<Customer> model = db.Customers.ToList();
            return View(model);
        }
        public ActionResult SaveOrder(long cutomerId, string name, String address, Order[] order)
        {
            string result = "Error! Order Is Not Complete!";
            if (name != null && address != null && order != null)
            {               
                Customer model = new Customer();
                model.CustomerId = cutomerId;   
                model.Name = name;
                model.Address = address;
                model.OrderDate = DateTime.Now;
                db.Customers.Add(model);

                foreach (var item in order)
                {
                    Order O = new Order();
                    O.OrderId = item.OrderId;
                    O.ProductName = item.ProductName;
                    O.Quantity = item.Quantity;
                    O.Price = item.Price;
                    O.Amount = item.Amount;
                    O.CustomerId = item.CustomerId;
                    db.Orders.Add(O);
                }
                db.SaveChanges();
                result = "Success! Order Is Complete!";
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}