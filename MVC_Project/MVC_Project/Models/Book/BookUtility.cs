using MVC_Project.Data;
using MVC_Project.Models;
namespace MVC_Project.Models.Book
{
    public class BookUtility
    {

        public Dictionary<string, Func<Book, string>> BookUtils;
        Func<Book, string> Title = x => x.Title;
        Func<Book, string> Author = x => x.Author;
        Func<Book, string> PublishedDate = x => x.PublishedDate.ToString();
        Func<Book, string> Id = x => x.Id.ToString();
        Func<Book, string> CreatedAt = x => x.createdAt.ToString();
        Func<Book, string> UpdatedAt = x => x.updatedAt.ToString();
       

        public BookUtility()
        {
            BookUtils = new Dictionary<string, Func<Book, string>>();
            BookUtils.Add("Title", Title);
            BookUtils.Add("Id", Id);
            BookUtils.Add("Author", Author);
            BookUtils.Add("PublishedDate", PublishedDate);
            BookUtils.Add("createdAt", CreatedAt);
            BookUtils.Add("updatedAt", UpdatedAt);
        }

        

        public Dictionary<string, string> userAttrs(Book book)
        {
            return new Dictionary<string, string>()
            {
                { "Title", book.Title },
                {"Author",book.Author },
                {"PublishedDate",book.PublishedDate.ToShortDateString()},
            };
        }
    }
}

