﻿namespace FinDox.Domain.Response
{
    public class GroupWithUsers
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<UserResponse> Users { get; set; }
    }
}