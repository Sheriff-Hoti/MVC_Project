using Microsoft.AspNetCore.Mvc;
using MVC_Project.Models.Category;
using MVC_Project.Wrappers;

namespace MVC_Project.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CategoryServices categoryServices;

        public CategoryController(CategoryServices _cs)
        {
            categoryServices = _cs;
        }

        //disa te dhena qe nuk shfaqen n front mund ta perdorim viewbag per ti dergu
        public async Task<IActionResult> Index(
            [FromQuery]
            PageRequest<Category> request
            )
        {
            PageResponse<Category> response = await categoryServices.List(request);
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

            var category = await categoryServices.Details(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        //GET
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            Category cat)
        {
            if (categoryServices.GetByName(cat.Name) != null)
            {
                ModelState.AddModelError("Name", "Name taken");
            }
          
            if (!ModelState.IsValid)
            {
                return View(cat);
            }

            Category category = new Category()
            {
                Id = cat.Id,
                Name = cat.Name,
                Description = cat.Description,
                createdAt = DateTime.Now,
                updatedAt = DateTime.Now,
            };

            await categoryServices.AddCategory(category);
            TempData["success"] = "Category created successfully";
            return RedirectToAction("Index");
        }

        //GET
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var category = await categoryServices.GetById(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        //PUT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id,
            [Bind("Id,Name,Description")]
            Category category)
        {
            var cat = categoryServices.GetByName(category.Name);
            if (cat != null && cat.Id != cat.Id)
            {
                ModelState.AddModelError("Name", "Name taken");
            }
           
            if (!ModelState.IsValid)
            {
                return View(cat);
            }
            await categoryServices.UpdateCategory(cat);
            TempData["success"] = "Category updated successfully";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var cat = await categoryServices.GetById(id);
            if (cat == null)
            {
                return NotFound();
            }
            return View(cat);
        }

        //PUT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Category cat)
        {
            if (cat == null)
            {
                return NotFound();
            }
            await categoryServices.RemoveCategory(cat);
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");
        }

    }
}
