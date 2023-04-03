using System.Web.Helpers;
using AuthServiceDataAccess;
using AuthServiceModelLibrary.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

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
        public async Task RegisterAndLogin_Administrator_ReturnsSuccessCode()
        {
            // Arrange
            var client = _authServiceFactory.CreateClient();

            var user = new Dictionary<string, string>()
            {
                {"Role", "Administrator"},
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
            
            // Act
            await client.PostAsync("/api/authentication/register", form);
            var response = await client.PostAsync("/api/authentication/login", loginForm);

            // Assert
            Assert.That(response.IsSuccessStatusCode);
        }

        [Test]
        public async Task HappyPath_Owner_ReturnsSuccessCodes()
        {
            // Arrange
            var client = _authServiceFactory.CreateClient();

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

            // Act
            // Registration 
            await client.PostAsync("/api/authentication/register", form);

            // Login, get token 
            HttpResponseMessage responseLogin = await client.PostAsync("/api/authentication/login", loginForm);
            string s = await responseLogin.Content.ReadAsStringAsync();
            LoginResultDTO r = JsonConvert.DeserializeObject<LoginResultDTO>(s);
            string token = r.Token;
            
            // register a property
            // register a rental property
            // add financial objects each of them, check the response codes
            // delete user check status code

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(responseLogin.IsSuccessStatusCode);
            });
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            _authServiceFactory.Dispose();
            _ppmAPIFactory.Dispose();
        }
    }
}