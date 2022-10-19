using FinDox.Application.Commands;
using FinDox.Domain.Entities;
using FinDox.Domain.Interfaces;
using FluentAssertions;
using Moq;

namespace FinDox.UnitTests.Application.Commands
{
    public class RemoveUserFromGroupCommandHandlerTests : BaseTest<RemoveUserFromGroupCommandHandler>
    {
        private Mock<IGroupRepository> _groupRepositoryMock;

        public override RemoveUserFromGroupCommandHandler CreateInstanceUnderTest()
        {
            return new RemoveUserFromGroupCommandHandler(_groupRepositoryMock.Object);
        }

        public override void SetUp()
        {
            _groupRepositoryMock = new Mock<IGroupRepository>();
        }

        [Test]
        public async Task Handle_should_return_true_when_user_is_removed_from_group()
        {
            //Arrange
            var request = new RemoveUserFromGroupCommand(new()
            {
                GroupId = 1,
                UserId = 1
            });

            _groupRepositoryMock.Setup(d => d.RemoveUser(It.IsAny<UserGroup>()))
                .ReturnsAsync(true);

            //Act
            var result = await InstanceUnderTest.Handle(request, It.IsAny<CancellationToken>());

            //Assert
            result.Should().BeTrue();
            _groupRepositoryMock.Verify(d => d.RemoveUser(It.Is<UserGroup>(d => d.UserId == request.UserGroup.UserId
                && d.GroupId == request.UserGroup.GroupId)));
        }

        [Test]
        public async Task Handle_should_return_false_when_user_is_not_removed_from_group()
        {
            //Arrange
            var request = new RemoveUserFromGroupCommand(new()
            {
                GroupId = 1,
                UserId = 1
            });

            _groupRepositoryMock.Setup(d => d.RemoveUser(It.IsAny<UserGroup>()))
                .ReturnsAsync(false);

            //Act
            var result = await InstanceUnderTest.Handle(request, It.IsAny<CancellationToken>());

            //Assert
            result.Should().BeFalse();
            _groupRepositoryMock.Verify(d => d.RemoveUser(It.Is<UserGroup>(d => d.UserId == request.UserGroup.UserId
                && d.GroupId == request.UserGroup.GroupId)));
        }
    }
}
