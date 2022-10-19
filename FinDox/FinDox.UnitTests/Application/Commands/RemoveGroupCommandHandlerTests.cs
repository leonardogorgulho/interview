using FinDox.Application.Commands;
using FinDox.Domain.Interfaces;
using FluentAssertions;
using Moq;

namespace FinDox.UnitTests.Application.Commands
{
    public class RemoveGroupCommandHandlerTests : BaseTest<RemoveGroupCommandHandler>
    {
        private Mock<IGroupRepository> _groupRepositoryMock;

        public override RemoveGroupCommandHandler CreateInstanceUnderTest()
        {
            return new RemoveGroupCommandHandler(_groupRepositoryMock.Object);
        }

        public override void SetUp()
        {
            _groupRepositoryMock = new Mock<IGroupRepository>();
        }

        [Test]
        public async Task Handle_should_return_true_when_group_is_removed()
        {
            //Arrange
            var request = new RemoveGroupCommand(1);

            _groupRepositoryMock.Setup(d => d.Remove(It.IsAny<int>()))
                .ReturnsAsync(true);

            //Act
            var result = await InstanceUnderTest.Handle(request, It.IsAny<CancellationToken>());

            //Assert
            result.Should().BeTrue();
            _groupRepositoryMock.Verify(d => d.Remove(It.Is<int>(d => d == request.Id)));
        }

        [Test]
        public async Task Handle_should_return_false_when_group_is_not_removed()
        {
            //Arrange
            var request = new RemoveGroupCommand(1);

            _groupRepositoryMock.Setup(d => d.Remove(It.IsAny<int>()))
                .ReturnsAsync(false);

            //Act
            var result = await InstanceUnderTest.Handle(request, It.IsAny<CancellationToken>());

            //Assert
            result.Should().BeFalse();
            _groupRepositoryMock.Verify(d => d.Remove(It.Is<int>(d => d == request.Id)));
        }
    }
}
