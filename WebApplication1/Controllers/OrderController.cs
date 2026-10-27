using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{

    public class OrderController: Controller
    {
        private AppDb db;
        public OrderController()
        {
            db = new AppDb();
        }
        [HttpGet]
        public IActionResult SubmitOrder()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult SubmitOrder(OrderModel order)
        {
            db.order.Add(order);
            int res = db.SaveChanges(); // this returns the number of rows effected
            if (res > 0)
            {
                return RedirectToAction("GetOrder"); // redict GetOrder action method
            }
            return View();
        }

        [HttpGet]
        public IActionResult GetOrder()
        {
            List<OrderModel> orders = db.order.ToList();
            return View(orders);
        }
        [HttpPost]
        public IActionResult DeleteOrder(int orderId)
        {
            var order = db.order.Find(orderId); // Find the order by its ID
            if (order != null)
            {
                db.order.Remove(order);
                db.SaveChanges();
            }
            return RedirectToAction("GetOrder");
        }

        [HttpGet]
        public IActionResult UpdateOrder(int orderId)
        {
            OrderModel order = db.order.Find(orderId);
            if (order != null)
            {
                return View(order);
                // here the order object is passed to the view
                // so we can directly use the order object as a model in the view 
                // we just need to use asp-for tag helper to bind the order object to the form fields in the view
            }
            return RedirectToAction("GetOrder");
        }
        [HttpPost]
        public IActionResult UpdateOrder(OrderModel order)
        {
            db.order.Update(order);
            int res = db.SaveChanges();
            if (res > 0)
            {
                return RedirectToAction("GetOrder");
            }
            return View(order);
        }
    }
}
