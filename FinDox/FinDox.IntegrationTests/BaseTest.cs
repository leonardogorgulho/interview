using FinDox.Domain.DataTransfer;
using FinDox.Domain.Entities;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
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

        public void Dispose()
        {
            _application.Dispose();
        }

        private async Task<string> GetToken()
        {
            var httpResponse = await Client.PostAsJsonAsync("/account/login", new LoginRequest
            {
                Login = Constants.Login,
                Password = Constants.Password
            });
            var content = Newtonsoft.Json.JsonConvert.DeserializeObject<LoginResponse>(
                await httpResponse.Content.ReadAsStringAsync());

            return content.Token;
        }

        public async Task<Group> PostGroup()
        {
            var expectedGroupName = Guid.NewGuid().ToString();

            var httpResponse = await Client.PostAsJsonAsync("/Group", new GroupRequest
            {
                Name = expectedGroupName
            });

            var group = JsonConvert.DeserializeObject<Group>(await httpResponse.Content.ReadAsStringAsync());

            group.Should().NotBeNull();
            group.GroupId.Should().BeGreaterThan(0);
            group.Name.Should().Be(expectedGroupName);

            return group;
        }

        public async Task<HttpResponseMessage> DeleteGroup(int groupId)
        {
            return await Client.DeleteAsync($"/Group/{groupId}");
        }

        public async Task<UserResponse> PostUser()
        {
            var uniqueData = Guid.NewGuid().ToString();
            var expectedUserName = uniqueData;
            var login = uniqueData.Substring(0, 10);
            var password = "12345678901234567890123456789012";
            var role = "R";

            var httpResponse = await Client.PostAsJsonAsync("/User", new NewUserRequest
            {
                Name = expectedUserName,
                Login = login,
                Password = password,
                Role = role
            });

            var user = JsonConvert.DeserializeObject<UserResponse>(await httpResponse.Content.ReadAsStringAsync());

            user.Should().NotBeNull();
            user.UserId.Should().BeGreaterThan(0);
            user.Name.Should().Be(expectedUserName);

            return user;
        }

        public async Task<HttpResponseMessage> DeleteUser(int userId)
        {
            return await Client.DeleteAsync($"/User/{userId}");
        }
    }
}
