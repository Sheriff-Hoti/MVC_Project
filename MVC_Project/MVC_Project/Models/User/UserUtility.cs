using MVC_Project.Data;
using MVC_Project.Models;

namespace MVC_Project.Models.User
{
    public class UserUtility
    {
        public Dictionary<string, Func<User, string>> userUtils;
        Func<User, string> Name = x => x.Name;
        Func<User, string> Id = x => x.Id.ToString();
        Func<User, string> Surname = x => x.Surname;
        Func<User, string> Dob = x => x.DoB.ToString();
        Func<User, string> Gender = x => x.Gender;
        Func<User, string> Email = x => x.Email;
        Func<User, string> CreatedAt = x => x.createdAt.ToString();
        Func<User, string> UpdatedAt = x => x.updatedAt.ToString();

        public UserUtility()
        {
            userUtils = new Dictionary<string, Func<User, string>>();
            userUtils.Add("Name", Name);
            userUtils.Add("Id", Id);
            userUtils.Add("Surname", Surname);
            userUtils.Add("DoB", Dob);
            userUtils.Add("Gender", Gender);
            userUtils.Add("Email", Email);
            userUtils.Add("createdAt", CreatedAt);
            userUtils.Add("updatedAt", UpdatedAt);
        }

        public bool IsAgeValid(DateTime birthday, DateTime present)
        {
            int deltaYear = present.Year - birthday.Year;
            int deltaDay =  present.DayOfYear - birthday.DayOfYear;
            if (deltaYear >= 18 && deltaYear < 120 && deltaDay >= 0) return true;
            return false;
        }

        public Dictionary<string,string> userAttrs(User user)
        {
            return new Dictionary<string, string>()
            {
                { "Name", user.Name },
                {"Surname",user.Surname }
            };
        }
    }
}
