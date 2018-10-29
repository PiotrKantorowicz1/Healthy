using System;
using System.Linq;
using System.Threading.Tasks;

namespace Healthy.ReadSide.Models
{
    public class User
    {
        public Avatar Avatar { get; set; }
        public string UserId { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string PasswordHash { get; set; }
        public string Provider { get; set; }
        public string Role { get; set; }
        public string State { get; set; }
        public string ExternalUserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}