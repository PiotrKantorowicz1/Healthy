using System;
using Healthy.Core.Domain.BaseClasses;
using Healthy.Core.Extensions;

namespace Healthy.Core.Domain.Diets.Entities
{
    public class ProductCategory : ValueObject<ProductCategory>
    {
        public Guid Id { get; protected set; }
        public string Name { get; protected set; }

        protected ProductCategory()
        {
        }

        public ProductCategory(Guid id, string name)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Category id can not be empty.", nameof(id));
            }

            if (name.Empty())
            {
                throw new ArgumentException("Category name can not be empty.", nameof(name));
            }

            Id = id;
            Name = name;
        }

        public static ProductCategory Create(Category category)
            => new ProductCategory(category.Id, category.Name);

        protected override bool EqualsCore(ProductCategory other) => Id.Equals(other.Id);

        protected override int GetHashCodeCore() => Id.GetHashCode();
    }
}