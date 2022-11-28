using MVC_Project.Data;
using MVC_Project.Models;

namespace MVC_Project.Utility
{
    public class UserUtility
    {
        public Dictionary<string, Func<User, String>> userUtils;
        Func<User, String> Name = x => x.Name;
        Func<User, String> Id = x => x.Id.ToString();
        Func<User, String> Surname = x => x.Surname;
        Func<User, String> Dob = x => x.DoB.ToString();
        Func<User, String> Gender = x => x.Gender;
        Func<User, String> Email = x => x.Email;
        Func<User, String> CreatedAt = x => x.createdAt.ToString();
        Func<User, String> UpdatedAt = x => x.updatedAt.ToString();
       
        public UserUtility()
        {
            userUtils = new Dictionary<string,Func<User,String>>();
            userUtils.Add("Name", Name);
            userUtils.Add("Id", Id);
            userUtils.Add("Surname", Surname);
            userUtils.Add("DoB", Dob);
            userUtils.Add("Gender", Gender);
            userUtils.Add("Email", Email);
            userUtils.Add("createdAt", CreatedAt);
            userUtils.Add("updatedAt", UpdatedAt);
        }
    }
}
