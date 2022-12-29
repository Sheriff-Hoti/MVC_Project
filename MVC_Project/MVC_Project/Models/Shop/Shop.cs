using MVC_Project.Wrappers;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MVC_Project.Models.Shop
{
    public class Shop : BaseEntity
    {
        
        [Required]
        public string Address { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [DisplayName("Opening Date")]
        public DateTime OpeningDate { get; set; }

        public IEnumerable<Employee.Employee>? Employees { get; set; }
    }
}
