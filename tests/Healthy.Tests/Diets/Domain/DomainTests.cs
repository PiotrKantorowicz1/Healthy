using System;
using Healthy.Core.Domain.Diets.Entities;
using Healthy.Core.Exceptions;
using It = Machine.Specifications.It;
using Machine.Specifications;
using Xunit;

namespace Healthy.Tests.Diets.Domain
{
    public class DomainTests
    {
        public abstract class DomainTestsBase
        {
            protected static Category Category;
            protected static Day Day;
            protected static DailySupplementation DailySupplementation;
            protected static Interval Interval;
            protected static Meal Meal;
            protected static NutritionValues NutritionValues;
            protected static Product Product;
            protected static ProductCategory ProductCategory;
            protected static Slot Slot;
            protected static Supplementation Supplementation;
            protected static Exception Exception;

            protected static void Initialize()
            {
                NutritionValues = new NutritionValues(140, 10, 2, 22, 12);
                Category = new Category(Guid.NewGuid(), "Meats");
                ProductCategory = new ProductCategory(Guid.NewGuid(), "Butters");
                Product = new Product(Guid.NewGuid(), "Mlekovita butter", "Super duper butter futer", 3,
                    NutritionValues, Category);
                Meal = new Meal(Guid.NewGuid(), 2);
                Meal.AddProduct(Product, Category);
                Day = new Day("Monday", "WorkoutDay", DateTime.UtcNow);
                Slot = new Slot(3, 12);
                DailySupplementation = new DailySupplementation(Guid.NewGuid(), Day);
                DailySupplementation.AddMeal(Meal);
                DailySupplementation.AddSlot(Slot);
                Interval = new Interval(new DateTime(2018, 09, 27), DateTime.UtcNow);
                Supplementation = new Supplementation(Guid.NewGuid(), Guid.NewGuid(), Interval);
                Supplementation.AddDailySupplementation(DailySupplementation);
            }
        }

        [Subject("Category")]
        public class When_exception_should_be_ivoked_by_empty_name : DomainTestsBase
        {
            Establish context = () => Initialize();

            Because of = () => Exception = Catch.Exception(() => Category.SetName(""));

            It should_throw_domain_exception = () =>
            {
                Exception.ShouldBeOfExactType<DomainException>();
            };
        }

        [Subject("Category")]
        public class When_exception_should_be_ivoked_by_to_many_characters_in_name : DomainTestsBase
        {
            Establish context = () => Initialize();

            Because of = () => Exception = Catch.Exception(() => Category.SetName(@"SWRlYWx5IHNhIGphayBnd2l
            hemR5IC0gbmllIG1vem5hIGljaCBvc2lhZ25hYywgYWxlIG1vsKJDJSSHBJDkj23em5hIHNpZSBuaW1pIGtpZXJvd2FjLg0K"));

            It should_throw_domain_exception = () =>
            {
                Exception.ShouldBeOfExactType<DomainException>();
            };
        }


    }
}