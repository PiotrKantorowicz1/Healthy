using System;
using System.Collections.Generic;
using Healthy.Core.Domain.BaseClasses;

namespace Healthy.Core.Domain.Diets.Entities
{
    public class DailySupplementation : Entity
    {
        public ISet<Meal> _meals = new HashSet<Meal>();
        public ISet<Slot> _slots = new HashSet<Slot>();
        public Guid SuplementationId { get; set; }
        public Day Day { get; set; }
        public bool Success { get; set; }

        public IEnumerable<Slot> Slots
        {
            get { return _slots; }
            protected set { _slots = new HashSet<Slot>(value); }
        }

        public IEnumerable<Meal> Meals
        {
            get { return _meals; }
            protected set { _meals = new HashSet<Meal>(value); }
        }
    }
}