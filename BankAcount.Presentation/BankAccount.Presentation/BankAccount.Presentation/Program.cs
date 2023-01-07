using BankAccount.Common.Helpers;
using BankAccount.Data;
using BankAccount.Data.Model;
using BankAccount.Presentation.Common;
using BankAccount.Presentation.DataAccess;
using BankAccount.Presentation.Services;
using BankAccount.Presentation.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddJsonOptions(o =>
{

    o.JsonSerializerOptions.NumberHandling = JsonNumberHandling.AllowReadingFromString;
    o.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    o.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
    o.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    o.JsonSerializerOptions.WriteIndented = true;
});

builder.Services.AddDbContext<AppDbContext>(x =>
{
    x.UseSqlite(builder.Configuration.GetConnectionString("Sqlite"));
    
})
    .AddIdentity<ApplicationUser, IdentityRole>(options =>
    {
        options.Password.RequireUppercase = false;
        options.Password.RequireDigit = false;

    })
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders()
    .AddUserManager<UserManager<ApplicationUser>>()
    .AddSignInManager();

var cfg = builder.Configuration.GetSection("ServicesEndpoints").Get<ServicesEndpoints>();
builder.Services.AddScoped<ServicesEndpoints>(s => cfg);
builder.Services.AddSingleton<HttpClient>(c => new HttpClient { BaseAddress = new System.Uri(cfg.BaseServicesUrl) });

builder.Services.AddScoped<IAccountServices, AccountServices>();
builder.Services.AddScoped<IUserServices, UserServices>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
using var scope = app.Services.CreateScope();
using(var dbContext = scope.ServiceProvider.GetService<AppDbContext>())
{
    var deleteDbOnStartup = app.Configuration.TryGetSection<bool>("DeleteDbOnStartup");
    if (deleteDbOnStartup)
        await dbContext.Database.EnsureDeletedAsync();

    await dbContext.Database.EnsureCreatedAsync();
    await dbContext.Database.MigrateAsync();

    var filePath = Directory.GetFiles(Directory.GetCurrentDirectory()).FirstOrDefault(x => Path.GetExtension(x) == ".js");

    if (filePath != null)
    {
        using (var userManager = scope.ServiceProvider.GetService<UserManager<ApplicationUser>>())
        {
            await userManager.FeedDbUsers(filePath);
        }
    }
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
