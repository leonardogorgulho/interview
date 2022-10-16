using FinDox.Domain.DataTransfer;
using FluentAssertions;
using System.Net.Http.Json;
using System.Security.Cryptography;

namespace FinDox.IntegrationTests
{
    public class AccountControllerTests : BaseTest
    {
        [Test]
        public async Task ConvertPassword_should_hash_raw_password()
        {
            var rawPassword = "123";
            byte[] bytes = System.Text.Encoding.ASCII.GetBytes(rawPassword);
            using var md5 = MD5.Create();
            byte[] hashBytes = md5.ComputeHash(bytes);
            var expectedResult = Convert.ToHexString(hashBytes);

            var httpResponse = await Client.PostAsJsonAsync<string>("/account/ConvertPassword", rawPassword);
            var result = await httpResponse.Content.ReadAsStringAsync();

            result.Should().Be(expectedResult);
        }

        [Test]
        public async Task Login_should_log_user_in_successfully()
        {
            var httpResponse = await Client.PostAsJsonAsync("/account/login", new LoginRequest
            {
                Login = "admin",
                Password = "202CB962AC59075B964B07152D234B70"
            });
            var content = Newtonsoft.Json.JsonConvert.DeserializeObject<LoginResponse>(
                await httpResponse.Content.ReadAsStringAsync());
        }
    }
}