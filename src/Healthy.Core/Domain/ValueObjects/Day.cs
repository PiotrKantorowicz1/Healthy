using System;
using Healthy.Core.Domain.BaseClasses;
using Healthy.Core.Extensions;

namespace Healthy.Core.Domain.Shared.ValueObjects
{
    public class Day : ValueObject<Day>
    {
        public string Name { get; protected set; }
        public string DayType { get; protected set; }
        public DateTime Date { get; protected set; }

        public Day(string name, string dayType, DateTime date)
        {
            if (name.Length > 20 || name.Empty())
            {
                throw new ArgumentException("Day name can not be longer than 20 chars and can not be empty.",
                    nameof(name));
            }

            if (dayType == string.Empty)
            {
                throw new ArgumentException("Day type can not be empty.", nameof(dayType));
            }

            if (date == null)
            {
                throw new ArgumentException("Date can not be null.", nameof(Date));
            }

            Name = name;
            DayType = dayType;
            Date = date;
        }

        public static Day Create(string name, string dayType, DateTime date)
            => new Day(name, dayType, date);

        protected override bool EqualsCore(Day other) => Name.Equals(other.Name)
                                                         && Date.Equals(other.Date) && DayType.Equals(other.DayType);

        protected override int GetHashCodeCore()
        {
            var hash = 13;
            hash = (hash * 7) + Name.GetHashCode();
            hash = (hash * 7) + Date.GetHashCode();
            hash = (hash * 7) + DayType.GetHashCode();

            return hash;
        }
    }
}