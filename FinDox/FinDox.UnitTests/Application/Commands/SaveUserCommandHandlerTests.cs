using AutoFixture;
using FinDox.Application.Commands;
using FinDox.Domain.DataTransfer;
using FinDox.Domain.Entities;
using FinDox.Domain.Exceptions;
using FinDox.Domain.Extensions;
using FinDox.Domain.Interfaces;
using FluentAssertions;
using Moq;

namespace FinDox.UnitTests.Application.Commands
{
    public class SaveUserCommandHandlerTests : BaseTest<SaveUserCommandHandler>
    {
        private Mock<IUserRepository> _userRepositoryMock;
        private Fixture _fixture;

        public override SaveUserCommandHandler CreateInstanceUnderTest()
        {
            return new SaveUserCommandHandler(_userRepositoryMock.Object);
        }

        public override void SetUp()
        {
            _fixture = new();
            _userRepositoryMock = new Mock<IUserRepository>();
        }

        [Test]
        public async Task Handle_should_add_user_and_return_result_successfully()
        {
            //Arrange
            var userEntry = _fixture.Create<NewUserRequest>();
            var command = new SaveNewUserCommand(userEntry);
            var expectedUser = userEntry.ToEntity();
            expectedUser.UserId = 1;

            _userRepositoryMock.Setup(d => d.Add(It.IsAny<User>()))
                .ReturnsAsync(expectedUser);

            //Act
            var result = await InstanceUnderTest.Handle(command, It.IsAny<CancellationToken>());

            //Assert
            var expectedUserResponse = expectedUser.ToUserResponse();
            result.Should().NotBeNull();
            result.IsValid.Should().BeTrue();
            result.GetType().Should().Be(typeof(CommandResult<UserResponse>));
            result.Data.Should().BeEquivalentTo(expectedUserResponse);
        }

        [Test]
        public async Task Handle_should_return_failure_when_login_already_exists()
        {
            //Arrange
            var userEntry = _fixture.Create<NewUserRequest>();
            userEntry.Login = "admin";
            var command = new SaveNewUserCommand(userEntry);
            var expectedException = new ExistingLoginException(userEntry.Login);
            _userRepositoryMock.Setup(d => d.Add(It.IsAny<User>()))
                .ThrowsAsync(expectedException);

            //Act
            var result = await InstanceUnderTest.Handle(command, It.IsAny<CancellationToken>());

            //Assert
            result.Should().NotBeNull();
            result.IsValid.Should().BeFalse();
            result.GetType().Should().Be(typeof(CommandResult<UserResponse>));
            result.Data.Should().BeNull();
            result.Errors.Should().Contain(expectedException.Message);
        }

        [Test]
        public async Task Handle_should_update_user_and_return_result_successfully()
        {
            //Arrange
            var userEntry = _fixture.Create<ChangeUserRequest>();
            var command = new SaveChangedUserCommand(userEntry, 1);
            var expectedUser = userEntry.ToEntity();

            _userRepositoryMock.Setup(d => d.Update(It.IsAny<User>()))
                .ReturnsAsync(expectedUser);

            //Act
            var result = await InstanceUnderTest.Handle(command, It.IsAny<CancellationToken>());

            //Assert
            var expectedUserResponse = expectedUser.ToUserResponse();
            result.Should().NotBeNull();
            result.IsValid.Should().BeTrue();
            result.GetType().Should().Be(typeof(CommandResult<UserResponse>));
            result.Data.Should().BeEquivalentTo(expectedUserResponse);
        }
    }
}
