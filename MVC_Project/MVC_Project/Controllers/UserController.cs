using Microsoft.AspNetCore.Mvc;
using MVC_Project.Models.User;
using MVC_Project.Wrappers;

namespace MVC_Project.Controllers
{
    public class UserController : Controller
    {
        private readonly UserServices userServices;

        public UserController(UserServices _us)
        {
            userServices = _us;
        }

        public async Task<IActionResult> Index(
            [FromQuery]
            PageRequest<User> request
            )
        {
            PageResponse<User> response =await userServices.List(request);
           return View("_Table",response);
           
        }

        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await userServices.Details(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }
        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(User user)
        {
            if (userServices.GetByEmail(user.Email) != null)
            {
                ModelState.AddModelError("Email", "Email taken");
            }
            if (!userServices.isAgeValid(user))
            {
                ModelState.AddModelError("DoB", "Must be beetween 18 and 120 years old");
            }
            if (!ModelState.IsValid)
            {
                return View(user);
            }
            
            await userServices.AddUser(user);
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
            var user = await userServices.GetById(id);
            if(user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        //PUT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id,
            [Bind("Id,Name,Email,Surname,DoB,Gender")]
            User user)
        {
            var us = userServices.GetByEmail(user.Email);
            if (us != null && us.Id != user.Id)
            {
                ModelState.AddModelError("Email", "Email taken");
            }
            if (!userServices.isAgeValid(user))
            {
                ModelState.AddModelError("DoB", "Must be beetween 18 and 120 years old");
            }
            if (!ModelState.IsValid)
            {
                return View(user);
            }
            await userServices.UpdateUser(user);
            TempData["success"] = "User updated successfully";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var user =await userServices.GetById(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }


        //PUT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(User user)
        {
            if(user == null)
            {
                return NotFound();
            }
            await userServices.RemoveUser(user);
            TempData["success"] = "User deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
