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
        public double Quantity { get; set; }
        public NutritionValues NutritionsValues { get; set; }
        public ProductCategory Category { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime CreatedAt { get; set; }

        protected Product()
        {
        }

        public Product(Guid id, string name, string description, double quantity, 
            NutritionValues nutritionValue, Category category)
        {
            Id = id;
            SetName(name);
            SetDescription(description);
            SetQuantity(quantity);
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

        public void SetQuantity(double quantity)
        {
            if (quantity < 0 || quantity > 500)
            {
                throw new DomainException(ErrorCodes.InvalidQuantity,
                    "Product quantity can not be less than 0 and grather than 500.");
            }

            Quantity = quantity;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetNutritionValue(NutritionValues nutritionValues)
        {
            if (nutritionValues == null)
            {
                throw new DomainException(ErrorCodes.NutritionValuesNotProvided,
                    "Product nutrition value can not be null.");
            }

            var energyValue = nutritionValues.EnergyValue * Quantity;
            var fats = nutritionValues.Fats * Quantity;
            var protein = nutritionValues.Protein * Quantity;
            var carbo = nutritionValues.Carbo * Quantity;
            var sugars = nutritionValues.Sugars * Quantity;

            NutritionsValues = nutritionValues;
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