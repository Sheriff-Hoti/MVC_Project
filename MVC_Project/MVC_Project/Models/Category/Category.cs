using MVC_Project.Wrappers;
using System.ComponentModel.DataAnnotations;

namespace MVC_Project.Models.Category
{
    public class Category : BaseEntity
    {

        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }

        //public List<Book> Books { get; set; }
        public DateTime createdAt { get; set; } = DateTime.Now;

        public DateTime updatedAt { get; set; }

    }
}
