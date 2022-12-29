using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
using MVC_Project.Data;
using MVC_Project.Wrappers;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Net.NetworkInformation;
using System;
using X.PagedList;
using static System.Net.Mime.MediaTypeNames;

namespace MVC_Project.Models.Employee
{
    public class EmployeeServices
    {

        private readonly ApplicationDbContext _db;

        private readonly EmployeeUtility _eu;

        public EmployeeServices(ApplicationDbContext db, EmployeeUtility EmployeeUtility)
        {
            _db = db;
            _eu = EmployeeUtility;
        }


        public async Task<PageResponse<Employee>> List(PageRequest<Employee> request)
        {
            if (request.SortColumn == null)
            {
                request.SortColumn = "Id";
            }
            if (!_eu.EmployeeUtils.ContainsKey(request.SortColumn))
            {
                request.SortColumn = "Id";
            }

            IEnumerable<Employee> objUserList = await _db.Employee.ToListAsync();


            if (!String.IsNullOrEmpty(request.FilterString))
            {
                objUserList = objUserList.Where(
                u => u.Name.Contains(request.FilterString) ||
                u.Surname.Contains(request.FilterString) ||
                u.Email.Contains(request.FilterString));
            }
            int totalCount = objUserList.Count();
            //objUserList = objUserList.Where(u => 
            //    u.Name.Contains(request.FilterMap["Name"])&&
            //    u.Surname.Contains(request.FilterMap["Surname"])&&
            //    u.DoB.ToString().Contains(request.FilterMap["DoB"]) &&
            //    u.Surname.Contains(request.FilterMap["Name"]) &&
            //    u.Gender.Contains(request.FilterMap["Gender"]) &&
            //    u.Email.Contains(request.FilterMap["Email"]) &&
            //    u.createdAt.ToString().Contains(request.FilterMap["createdAt"]) &&
            //    u.updatedAt.ToString().Contains(request.FilterMap["updatedAt"])
            //);
            if (request.SortDirection == "DESC")
            {
                objUserList = objUserList.OrderByDescending(_eu.EmployeeUtils[request.SortColumn]);
            }
            else
            {
                objUserList = objUserList.OrderBy(_eu.EmployeeUtils[request.SortColumn]);
            }

            if (request.Page <= 0) request.Page = 1;

            if (request.Size <= 0) request.Size = 5;

            int totalPages = (int)Math.Ceiling((decimal)(totalCount / request.Size)) + 1;
            if (totalCount == (totalPages - 1) * request.Size)
            {
                totalPages--;
            }

            return new PageResponse<Employee>
            {
                data = objUserList.ToPagedListAsync(request.Page, request.Size),
                SortDirection = request.SortDirection,
                SortColumn = request.SortColumn,
                TotalCount = totalCount,
                Size = request.Size,
                Page = request.Page,
                TotalPages = totalPages,
                FilterString = request.FilterString,
            };

        }

        public async Task<Employee?> Details(Guid? Id)
        {
            var user = await _db.Employee
                .FirstOrDefaultAsync(u => u.Id == Id);
            var shop = _db.Shop.FirstOrDefault(x => x.Id == user.WorkingLocationId);
            user.WorkingLocation = shop;
            return user;
        }

        public Employee? GetByEmail(string email)
        {
            var user = _db.Employee.AsNoTracking().FirstOrDefault(x => x.Email == email);
            return user;
        }

        public async Task<Employee> AddEmployee(Employee user)
        {
            user.createdAt = DateTime.Now.ToUniversalTime();
            user.Id = Guid.NewGuid();
            user.updatedAt = DateTime.Now.ToUniversalTime();
            _db.Employee.Add(user);
            await _db.SaveChangesAsync();
            return user;
        }

        public async Task<Employee?> GetById(Guid? guid)
        {
            return await _db.Employee.FirstOrDefaultAsync(u => u.Id == guid);
        }

        public async Task<Employee?> UpdateEmployee(Employee user)
        {
            user.updatedAt = DateTime.Now.ToUniversalTime();
            _db.Employee.Update(user);
            await _db.SaveChangesAsync();
            return user;
        }
        public async Task<Employee> RemoveEmployee(Employee user)
        {
            _db.Employee.Remove(user);
            await _db.SaveChangesAsync();
            return user;
        }

