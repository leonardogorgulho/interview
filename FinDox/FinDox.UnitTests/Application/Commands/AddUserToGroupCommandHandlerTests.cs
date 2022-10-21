using FinDox.Application.Commands;
using FinDox.Domain.Exceptions;
using FinDox.Domain.Interfaces;
using FluentAssertions;
using Moq;

namespace FinDox.UnitTests.Application.Commands
{
    public class AddUserToGroupCommandHandlerTests : BaseTest<AddUserToGroupCommandHandler>
    {
        private Mock<IGroupRepository> _groupRepositoryMock;

        public override AddUserToGroupCommandHandler CreateInstanceUnderTest()
        {
            return new AddUserToGroupCommandHandler(_groupRepositoryMock.Object);
        }

        public override void SetUp()
        {
            _groupRepositoryMock = new Mock<IGroupRepository>();
        }

        [Test]
        public async Task Handle_should_return_success_when_data_is_valid()
        {
            //Arrange
            var request = new AddUserToGroupCommand(new()
            {
                GroupId = 1,
                UserId = 1
            });

            _groupRepositoryMock.Setup(d => d.AddUser(request.UserGroup))
                .ReturnsAsync(true);

            //Act
            var result = await InstanceUnderTest.Handle(request, It.IsAny<CancellationToken>());

            //Assert
            result.Should().NotBeNull();
            result.IsValid.Should().Be(true);
            result.Errors.Should().BeNull();
        }

        [Test]
        public async Task Handle_should_return_failure_when_there_is_a_invalid_id()
        {
            //Arrange
            var request = new AddUserToGroupCommand(new()
            {
                GroupId = 0,
                UserId = 1
            });

            _groupRepositoryMock.Setup(d => d.AddUser(request.UserGroup))
                .Throws(new InvalidUserGroupException(request.UserGroup.UserId, request.UserGroup.GroupId));

            //Act
            var result = await InstanceUnderTest.Handle(request, It.IsAny<CancellationToken>());

            //Assert
            result.Should().NotBeNull();
            result.IsValid.Should().Be(false);
            result.Errors.Should().NotBeNull();
            result.Errors.Should().HaveCount(1);
            result.Errors.First().Should().Be($"One or more provided Ids are not valid (user id: {request.UserGroup.UserId}, group id: {request.UserGroup.GroupId})");
        }
    }
}
