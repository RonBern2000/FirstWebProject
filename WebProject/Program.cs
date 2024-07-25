using Microsoft.EntityFrameworkCore;
using WebProject.Data;
using WebProject.Repository;

var builder = WebApplication.CreateBuilder(args);
string stringConnection = builder.Configuration["ConnectionStrings:DefaultConnection"]!; // getting the stringConnection from appsettings.json
builder.Services.AddDbContext<ZooContext>(options => options.UseLazyLoadingProxies().UseSqlServer(stringConnection));// adding the dbContext service and setting up the options to use lazy loading and sqlServer
builder.Services.AddControllersWithViews(); // Enabling cotrollers and views
builder.Services.AddTransient<IRepository,Repository>(); // adding the Repository service

var app = builder.Build();
app.UseStaticFiles(); // For enabling local images

using(var scope = app.Services.CreateScope()) // Reseting the db and filling it up again
{
    var ctx = scope.ServiceProvider.GetRequiredService<ZooContext>();
    ctx.Database.EnsureDeleted();
    ctx.Database.EnsureCreated();
}

app.UseRouting(); // using routing 
app.MapDefaultControllerRoute(); // using the deafault

app.Run();
