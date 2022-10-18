using FinDox.Domain.DataTransfer;
using FinDox.Domain.Entities;
using FluentAssertions;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace FinDox.IntegrationTests.Documents
{
    public class DocumentControllerTests : BaseTest
    {
        [Test]
        public async Task Post_should_add_successfully_the_document()
        {
            var postedDocument = await PostDocument();

            var docAccess = await GrantAccess(postedDocument.DocumentId);

            var documentAfterPost = await GetDocument(postedDocument.DocumentId, true);

            await DeleteDocument(postedDocument.DocumentId);

            var documentAfterDelete = await GetDocument(postedDocument.DocumentId, false);
        }

        private async Task<Document> PostDocument()
        {
            var fileStreamContent = new StreamContent(File.OpenRead("Assets\\test.pdf"));
            fileStreamContent.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");

            using var multipartFormContent = new MultipartFormDataContent();
            multipartFormContent.Add(fileStreamContent, name: "file", fileName: "test.pdf");

            Client.DefaultRequestHeaders.Add("description", "test description");
            var response = await Client.PostAsync("/Document", multipartFormContent);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);

            return JsonConvert.DeserializeObject<Document>(await response.Content.ReadAsStringAsync());
        }

        private async Task DeleteDocument(int documentId)
        {
            var response = await Client.DeleteAsync($"/Document/{documentId}");

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
        }

        private async Task<Document> GetDocument(int documentId, bool documentExists)
        {
            var response = await Client.GetAsync($"/Document/{documentId}");

            if (!documentExists)
            {
                response.StatusCode.Should().Be(System.Net.HttpStatusCode.Unauthorized);
                return null;
            }

            var document = JsonConvert.DeserializeObject<DocumentWithFile>(await response.Content.ReadAsStringAsync());
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            document.Should().NotBeNull();
            document.DocumentId.Should().Be(documentId);

            return document;
        }

        private async Task<UsersAndGroupsIds> GrantAccess(int documentId)
        {
            var usersAndGroups = new UsersAndGroupsIds
            {
                GroupIds = new int[] { },
                UserIds = new int[] { Constants.UserId }
            };
            var response = await Client.PutAsJsonAsync<UsersAndGroupsIds>($"/Document/{documentId}/GrantPermission", usersAndGroups);

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

            return JsonConvert.DeserializeObject<UsersAndGroupsIds>(await response.Content.ReadAsStringAsync());
        }
    }
}
