using Healthy.Contracts.Commands;

namespace Healthy.Contracts.Commands.Users
{
    public class ChangeUsername : ICommand
    {
        public string UserId { get; }
        public string Name { get; }
        
        public ChangeUsername(string userId, string name)
        {
            UserId = userId;
            Name = name;
        }
    }
}