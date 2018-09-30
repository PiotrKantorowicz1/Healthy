using System;
using Healthy.Core.Domain.BaseClasses;
using Healthy.Core.Exceptions;
using Healthy.Core.Extensions;

namespace Healthy.Core.Domain.Diets.DomainClasses
{
    public class MealItem : Entity
    {
        public string Name { get; protected set; }
        public double Quantity { get; protected set; }
        public NutritionValues NutritionValuesSummary { get; protected set; }

        protected MealItem()
        {
        }

        public MealItem(Guid id, string name, double quantity, NutritionValues nutritionValuesSummary)
        {
            Id = id;
            SetName(name);
            SetQuantity(quantity);
            SetNutritionValue(nutritionValuesSummary);
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
        }

        public void SetQuantity(double quantity)
        {
            if (quantity < 0 || quantity > 500)
            {
                throw new DomainException(ErrorCodes.InvalidQuantity,
                    "Product quantity can not be less than 0 and greater than 500.");
            }

            Quantity = quantity;
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
            var carbohydrates = nutritionValues.Carbohydrates * Quantity;
            var sugars = nutritionValues.Sugars * Quantity;

            var nutritionValuesSummary = NutritionValues.Create(energyValue, fats,
                carbohydrates, sugars, protein);

            NutritionValuesSummary = nutritionValuesSummary;
        }
    }
}