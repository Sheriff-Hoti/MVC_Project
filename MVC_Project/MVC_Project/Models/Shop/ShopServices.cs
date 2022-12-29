using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_Project.Data;
using MVC_Project.Models.Employee;
using MVC_Project.Wrappers;
using X.PagedList;

namespace MVC_Project.Models.Shop
{
    public class ShopServices
    {
        private readonly ApplicationDbContext _db;

        private readonly ShopUtility _shu;

        public ShopServices(ApplicationDbContext db, ShopUtility shopUtility)
        {
            _db = db;
            _shu = shopUtility;
        }


        public async Task<PageResponse<Shop>> List(PageRequest<Shop> request)
        {
            if (request.SortColumn == null)
            {
                request.SortColumn = "Id";
            }
            if (!_shu.ShopUtils.ContainsKey(request.SortColumn))
            {
                request.SortColumn = "Id";
            }

            IEnumerable<Shop> objList = await _db.Shop.ToListAsync();


            if (!String.IsNullOrEmpty(request.FilterString))
            {
                objList = objList.Where(
                u => u.Name.Contains(request.FilterString) ||
                u.Address.Contains(request.FilterString) ||
                u.Location.Contains(request.FilterString));
            }
            int totalCount = objList.Count();
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
                objList = objList.OrderByDescending(_shu.ShopUtils[request.SortColumn]);
            }
            else
            {
                objList = objList.OrderBy(_shu.ShopUtils[request.SortColumn]);
            }

            if (request.Page <= 0) request.Page = 1;

            if (request.Size <= 0) request.Size = 5;

            int totalPages = (int)Math.Ceiling((decimal)(totalCount / request.Size)) + 1;
            if (totalCount == (totalPages - 1) * request.Size)
            {
                totalPages--;
            }

            return new PageResponse<Shop>
            {
                data = objList.ToPagedListAsync(request.Page, request.Size),
                SortDirection = request.SortDirection,
                SortColumn = request.SortColumn,
                TotalCount = totalCount,
                Size = request.Size,
                Page = request.Page,
                TotalPages = totalPages,
                FilterString = request.FilterString,
            };

        }

        public async Task<Shop?> Details(Guid? Id)
        {
            var shop = await _db.Shop
                .FirstOrDefaultAsync(u => u.Id == Id);
            IEnumerable<Employee.Employee> employees = 
                await _db.Employee.Where(x => x.WorkingLocationId == Id).ToListAsync();
            shop.Employees = employees;
            return shop;
        }

        public async Task<Shop> Add(Shop shop)
        {
            shop.OpeningDate = DateTime.Now.ToUniversalTime();
            shop.Id = Guid.NewGuid();
            _db.Shop.Add(shop);
            await _db.SaveChangesAsync();
            return shop;
        }

        public async Task<Shop?> GetById(Guid? guid)
        {
            return await _db.Shop.FirstOrDefaultAsync(u => u.Id == guid);
        }

        public async Task<Shop?> Update(Shop shop)
        {
            _db.Shop.Update(shop);
            await _db.SaveChangesAsync();
            return shop;
        }

        public async Task<Shop> Remove(Shop shop)
        {
            var employees = _db.Employee.Where(x => x.WorkingLocationId == shop.Id);
            foreach(var emp in employees)
            {
                emp.WorkingLocationId = null;
            }
            _db.Employee.UpdateRange(employees);
            _db.Shop.Remove(shop);
            await _db.SaveChangesAsync();
            return shop;
        }
    }
}
