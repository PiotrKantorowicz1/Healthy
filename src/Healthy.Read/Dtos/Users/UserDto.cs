namespace Healthy.Read.Dtos.Users
{
    public class UserDto : UserInfoDto
    {
        public string Email { get; set; }
        public string Provider { get; set; }
        public string ExternalUserId { get; set; }
    }
}