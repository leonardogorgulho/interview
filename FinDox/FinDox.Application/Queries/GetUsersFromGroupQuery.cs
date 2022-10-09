﻿using FinDox.Domain.Response;
using MediatR;

namespace FinDox.Application.Queries
{
    public class GetUsersFromGroupQuery : IRequest<GroupWithUsers>
    {
        public int GroupId { get; set; }

        public GetUsersFromGroupQuery(int groupId)
        {
            GroupId = groupId;
        }
    }
}
