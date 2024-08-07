using Microsoft.EntityFrameworkCore;
using WebProject.Data;
using WebProject.Repository;
using Serilog;
using WebProject.Filters;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
string stringZooConnection = builder.Configuration["ConnectionStrings:DefaultConnection"]!; // getting the stringConnection from appsettings.json
string stringUsersConnection = builder.Configuration["ConnectionStrings:UsersConnection"]!;

builder.Services.AddDbContext<ZooContext>(options => options.UseLazyLoadingProxies().UseSqlServer(stringZooConnection));// adding the dbContext service and setting up the options to use lazy loading and sqlServer

builder.Services.AddDbContext<UsersContext>(options => options.UseSqlServer(stringUsersConnection));
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<UsersContext>();

builder.Services.AddControllersWithViews(); // Enabling cotrollers and views

builder.Services.AddAuthorization(options => // Adding authorization policies to be more preciese with who is allowd to do what
{
    options.AddPolicy("ManageUsersPolicy", policy =>
            policy.RequireClaim("Permission", "ManageUsers"));

    options.AddPolicy("ManageContentPolicy", policy =>
        policy.RequireClaim("Permission", "ManageContent"));

    options.AddPolicy("ViewContentPolicy", policy =>
        policy.RequireClaim("Permission", "ViewContent"));

    options.AddPolicy("CommentPolicy", policy =>
        policy.RequireClaim("Permission", "Comment"));

    options.AddPolicy("ManageUsersAndContentPolicy", policy =>
        policy.RequireAssertion(context =>
            context.User.HasClaim(c => c.Type == "Permission" && (c.Value == "ManageUsers" || c.Value == "ManageContent"))));

    options.AddPolicy("ViewAndCommentPolicy", policy =>
        policy.RequireAssertion(context =>
            context.User.HasClaim(c => c.Type == "Permission" && (c.Value == "ViewContent" || c.Value == "Comment"))));
});

builder.Services.AddTransient<IRepository,Repository>(); // adding the Repository service

builder.Host.UseSerilog((ctx, lc) => 
        lc.ReadFrom.Configuration(ctx.Configuration)); // adding logs
builder.Services.AddScoped<ActionsFilter>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var ctx = scope.ServiceProvider.GetRequiredService<UsersContext>();
    ctx.Database.EnsureDeleted();
    ctx.Database.EnsureCreated();

    var services = scope.ServiceProvider;
    var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

    await SeedData.Initialize(userManager, roleManager);
}

if(app.Environment.IsStaging() || app.Environment.IsProduction())
{
    app.UseExceptionHandler("/Error/Index");
}

app.UseStaticFiles(); // For enabling local images

using(var scope = app.Services.CreateScope()) // Reseting the db and filling it up again
{
    var ctx = scope.ServiceProvider.GetRequiredService<ZooContext>();
    ctx.Database.EnsureDeleted();
    ctx.Database.EnsureCreated();
}

app.UseAuthentication();

app.UseRouting(); // using routing 

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=LoginForm}/{id?}");

app.Run();
