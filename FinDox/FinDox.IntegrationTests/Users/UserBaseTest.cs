using FinDox.Domain.DataTransfer;
using FluentAssertions;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace FinDox.IntegrationTests.Users
{
    public class UserBaseTest : BaseTest
    {
        public async Task<UserResponse> PostUser()
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

        public async Task<HttpResponseMessage> DeleteUser(int userId)
        {
            return await Client.DeleteAsync($"/User/{userId}");
        }
    }
}
