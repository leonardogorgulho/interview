using FinDox.Application.Commands;
using FinDox.Domain.DataTransfer;
using FinDox.Domain.Exceptions;
using FinDox.Domain.Interfaces;
using FluentAssertions;
using Moq;

namespace FinDox.UnitTests.Application.Commands
{
    public class SaveDocumentPermissionHandlerTests : BaseTest<SaveDocumentPermissionHandler>
    {
        private Mock<IDocumentRepository> _documentRepositoryMock;

        public override SaveDocumentPermissionHandler CreateInstanceUnderTest()
        {
            return new SaveDocumentPermissionHandler(_documentRepositoryMock.Object);
        }

        public override void SetUp()
        {
            _documentRepositoryMock = new Mock<IDocumentRepository>();
        }

        [Test]
        public async Task Handle_should_remove_document_permission_successfully()
        {
            //Arrange
            var command = new RemoveDocumentPermission(new()
            {
                DocumentId = 1,
                GroupIds = new int[] { 1, 2 },
                UserIds = new int[] { 1 }
            });
            _documentRepositoryMock.Setup(d => d.RemoveAccess(It.IsAny<DocumentPermissionEntry>()))
                .ReturnsAsync(true);

            //Act
            var result = await InstanceUnderTest.Handle(command, It.IsAny<CancellationToken>());

            //Assert
            result.Should().NotBeNull();
            result.IsValid.Should().Be(true);
            _documentRepositoryMock.Verify(d => d.RemoveAccess(It.Is<DocumentPermissionEntry>(dp => dp == command.Permission)));
        }

        [Test]
        public async Task Handle_should_return_true_when_permission_is_granted()
        {
            //Arrange
            var command = new GrantDocumentPermission(new()
            {
                DocumentId = 1,
                GroupIds = new int[] { 1, 2 },
                UserIds = new int[] { 1 }
            });
            _documentRepositoryMock.Setup(d => d.GrantAccess(It.IsAny<DocumentPermissionEntry>()))
                .ReturnsAsync(true);

            //Act
            var result = await InstanceUnderTest.Handle(command, It.IsAny<CancellationToken>());

            //Assert
            result.Should().NotBeNull();
            result.IsValid.Should().Be(true);
            _documentRepositoryMock.Verify(d => d.GrantAccess(It.Is<DocumentPermissionEntry>(dp => dp == command.Permission)));
        }

        [Test]
        public async Task Handle_should_return_false_when_parameter_has_wrong_data()
        {
            //Arrange
            var command = new GrantDocumentPermission(new()
            {
                DocumentId = 1,
                GroupIds = new int[] { 0 },
                UserIds = new int[] { 1 }
            });
            _documentRepositoryMock.Setup(d => d.GrantAccess(It.IsAny<DocumentPermissionEntry>()))
                .Throws(new InvalidDocumentPermissionEntryException(command.Permission));

            //Act
            var result = await InstanceUnderTest.Handle(command, It.IsAny<CancellationToken>());

            //Assert
            result.Should().NotBeNull();
            result.IsValid.Should().Be(false);
            result.Errors.Should().HaveCount(1);
            result.Errors.First().Should().Be($"One or more provided ids are not valid (UserIds: {string.Join(", ", command.Permission.UserIds)}, GroupIds: {string.Join(", ", command.Permission.GroupIds)})");
            _documentRepositoryMock.Verify(d => d.GrantAccess(It.Is<DocumentPermissionEntry>(dp => dp == command.Permission)));
        }
    }
}
