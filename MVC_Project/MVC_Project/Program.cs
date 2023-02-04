using Microsoft.EntityFrameworkCore;
using MVC_Project.Data;
using MVC_Project.Models.Book;
using MVC_Project.Models.Category;
using MVC_Project.Models.Employee;
using MVC_Project.Models.Shop;
using MVC_Project.Models.User;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(
    builder.Configuration.GetConnectionString("DefaultConnection")
    )
);
builder.Services.AddRazorPages();

builder.Services.AddTransient<UserServices>();
builder.Services.AddTransient<UserUtility>();
builder.Services.AddTransient<EmployeeServices>();
builder.Services.AddTransient<EmployeeUtility>();
builder.Services.AddTransient<ShopServices>();
builder.Services.AddTransient<ShopUtility>();
builder.Services.AddTransient<CategoryServices>();
builder.Services.AddTransient<CategoryUtility>();
builder.Services.AddTransient<BookServices>();
builder.Services.AddTransient<BookUtility>();


var app = builder.Build();
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
