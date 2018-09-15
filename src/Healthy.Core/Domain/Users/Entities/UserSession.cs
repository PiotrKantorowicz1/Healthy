using System;
using Healthy.Core.Exceptions;
using Healthy.Core.Domain.BaseClasses;

namespace Healthy.Core.Domain.Users.Entities
{
    public class UserSession : ITimestampable
    {
        public Guid Id { get; protected set; }
        public string UserId { get; protected set; }
        public string Key { get; protected set; }
        public string UserAgent { get; protected set; }
        public string IpAddress { get; protected set; }
        public Guid? ParentId { get; protected set; }
        public bool Refreshed { get; protected set; }
        public bool Destroyed { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime UpdatedAt { get; protected set; }

        protected UserSession()
        {
        }

        public UserSession(Guid id, string userId) : this(id, userId, null)
        {
        }

        public UserSession(Guid id, string userId, string key,
            string ipAddress = null, string userAgent = null,
            Guid? parentId = null)
        {
            Id = id;
            UserId = userId;
            Key = key;
            UserAgent = userAgent;
            IpAddress = ipAddress;
            ParentId = parentId;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Destroy()
        {
            CheckIfAlreadyRefreshedOrDestroyed();
            Destroyed = true;
            UpdatedAt = DateTime.UtcNow;
        }

        public UserSession Refresh(Guid newSessionId, string key, Guid parentId,
            string ipAddress = null, string userAgent = null)
        {
            CheckIfAlreadyRefreshedOrDestroyed();
            ParentId = parentId;
            Refreshed = true;
            UpdatedAt = DateTime.UtcNow;

            return new UserSession(newSessionId, UserId, key, ipAddress, userAgent, parentId);
        }

        private void CheckIfAlreadyRefreshedOrDestroyed()
        {
            if (Refreshed)
            {
                throw new DomainException(ErrorCodes.SessionRefreshed,
                    $"Session for user id: '{UserId}' " +
                    $"with key: '{Key}' has been already refreshed.");
            }
            if (Destroyed)
            {
                throw new DomainException(ErrorCodes.SessionDestroyed,
                    $"Session for user id: '{UserId}' " +
                    $"with key: '{Key}' has been already destroyed.");
            }
        }
    }
}
