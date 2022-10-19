using FinDox.Application.Commands;
using FinDox.Domain.Interfaces;
using FluentAssertions;
using Moq;

namespace FinDox.UnitTests.Application.Commands
{
    public class RemoveUserCommandHandlerTests : BaseTest<RemoveUserCommandHandler>
    {
        private Mock<IUserRepository> _userRepositoryMock;

        public override RemoveUserCommandHandler CreateInstanceUnderTest()
        {
            return new RemoveUserCommandHandler(_userRepositoryMock.Object);
        }

        public override void SetUp()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
        }

        [Test]
        public async Task Handle_should_return_true_when_user_is_removed()
        {
            //Arrange
            var request = new RemoveUserCommand(1);

            _userRepositoryMock.Setup(d => d.Remove(It.IsAny<int>()))
                .ReturnsAsync(true);

            //Act
            var result = await InstanceUnderTest.Handle(request, It.IsAny<CancellationToken>());

            //Assert
            result.Should().BeTrue();
            _userRepositoryMock.Verify(d => d.Remove(It.Is<int>(d => d == request.Id)));
        }

        [Test]
        public async Task Handle_should_return_false_when_user_not_removed()
        {
            //Arrange
            var request = new RemoveUserCommand(1);

            _userRepositoryMock.Setup(d => d.Remove(It.IsAny<int>()))
                .ReturnsAsync(false);

            //Act
            var result = await InstanceUnderTest.Handle(request, It.IsAny<CancellationToken>());

            //Assert
            result.Should().BeFalse();
            _userRepositoryMock.Verify(d => d.Remove(It.Is<int>(d => d == request.Id)));
        }
    }
}
