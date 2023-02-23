using AuthService.Authentication;
using AuthService.DataAccess;
using AuthService.DataAccess.UserTableQueries;
using AuthService.ModelConverter;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json");
string connString = builder.Configuration.GetConnectionString("ConnString");

// Add services to the container.
builder.Services.AddDbContext<AuthDbContext>(options =>
{
    options.UseSqlServer(connString);
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddTransient<ISecurityUtil, SecurityUtil>();
builder.Services.AddTransient<IApplicationUserFactory, ApplicationUserFactory>();
builder.Services.AddTransient<IAuthOperations, AuthOperations>();
builder.Services.AddTransient<IUserTableQueries, UserTableQueries>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.UseHttpsRedirection();

app.Run();
