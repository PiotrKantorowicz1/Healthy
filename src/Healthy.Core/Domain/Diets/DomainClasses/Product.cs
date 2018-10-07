using System;
using Healthy.Core.Domain.BaseClasses;
using Healthy.Core.Exceptions;
using Healthy.Core.Extensions;

namespace Healthy.Core.Domain.Diets.DomainClasses
{
    public class Product : Entity, ITimestampable
    {
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public NutritionValues NutritionValues { get; protected set; }
        public ProductCategory Category { get; protected set; }
        public DateTime? UpdatedAt { get; protected set; }
        public DateTime CreatedAt { get; protected set; }

        protected Product()
        {
        }

        public Product(Guid id, string name, string description,
            NutritionValues nutritionValue, Category category)
        {
            Id = id;
            SetName(name);
            SetDescription(description);
            SetNutritionValue(nutritionValue);
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
                throw new DomainException(ErrorCodes.InvalidName,
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

        public void SetNutritionValue(NutritionValues nutritionValues)
        {
            if (nutritionValues == null)
            {
                throw new DomainException(ErrorCodes.NutritionValuesNotProvided,
                    "Product nutrition value can not be null.");
            }

            NutritionValues = nutritionValues;
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