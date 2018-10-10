using System;
using System.Collections.Generic;
using System.Linq;
using Healthy.Core.Domain.BaseClasses;
using Healthy.Core.Exceptions;
using Healthy.Core.Types;

namespace Healthy.Core.Domain.Diets.DomainClasses
{
    public class Meal : AggregateRoot, ITimestampable
    {
        private ISet<MealItem> _mealItems = new HashSet<MealItem>();
        public int MealNumber { get; protected set; }
        public DateTime UpdatedAt { get; protected set; }
        public DateTime CreatedAt { get; protected set; }

        public IEnumerable<MealItem> MealItems
        {
            get => _mealItems;
            protected set => _mealItems = new HashSet<MealItem>(value);
        }

        protected Meal()
        {
        }

        public Meal(Guid id, int mealNumber)
        {
            Id = id;
            SetMealNumber(mealNumber);
            UpdatedAt = DateTime.UtcNow;
            CreatedAt = DateTime.UtcNow;
        }

        public void SetMealNumber(int mealNumber)
        {
            if (mealNumber < 0 || mealNumber > 12)
            {
                throw new DomainException(ErrorCodes.InvalidMealNumber,
                    "Meal number can not be less than 0 and greater than 12.");
            }

            MealNumber = mealNumber;
            UpdatedAt = DateTime.UtcNow;
        }

        public void AddMealItem(MealItem item)
        {
            _mealItems.Add(new MealItem(item.MealId, item.ProductId, 
                item.Name, item.Quantity, item.NutritionValuesSummary));

            UpdatedAt = DateTime.UtcNow;
        }

        public void RemoveMealItem(Guid id)
        {
            var mealItem = GetMealItemOrFail(id);
            _mealItems.Remove(mealItem);
            UpdatedAt = DateTime.UtcNow;
        }

        public MealItem GetMealItemOrFail(Guid id)
        {
            var product = GetMealItem(id);
            if (product.HasNoValue)
            {
                throw new DomainException(ErrorCodes.MealItemNotFound,
                    $"Meal item with id: '{id}' was not found.");
            }

            return product.Value;
        }

        private Maybe<MealItem> GetMealItem(Guid productId)
            => MealItems.SingleOrDefault(x => x.ProductId == productId);
    }
}