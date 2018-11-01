using System;

namespace Healthy.Storage.Models.Users
{
    public class UserSessionRM
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public string Key { get; set; }
        public string UserAgent { get; set; }
        public string IpAddress { get; set; }
        public Guid? ParentId { get; set; }
        public bool Refreshed { get; set; }
        public bool Destroyed { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}