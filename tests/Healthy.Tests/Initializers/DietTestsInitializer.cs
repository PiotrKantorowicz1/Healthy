using System;
using Healthy.Core.Domain.Diets.DomainClasses;
using Healthy.Core.Domain.Shared.DomainClasses;

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
        protected MealItem MealItem;
        protected ProductCategory ProductCategory;
        protected Slot Slot;
        protected Supplementation Supplementation;

        protected void Initialize()
        {
            NutritionValues = new NutritionValues(140, 10, 2, 22, 12);
            Category = new Category(Guid.NewGuid(), "Meats");
            ProductCategory = new ProductCategory(Guid.NewGuid(), "Butters");
            Product = new Product(Guid.NewGuid(), "Mlekovita butter", "Super duper butter futer", 
                NutritionValues, Category);
            Meal = new Meal(Guid.NewGuid(), 2);
            MealItem = new MealItem(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(),  "maslo", 
                32, NutritionValues);
            Day = new Day("Monday", "WorkoutDay", DateTime.UtcNow);
            Slot = new Slot(3, 12);
            DailySupplementation = new DailySupplementation(Guid.NewGuid(), Day);
            Interval = new Interval(new DateTime(2018, 09, 27), DateTime.UtcNow);
            Supplementation = new Supplementation(Guid.NewGuid(), Guid.NewGuid(), Interval);
        }
    }
}