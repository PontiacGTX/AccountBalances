using BankAccount.Api.Middleware;
using BankAccount.Common;
using BankAccount.Common.HelperClass;
using BankAccount.Common.Helpers;
using BankAccount.Data.Model;
using BankAccount.DataAccess;
using BankAccount.DataAccess.Interfaces;
using BankAccount.DataAccess.Repository;
using BankAccount.Services.Interfaces;
using BankAccount.Services.Services;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
var res =PseudoIdGenerator.NewId();
// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(o =>
{
    o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    o.JsonSerializerOptions.WriteIndented = true;
});

builder.Services.AddDbContext<AppDbContext>(o =>
{
    o.UseSqlite(builder.Configuration.GetConnectionString("Sqlite"));

});

builder.Services.AddSingleton(typeof(ILogger<>),typeof(Logger<>));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IAccountServices, AccountServices>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddLogging(config =>
{
    config.AddDebug();
    config.AddConsole();
});

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using var scope = app.Services.CreateScope();
using (AppDbContext ctx = scope.ServiceProvider.GetRequiredService<AppDbContext>())
{

    var deleteOnStartUp = app.Configuration.TryGetSection<bool>("DeleteDbOnStartup");

    if(deleteOnStartUp)
      await ctx.Database.EnsureDeletedAsync();

    await ctx.Database.EnsureCreatedAsync();
    await ctx.Database.MigrateAsync();
    var filePath = Directory.GetFiles(Directory.GetCurrentDirectory()).FirstOrDefault(x=>Path.GetExtension(x) ==".js");
    
    if(filePath != null)
    await ctx.FeedDb(filePath);

}
app.UseCors(option => option.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseRouting();

app.UseMiddleware<GlobalResquestHandlerMiddleware>();
app.Run();
