namespace FinDox.Domain.DataTransfer
{
    public class GroupWithUsers
    {
        public int GroupId { get; set; }

        public string Name { get; set; }

        public List<UserResponse> Users { get; set; }
    }
}
