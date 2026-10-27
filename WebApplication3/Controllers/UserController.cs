
using DataLayer.Irepo;
using DataLayer.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApplication3.Middlewares;
using WebApplication3.Models;
namespace WebApplication3.Controllers
{
    public class UserController:Controller
    {
        private readonly Iuser _repo;
        public UserController(Iuser repo)
        {
            _repo = repo;
        }
        [HttpGet]
       //[Route("[Controller]/create")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        //[Route("[Controller]/create")]
        public async Task<IActionResult> Create(UserDTO userdto)
        {
            if (!ModelState.IsValid)  
            {
                return View(userdto);   
            }
            UserModel user = new UserModel
            {
                Name = userdto.Name,
                Email = userdto.Email,
                Password = userdto.Password,
                Dob = userdto.Dob,
                Age = userdto.Age,
            };
            bool result = await _repo.CreateUser(user);
            if (result)
            {
                return RedirectToAction("Login");
            }
            return View();
        }
        [HttpGet]
        //[Auth]  This is written by me that uses session 
        [Authorize] //This is a defualt middleware/filter that authroize the user if the user is valid
        //[Route("[Controller]/users")]
        public async Task<IActionResult> Index()
        {
            ViewBag.name = User.FindFirst(ClaimTypes.Name)?.Value;
            List<UserModel> users = await _repo.GetUsers();
            List<UserDTO> usersdto = users.Select(user =>
            {
                return new UserDTO
                {
                    ID = user.ID,
                    Name = user.Name,
                    Email = user.Email,
                    Password = user.Password,
                    Dob = user.Dob,
                    Age = user.Age,
                    ConfirmPassword = user.Password
                };
            }).ToList();
            return View(usersdto);
        } 
        [HttpGet]
        //[Route("")]
        public async Task<IActionResult> Login()
        {
            return View();
        }
        [HttpPost]
        //[Route("")]
        public async Task<IActionResult> Login(string Email, string Password)
        {
            bool result = await _repo.ValidateUser(Email, Password);
            if (result)
            {
                UserModel user = await _repo.GetUserByEmail(Email);
                List<Claim> identityclaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,user.Name),
                    new Claim(ClaimTypes.Email,user.Email)

                }; //Claim is just informations ;

                //This is collection of claims that represent the user
                var identity = new ClaimsIdentity(identityclaims,CookieAuthenticationDefaults.AuthenticationScheme);
                //This represent the authenticated user;
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal); // This basically tells to login the user ans also send the cookie with the encrypted data 
                return RedirectToAction("Index");
            }
            return View();
        }
        [HttpGet]
        //[Route("[Controller]/update/{id}")]
        //[Auth]
        public async Task<IActionResult> Update(int id)
        {
            try
            {
                UserModel user = await _repo.GetUserById(id);
                UserDTO userdto = new UserDTO{
                    ID = user.ID,
                    Name = user.Name,
                    Email = user.Email,
                    Password = user.Password,
                    Dob = user.Dob,
                    Age = user.Age,
                    ConfirmPassword = user.Password
                };
                return View(userdto);
            }
            catch(Exception e)
            {
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        //[Route("[Controller]/update")]
        //[Auth]
        public async Task<IActionResult> Update(UserDTO userdto)
        {
            UserModel user = new UserModel
            {
                ID = userdto.ID,
                Name = userdto.Name,
                Email = userdto.Email,
                Password = userdto.Password,
                Dob = userdto.Dob,
                Age = userdto.Age,
            };
            bool result = await _repo.UpdateUser(user);
            if(result) return RedirectToAction("Index");
            return View(user);
           
        }
        [HttpGet]
        //[Route("[Controller]/delete/{id}")]
        //[Auth]
        public async Task<IActionResult> Delete(int id)
        {
            bool result = await _repo.DeleteUser(id);
            return RedirectToAction("Index");
        }
        [HttpPost]
        [Authorize]
        //[Route("[Controller]/logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}
