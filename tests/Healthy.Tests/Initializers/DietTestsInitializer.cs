using System;
using Healthy.Core.Domain.Diets.Entities;
using Healthy.Core.Exceptions;
using Healthy.Core.Domain.Users.Entities;

namespace Healthy.Tests.Initializers
{
    public abstract class DietTestsInitializer
    {
        protected Category Category;
        protected Day Day;
        protected DailySupplementation DailySupplementation;
        protected Interval Interval;
        protected Meal Meal;
        protected NutritionValues NutritionValues;
        protected Product Product;
        protected ProductCategory ProductCategory;
        protected Slot Slot;
        protected Supplementation Supplementation;

        protected void Initialize()
        {
            NutritionValues = new NutritionValues(140, 10, 2, 22, 12);
            Category = new Category(Guid.NewGuid(), "Meats");
            ProductCategory = new ProductCategory(Guid.NewGuid(), "Butters");
            Product = new Product(Guid.NewGuid(), "Mlekovita butter", "Super duper butter futer", 3,
                NutritionValues, Category);
            Meal = new Meal(Guid.NewGuid(), 2);
            Day = new Day("Monday", "WorkoutDay", DateTime.UtcNow);
            Slot = new Slot(3, 12);
            DailySupplementation = new DailySupplementation(Guid.NewGuid(), Day);
            Interval = new Interval(new DateTime(2018, 09, 27), DateTime.UtcNow);
            Supplementation = new Supplementation(Guid.NewGuid(), Guid.NewGuid(), Interval);
        }
    }
}