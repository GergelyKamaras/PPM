using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PPMAPIDataAccess;
using PPMAPIDataAccess.DbTableQueries.AddressQueries;
using PPMAPIDataAccess.DbTableQueries.CostsQueries;
using PPMAPIDataAccess.DbTableQueries.OwnersQueries;
using PPMAPIDataAccess.DbTableQueries.PropertiesQueries;
using PPMAPIDataAccess.DbTableQueries.RevenuesQueries;
using PPMAPIDataAccess.DbTableQueries.TenantsQueries;
using PPMAPIDataAccess.DbTableQueries.ValueDecreasesQueries;
using PPMAPIDataAccess.DbTableQueries.ValueIncreasesQueries;
using PPMAPIServiceLayer.InputDTOConverter;
using PPMAPIServiceLayer.OutputDTOFactory;
using PPMAPIServiceLayer.Validation;


var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json");
string connString = builder.Configuration.GetConnectionString("APIConnString");

// Add services to the container.
builder.Services.AddDbContext<PPMDbContext>(options =>
{
    options.UseSqlServer(connString);
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

// Register own data access services
builder.Services.AddTransient<IAddressesQueries, AddressesQueries>();
builder.Services.AddTransient<ICostsQueries, CostsQueries>();
builder.Services.AddTransient<IRevenuesQueries, RevenuesQueries>();
builder.Services.AddTransient<IValueIncreasesQueries, ValueIncreasesQueries>();
builder.Services.AddTransient<IValueDecreasesQueries, ValueDecreasesQueries>();
builder.Services.AddTransient<IPropertiesQueries, PropertiesQueries>();
builder.Services.AddTransient<IOwnersQueries, OwnersQueries>();
builder.Services.AddTransient<ITenantsQueries, TenantsQueries>();

// Register service layer services
builder.Services.AddTransient<IFinancialObjectFactory, FinancialObjectFactory>();
builder.Services.AddTransient<IPropertyFactory, PropertyFactory>();
builder.Services.AddTransient<IFinancialObjectOutputDTOFactory, FinancialObjectOutputDTOFactory>();
builder.Services.AddTransient<IPropertyOutputDTOFactory, PropertyOutputDTOFactory>();

// Register Validators
builder.Services.AddTransient<IFinancialInputDTOValidator, FinancialInputDTOValidator>();
builder.Services.AddTransient<IPropertyInputDTOValidator, PropertyInputDTOValidator>();
builder.Services.AddTransient<IFinancialObjectValidator, FinancialObjectValidator>();
builder.Services.AddTransient<IPropertyValidator, PropertyValidator>();

// CORS
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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.UseHttpsRedirection();

app.UseCors("CorsPolicy");
app.UseAuthentication();
app.UseAuthorization();

app.Run();
