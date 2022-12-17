using Microsoft.EntityFrameworkCore;
using MVC_Project.Models.User;
using MVC_Project.Models.Shop;

namespace MVC_Project.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {

        }

        public DbSet<User> Users { get; set; }

        public DbSet<Shop> Shop { get; set; }
    }
}
