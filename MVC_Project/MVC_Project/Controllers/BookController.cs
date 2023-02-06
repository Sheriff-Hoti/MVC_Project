using Microsoft.AspNetCore.Mvc;
using MVC_Project.Models.Book;
using MVC_Project.Wrappers;

namespace MVC_Project.Controllers
{
    public class BookController : Controller
    {

        private readonly BookServices bookServices;
        
        public BookController(BookServices _bs)
        {
            bookServices = _bs;
        }
        public async Task<IActionResult> Index(
            [FromQuery]
            PageRequest<Book> request
            )
        {
            PageResponse<Book> response = await bookServices.List(request);
            ViewBag.response = response;
            ViewBag.request = request;
            return View(response);
        }
        public async Task<IActionResult> Details(Guid? id)
        {
        if (id == null)
            {
                return NotFound();
            }
            var book = await bookServices.Details(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        //GET
        [HttpGet]
        public async Task<IActionResult> Create()
        {
           // ViewBag.categories = await bookServices.GetAllCategories();
            return View();
        }

        //POST
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            Book book)
        {
            if (bookServices.GetByTitle(book.Title) != null)
            {
                ModelState.AddModelError("Title", "Title taken");
            }
            if (!ModelState.IsValid)
            {
                return View(book);
            }

            Book bookk = new Book()
            {
                Id = book.Id,
                Title = book.Title,
                createdAt = DateTime.Now,
                Author = book.Author,
                PublishedDate = book.PublishedDate,
                updatedAt = DateTime.Now,
                TypeBookCategoryId = book.TypeBookCategoryId
            };

            await bookServices.AddBook(book);
            TempData["success"] = "Book created successfully";
            return RedirectToAction("Index");
        }
        //GET
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var book = await bookServices.GetById(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }
        //PUT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id,
            [Bind("Id,Titile,Author,PublishedDate")]
            Book book)
        {
            var bs = bookServices.GetByTitle(book.Title);
            if (bs != null && bs.Id != book.Id)
            {
                ModelState.AddModelError("Titile", "Titile taken");
            }
            if (!ModelState.IsValid)
            {
                return View(book);
            }
            await bookServices.UpdateBook(book);
            TempData["success"] = "Book updated successfully";
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(Guid id)
        {
            var book = await bookServices.GetById(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        //PUT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Book book)
        {
            if (book == null)
            {
                return NotFound();
            }
            await bookServices.RemoveBook(book);
            TempData["success"] = "Book deleted successfully";
            return RedirectToAction("Index");
        }

    }
}
