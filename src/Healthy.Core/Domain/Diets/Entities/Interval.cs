using System;
using Healthy.Core.Domain.BaseClasses;

namespace Healthy.Core.Domain.Diets.Entities
{
    public class Interval : ValueObject<Interval>
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public Interval(DateTime startDate, DateTime endDate)
        {
            if (startDate >= endDate)
            {
                throw new ArgumentException("Start date can not be grather than end date.", nameof(startDate));
            }
            StartDate = startDate;
            EndDate = endDate;
        }

        public static Interval Create(DateTime startDate, DateTime endDate)
            => new Interval(startDate, endDate);

        protected override bool EqualsCore(Interval other)
            => StartDate.Equals(other.StartDate) && EndDate.Equals(other.EndDate);

        protected override int GetHashCodeCore()
        {
            var hash = 13;
            hash = (hash * 7) + StartDate.GetHashCode(); 
            hash = (hash * 7) + EndDate.GetHashCode(); 

            return hash;
        }
    }
}