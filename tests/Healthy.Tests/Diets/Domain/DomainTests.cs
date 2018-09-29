using System;
using Healthy.Core.Domain.Diets.Entities;
using Healthy.Core.Exceptions;
using Xunit;
using Healthy.Core.Domain.Users.Entities;
using Healthy.Tests.Initializers;

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
            //arrange
            var category = Category;

            //act
            Action act = () => Category.SetName("");

            //assert
            Assert.Throws<DomainException>(act);
        }

        [Fact]
        public void Set_category_name_should_be_raise_error_when_category_name_is_too_long()
        {
            //arrange
            var category = Category;

            //act
            Action act = () => category.SetName(@"YXNkaW9manNpYXVoZ2Zpb3Nwa29pZHNoZ29wc2tmZG9pa
                HNqYWlnb2thamRzb2lnZmhqcGFqYTlbcGZqYXNkZ2lhc2RqZmdwW2FzamdkOWFpc1tkZ2ZoYXNnZmlh
                c2RmZ2FzaWRnZmg5MGFzcGRvZ2FzZmdrZGE=");

            //assert
            Assert.Throws<DomainException>(act);
        }

        [Fact]
        public void Get_slot_should_be_raise_error_when_this_slot_not_found()
        {
            //arrange
            var slot = Slot;
            var dailySupplementation = DailySupplementation;
            dailySupplementation.AddSlot(slot);

            //act
            Action act = () => dailySupplementation.GetSlotOrFail(500);

            //assert
            Assert.Throws<DomainException>(act);
        }


        [Fact]
        public void Set_day_should_be_raise_error_when_day_is_null()
        {
            //arrange
            var dailySupplementation = DailySupplementation;

            //act
            Action act = () => dailySupplementation.SetDay(null);

            //assert
            Assert.Throws<DomainException>(act);
        }

        [Fact]
        public void Get_meal_should_be_raise_error_when_meal_not_found()
        {
            //arrange
            var dailySupplementation = DailySupplementation;
            var meal = Meal;
            dailySupplementation.AddMeal(meal);

            //act
            Action act = () => dailySupplementation.GetMealOrFail(Guid.Empty);

            //assert
            Assert.Throws<DomainException>(act);
        }
    }
}