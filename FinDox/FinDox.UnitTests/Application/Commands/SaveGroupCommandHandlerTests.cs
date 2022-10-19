using FinDox.Application.Commands;
using FinDox.Domain.Entities;
using FinDox.Domain.Exceptions;
using FinDox.Domain.Interfaces;
using FluentAssertions;
using Moq;

namespace FinDox.UnitTests.Application.Commands
{
    public class SaveGroupCommandHandlerTests : BaseTest<SaveGroupCommandHandler>
    {
        private Mock<IGroupRepository> _groupRepositoryMock;

        public override SaveGroupCommandHandler CreateInstanceUnderTest()
        {
            return new SaveGroupCommandHandler(_groupRepositoryMock.Object);
        }

        public override void SetUp()
        {
            _groupRepositoryMock = new Mock<IGroupRepository>();
        }

        [Test]
        public async Task Handle_should_add_successfully_group()
        {
            //Arrange
            var command = new SaveGroupCommand(new() { Name = "Group1" });
            var returnedGroup = new Group { GroupId = 1, Name = command.GroupRequest.Name };
            _groupRepositoryMock.Setup(d => d.Add(It.IsAny<Group>()))
                .ReturnsAsync(returnedGroup);

            //Act
            var result = await InstanceUnderTest.Handle(command, It.IsAny<CancellationToken>());

            //Assert
            result.Should().NotBeNull();
            result.IsValid.Should().Be(true);
            result.Data.Should().BeEquivalentTo(returnedGroup);
            _groupRepositoryMock.Verify(d => d.Add(It.Is<Group>(d => d.Name == command.GroupRequest.Name)));
        }

        [Test]
        public async Task Handle_should_update_successfully_group()
        {
            //Arrange
            var command = new SaveGroupCommand(new() { Name = "Group1" }, 1);
            var returnedGroup = new Group { GroupId = 1, Name = "Group2" };
            _groupRepositoryMock.Setup(d => d.Update(It.IsAny<Group>()))
                .ReturnsAsync(returnedGroup);

            //Act
            var result = await InstanceUnderTest.Handle(command, It.IsAny<CancellationToken>());

            //Assert
            result.Should().NotBeNull();
            result.IsValid.Should().Be(true);
            result.Data.Should().BeEquivalentTo(returnedGroup);
            _groupRepositoryMock.Verify(d => d.Update(It.Is<Group>(d => d.Name == command.GroupRequest.Name)));
        }

        [Test]
        public async Task Handle_should_return_failure_when_group_already_exists()
        {
            //Arrange
            var command = new SaveGroupCommand(new() { Name = "Group1" });
            _groupRepositoryMock.Setup(d => d.Add(It.IsAny<Group>()))
                .Throws(new ExistingGroupException(command.GroupRequest.Name));

            //Act
            var result = await InstanceUnderTest.Handle(command, It.IsAny<CancellationToken>());

            //Assert
            result.Should().NotBeNull();
            result.IsValid.Should().Be(false);
            result.Data.Should().BeNull();
            result.Errors.Should().HaveCount(1);
            result.Errors.First().Should().Be($"A group with name '{command.GroupRequest.Name}' already exists");
            _groupRepositoryMock.Verify(d => d.Add(It.Is<Group>(d => d.Name == command.GroupRequest.Name)));
        }
    }
}
