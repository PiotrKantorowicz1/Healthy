using System;
using Healthy.Core.Domain.BaseClasses;
using Healthy.Core.Exceptions;
using Healthy.Core.Extensions;

namespace Healthy.Core.Domain.Diets.Entities
{
    public class Product : Entity, ITimestampable
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ProductSpecs NutritionsValue { get; set; }
        public ProductCategory Category { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime CreatedAt { get; set; }

        protected Product()
        {
        }

        public Product(Guid id, string name, string description, ProductSpecs nutritionValue,
            Category category)
        {
            Id = id;
            SetCategory(category);
            UpdatedAt = DateTime.UtcNow;
            CreatedAt = DateTime.UtcNow;
        }

        public void SetName(string name)
        {
            if (name.Empty())
            {
                throw new DomainException(ErrorCodes.NameNotProvided,
                    "Product name cannot be empty.");
            }
            if (name.Length > 150)
            {
                throw new DomainException(ErrorCodes.InvalidProductName,
                    "Product name is too long.");
            }
            Name = name;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetDescription(string description)
        {
            if (description.Empty())
            {
                throw new DomainException(ErrorCodes.DescriptionNotProvided,
                    "Product description cannot be empty.");
            }
            if (description.Length > 500)
            {
                throw new DomainException(ErrorCodes.InvalidDescription,
                    "Product description is too long.");
            }
            Description = description;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetNutritionValue(ProductSpecs nutritionValue)
        {
            if (nutritionValue == null)
            {
                throw new DomainException(ErrorCodes.NutritionValueNotProvided,
                    "Product nutrition value can not be null.");
            }
            NutritionsValue = nutritionValue;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetCategory(Category category)
        {
            if (category == null)
            {
                throw new DomainException(ErrorCodes.CategoryNotProvided,
                    "Product category can not be null.");
            }
            Category = ProductCategory.Create(category);
            UpdatedAt = DateTime.UtcNow;
        }
    }
}