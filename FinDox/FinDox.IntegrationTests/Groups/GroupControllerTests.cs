using FinDox.Domain.DataTransfer;
using FinDox.Domain.Entities;
using FluentAssertions;
using Newtonsoft.Json;
using System.Net.Http.Json;


namespace FinDox.IntegrationTests.Groups
{
    public class GroupControllerTests : BaseTest
    {
        [Test]
        public async Task Post_should_add_successfully_the_group()
        {
            var postedGroup = await PostGroup();

            _ = await DeleteGroup(postedGroup.GroupId);
        }

        [Test]
        public async Task Put_should_update_successfully_the_group()
        {
            var group = await PostGroup();

            var expectedGroupName = Guid.NewGuid().ToString();

            var httpResponse = await Client.PutAsJsonAsync($"/Group/{group.GroupId}", new GroupRequest
            {
                Name = expectedGroupName
            });

            group = JsonConvert.DeserializeObject<Group>(await httpResponse.Content.ReadAsStringAsync());

            group.Should().NotBeNull();
            group.GroupId.Should().BeGreaterThan(0);
            group.Name.Should().Be(expectedGroupName);

            _ = await DeleteGroup(group.GroupId);
        }

        [Test]
        public async Task Get_should_retrieve_group()
        {
            var postedGroup = await PostGroup();

            var group = await Client.GetFromJsonAsync<Group>($"/Group/{postedGroup.GroupId}");

            group.Should().BeEquivalentTo(postedGroup);

            _ = await DeleteGroup(postedGroup.GroupId);
        }

        [Test]
        public async Task Delete_should_delete_successfully_group()
        {
            var postedGroup = await PostGroup();

            var httpResponse = await DeleteGroup(postedGroup.GroupId);

            httpResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        [Test]
        public async Task AddUser_should_link_user_to_group()
        {
            var postedGroup = await PostGroup();
            var postedUser = await PostUser();

            var userGroup = new UserGroup
            {
                GroupId = postedGroup.GroupId,
                UserId = postedUser.UserId
            };

            var httpResponse = await Client.PostAsJsonAsync<UserGroup>($"/Group/AddUserToGroup", userGroup);
            var returnedUserGroup = JsonConvert.DeserializeObject<UserGroup>(await httpResponse.Content.ReadAsStringAsync());

            var usersFromGroup = await Client.GetFromJsonAsync<GroupWithUsers>($"/Group/{postedGroup.GroupId}/users");

            httpResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            userGroup.Should().BeEquivalentTo(returnedUserGroup);
            postedUser.Should().BeEquivalentTo(usersFromGroup.Users.FirstOrDefault(u => u.UserId == returnedUserGroup.UserId));

            await DeleteGroup(postedGroup.GroupId);
            await DeleteUser(postedUser.UserId);
        }
    }
}