using Healthy.Contracts.Commands;

namespace Healthy.Contracts.Commands.Users
{
    public class DeleteAccount : ICommand
    {
        public string UserId { get; }
        public bool Soft { get; }
        
        public DeleteAccount(string userId, bool soft)
        {
            UserId = userId;
            Soft = soft;
        }
    }
}