using LibraryMngSys.Areas.Identity.Data;
using LibraryMngSys.Models.Book;
using LibraryMngSys.Models.Category;
using LibraryMngSys.Models.Shop;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LibraryMngSys.Models.Author;
using LibraryMngSys.Models.Supplier;
using LibraryMngSys.Models.Order;

namespace LibraryMngSys.Data;

public class LibraryMngSysContext : IdentityDbContext<LibraryMngSysUser>
{
    public LibraryMngSysContext(DbContextOptions<LibraryMngSysContext> options)
        : base(options)
    {
    }


    public DbSet<Shop> Shop { get; set; }

    public DbSet<Category> Category { get; set; }

    public DbSet<Book> Book { get; set; }

    public DbSet<Author> Author { get; set; }

    public DbSet<Supplier> Supplier { get; set; }

    public DbSet<Order> Order { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
        builder.ApplyConfiguration(new LibraryMngSysUserEntityConfig());
    }

}

public class LibraryMngSysUserEntityConfig : IEntityTypeConfiguration<LibraryMngSysUser>
{
    public void Configure(EntityTypeBuilder<LibraryMngSysUser> builder)
    {
        builder.Property(u => u.Name).IsRequired().HasMaxLength(128);
        builder.Property(u => u.Surname).IsRequired().HasMaxLength(128);

    }
}