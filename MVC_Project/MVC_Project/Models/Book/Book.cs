using MVC_Project.Wrappers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace MVC_Project.Models.Book
{
    public class Book : BaseEntity
    {

        [Required]
        public string Title { get; set; }

        [Required]
        public string Author { get; set; }
        [Required]
        public DateTime PublishedDate { get; set; }
        public DateTime createdAt { get; set; } = DateTime.Now;

        public DateTime updatedAt { get; set; }

        [ForeignKey("Category")]
        public Guid TypeBookCategoryId { get;  set; }

        public virtual Category.Category? TypeBookCategory { get; set; }

    }
}
