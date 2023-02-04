using MVC_Project.Models.Category;
using System.ComponentModel.DataAnnotations;

namespace MVC_Project.ViewModels
{
    public class BookCategoryViewModel
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string Title { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public DateTime PublishedDate { get; set; }
        public DateTime createdAt { get; set; } = DateTime.Now;

        public DateTime updatedAt { get; set; }

        public Guid CategoryId { get; set; }

        public List<Category> categories { get; set; }
    }
}
