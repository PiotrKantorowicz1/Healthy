using System;
using Healthy.Core.Domain.Diets.Entities;
using Healthy.Core.Exceptions;
using Xunit;
using Healthy.Core.Domain.Users.Entities;
using Healthy.Tests.Initializers;
using System.Linq;
using Healthy.Core;
using FluentAssertions;

namespace Healthy.Tests.Diets.Domain
{
    public class DomainTests : DietTestsInitializer
    {
        public DomainTests()
        {
            base.Initialize();
        }

        [Fact]
        public void Set_category_name_should_be_raise_error_when_category_name_is_not_set()
        {
            Action act = () => Category.SetName(string.Empty);

            var ex = Assert.Throws<DomainException>(act);
            Assert.Equal(ErrorCodes.InvalidCategory, ex.Code);
        }

        [Fact]
        public void Set_category_name_should_be_raise_error_when_category_name_is_too_long()
        {
            Action act = () => Category.SetName(@"YXNkaW9manNpYXVoZ2Zpb3Nwa29pZHNoZ29wc2tmZG9pa
                HNqYWlnb2thamRzb2lnZmhqcGFqYTlbcGZqYXNkZ2lhc2RqZmdwW2FzamdkOWFpc1tkZ2ZoYXNnZmlh
                c2RmZ2FzaWRnZmg5MGFzcGRvZ2FzZmdrZGE=");

            var ex = Assert.Throws<DomainException>(act);
            Assert.Equal(ErrorCodes.InvalidCategory, ex.Code);
        }

        [Fact]
        public void Get_slot_should_be_raise_error_when_this_slot_not_found()
        {
            Action act = () => DailySupplementation.GetSlotOrFail(500);

            var ex = Assert.Throws<DomainException>(act);
            Assert.Equal(ErrorCodes.SlotNotFound, ex.Code);
        }


        [Fact]
        public void Set_day_should_be_raise_error_when_day_is_null()
        {
            Action act = () => DailySupplementation.SetDay(null);

            var ex = Assert.Throws<DomainException>(act);
            Assert.Equal(ErrorCodes.InvalidDay, ex.Code);
        }

        [Fact]
        public void Get_meal_should_be_raise_error_when_meal_not_found()
        {
            Action act = () => DailySupplementation.GetMealOrFail(Guid.NewGuid());

            var ex = Assert.Throws<DomainException>(act);
            Assert.Equal(ErrorCodes.MealNotFound, ex.Code);
        }

        [Fact]
        public void Add_slot_should_be_raise_error_when_daily_supplementation_have_too_many_slots()
        {
            var dailySupplementation = DailySupplementation;

            for (int i = 0; i <= 12; i++)
            {
                dailySupplementation.AddSlot(new Slot(1, 5 + i));
            }

            Action act = () => dailySupplementation.AddSlot(Slot);

            var ex = Assert.Throws<DomainException>(act);
            Assert.Equal(ErrorCodes.ToManySlots, ex.Code);
        }

        [Fact]
        public void Set_meal_number_should_be_raise_error_when_meal_number_is_less_than_zero()
        {
            Action act = () => Meal.SetMealNumber(-1);

            var ex = Assert.Throws<DomainException>(act);
            Assert.Equal(ErrorCodes.InvalidMealNumber, ex.Code);
        }

        [Fact]
        public void Set_meal_number_should_be_raise_error_when_meal_number_is_grather_than_12()
        {
            Action act = () => Meal.SetMealNumber(13);

            var ex = Assert.Throws<DomainException>(act);
            Assert.Equal(ErrorCodes.InvalidMealNumber, ex.Code);
        }

        [Fact]
        public void Get_Product_should_be_raise_error_when_product_not_found()
        {
            Action act = () => Meal.GetProductOrFail(Guid.NewGuid());

            var ex = Assert.Throws<DomainException>(act);
            Assert.Equal(ErrorCodes.ProductNotFound, ex.Code);
        }

        [Fact]
        public void Set_name_should_be_raise_error_when_name_is_empty()
        {
            Action act = () => Product.SetName(string.Empty);

            var ex = Assert.Throws<DomainException>(act);
            Assert.Equal(ErrorCodes.NameNotProvided, ex.Code);
        }

        [Fact]
        public void Set_namet_should_be_raise_error_when_name_is_too_long()
        {
            Action act = () => Product.SetName(@"YXNkaW9manNpYXVoZ2Zpb3Nwa29pZHNoZ29wc2tmZG9pa
                HNqYWlnb2thamRzb2lnZmhqcGFqYTlbcGZqYXNkZ2lhc2RqZmdwW2FzamdkOWFpc1tkZ2ZoYXNnZmlh
                c2RmZ2FzaWRnZmg5MGFzcGRvZ2FzZmdrZGE=");

            var ex = Assert.Throws<DomainException>(act);
            Assert.Equal(ErrorCodes.InvalidProductName, ex.Code);
        }


        [Fact]
        public void Set_description_should_be_raise_error_when_description_is_empty()
        {
            Action act = () => Product.SetDescription(string.Empty);

            var ex = Assert.Throws<DomainException>(act);
            Assert.Equal(ErrorCodes.DescriptionNotProvided, ex.Code);
        }

        [Fact]
        public void Set_description_should_be_raise_error_when_description_is_too_long()
        {
            Action act = () => Product.SetDescription(@"YXNkaW9manNpYXVoZ2Zpb3Nwa29pZHNoZ29wc2tmZG9pa
                HNqYWlnb2thamRzb2lnZmhqcGFqYTlbcGZqYXNkZ2lhc2RqZmdwW2FzamdkOWFpc1tkZ2ZoYXNnZmlh
                c2RmZ2FzaWRnZmg5MGFzcGRvZ2FzZmdrZGE=XNkaW9manNpYXVoZ2Zpb3Nwa29pZHNoZ29wc2tmZG9pa
                HNqYWlnb2thamRzb2lnZmhqcGFqYTlbcGZqYXNkZ2lhc2RqZmdwW2FzamdkOWFpc1tkZ2ZoYXNnZmlh
                c2RmZ2FzaWRnZmg5MGFzcGRvZ2FzZmdrZGE=XNkaW9manNpYXVoZ2Zpb3Nwa29pZHNoZ29wc2tmZG9pa
                HNqYWlnb2thamRzb2lnZmhqcGFqYTlbcGZqYXNkZ2lhc2RqZmdwW2FzamdkOWFpc1tkZ2ZoYXNnZmlh
                c2RmZ2FzaWRnZmg5MGFzcGRvZ2FzZmdrZGE=");

            var ex = Assert.Throws<DomainException>(act);
            Assert.Equal(ErrorCodes.InvalidDescription, ex.Code);
        }

        [Fact]
        public void Set_quantity_should_be_raise_error_when_quantity_is_less_than_zero()
        {
            Action act = () => Product.SetQuantity(-1);

            var ex = Assert.Throws<DomainException>(act);
            Assert.Equal(ErrorCodes.InvalidQuantity, ex.Code);
        }

        [Fact]
        public void Set_quantity_should_be_raise_error_when_quantity_is_greather_than_500()
        {
            Action act = () => Product.SetQuantity(501);

            var ex = Assert.Throws<DomainException>(act);
            Assert.Equal(ErrorCodes.InvalidQuantity, ex.Code);
        }

        [Fact]
        public void Set_nutrition_values_should_be_raise_error_when_nutrition_values_is_null()
        {
            Action act = () => Product.SetNutritionValue(null);

            var ex = Assert.Throws<DomainException>(act);
            Assert.Equal(ErrorCodes.NutritionValuesNotProvided, ex.Code);
        }

        [Fact]
        public void Set_category_should_be_raise_error_when_category_is_null()
        {
            Action act = () => Product.SetCategory(null);

            var ex = Assert.Throws<DomainException>(act);
            Assert.Equal(ErrorCodes.CategoryNotProvided, ex.Code);
        }

        [Fact]
        public void Set_interval_should_be_raise_error_when_interval_is_null()
        {
            Action act = () => Supplementation.SetInterval(null);

            var ex = Assert.Throws<DomainException>(act);
            Assert.Equal(ErrorCodes.IntervalNotProvided, ex.Code);
        }

        [Fact]
        public void Get_daily_supplementation_should_be_raise_error_when_daily_supplementation_not_found()
        {
            Action act = () => Supplementation.GetDailySupplementationOrFail(Guid.NewGuid());

            var ex = Assert.Throws<DomainException>(act);
            Assert.Equal(ErrorCodes.DailySupplementationNotFound, ex.Code);
        }
    }
}