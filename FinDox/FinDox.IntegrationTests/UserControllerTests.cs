using FinDox.Domain.DataTransfer;
using FluentAssertions;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace FinDox.IntegrationTests
{
    public class UserControllerTests : BaseTest
    {
        [Test, Order(1)]
        public async Task Post_should_add_successfully_the_user()
        {
            _ = await PostUser();
        }

        [Test, Order(2)]
        public async Task Put_should_update_successfully_the_user()
        {
            var postedUser = await PostUser();

            var uniqueData = Guid.NewGuid().ToString();
            var expectedUserName = uniqueData;
            var login = uniqueData.Substring(0, 10);
            var password = "12345678901234567890123456789012";
            var role = "R";

            var httpResponse = await Client.PutAsJsonAsync($"/User/{postedUser.UserId}", new UserEntryRequest
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
        }

        [Test, Order(3)]
        public async Task Get_should_retrieve_user()
        {
            var postedUser = await PostUser();

            var user = await Client.GetFromJsonAsync<UserResponse>($"/User/{postedUser.UserId}");

            postedUser.Should().BeEquivalentTo(user);
        }

        [Test, Order(4)]
        public async Task GetUsers_should_retrieve_user()
        {
            var postedUser1 = await PostUser();
            var postedUser2 = await PostUser();

            var users = await Client.GetFromJsonAsync<List<UserResponse>>($"/User/list?skip=0&take=50");

            postedUser1.Should().BeEquivalentTo(users.FirstOrDefault(d => d.UserId == postedUser1.UserId));
            postedUser2.Should().BeEquivalentTo(users.FirstOrDefault(d => d.UserId == postedUser2.UserId));
        }

        [Test, Order(5)]
        public async Task Delete_should_delete_successfully_user()
        {
            var postedUser = await PostUser();

            var httpResponse = await Client.DeleteAsync($"/User/{postedUser.UserId}");

            httpResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        private async Task<UserResponse> PostUser()
        {
            var uniqueData = Guid.NewGuid().ToString();
            var expectedUserName = uniqueData;
            var login = uniqueData.Substring(0, 10);
            var password = "12345678901234567890123456789012";
            var role = "R";

            var httpResponse = await Client.PostAsJsonAsync("/User", new UserEntryRequest
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
    }
}
