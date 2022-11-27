using Microsoft.AspNetCore.Mvc;
using MVC_Project.Data;
using MVC_Project.Models;

namespace MVC_Project.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _db;

        public UserController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<User> objCategoryList = _db.Users;
            return View(objCategoryList);
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(User user)
        {
            if (_db.Users.FirstOrDefault(usr => usr.Email == user.Email) != null)
            {
                ModelState.AddModelError("Email", "Email taken");
            }
            if (!ModelState.IsValid)
            {
                return View(user);
            }
            user.createdAt = DateTime.Now.ToUniversalTime();
            user.Id= Guid.NewGuid();
            user.updatedAt = DateTime.Now.ToUniversalTime(); 
            _db.Users.Add(user);
            _db.SaveChanges();
            TempData["success"] = "User created successfully";
            return RedirectToAction("Index");
        }

        //GET
        public IActionResult Edit(Guid id)
        {
            var user = _db.Users.Find(id);
            if(user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        //PUT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(User user)
        {

            //if(_db.Users.Find(user.Id) == null) {
            //    return NotFound();
            //}
            if (!ModelState.IsValid)
            {
                return View(user);
            }
            user.updatedAt = DateTime.Now.ToUniversalTime(); 
            _db.Users.Update(user);
            _db.SaveChanges();
            TempData["success"] = "User updated successfully";
            return RedirectToAction("Index");
        }

        public IActionResult Delete(Guid id)
        {
            var user = _db.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        //PUT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(User user)
        {
            if(user == null)
            {
                return NotFound();
            }
            _db.Users.Remove(user);
            _db.SaveChanges();
            TempData["success"] = "User deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
