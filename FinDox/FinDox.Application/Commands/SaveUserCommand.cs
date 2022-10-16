﻿using FinDox.Domain.DataTransfer;
using MediatR;

namespace FinDox.Application.Commands
{
    public class SaveUserCommand : IRequest<CommandResult<UserResponse>>
    {
        public SaveUserCommand(UserEntryRequest userEntry)
        {
            UserEntry = userEntry;
        }

        public SaveUserCommand(UserEntryRequest userEntry, int? id)
        {
            UserEntry = userEntry;
            Id = id;
        }

        public int? Id { get; set; }

        public UserEntryRequest UserEntry { get; set; }

        public bool IsNewUser => !Id.HasValue;
    }
}
