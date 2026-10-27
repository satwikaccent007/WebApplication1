using Microsoft.AspNetCore.Mvc;
using DataAccessLayer.Models;
using DataAccessLayer.Irepo;
namespace WebApplication2.Controllers
{
    public class UserController:Controller
    {
        private readonly Iuser _userRepo;
        public UserController(Iuser userRepo)
        {
            _userRepo = userRepo;
        }
        [HttpGet]
        public IActionResult AddUser()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddUser(UserModel user)
        {
            Task<bool> operation = _userRepo.AddUser(user); // here the task starts executing
            Console.WriteLine("Doing something else"); // in between I can do other things
            bool isAdded = await operation; // here I await the result of the task
            // if i do not wait then as Adduser is async it is non blcking so it will progress
            if (isAdded)
            {
                return RedirectToAction("GetUsers");
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            List<UserModel> users = await _userRepo.GetUsers();
            return View(users);
        }
    }
}
