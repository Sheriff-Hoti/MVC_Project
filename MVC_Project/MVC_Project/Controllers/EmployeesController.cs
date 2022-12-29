using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC_Project.Models.Employee;
using MVC_Project.Models.User;
using MVC_Project.ViewModels;
using MVC_Project.Wrappers;

namespace MVC_Project.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly EmployeeServices employeeServices;

        public EmployeesController(EmployeeServices _es)
        {
            employeeServices = _es;
        }

        public async Task<IActionResult> Index(
            [FromQuery]
            PageRequest<Employee> request
            )
        {
            PageResponse<Employee> response = await employeeServices.List(request);
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

            var user = await employeeServices.Details(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }
        //GET
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.shops = await employeeServices.GetAllShops();
            return View();
        }

        //POST
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            Employee user)
        {
            if (employeeServices.GetByEmail(user.Email) != null)
            {
                ModelState.AddModelError("Email", "Email taken");
            }
            //if (!employeeServices.isAgeValid(user))
            //{
            //    ModelState.AddModelError("DoB", "Must be beetween 18 and 120 years old");
            //}
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            Employee employee = new Employee()
            {
                Id= user.Id,
                Name = user.Name,
                createdAt= DateTime.Now,
                Email= user.Email,
                Gender= user.Gender,
                PhoneNumber= user.PhoneNumber,
                DoB = user.DoB,
                Surname= user.Surname,
                updatedAt= DateTime.Now,
                WorkingLocationId = user.WorkingLocationId
            };

            await employeeServices.AddEmployee(employee);
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
            var user = await employeeServices.GetById(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        //PUT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id,
            [Bind("Id,Name,Email,Surname,DoB,PhoneNumber,Gender")]
            Employee user)
        {
            var us = employeeServices.GetByEmail(user.Email);
            if (us != null && us.Id != user.Id)
            {
                ModelState.AddModelError("Email", "Email taken");
            }
            if (!employeeServices.isAgeValid(user))
            {
                ModelState.AddModelError("DoB", "Must be beetween 18 and 120 years old");
            }
            if (!ModelState.IsValid)
            {
                return View(user);
            }
            await employeeServices.UpdateEmployee(user);
            TempData["success"] = "User updated successfully";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(Guid id)
        {
            var user = await employeeServices.GetById(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }


        //PUT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Employee user)
        {
            if (user == null)
            {
                return NotFound();
            }
            await employeeServices.RemoveEmployee(user);
            TempData["success"] = "User deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
