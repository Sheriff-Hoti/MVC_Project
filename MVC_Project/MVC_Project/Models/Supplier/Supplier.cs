using MVC_Project.Wrappers;
using System.ComponentModel.DataAnnotations;

namespace MVC_Project.Models.Supplier
{
    public class Supplier : BaseEntity
    {

        [Required]
        public string Name { get; set; }
        [Required]
        public string ContactNumber { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Address { get; set; }
        public DateTime createdAt { get; set; } = DateTime.Now;

        public DateTime updatedAt { get; set; }

    }
}
