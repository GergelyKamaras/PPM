using System.Text;
using AuthServiceServiceLayer.Authentication.AuthOperations;
using AuthServiceServiceLayer.Authentication.JWTService;
using AuthServiceServiceLayer.Authentication.Roles;
using AuthServiceServiceLayer.Authentication.Roles.Validator;
using AuthServiceServiceLayer.Authentication.SecurityUtil;
using AuthServiceAPI.Controller;
using AuthServiceDataAccess;
using AuthServiceDataAccess.UserTableQueries;
using AuthServiceAPI.DataSeed;
using AuthServiceServiceLayer.ModelConverter;
using AuthServiceModelLibrary.ApplicationUser;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

public class AuthServiceProgram
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Configuration.AddJsonFile("appsettings.json");
        string connString = builder.Configuration.GetConnectionString("ConnString");
        string seedUserPassword = builder.Configuration["AdminOnePassword"];
        string seedUserEmail = builder.Configuration["AdminOneEmail"];

        builder.Services.AddCors(o =>
        {
            o.AddPolicy(name: "CorsPolicy",
                policy =>
                {
                    policy.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
        });

        // Add services to the container.
        builder.Services.AddDbContext<AuthDbContext>(options =>
        {
            options.UseSqlServer(connString);
        });

        builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<AuthDbContext>();


        // Auth, JWT
        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(o =>
        {
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey
                    (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"])),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true
            };
        });

        builder.Services.AddAuthorization();

        // Swagger
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddControllers();

        // Register own services
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
            seedService.GetRequiredService<IRoleValidator>(), seedService.GetRequiredService<UserManager<ApplicationUser>>(),
            seedService.GetRequiredService<IJWTService>(), seedService.GetRequiredService<IConfiguration>());

        SeedUser.Init(controller, seedService.GetRequiredService<IUserTableQueries>(), seedUserEmail, seedUserPassword);

        app.MapControllers();

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();
        app.UseCors("CorsPolicy");

        app.Run();
    }
}