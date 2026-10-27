using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
namespace WebApplication1.Controllers
{
    public class BooksController : Controller
    {
        private AppDb db;
        public BooksController()
        {
            db = new AppDb();
        }
        [HttpGet]
        public IActionResult AddBooks()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddBooks(BookModel book)
        {
            db.books.Add(book);
            int rowsAffected = db.SaveChanges();
            if(rowsAffected > 0)
            {
                return RedirectToAction("ShowBooks");
            }
            return View();
        }
        [HttpGet]
        public IActionResult ShowBooks()
        {
            List<BookModel> books = db.books.ToList();
            return View(books); // anything i send to view is stored like ViewBag.Model or ViewData.Model
        }

        [HttpGet]
        public IActionResult BookDetails(int BookId)
        {
            BookModel book  = db.books.Find(BookId);
            if(book == null)
            {
                return RedirectToAction("ShowBooks");
            }
            return View(book);
            // here also the book object is sent to view and stored in ViewBag.Model or ViewData.Model
            // so to access i can directly do @Model.PropertyName in the view
        }
        [HttpGet]
        public IActionResult EditBook(int BookId)
        {
            BookModel book = db.books.Find(BookId);
            if(book == null)
            {
                return RedirectToAction("ShowBooks");
            }
            return View(book);
        }
        [HttpPost]
        public IActionResult EditBook(BookModel book)
        {
            db.books.Update(book);
            int rowsAffected = db.SaveChanges();
            if(rowsAffected > 0)
            {
                return RedirectToAction("ShowBooks");
            }
            return View(book);
        }
        [HttpPost]
        public IActionResult DeleteBook(int BookId)
        {
            BookModel book = db.books.Find(BookId);
            if (book != null)
            {
                db.books.Remove(book);
                db.SaveChanges();
            }
            return RedirectToAction("ShowBooks");
        }
    }
}
