using Bajaj.eCommerce.Dal;
using Bajaj.eCommerce.Mvc.UI.ExtensionMethods;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Bajaj.eCommerce.Mvc.UI.Data;
using Microsoft.AspNetCore.Identity;
using Bajaj.eCommerce.Mvc.UI.Filters;
using Bajaj.eCommerce.Mvc.UI.CustomMiddleWare;
using Serilog;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddControllersWithViews(options =>
//{
//    options.Filters.Add<GlobalFilter>();
//});
builder.Services.AddRazorPages();
builder.Services.AddHttpClient(); 

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(20);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
}
);
builder.Services.AddDbContext<eCommerceDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("BajajEComConStr"));
});
builder.Services.AddDbContext<BajajeSecurityDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("BajajeSecurityDbContextConnection"));
});

builder.Services.AddDefaultIdentity<IdentityUser>(options => 
options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<BajajeSecurityDbContext>();
builder.Services.AddRepositriesServices();
builder.Services.AddAutoMapper(cfg=>
{
    cfg.AddMaps(Assembly.GetExecutingAssembly());
}
);

//app.UseMiddleware();

var app = builder.Build();
//app.UseMiddleware<ExceptionMiddleware>();
//app.UseMiddleware<MyCustomMiddleware>();
//app.UseMiddleware<LoggingMiddleware>();
//app.UseLoggingMiddleware();
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
app.UseSession();

app.UseAuthentication();
app.UseAuthorization();



app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
          );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

#region Seed Security

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var roles = new[] { "Admin", "Customer" };
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
}


//Creating Users  - This block will get executed everytime the application starts/restarts. We are seeding the users
using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
    string adminUser = "admin@ecommerce.com";
    string adminPassword = "Welcome@123";
    if (await userManager.FindByEmailAsync(adminUser) == null)
    {
        var user = new IdentityUser(adminUser) { UserName = adminUser, Email = adminUser };
        await userManager.CreateAsync(user, adminPassword);
        await userManager.AddToRoleAsync(user, "Admin");
    }
}
#endregion
// app.Use(async (context, next) =>
// {
//     Console.WriteLine($"Before Request: {context.Request.Path}");
//
//     await next();
//
//     Console.WriteLine($"After Request: {context.Request.Path}");
// });
app.Run();
