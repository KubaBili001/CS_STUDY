using Microsoft.EntityFrameworkCore;
using MVC_Students.Models;
using MVC_Students.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var configValue = builder.Configuration.GetValue<string>("ConnectionStrings:myDb");

builder.Services.AddDbContext<DatabaseContext>(opt =>
{
    opt.UseSqlServer(configValue);
    opt.LogTo(Console.WriteLine);
});

builder.Services.AddTransient<IStudentService, StudentService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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