        public bool isAgeValid(Employee user)
        {
            return _eu.IsAgeValid(user.DoB, DateTime.Now);
        }

        public async Task<List<SelectListItem>> GetAllShops()
        {
            var ListShops = new List<SelectListItem>();
            List<Shop.Shop> Shops = await _db.Shop.ToListAsync();
            ListShops = Shops.Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Name,
            }).ToList();
            return ListShops;
        }
//        To create an order and select the customer from a drop-down list in an ASP.NET MVC application, you can follow these steps:

//Create a view model class to represent the data that you want to display in the view.This view model should include a property for the order details (such as product and quantity), as well as a property for the list of customers that will be displayed in the drop-down list.
//Copy code
//public class OrderViewModel
//        {
//            public string Product { get; set; }
//            public int Quantity { get; set; }
//            public IEnumerable<SelectListItem> Customers { get; set; }
//        }
//        In the controller, retrieve the list of customers from the database and create an instance of the view model.Set the Customers property of the view model to a list of SelectListItem objects that represent the options for the drop-down list.
//Copy code
//public ActionResult Create()
//        {
//            var model = new OrderViewModel();
//            var customers = _dbContext.Customers.ToList();
//            model.Customers = customers.Select(c => new SelectListItem
//            {
//                Value = c.CustomerId.ToString(),
//                Text = c.Name
//            });
//            return View(model);
//        }
//        In the view, use the Html.DropDownListFor helper method to render the drop-down list.This helper method will automatically bind the selected value to the corresponding property in the view model when the form is submitted.
//        Copy code
//        @model OrderViewModel
        

//        @using (Html.BeginForm())
//        {
//    < div >
//        @Html.LabelFor(m => m.Product)
//                @Html.TextBoxFor(m => m.Product)
//            </ div >
//    < div >
//            @Html.LabelFor(m => m.Quantity)
//                @Html.TextBoxFor(m => m.Quantity)
//            </ div >
//    < div >
//            @Html.LabelFor(m => m.CustomerId)
//                @Html.DropDownListFor(m => m.CustomerId, Model.Customers)
//    </ div >
//    < button type = "submit" > Create Order </ button >
//        }
//        In the controller, add an action method to handle the form submission.This action method should retrieve the order details and the selected customer from the view model, create a new order object, and save it to the database.
//        Copy code
//        [HttpPost]
//public ActionResult Create(OrderViewModel model)
//        {
//            var customer = _dbContext.Customers.Find(model.CustomerId);
//            var order = new Order
//            {
//                Product = model.Product,
//                Quantity = model.Quantity,
//                Customer = customer
//            };
//            _dbContext.Orders.Add(order);
//            _dbContext.SaveChanges();
//            return RedirectToAction("Index");
//        }
//        By following these steps, you can create an order and select the customer from a drop-down list in an ASP.NET MVC application.




//        can you tell me more about the selectlistitem from the code above
//        SelectListItem is a class that represents an item in a drop-down list in an ASP.NET MVC application. It has the following properties:


//        Text: The text to display for the item in the drop-down list.
//Value: The value to associate with the item.This value is returned to the controller when the form is submitted.
//        Selected: A boolean value that specifies whether the item is selected by default.
//You can use the SelectListItem class to populate a drop-down list in a view by creating a list of SelectListItem objects and assigning it to the Customers property of the view model.

//For example, the following code creates a list of SelectListItem objects that represent the customers in the database:

//Copy code
//var customers = _dbContext.Customers.ToList();
//        model.Customers = customers.Select(c => new SelectListItem
//{
//    Value = c.CustomerId.ToString(),
//    Text = c.Name
//    });
//You can then use the Html.DropDownListFor helper method to render the drop-down list in the view:

//Copy code
//@Html.DropDownListFor(m => m.CustomerId, Model.Customers)
//When the form is submitted, the selected value of the drop-down list will be bound to the CustomerId property of the view model.You can then use this value to retrieve the selected customer from the database and create the order.
    }
}

