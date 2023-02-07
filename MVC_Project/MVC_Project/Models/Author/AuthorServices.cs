using Microsoft.EntityFrameworkCore;
using MVC_Project.Data;
using MVC_Project.Wrappers;
using X.PagedList;

namespace MVC_Project.Models.Author
{
    public class AuthorServices
    {
        private readonly ApplicationDbContext _db;
        private readonly AuthorUtility _AuthorUtility;

        public AuthorServices(ApplicationDbContext db ,AuthorUtility AuthorUtility)
        {
            _db = db;
            _AuthorUtility = AuthorUtility;
        }
        public async Task<PageResponse<Author>> List(PageRequest<Author> request)
        {
            if (request.SortColumn == null)
            {
                request.SortColumn = "Id";
            }
            if (!new AuthorUtility().AuthorUtils.ContainsKey(request.SortColumn))
            {
                request.SortColumn = "Id";
            }

            IEnumerable<Author> objAuthorList = await _db.Author.ToListAsync();


            if (!String.IsNullOrEmpty(request.FilterString))
            {
                objAuthorList = objAuthorList.Where(
                u => u.FirstName.Contains(request.FilterString) ||
                 u.LastName.Contains(request.FilterString) ||
                u.Email.Contains(request.FilterString));


            }
            int totalCount = objAuthorList.Count();


            if (request.SortDirection == "DESC")
            {
                objAuthorList = objAuthorList.OrderByDescending(_AuthorUtility.AuthorUtils[request.SortColumn]);
            }
            else
            {
                objAuthorList = objAuthorList.OrderBy(_AuthorUtility.AuthorUtils[request.SortColumn]);
            }

            if (request.Page <= 0) request.Page = 1;

            if (request.Size <= 0) request.Size = 5;

            int totalPages = (int)Math.Ceiling((decimal)(totalCount / request.Size)) + 1;
            if (totalCount == (totalPages - 1) * request.Size)
            {
                totalPages--;
            }

            return new PageResponse<Author>
            {
                data = objAuthorList.ToPagedListAsync(request.Page, request.Size),
                SortDirection = request.SortDirection,
                SortColumn = request.SortColumn,
                TotalCount = totalCount,
                Size = request.Size,
                Page = request.Page,
                TotalPages = totalPages,
                FilterString = request.FilterString,
            };

        }
        public async Task<Author?> Details(Guid? Id)
        {
            var author = await _db.Author
                .FirstOrDefaultAsync(u => u.Id == Id);
            IEnumerable<Book.Book> Books =
                await _db.Book.Where(x => x.AuthorOfBooksId == Id).ToListAsync();
            author.Books = Books;
            return author;
        }

        public async Task<Author> Add(Author author)
        {
          //  author.OpeningDate = DateTime.Now.ToUniversalTime();
            author.Id = Guid.NewGuid();
            _db.Author.Add(author);
            await _db.SaveChangesAsync();
            return author;
        }
        public async Task<Author?> GetById(Guid? guid)
        {
            return await _db.Author.FirstOrDefaultAsync(u => u.Id == guid);
        }

        public async Task<Author?> Update(Author author)
        {
            _db.Author.Update(author);
            await _db.SaveChangesAsync();
            return author;
        }
        public async Task<Author> Remove(Author author)
        {
            // var Books = _db.Books.Where(x => x.AuthorOfBooksId == shop.Id);
            //foreach (var emp in employees)
            //{
            //    emp.AuthorOfBooksId = null;
            //}
            //_db.Book.UpdateRange(book);
            _db.Author.Remove(author);
            await _db.SaveChangesAsync();
            return author;
        }


    }
}
