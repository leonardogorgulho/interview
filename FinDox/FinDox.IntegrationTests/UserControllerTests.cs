using FinDox.Domain.DataTransfer;
using FluentAssertions;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace FinDox.IntegrationTests
{
    public class UserControllerTests : BaseTest
    {
        UserResponse _user;

        [Test, Order(1)]
        public async Task Post_should_add_successfully_the_user()
        {
            var uniqueData = DateTime.Now.ToString("yyyyMMddhhmmss");
            var expectedUserName = uniqueData;
            var login = uniqueData;
            var password = "12345678901234567890123456789012";
            var role = "R";

            var httpResponse = await Client.PostAsJsonAsync("/User", new UserEntryRequest
            {
                Name = expectedUserName,
                Login = login,
                Password = password,
                Role = role
            });

            _user = JsonConvert.DeserializeObject<UserResponse>(await httpResponse.Content.ReadAsStringAsync());

            _user.Should().NotBeNull();
            _user.UserId.Should().BeGreaterThan(0);
            _user.Name.Should().Be(expectedUserName);
        }

        [Test, Order(2)]
        public async Task Put_should_update_successfully_the_user()
        {
            var uniqueData = DateTime.Now.ToString("yyyyMMddhhmmss");
            var expectedUserName = uniqueData;
            var login = uniqueData;
            var password = "12345678901234567890123456789012";
            var role = "R";

            var httpResponse = await Client.PutAsJsonAsync($"/User/{_user.UserId}", new UserEntryRequest
            {
                Name = expectedUserName,
                Login = login,
                Password = password,
                Role = role
            });

            _user = JsonConvert.DeserializeObject<UserResponse>(await httpResponse.Content.ReadAsStringAsync());

            _user.Should().NotBeNull();
            _user.UserId.Should().BeGreaterThan(0);
            _user.Name.Should().Be(expectedUserName);
        }

        [Test, Order(3)]
        public async Task Get_should_retrieve_user()
        {
            var user = await Client.GetFromJsonAsync<UserResponse>($"/User/{_user.UserId}");

            _user.Should().BeEquivalentTo(user);
        }

        [Test, Order(4)]
        public async Task GetUsers_should_retrieve_user()
        {
            var users = await Client.GetFromJsonAsync<List<UserResponse>>($"/User/list?skip=0&take=50");

            var foundUser = users.FirstOrDefault(d => d.UserId == _user.UserId);
            _user.Should().BeEquivalentTo(foundUser);
        }

        [Test, Order(5)]
        public async Task Delete_should_delete_successfully_user()
        {
            var httpResponse = await Client.DeleteAsync($"/User/{_user.UserId}");

            httpResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        [Test, Order(6)]
        public async Task Get_should_return_not_found()
        {
            var httpResponse = await Client.GetAsync($"/User/{_user.UserId}");

            httpResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }
    }
}
