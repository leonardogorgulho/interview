using FinDox.Application.Commands;
using FinDox.Domain.Interfaces;
using FluentAssertions;
using Moq;

namespace FinDox.UnitTests.Application.Commands
{
    public class RemoveDocumentCommandHandlerTests : BaseTest<RemoveDocumentCommandHandler>
    {
        private Mock<IDocumentRepository> _documentRepositoryMock;

        public override RemoveDocumentCommandHandler CreateInstanceUnderTest()
        {
            return new RemoveDocumentCommandHandler(_documentRepositoryMock.Object);
        }

        public override void SetUp()
        {
            _documentRepositoryMock = new Mock<IDocumentRepository>();
        }

        [Test]
        public async Task Handle_should_return_true_when_document_is_removed()
        {
            //Arrange
            var request = new RemoveDocumentCommand(1);

            _documentRepositoryMock.Setup(d => d.Remove(It.IsAny<int>()))
                .ReturnsAsync(true);

            //Act
            var result = await InstanceUnderTest.Handle(request, It.IsAny<CancellationToken>());

            //Assert
            result.Should().BeTrue();
            _documentRepositoryMock.Verify(d => d.Remove(It.Is<int>(d => d == request.DocumentId)));
        }

        [Test]
        public async Task Handle_should_return_false_when_document_is_not_removed()
        {
            //Arrange
            var request = new RemoveDocumentCommand(1);

            _documentRepositoryMock.Setup(d => d.Remove(It.IsAny<int>()))
                .ReturnsAsync(false);

            //Act
            var result = await InstanceUnderTest.Handle(request, It.IsAny<CancellationToken>());

            //Assert
            result.Should().BeFalse();
            _documentRepositoryMock.Verify(d => d.Remove(It.Is<int>(d => d == request.DocumentId)));
        }
    }
}
