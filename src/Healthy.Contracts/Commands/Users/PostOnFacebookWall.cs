using Healthy.Contracts.Commands;

namespace Healthy.Contracts.Commands.Users
{
    public class PostOnFacebookWall : ICommand
    {
        public string UserId { get; }
        public string AccessToken { get; }
        public string Message { get; }
        
        public PostOnFacebookWall(string userId, string accessToken, string message)
        {
            UserId = userId;
            AccessToken = accessToken;
            Message = message;
        }
    }
}