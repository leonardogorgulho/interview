using FinDox.Domain.DataTransfer;
using FinDox.Domain.Entities;
using FluentAssertions;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace FinDox.IntegrationTests
{
    public class GroupControllerTests : BaseTest
    {
        private Group _group;

        [Test, Order(1)]
        public async Task Post_should_add_successfully_the_group()
        {
            var expectedGroupName = DateTime.Now.ToString("yyyyMMddhhmmss");

            var httpResponse = await Client.PostAsJsonAsync("/Group", new GroupRequest
            {
                Name = expectedGroupName
            });

            _group = JsonConvert.DeserializeObject<Group>(await httpResponse.Content.ReadAsStringAsync());

            _group.Should().NotBeNull();
            _group.GroupId.Should().BeGreaterThan(0);
            _group.Name.Should().Be(expectedGroupName);
        }

        [Test, Order(2)]
        public async Task Put_should_update_successfully_the_group()
        {
            var expectedGroupName = DateTime.Now.ToString("yyyyMMddhhmmss");

            var httpResponse = await Client.PutAsJsonAsync($"/Group/{_group.GroupId}", new GroupRequest
            {
                Name = expectedGroupName
            });

            _group = JsonConvert.DeserializeObject<Group>(await httpResponse.Content.ReadAsStringAsync());

            _group.Should().NotBeNull();
            _group.GroupId.Should().BeGreaterThan(0);
            _group.Name.Should().Be(expectedGroupName);
        }

        [Test, Order(3)]
        public async Task Get_should_retrieve_group()
        {
            var group = await Client.GetFromJsonAsync<Group>($"/Group/{_group.GroupId}");

            _group.Should().BeEquivalentTo(group);
        }

        [Test, Order(4)]
        public async Task Delete_should_delete_successfully_group()
        {
            var httpResponse = await Client.DeleteAsync($"/Group/{_group.GroupId}");

            httpResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        [Test, Order(5)]
        public async Task Get_should_return_not_found()
        {
            var httpResponse = await Client.GetAsync($"/Group/{_group.GroupId}");

            httpResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.NotFound);
        }
    }
}