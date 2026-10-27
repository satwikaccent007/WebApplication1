using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
namespace WebApplication1.Controllers
{
    public class LoginController : Controller
    {
        private AppDb db;
        public LoginController()
        {
            db = new AppDb(); // this is an instanc of DbContext class
                              // this will connect to db and also provide various methods to perform CRUD operations
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(UserModel user)
        {
            db.user.Add(user); //add new data to user table
            db.SaveChanges(); //this save the chnages
            return View();
        }
    }
}
