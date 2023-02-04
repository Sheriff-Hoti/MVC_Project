namespace MVC_Project.Models.Book
{
    public class BookCategory
    {
        public Book bookk { get; set; }

        public IEnumerable<Category.Category> categories { get; set; }
    }
}
