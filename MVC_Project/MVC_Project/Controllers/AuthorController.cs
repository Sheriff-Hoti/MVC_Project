using Microsoft.AspNetCore.Mvc;
using MVC_Project.Models.Author;
using MVC_Project.Wrappers;

namespace MVC_Project.Controllers
{
    public class AuthorController : Controller
    {
        private readonly AuthorServices authorServices;

        public AuthorController(AuthorServices _aus)
        {
            authorServices = _aus;
        }

        public async Task<IActionResult> Index(
            [FromQuery]
            PageRequest<Author> request
            )
        {
            PageResponse<Author> response = await authorServices.List(request);
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

            var author = await authorServices.Details(id);
            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }
       // GET
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Author author)
        {
            if (!ModelState.IsValid)
            {
                return View(author);
            }

            await authorServices.Add(author);
            TempData["success"] = "User created successfully";
            return RedirectToAction("Index");
        }

        //GET
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var author = await authorServices.GetById(id);
            if (author == null)
            {
                return NotFound();
            }
            return View(author);
        }

        //PUT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id,
            [Bind("Id,Name,LastName,Email,Number")]
            Author author)
        {
            if (!ModelState.IsValid)
            {
                return View(author);
            }
            await authorServices.Update(author);
            TempData["success"] = "User updated successfully";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var user = await authorServices.GetById(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }


        //PUT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Author author)
        {
            if (author == null)
            {
                return NotFound();
            }
            await authorServices.Remove(author);
            TempData["success"] = "User deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
