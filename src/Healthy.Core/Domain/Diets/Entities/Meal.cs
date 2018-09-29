using System;
using System.Collections.Generic;
using System.Linq;
using Healthy.Core.Domain.BaseClasses;
using Healthy.Core.Exceptions;
using Healthy.Core.Types;

namespace Healthy.Core.Domain.Diets.Entities
{
    public class Meal : Entity, ITimestampable
    {
        public ISet<Product> _products = new HashSet<Product>();
        public int MealNumber { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime CreatedAt { get; set; }

        public IEnumerable<Product> Products
        {
            get { return _products; }
            protected set { _products = new HashSet<Product>(value); }
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
                    "Meal number can not be less than 0 and grather than 12.");
            }
            MealNumber = mealNumber;
            UpdatedAt = DateTime.UtcNow;
        }

        public void AddProduct(Product product, Category category)
        {
            _products.Add(new Product(product.Id, product.Name, product.Description, 
                product.Quantity, product.NutritionsValues, category));

            UpdatedAt = DateTime.UtcNow;
        }

        public void RemoveProduct(Guid id)
        {
            var product = GetProductOrFail(id);
            _products.Remove(product);
            UpdatedAt = DateTime.UtcNow;
        }

        public Product GetProductOrFail(Guid id)
        {
            var product = GetProduct(id);
            if (product.HasNoValue)
            {
                throw new DomainException(ErrorCodes.ProductNotFound,
                    $"Meal product with id: '{id}' was not found.");
            }
            return product.Value;
        }

        public Maybe<Product> GetProduct(Guid id)
            => Products.SingleOrDefault(x => x.Id == id);
    }
}