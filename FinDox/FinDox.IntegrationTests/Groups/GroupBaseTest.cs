using FinDox.Domain.DataTransfer;
using FinDox.Domain.Entities;
using FluentAssertions;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace FinDox.IntegrationTests.Groups
{
    public class GroupBaseTest : BaseTest
    {
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
    }
}
