using MVC_Project.Wrappers;
using System.ComponentModel.DataAnnotations;

namespace MVC_Project.Models.Author
{
    public class Author : BaseEntity
    {

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string Email { get; set; }        
        public string Number { get; set; }
        public IEnumerable<Book.Book> Books { get; internal set; }
    }
}
