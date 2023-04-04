using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using AuthServiceDataAccess;
using AuthServiceModelLibrary.ApplicationUser;
using AuthServiceModelLibrary.DTOs;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using PPMAPIDTOModelLibrary.OutputDTOs.Properties;
using PPMAPIDTOModelLibrary.SharedDTOs;
using PPMDTOModelLibrary.InputDTOs.Properties;

namespace IntegrationTests
{
    public class AuthServiceAPITests
    {
        private WebApplicationFactory<AuthServiceProgram> _authServiceFactory;
        private WebApplicationFactory<PPMAPIProgram> _ppmAPIFactory;

        [OneTimeSetUp]
        public void Setup()
        {
            _authServiceFactory = new WebApplicationFactory<AuthServiceProgram>()
                .WithWebHostBuilder(host =>
                {
                    host.ConfigureServices(services =>
                    {
                        var descriptor = services.SingleOrDefault(
                            d => d.ServiceType ==
                                 typeof(DbContextOptions<AuthDbContext>));

                        services.Remove(descriptor);

                        services.AddDbContext<AuthDbContext>(options =>
                        {
                            options.UseInMemoryDatabase("AuthTestDb");
                        });
                    });
                });
            
            _ppmAPIFactory = new WebApplicationFactory<PPMAPIProgram>()
                .WithWebHostBuilder(host =>
                {
                    host.ConfigureServices(services =>
                    {
                        var descriptor = services.SingleOrDefault(
                            d => d.ServiceType ==
                                 typeof(DbContextOptions<AuthDbContext>));

                        services.Remove(descriptor);

                        services.AddDbContext<AuthDbContext>(options =>
                        {
                            options.UseInMemoryDatabase("PPMTestDb");
                        });
                    });
                });
            
        }

        [Test]
        public async Task HappyPath_Owner_ReturnsSuccessCodes()
        {
            // Setup clients
            HttpClient authClient = _authServiceFactory.CreateClient();
            HttpClient apiClient = _ppmAPIFactory.CreateClient();

            // Register admin
            var admin = new Dictionary<string, string>()
            {
                {"Role", "Administrator"},
                {"Email", "AdminEmail@Host.com"},
                {"FirstName", "Jolan"},
                {"LastName", "Hegyi"},
                {"Password", "VeryPassword_123"},
                {"Username", "Admin"}
            };

            FormUrlEncodedContent formAdminRegistration = new FormUrlEncodedContent(admin);
            HttpResponseMessage responseAdminRegistration = await authClient.PostAsync("/api/authentication/register", formAdminRegistration);

            var loginAdminData = new Dictionary<string, string>()
            {
                {"Email", "AdminEmail@Host.com"},
                {"Password", "VeryPassword_123"}
            };
            var loginAdminForm = new FormUrlEncodedContent(loginAdminData);

            HttpResponseMessage responseAddminLogin = await authClient.PostAsync("/api/authentication/login", loginAdminForm);
            
            // Get admin's jwt token
            string responseContentString = await responseAddminLogin.Content.ReadAsStringAsync();
            LoginResultDTO adminLoginResult = JsonConvert.DeserializeObject<LoginResultDTO>(responseContentString);

            // Register user
            var user = new Dictionary<string, string>()
            {
                {"Role", "Owner"},
                {"Email", "ValidEmail@Host.com"},
                {"FirstName", "Jolan"},
                {"LastName", "Hegyi"},
                {"Password", "VeryPassword_123"},
                {"Username", "Hegyine"}
            };

            FormUrlEncodedContent form = new FormUrlEncodedContent(user);

            var loginData = new Dictionary<string, string>()
            {
                {"Email", "ValidEmail@Host.com"},
                {"Password", "VeryPassword_123"}
            };
            var loginForm = new FormUrlEncodedContent(loginData);

            // Register on AuthService
            await authClient.PostAsync("/api/authentication/register", form);
            
            // Login, get token, get userId
            HttpResponseMessage responseOwnerLogin = await authClient.PostAsync("/api/authentication/login", loginForm);
            string s = await responseOwnerLogin.Content.ReadAsStringAsync();
            LoginResultDTO r = JsonConvert.DeserializeObject<LoginResultDTO>(s);
            var token = new JwtSecurityTokenHandler().ReadJwtToken(r.Token);
            string userId = token.Claims.FirstOrDefault(c => c.Type == "Id").Value;
            
            /*
             * Register on API server
             * TODO Should be deleted once inter server communication is established in test environment
             */

            apiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", adminLoginResult.Token);
            Dictionary<string, string> userData = new Dictionary<string, string>()
            {
                { "userId", userId },
                {"role", "Owner"}
            };
            var content = new FormUrlEncodedContent(userData);

            HttpResponseMessage responseOwnerAPIRegistration = await apiClient.PostAsync("api/users", content);

            // Add owner's authorization to following requests
            authClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", r.Token);
            apiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", r.Token);

            // Register a property
            PropertyInputDTO property = new PropertyInputDTO()
            {
                IsRental = false,
                Name = "TestProperty",
                OwnerId = userId,
                PurchaseDate = new DateTime(2023, 02, 01),
                PurchasePrice = 500,
                Size = 35,
                Address = new AddressDTO()
                {
                    Country = "Nomadisztan",
                    City = "ARealCity",
                    ZipCode = "666",
                    Street = "DontLiveHere",
                    StreetNumber = 13,
                    AdditionalInfo = "nope"
                },
            };

            HttpResponseMessage responsePropertyRegistration = await apiClient.PostAsJsonAsync<PropertyInputDTO>("api/properties", property);

            List<PropertyOutputDTO> propertyDTOs = await apiClient.GetFromJsonAsync<List<PropertyOutputDTO>>($"api/properties/property/owners/{userId}");
            PropertyOutputDTO testProperty = propertyDTOs[0];
            
            // Delete the property
            HttpResponseMessage responsePropertyDelete = await apiClient.DeleteAsync($"api/properties/property/{testProperty.Id}");

            // Perform deletion of user from AuthService
            HttpResponseMessage responseOwnerDelete = await authClient.DeleteAsync($"/api/authentication/{userId}");
            
            // register a property
            // register a rental property
            // add financial objects each of them, check the response codes

            // Assert
            Assert.That(responseAdminRegistration.IsSuccessStatusCode);
            Assert.That(responseAddminLogin.IsSuccessStatusCode);
            Assert.That(responseOwnerAPIRegistration.IsSuccessStatusCode);
            Assert.That(responseOwnerLogin.IsSuccessStatusCode);
            Assert.That(responsePropertyRegistration.IsSuccessStatusCode);
            Assert.That(responsePropertyDelete.IsSuccessStatusCode);
            Assert.That(responseOwnerDelete.IsSuccessStatusCode);
            
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            _authServiceFactory.Dispose();
            _ppmAPIFactory.Dispose();
        }
    }
}