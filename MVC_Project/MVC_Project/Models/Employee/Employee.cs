using MVC_Project.Wrappers;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_Project.Models.Employee
{
    public class Employee : BaseEntity
    {

        [Required]
        public string Name { get; set; }

        [Required]
        public string Surname { get; set; }

        [Required]
        [DisplayName("Date of Birth")]
        public DateTime DoB { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        [DisplayName("Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        public string Email { get; set; }

        public DateTime createdAt { get; set; } = DateTime.Now;

        public DateTime updatedAt { get; set; }

        [ForeignKey("Shop")]
        public Guid? WorkingLocationId { get; set; }

        public virtual Shop.Shop? WorkingLocation { get; set; }
        //public Guid WorkingLocation { get; set; }

    }
}
