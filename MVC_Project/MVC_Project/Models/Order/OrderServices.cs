using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_Project.Data;
using MVC_Project.Wrappers;
using X.PagedList;

namespace MVC_Project.Models.NewFolder
{
    public class OrderServices
    {
        private readonly ApplicationDbContext _db;
        private readonly OrderUtility _os;

        public OrderServices(ApplicationDbContext db,OrderUtility OrderUtility)
        {
            _db = db;
            _os = OrderUtility;
        }

        public async Task<PageResponse<Order>> List(PageRequest<Order> request)
        {
            if (request.SortColumn == null)
            {
                request.SortColumn = "Id";
            }
            if (!_os.OrderUtils.ContainsKey(request.SortColumn))
            {
                request.SortColumn = "Id";
            }

            IEnumerable<Order> objUserList = await _db.Order.ToListAsync();


            if (!String.IsNullOrEmpty(request.FilterString))
            {
                objUserList = objUserList.Where(
                u => u.Name.Contains(request.FilterString)); 
            }
            int totalCount = objUserList.Count();
       
            if (request.SortDirection == "DESC")
            {
                objUserList = objUserList.OrderByDescending(_os.OrderUtils[request.SortColumn]);
            }
            else
            {
                objUserList = objUserList.OrderBy(_os.OrderUtils[request.SortColumn]);
            }

            if (request.Page <= 0) request.Page = 1;

            if (request.Size <= 0) request.Size = 5;

            int totalPages = (int)Math.Ceiling((decimal)(totalCount / request.Size)) + 1;
            if (totalCount == (totalPages - 1) * request.Size)
            {
                totalPages--;
            }

            return new PageResponse<Order>
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

        //public async Task<Employee?> Details(Guid? Id)
        //{
        //    var user = await _db.Employee
        //        .FirstOrDefaultAsync(u => u.Id == Id);
        //    var shop = _db.Shop.FirstOrDefault(x => x.Id == user.WorkingLocationId);
        //    user.WorkingLocation = shop;
        //    return user;
        //}

        public Order? GetByName(string name)
        {
            var order = _db.Order.AsNoTracking().FirstOrDefault(x => x.Name == name);
            return order;
        }

        public async Task<Order> AddOrder(Order order)
        {
            //order.createdAt = DateTime.Now.ToUniversalTime();
            order.Id = Guid.NewGuid();
           // user.updatedAt = DateTime.Now.ToUniversalTime();
            _db.Order.Add(order);
            await _db.SaveChangesAsync();
            return order;
        }

        public async Task<Order?> GetById(Guid? guid)
        {
            return await _db.Order.FirstOrDefaultAsync(u => u.Id == guid);
        }

        public async Task<Order?> UpdateOrder(Order order)
        {
           // order.updatedAt = DateTime.Now.ToUniversalTime();
            _db.Order.Update(order);
            await _db.SaveChangesAsync();
            return order;
        }

        public async Task<Order> RemoveOrder(Order order)
        {
            _db.Order.Remove(order);
            await _db.SaveChangesAsync();
            return order;
        }


        //public async Task<List<SelectListItem>> GetAllSuppliers()
        //{
        //    var ListSuppliers = new List<SelectListItem>();
        //    List<Shop.Shop> Shops = await _db.Shop.ToListAsync();
        //    ListShops = Shops.Select(x => new SelectListItem()
        //    {
        //        Value = x.Id.ToString(),
        //        Text = x.Name,
        //    }).ToList();
        //    return ListShops;
        //}
    }
}
