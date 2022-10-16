using AutoFixture;
using FinDox.Application.Commands;
using FinDox.Domain.DataTransfer;
using FinDox.Domain.Entities;
using FinDox.Domain.Interfaces;
using FluentAssertions;
using Moq;

namespace FinDox.UnitTests.Application.Commands
{
    public class AddDocumentCommandHandlerTests : BaseTest<AddDocumentCommandHandler>
    {
        private Mock<IDocumentRepository> _documentRepositoryMock;
        private Mock<IFileRepository> _fileRepositoryMock;
        private Fixture _fixture;

        public override AddDocumentCommandHandler CreateInstanceUnderTest()
        {
            return new AddDocumentCommandHandler(
                _documentRepositoryMock.Object,
                _fileRepositoryMock.Object);
        }

        public override void SetUp()
        {
            _documentRepositoryMock = new Mock<IDocumentRepository>();
            _fileRepositoryMock = new Mock<IFileRepository>();
            _fixture = new Fixture();
        }

        [Test]
        public async Task Handle_should_fail_if_file_content_is_null()
        {
            //Arrange
            var command = new AddDocumentCommand(It.IsAny<DocumentWithFile>(), null);

            //Act
            var result = await InstanceUnderTest.Handle(command, It.IsAny<CancellationToken>());

            //Assert
            result.Should().NotBeNull();
            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain("File content cannot be null");
        }

        [Test]
        public async Task Handle_should_pass_with_valid_data()
        {
            //Arrange
            var documentFile = _fixture.Create<DocumentWithFile>();
            var content = _fixture.Create<byte[]>();
            var command = new AddDocumentCommand(documentFile, content);
            var expectedFileId = 1;
            var expectedDocument = _fixture.Create<Document>();
            expectedDocument.FileId = expectedFileId;
            var expectedResult = CommandResult<Document>.Success(expectedDocument);

            _fileRepositoryMock.Setup(d => d.AddFile(content))
                .ReturnsAsync(expectedFileId);
            _documentRepositoryMock.Setup(d => d.Add(It.IsAny<Document>()))
                .ReturnsAsync(expectedDocument);

            //Act
            var result = await InstanceUnderTest.Handle(command, It.IsAny<CancellationToken>());

            //Assert
            command.FileContent.Should().NotBeNull();
            command.Document.Should().NotBeNull();
            result.Should().NotBeNull();
            result.IsValid.Should().BeTrue();
            result.Should().BeEquivalentTo(expectedResult);
        }
    }
}
