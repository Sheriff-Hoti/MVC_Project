using Microsoft.AspNetCore.Mvc;
using MVC_Project.Models.NewFolder;
using MVC_Project.Wrappers;

namespace MVC_Project.Controllers
{
    public class OrderController : Controller
    {
        private readonly OrderServices orderServices;

        public OrderController(OrderServices _os)
        {
            orderServices = _os;
        }

        public async Task<IActionResult> Index(
           [FromQuery]
            PageRequest<Order> request
           )
        {
            PageResponse<Order> response = await orderServices.List(request);
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

        //    var order = await orderServices.Details(id);
        //    if (order == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(order);
        //}

        //GET
        [HttpGet]
        public async Task<IActionResult> Create()
        {
           // ViewBag.suppliers = await orderServices.GetAllSuppliers();
            return View();
        }
        //POST
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(
        //    Order order)
        //{
        //    if (orderServices.GetByName(order.Name) != null)
        //    {
        //        ModelState.AddModelError("Name", "Name taken");
        //    }

        //    if (!ModelState.IsValid)
        //    {
        //        return View(order);
        //    }

        //    Order order = new Order()
        //    {
        //        Id = order.Id,
        //        Name = order.Name,
        //        OrderDate = DateTime.Now,
        //        WorkingLocationId = user.WorkingLocationId duher mja shtu ni fk prej supplier
        //    };

        //    await orderServices.AddOrder(order);
        //    TempData["success"] = "User created successfully";
        //    return RedirectToAction("Index");
        //}

        //GET
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var order = await orderServices.GetById(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }


        //PUT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id,
            [Bind("Id,Name,OrderDate")]
            Order order)
        {
            var os = orderServices.GetByName(order.Name);
            if (os != null && os.Id != order.Id)
            {
                ModelState.AddModelError("Name", "Name taken");
            }
      
            if (!ModelState.IsValid)
            {
                return View(order);
            }
            await orderServices.UpdateOrder(order);
            TempData["success"] = "User updated successfully";
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Delete(Guid id)
        {
            var order = await orderServices.GetById(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        //PUT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Order order)
        {
            if (order == null)
            {
                return NotFound();
            }
            await orderServices.RemoveOrder(order);
            TempData["success"] = "User deleted successfully";
            return RedirectToAction("Index");
        }


    }
}
