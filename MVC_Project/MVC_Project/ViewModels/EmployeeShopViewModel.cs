using MVC_Project.Models.Shop;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MVC_Project.ViewModels
{
    public class EmployeeShopViewModel
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

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

        public Guid ShopId { get; set; }

        public List<Shop> shops { get; set;}
    }
}