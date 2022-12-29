using MVC_Project.Data;
using MVC_Project.Models;
using MVC_Project.Wrappers;

namespace MVC_Project.Models.User
{
    public class UserUtility : Utility<User>
    {
        public Dictionary<string, Func<User, string>> userUtils;
        public Dictionary<string, string> header;
        Func<User, string> Name = x => x.Name;
        Func<User, string> Id = x => x.Id.ToString();
        Func<User, string> Surname = x => x.Surname;
        Func<User, string> Dob = x => x.DoB.ToString();
        Func<User, string> Gender = x => x.Gender;
        Func<User, string> Email = x => x.Email;
        Func<User, string> CreatedAt = x => x.createdAt.ToString();
        Func<User, string> UpdatedAt = x => x.updatedAt.ToString();
        //Func<User, List<string>> Attributes = x => new List<string>()
        //{
        //    x.Name,
        //    x.Surname,
        //    x.Email,
        //};

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
            header = new Dictionary<string, string>() {
                {"Name","Name"},
                {"Surname","Surname"},
                {"Email","Email"},
                {"DoB","Date of Birth"},
                {"Gender","Gender"},
                {"createdAt","Creation Date"},
                {"updatedAt","Updated Date"},
            };
        }

        public bool IsAgeValid(DateTime birthday, DateTime present)
        {
            int deltaYear = present.Year - birthday.Year;
            int deltaDay = present.DayOfYear - birthday.DayOfYear;
            if (deltaYear >= 18 && deltaYear < 120 && deltaDay >= 0) return true;
            return false;
        }

        
        public Dictionary<string, string> Attrs(User user)
        {
            return new Dictionary<string, string>()
            {
                { "Name", user.Name },
                {"Surname",user.Surname },
                { "Email",user.Email},
                { "Dob", user.DoB.ToShortDateString() },
                { "Gender", user.Gender},
                { "createdAt", user.createdAt.ToString()},
                { "updatedAt", user.updatedAt.ToString()}
            };
        }

        
    }
}
