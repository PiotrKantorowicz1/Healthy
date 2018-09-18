using System;

namespace Healthy.Core.Contracts.Commands.Users
{
    public class RefreshUserSession : ICommand
    {
        public Guid SessionId { get; set; }
        public Guid NewSessionId { get; set; }
        public string Key { get; set; }
    }
}