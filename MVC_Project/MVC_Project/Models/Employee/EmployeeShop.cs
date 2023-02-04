namespace MVC_Project.Models.Employee
{
    public class EmployeeShop
    {
        public Employee employee { get; set; }

        public IEnumerable<Shop.Shop> shops { get; set; }

    }
}
