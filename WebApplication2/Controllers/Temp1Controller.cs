using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query;

namespace WebApplication2.Controllers
{
    public class Temp1Controller:Controller
    {
        public IActionResult Index1() 
        {
            ViewBag.vb = "ViewBag";
            ViewData["vd"] = "ViewData";
            TempData["td"] = "TempData";
            return RedirectToAction("Index1","Temp2"); 
        }
        public IActionResult Index2()
        {
            var value = TempData["td"];
            ViewBag.vb = value;
            TempData.Keep();
            return View();
        }
    }
}
