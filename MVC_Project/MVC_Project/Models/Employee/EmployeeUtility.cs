using MVC_Project.Data;
using MVC_Project.Models;

namespace MVC_Project.Models.Employee
{
    public class EmployeeUtility
    {
        public Dictionary<string, Func<Employee, string>> EmployeeUtils;
        Func<Employee, string> Name = x => x.Name;
        Func<Employee, string> Id = x => x.Id.ToString();
        Func<Employee, string> Surname = x => x.Surname;
        Func<Employee, string> Dob = x => x.DoB.ToString();
        Func<Employee, string> Gender = x => x.Gender;
        Func<Employee, string> Email = x => x.Email;
        Func<Employee, string> CreatedAt = x => x.createdAt.ToString();
        Func<Employee, string> UpdatedAt = x => x.updatedAt.ToString();
        Func<Employee, string> PhoneNumber = x => x.PhoneNumber.ToString();

        public EmployeeUtility()
        {
            EmployeeUtils = new Dictionary<string, Func<Employee, string>>();
            EmployeeUtils.Add("Name", Name);
            EmployeeUtils.Add("Id", Id);
            EmployeeUtils.Add("Surname", Surname);
            EmployeeUtils.Add("DoB", Dob);
            EmployeeUtils.Add("Gender", Gender);
            EmployeeUtils.Add("PhoneNumber",PhoneNumber );
            EmployeeUtils.Add("Email", Email);
            EmployeeUtils.Add("createdAt", CreatedAt);
            EmployeeUtils.Add("updatedAt", UpdatedAt);
        }

        public bool IsAgeValid(DateTime birthday, DateTime present)
        {
            int deltaYear = present.Year - birthday.Year;
            int deltaDay = present.DayOfYear - birthday.DayOfYear;
            if (deltaYear >= 18 && deltaYear < 120 && deltaDay >= 0) return true;
            return false;
        }

        public Dictionary<string, string> userAttrs(Employee user)
        {
            return new Dictionary<string, string>()
            {
                { "Name", user.Name },
                {"Surname",user.Surname },
                {"DoB",user.DoB.ToShortDateString() }
            };
        }
    }
}
