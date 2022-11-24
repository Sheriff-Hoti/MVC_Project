using Microsoft.EntityFrameworkCore;
using MVC_Project.Models;

namespace MVC_Project.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {

        }

        public DbSet<User> Users { get; set; }    
    }
}
