namespace Healthy.Contracts.Commands.Users
{
    public class RemoveAvatar : ICommand
    {
        public string UserId { get; }
        
        public RemoveAvatar(string userId)
        {
            UserId = userId;
        }
    }
}