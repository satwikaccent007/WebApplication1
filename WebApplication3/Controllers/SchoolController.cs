using Microsoft.AspNetCore.Mvc;
using DataLayer.Models;
using DataLayer.Irepo;
namespace WebApplication3.Controllers
{
    public class SchoolController:Controller
    {
        private readonly Ischool _repo;

        public SchoolController(Ischool _repo)
        {
            this._repo = _repo;
        }
        public async Task<IActionResult> GetStudents()
        {
            List<Student> students = await _repo.GetStudents();
            return View(students);
        }
    }
}
