using MVC_Project.Wrappers;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace MVC_Project.Models.User
{
    public class User:BaseEntity
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
        public string Email { get; set; }

        public DateTime createdAt { get; set; } = DateTime.Now;

        public DateTime updatedAt { get; set; }
    }
}
