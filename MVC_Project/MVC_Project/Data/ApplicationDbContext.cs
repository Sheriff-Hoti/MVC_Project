using Microsoft.EntityFrameworkCore;
using MVC_Project.Models.User;
using MVC_Project.Models.Shop;
using MVC_Project.Models.Employee;

namespace MVC_Project.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {

        }

        public DbSet<User> Users { get; set; }

        public DbSet<Shop> Shop { get; set; }

        public DbSet<Employee> Employee { get; set; }
    }
}
