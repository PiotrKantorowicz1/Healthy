using System;
using Healthy.Core.Domain.BaseClasses;
using Healthy.Core.Exceptions;
using Healthy.Core.Extensions;

namespace Healthy.Core.Domain.Diets.Entities
{
    public class Category : Entity
    {
        public string Name { get; protected set; }

        protected Category()
        {
        }

        public Category(Guid id, string name)
        {
            Id = id;
            SetName(name);
        }

        public void SetName(string name)
        {
            if (name.Empty())
                throw new DomainException(ErrorCodes.InvalidCategory, 
                    "Category name can not be empty.");
            if (name.Length > 100)
                throw new DomainException(ErrorCodes.InvalidCategory, 
                    "Category name is too long.");
            if (Name.EqualsCaseInvariant(name))
                return;

            Name = name.ToLowerInvariant();
        }
    }
}
