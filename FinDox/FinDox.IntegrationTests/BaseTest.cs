using FinDox.Domain.DataTransfer;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace FinDox.IntegrationTests
{
    public abstract class BaseTest : IDisposable
    {
        private readonly WebApplicationFactory<Program> _application;

        public BaseTest()
        {
            _application = new WebApplicationFactory<Program>();
            Client = _application.CreateClient();
            Client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", GetToken().Result);
        }

        public HttpClient Client { get; }

        public string Token { get; }


        public void Dispose()
        {
            _application.Dispose();
        }

        private async Task<string> GetToken()
        {
            var httpResponse = await Client.PostAsJsonAsync("/account/login", new LoginRequest
            {
                Login = "admin",
                Password = "202CB962AC59075B964B07152D234B70"
            });
            var content = Newtonsoft.Json.JsonConvert.DeserializeObject<LoginResponse>(
                await httpResponse.Content.ReadAsStringAsync());

            return content.Token;
        }
    }
}
