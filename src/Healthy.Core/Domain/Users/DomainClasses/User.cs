using System;
using Healthy.Core.Exceptions;
using Healthy.Core.Extensions;
using Healthy.Core.Domain.BaseClasses;
using Microsoft.AspNetCore.Identity;
using Healthy.Core.Domain.Users.Enumerations;

namespace Healthy.Core.Domain.Users.DomainClasses
{
    public class User : AggregateRoot, IEditable, ITimestampable
    {
        public Avatar Avatar { get; protected set; }
        public Guid UserId { get; protected set; }
        public string Email { get; protected set; }
        public string Name { get; protected set; }
        public string PasswordHash { get; protected set; }
        public ProviderType Provider { get; protected set; }
        public Roles Role { get; protected set; }
        public States State { get; protected set; }
        public string ExternalUserId { get; protected set; }
        public bool TwoFactorAuthentication { get; protected set; }
        public DateTime CreatedAt { get; protected set; }
        public DateTime UpdatedAt { get; protected set; }

        public User()
        {
        }

        public User(Guid userId, string email, string role, string provider) : this()
        {
            SetUserId(userId);
            SetEmail(email);
            Avatar = Avatar.Empty;
            SetProvider(provider);
            SetRole(role);
            State = States.Incomplete;
            TwoFactorAuthentication = false;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
            UserId = userId;
            Name = $"user-{Id:N}";
        }

        public void SetUserId(Guid userId)
        {
            if (userId == Guid.Empty)
                throw new ArgumentException("User id can not be empty.", nameof(userId));

            UserId = userId;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetExternalUserId(string externalUserId)
        {
            ExternalUserId = externalUserId;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetEmail(string email)
        {
            if (email.Empty())
            {
                Email = string.Empty;
                UpdatedAt = DateTime.UtcNow;

                return;
            }

            if (!email.IsEmail())
            {
                throw new ArgumentException($"Invalid email {email}.", nameof(email));
            }

            if (Email.EqualsCaseInvariant(email))
            {
                return;
            }

            Email = email.ToLowerInvariant();
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetName(Guid userId, string name, string state)
        {
            if (State != States.Incomplete)
            {
                throw new DomainException(ErrorCodes.NameAlreadySet,
                    $"User name has been already set: {Name}");
            }

            if (name.Empty())
            {
                throw new ArgumentException("User name can not be empty.", nameof(name));
            }

            if (Name.EqualsCaseInvariant(name))
            {
                return;
            }

            if (name.Length < 2)
            {
                throw new ArgumentException("User name is too short.", nameof(name));
            }

            if (name.Length > 50)
            {
                throw new ArgumentException("User name is too long.", nameof(name));
            }

            if (name.IsName())
            {
                throw new ArgumentException("User name doesn't meet the required criteria.", nameof(name));
            }

            Name = name.ToLowerInvariant();
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetRole(string role)
        {
            var roleType = Enumeration.FromDisplayName<Roles>(role);

            Role = roleType;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetProvider(string provider)
        {
            var providerType = Enumeration.FromDisplayName<ProviderType>(provider);

            Provider = providerType;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetAvatar(Avatar avatar)
        {
            if (avatar == null)
            {
                return;
            }

            Avatar = avatar;
            UpdatedAt = DateTime.UtcNow;
        }

        public void RemoveAvatar()
        {
            Avatar = Avatar.Empty;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Lock()
        {
            if (State == States.Locked)
            {
                throw new DomainException(ErrorCodes.UserAlreadyLocked,
                    $"User with id: '{UserId}' was already locked.");
            }

            State = States.Locked;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Unlock()
        {
            if (State != States.Locked)
            {
                throw new DomainException(ErrorCodes.UserNotLocked,
                    $"User with id: '{UserId}' is not locked.");
            }

            State = States.Active;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Activate()
        {
            if (State == States.Active)
            {
                throw new DomainException(ErrorCodes.UserAlreadyActive,
                    $"User with id: '{UserId}' was already activated.");
            }

            State = States.Active;
            UpdatedAt = DateTime.UtcNow;
        }

        public void EnableTwoFactorAuthentication()
        {
            TwoFactorAuthentication = true;
            UpdatedAt = DateTime.UtcNow;
        }

        public void DisableTwoFactorAuthentication()
        {
            TwoFactorAuthentication = false;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetUnconfirmed()
        {
            if (State == States.Unconfirmed)
            {
                throw new DomainException(ErrorCodes.UserAlreadyUnconfirmed,
                    $"User with id: '{UserId}' was already set as unconfirmed.");
            }

            State = States.Unconfirmed;
            UpdatedAt = DateTime.UtcNow;
        }

        public void MarkAsDeleted()
        {
            if (State == States.Deleted)
            {
                throw new DomainException(ErrorCodes.UserAlreadyDeleted,
                    $"User with id: '{UserId}' was already marked as deleted.");
            }

            State = States.Deleted;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetPassword(string password, IPasswordHasher<User> passwordHasher)
        {
            if (password.Empty())
            {
                throw new DomainException(ErrorCodes.InvalidPassword,
                    "Password can not be empty.");
            }

            if (password.Length < 4)
            {
                throw new DomainException(ErrorCodes.InvalidPassword,
                    "Password must contain at least 4 characters.");
            }

            if (password.Length > 100)
            {
                throw new DomainException(ErrorCodes.InvalidPassword,
                    "Password can not contain more than 100 characters.");
            }

            PasswordHash = passwordHasher.HashPassword(this, password);
            UpdatedAt = DateTime.UtcNow;
        }

        public bool ValidatePassword(string password, IPasswordHasher<User> passwordHasher)
            => passwordHasher.VerifyHashedPassword(this, PasswordHash, password) != PasswordVerificationResult.Failed;
    }
}