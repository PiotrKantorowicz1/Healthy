using System;

namespace Healthy.Core.Contracts.Commands.Users
{
    public class SignOut : ICommand
    {
        public Guid SessionId { get; set; }
        public string UserId { get; set; }
    }
}