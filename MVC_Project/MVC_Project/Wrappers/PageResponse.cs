using Microsoft.IdentityModel.Tokens;
using MVC_Project.Models.User;
using System.ComponentModel.DataAnnotations;
using X.PagedList;

namespace MVC_Project.Wrappers
{
    public class PageResponse<T>
    {
        [Required]
        public Task<IPagedList<T>> data { get; set; }

        public Utility<T> utilities { get; set; }

        public string SortDirection { get; set; }

        public string SortColumn { get; set; }

        public int Size { get; set; }  

        public int Page { get; set; }

        public int TotalCount { get; set; }

        public int TotalPages { get; set; }

        public string FilterString { get; set; }

        public Dictionary<string, string> header { get; set; }

        public Dictionary<string, string> entityValues { get; set; }

    }
}
