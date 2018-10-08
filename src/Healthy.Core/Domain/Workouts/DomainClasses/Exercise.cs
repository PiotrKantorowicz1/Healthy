using System;
using Healthy.Core.Domain.BaseClasses;
using Healthy.Core.Exceptions;
using Healthy.Core.Extensions;

namespace Healthy.Core.Domain.Workouts.DomainClasses
{
    public class Exercise : AggregateRoot, ITimestampable
    {
        public string Name { get; protected set; }
        public string BodyGroup { get; protected set; }
        public string Description { get; protected set; }
        public DateTime? UpdatedAt { get; protected set; }
        public DateTime CreatedAt { get; protected set; }

        protected Exercise()
        {          
        }

        public Exercise(Guid id, string name, string bodyGroup,
            string description)
        {
            Id = id;
            SetName(name);
            SetBodyGroup(bodyGroup);
            SetDescription(description);
            UpdatedAt = DateTime.UtcNow;
            CreatedAt = DateTime.UtcNow;
        }

        public void SetName(string name)
        {
            if (name.Empty())
            {
                throw new DomainException(ErrorCodes.NameNotProvided,
                    "Exercise name cannot be empty.");
            }

            if (name.Length > 150)
            {
                throw new DomainException(ErrorCodes.InvalidName,
                    "Exercise name is too long.");
            }

            Name = name;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetBodyGroup(string bodyGroup)
        {
            if (!DomainClasses.BodyGroup.IsValid(bodyGroup))
            {
                throw new DomainException(ErrorCodes.InvalidBodyGroup,
                    "Body group is invalid!");
            }

            BodyGroup = bodyGroup;
            UpdatedAt = UpdatedAt;
        }
        
        public void SetDescription(string description)
        {
            if (description.Empty())
            {
                throw new DomainException(ErrorCodes.DescriptionNotProvided,
                    "Exercise description cannot be empty.");
            }

            if (description.Length > 1500)
            {
                throw new DomainException(ErrorCodes.InvalidDescription,
                    "Exercise description is too long.");
            }

            Description = description;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}