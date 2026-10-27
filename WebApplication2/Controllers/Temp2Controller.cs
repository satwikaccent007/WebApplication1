using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query;

namespace WebApplication2.Controllers
{
    public class Temp2Controller : Controller
    {
        public IActionResult Index1()
        {
            ViewBag.vb =  TempData.Peek("td");
            return View();
        }
    }
}
