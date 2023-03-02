using AuthService.Authentication;
using AuthService.Authentication.Roles;
using AuthService.Authentication.Roles.Validator;
using AuthService.Controller;
using AuthService.DataAccess;
using AuthService.DataAccess.UserTableQueries;
using AuthService.DataSeed;
using AuthService.ModelConverter;
using AuthServiceModelLibrary.ApplicationUser;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json");
string connString = builder.Configuration.GetConnectionString("ConnString");
string seedUserPassword = builder.Configuration["SeedAdminPassword"];

// Add services to the container.
builder.Services.AddDbContext<AuthDbContext>(options =>
{
    options.UseSqlServer(connString);
});

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<AuthDbContext>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddTransient<ISecurityUtil, SecurityUtil>();
builder.Services.AddTransient<IApplicationUserFactory, ApplicationUserFactory>();
builder.Services.AddTransient<IAuthOperations, AuthOperations>();
builder.Services.AddTransient<IUserTableQueries, UserTableQueries>();
builder.Services.AddTransient<IRoleValidator, RoleValidator>();
builder.Services.AddTransient<IJWTService, JWTService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Seed data to the DB
var seedService = app.Services.CreateScope().ServiceProvider; 
await RoleSeed.InitRoles(seedService.GetRequiredService<RoleManager<IdentityRole>>());

AuthController controller = new AuthController(seedService.GetRequiredService<IApplicationUserFactory>(), seedService.GetRequiredService<IAuthOperations>(),
    seedService.GetRequiredService<IRoleValidator>(), seedService.GetRequiredService<UserManager<ApplicationUser>>(), seedService.GetRequiredService<IJWTService>());

SeedUser.Init(controller, seedService.GetRequiredService<IUserTableQueries>(), seedUserPassword);

app.MapControllers();

app.UseHttpsRedirection();

app.Run();
