using System;
using System.Collections.Generic;
using System.Linq;
using Healthy.Core.Domain.BaseClasses;
using Healthy.Core.Exceptions;
using Healthy.Core.Types;

namespace Healthy.Core.Domain.Diets.Entities
{
    public class DailySupplementation : Entity, ITimestampable
    {
        private ISet<Meal> _meals = new HashSet<Meal>();
        private ISet<Slot> _slots = new HashSet<Slot>();
        public Day Day { get; protected set; }
        public string State { get; protected set; }
        public DateTime? UpdatedAt { get; protected set; }
        public DateTime CreatedAt { get; protected set; }

        public IEnumerable<Slot> Slots
        {
            get => _slots;
            protected set => _slots = new HashSet<Slot>(value);
        }

        public IEnumerable<Meal> Meals
        {
            get => _meals;
            protected set => _meals = new HashSet<Meal>(value);
        }

        protected DailySupplementation()
        {
        }

        public DailySupplementation(Guid id, Day day)
        {
            Id = id;
            SetDay(day);
            State = "Pending";
            UpdatedAt = DateTime.UtcNow;
            CreatedAt = DateTime.UtcNow;
        }

        public void SetDay(Day day)
        {
            if (day == null)
            {
                throw new DomainException(ErrorCodes.InvalidDay,
                    "day can not be null");
            }

            Day = day;
            UpdatedAt = DateTime.UtcNow;
        }

        public void AddSlot(Slot slot)
        {
            if (_slots.Count > 12)
            {
                throw new DomainException(ErrorCodes.ToManySlots,
                    $"Limit of 12 supplementation slots was reached.");
            }

            _slots.Add(new Slot(slot.SlotNumber, slot.Hour));
            UpdatedAt = DateTime.UtcNow;
        }

        public void RemoveSlot(int slotNumber)
        {
            var slot = GetSlotOrFail(slotNumber);
            _slots.Remove(slot);
            UpdatedAt = DateTime.UtcNow;
        }

        public void AddMeal(Meal meal)
        {
            _meals.Add(new Meal(meal.Id, meal.MealNumber));
            UpdatedAt = DateTime.UtcNow;
        }

        public void RemoveMeal(Guid id)
        {
            var meal = GetMealOrFail(id);
            _meals.Remove(meal);
            UpdatedAt = DateTime.UtcNow;
        }

        public Meal GetMealOrFail(Guid id)
        {
            var meal = GetMeal(id);
            if (meal.HasNoValue)
            {
                throw new DomainException(ErrorCodes.MealNotFound,
                    $"Meal with id: '{id}' was not found.");
            }

            return meal.Value;
        }

        public Slot GetSlotOrFail(int slotNumber)
        {
            var slot = GetSlot(slotNumber);
            if (slot.HasNoValue)
            {
                throw new DomainException(ErrorCodes.SlotNotFound,
                    $"Slot with slot number : '{slotNumber}' was not found.");
            }

            return slot.Value;
        }

        private Maybe<Meal> GetMeal(Guid id)
            => Meals.SingleOrDefault(x => x.Id == id);

        private Maybe<Slot> GetSlot(int slotNumber)
            => Slots.SingleOrDefault(x => x.SlotNumber == slotNumber);
    }
}