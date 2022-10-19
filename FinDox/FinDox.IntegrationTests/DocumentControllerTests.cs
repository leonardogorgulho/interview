using FinDox.Domain.DataTransfer;
using FinDox.Domain.Entities;
using FluentAssertions;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace FinDox.IntegrationTests
{
    public class DocumentControllerTests : BaseTest
    {
        [Test]
        public async Task Post_should_add_successfully_the_document_then_delete_it()
        {
            //Act
            var postedDocument = await PostDocument();

            //Assert
            postedDocument.Should().NotBeNull();
            postedDocument.DocumentId.Should().BeGreaterThan(0);

            await DeleteDocument(postedDocument.DocumentId);
        }

        [Test]
        public async Task Download_should_download_the_posted_file()
        {
            //Arrange
            var postedDocument = await PostDocument();

            //Act
            var response = await Client.GetAsync($"/Document/{postedDocument.DocumentId}/Download");

            //Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

            await DeleteDocument(postedDocument.DocumentId);
        }

        [Test]
        public async Task GrantPermission_should_create_link_between_user_and_document()
        {
            //Arrange
            var postedDocument = await PostDocument();
            var exptectedPermissions = new UsersAndGroupsIds
            {
                GroupIds = new int[] { },
                UserIds = new int[] { 1 }
            };

            //Act
            var grantedPermission = await GrantAccess(postedDocument.DocumentId);

            //Assert
            var permissions = await GetPermissions(postedDocument.DocumentId);
            grantedPermission.Should().NotBeNull();
            grantedPermission.Should().BeEquivalentTo(exptectedPermissions);
            permissions.Users.First(d => d.UserId == Constants.UserId).Should().NotBeNull();

            await DeleteDocument(postedDocument.DocumentId);
        }


        private async Task<Document> PostDocument()
        {
            //Arrange
            var fileStreamContent = new StreamContent(File.OpenRead("Assets\\test.pdf"));
            fileStreamContent.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");

            using var multipartFormContent = new MultipartFormDataContent();
            multipartFormContent.Add(fileStreamContent, name: "file", fileName: "test.pdf");

            //Act
            Client.DefaultRequestHeaders.Add("description", "test description");
            var response = await Client.PostAsync("/Document", multipartFormContent);

            //Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);

            return JsonConvert.DeserializeObject<Document>(await response.Content.ReadAsStringAsync());
        }

        private async Task DeleteDocument(int documentId)
        {
            //Act
            var response = await Client.DeleteAsync($"/Document/{documentId}");

            //Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        private async Task<Document> GetDocument(int documentId)
        {
            //Act
            var response = await Client.GetAsync($"/Document/{documentId}");

            var document = JsonConvert.DeserializeObject<DocumentWithFile>(await response.Content.ReadAsStringAsync());

            //Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            document.Should().NotBeNull();
            document.DocumentId.Should().Be(documentId);

            return document;
        }

        private async Task<UsersAndGroupsIds> GrantAccess(int documentId)
        {
            //Act
            var usersAndGroups = new UsersAndGroupsIds
            {
                GroupIds = new int[] { },
                UserIds = new int[] { Constants.UserId }
            };
            var response = await Client.PutAsJsonAsync($"/Document/{documentId}/GrantPermission", usersAndGroups);

            //Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

            return JsonConvert.DeserializeObject<UsersAndGroupsIds>(await response.Content.ReadAsStringAsync());
        }

        private async Task<DocumentPermissionResponse> GetPermissions(int documentId)
        {
            //Act
            var response = await Client.GetFromJsonAsync<DocumentPermissionResponse>($"/Document/{documentId}/Permissions");

            //Assert
            response.Should().NotBeNull();
            response.Users.Should().Satisfy(d => d.UserId == Constants.UserId);

            return response;
        }
    }
}
