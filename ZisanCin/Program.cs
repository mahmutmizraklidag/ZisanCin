using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using ZisanCin.Data;
using ZisanCin.Entities;
using ZisanCin.Models;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<DatabaseContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
});

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(x =>
    {
        x.LoginPath = "/Admin/Login";
        x.AccessDeniedPath = "/AccessDenied";
        x.LogoutPath = "/Admin/Logout";
        x.Cookie.Name = "Admin";
        x.Cookie.MaxAge = TimeSpan.FromDays(10);
        x.ExpireTimeSpan = TimeSpan.FromHours(3); // ?? 1 saat oturum süresi
        x.SlidingExpiration = true; // ?? Her işlemde süre yenilenir
        x.Cookie.IsEssential = true;
        x.Cookie.SameSite = SameSiteMode.Lax; // ?? None yerine Lax önerilir
        x.Cookie.HttpOnly = true;
    });

var app = builder.Build();

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

app.UseAuthentication();
app.UseAuthorization();

app.Use((context, next) =>
{
    var dbcontext = context.RequestServices.GetRequiredService<DatabaseContext>();

    DataRequestModel.ClearData();

   

    DataRequestModel.SiteSetting =
        dbcontext.SiteSettings.FirstOrDefault()
        ?? new SiteSetting(); // null kalmasın
   
    //DataRequestModel.About =
    //    dbcontext.Abouts.FirstOrDefault(x => x.Language.ToString() == lang)
    //    ?? new About();
    //DataRequestModel.Services =
    //    dbcontext.Services.Where(x => x.Language.ToString() == lang)
    //            .OrderBy(s => s.Title).ToList();
   
    return next();
});

app.MapControllerRoute(
           name: "areas",
           pattern: "{area:exists}/{controller=Main}/{action=Index}/{id?}"
         );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
