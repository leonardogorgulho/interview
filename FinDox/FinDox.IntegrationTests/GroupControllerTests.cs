﻿using FinDox.Domain.DataTransfer;
using FinDox.Domain.Entities;
using FluentAssertions;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace FinDox.IntegrationTests
{
    public class GroupControllerTests : BaseTest
    {
        [Test, Order(1)]
        public async Task Post_should_add_successfully_the_group()
        {
            _ = await PostGroup();
        }

        [Test, Order(2)]
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
        }

        [Test, Order(3)]
        public async Task Get_should_retrieve_group()
        {
            var postedGroup = await PostGroup();

            var group = await Client.GetFromJsonAsync<Group>($"/Group/{postedGroup.GroupId}");

            group.Should().BeEquivalentTo(postedGroup);
        }

        [Test, Order(4)]
        public async Task Delete_should_delete_successfully_group()
        {
            var postedGroup = await PostGroup();

            var httpResponse = await Client.DeleteAsync($"/Group/{postedGroup.GroupId}");

            httpResponse.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        private async Task<Group> PostGroup()
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
    }
}