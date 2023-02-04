using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using MVC_Project.Data;
using MVC_Project.Wrappers;
using System.Drawing;
using System.Globalization;
using System.Security.Policy;
using X.PagedList;

namespace MVC_Project.Models.User
{
    public class UserServices
    {

        private readonly ApplicationDbContext _db;

        private readonly UserUtility _userUtility;

        public UserServices(ApplicationDbContext db, UserUtility userUtility)
        {
            _db = db;
            _userUtility= userUtility;
        }


        public async Task<PageResponse<User>> List(PageRequest<User> request)
        {
            if (request.SortColumn == null)
            {
                request.SortColumn = "Id";
            }
            if (!new UserUtility().userUtils.ContainsKey(request.SortColumn))
            {
                request.SortColumn = "Id";
            }

            IEnumerable<User> objUserList =await _db.Users.ToListAsync();


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
                objUserList = objUserList.OrderByDescending(_userUtility.userUtils[request.SortColumn]);
            }
            else
            {
                objUserList = objUserList.OrderBy(_userUtility.userUtils[request.SortColumn]);
            }

            if (request.Page <= 0) request.Page = 1;

            if (request.Size <= 0) request.Size = 5;

            int totalPages = (int)Math.Ceiling((decimal)(totalCount / request.Size)) + 1;
            if (totalCount == (totalPages - 1) * request.Size)
            {
                totalPages--;
            }

            return new PageResponse<User>
            {
                data = objUserList.ToPagedListAsync(request.Page, request.Size),
                utilities = _userUtility,
                header = _userUtility.header,
                SortDirection = request.SortDirection,
                SortColumn = request.SortColumn,
                TotalCount = totalCount,
                Size = request.Size,
                Page = request.Page,
                TotalPages = totalPages,
                FilterString = request.FilterString,
            };

        }
        //return type object tipit user ose null
        public async Task<User?> Details(Guid? Id)
        {
            return await _db.Users
                .FirstOrDefaultAsync(u => u.Id == Id);
        }
        // opsionale ?
        public  User? GetByEmail(string email)
        {
            var user = _db.Users.AsNoTracking().FirstOrDefault(x => x.Email == email);
            return user;
        }

        public async Task<User> AddUser(User user)
        {
            user.createdAt = DateTime.Now.ToUniversalTime();
            user.Id = Guid.NewGuid();
            user.updatedAt = DateTime.Now.ToUniversalTime();
            _db.Users.Add(user);
            await _db.SaveChangesAsync();
            return user;
        }

        public async Task<User?> GetById(Guid? guid)
        {
            return await _db.Users.FirstOrDefaultAsync(u => u.Id == guid);
        }

        public async Task<User?> UpdateUser(User user) { 
            user.updatedAt = DateTime.Now.ToUniversalTime();
            _db.Users.Update(user);
            await _db.SaveChangesAsync();
            return user;
        }
        public async Task<User> RemoveUser(User user)
        {
            _db.Users.Remove(user);
            await _db.SaveChangesAsync();
            return user;
        }

        public bool isAgeValid(User user)
        {
            return _userUtility.IsAgeValid(user.DoB, DateTime.Now);
        }
    }
}
