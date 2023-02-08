using Microsoft.AspNetCore.Mvc;
using MVC_Project.Models.Supplier;
using MVC_Project.Wrappers;

namespace MVC_Project.Controllers
{
    public class SupplierController : Controller
    {
        private readonly SupplierServices supplierServices;

        public SupplierController(SupplierServices _ss)
        {
            supplierServices = _ss;
        }


        public async Task<IActionResult> Index(
           [FromQuery]
            PageRequest<Supplier> request
           )
        {
            PageResponse<Supplier> response = await supplierServices.List(request);
            ViewBag.response = response;
            ViewBag.request = request;
            return View(response);
        }
        //public async Task<IActionResult> Details(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var supplier = await supplerServices.Details(id);
        //    if (supplier == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(supplier);
        //}
        //GET
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            Supplier sup)
        {
            if (supplierServices.GetByName(sup.Name) != null)
            {
                ModelState.AddModelError("Name", "Name taken");
            }

            if (!ModelState.IsValid)
            {
                return View(sup);
            }

            Supplier supplier = new Supplier()
            {
                Id = sup.Id,
                Name = sup.Name,
                Email = sup.Email,
                ContactNumber=sup.ContactNumber,
                createdAt = DateTime.Now,
                updatedAt = DateTime.Now,
            };

            await supplierServices.AddSupplier(supplier);
            TempData["success"] = "Supplier created successfully";
            return RedirectToAction("Index");
        }


        //GET
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var supplier = await supplierServices.GetById(id);
            if (supplier == null)
            {
                return NotFound();
            }
            return View(supplier);
        }

        //PUT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id,
            [Bind("Id,Name,Email,ContactNumber")]
            Supplier supplier)
        {
            var sup = supplierServices.GetByName(supplier.Name);
            if (sup != null && sup.Id != sup.Id)
            {
                ModelState.AddModelError("Name", "Name taken");
            }

            if (!ModelState.IsValid)
            {
                return View(sup);
            }
            await supplierServices.UpdateSupplier(sup);
            TempData["success"] = "Supplier updated successfully";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var sup = await supplierServices.GetById(id);
            if (sup == null)
            {
                return NotFound();
            }
            return View(sup);
        }

        //PUT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Supplier sup)
        {
            if (sup == null)
            {
                return NotFound();
            }
            await supplierServices.RemoveSupplier(sup);
            TempData["success"] = "Supplier deleted successfully";
            return RedirectToAction("Index");
        }


    }
}
