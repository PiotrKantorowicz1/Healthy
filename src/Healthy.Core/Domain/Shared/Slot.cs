using System;
using Healthy.Core.Domain.BaseClasses;

namespace Healthy.Core.Domain.Shared
{
    public class Slot : ValueObject<Slot>
    {
        public int SlotNumber { get; protected set; }
        public int Hour { get; protected set; }

        public Slot(int slotNumber, int hour)
        {
            if (slotNumber == 0)
            {
                throw new ArgumentException("Slot number can not equals 0.", nameof(slotNumber));
            }

            if (hour < 0 || hour > 24)
            {
                throw new ArgumentException("Hour can not be less than 0 and grather than 24.", nameof(hour));
            }

            SlotNumber = slotNumber;
            Hour = hour;
        }

        public static Slot Create(int slotNumber, int hour) => new Slot(slotNumber, hour);

        protected override bool EqualsCore(Slot other)
            => SlotNumber.Equals(other.SlotNumber) && Hour.Equals(other.Hour);

        protected override int GetHashCodeCore()
        {
            var hash = 13;
            hash = (hash * 7) + SlotNumber.GetHashCode();
            hash = (hash * 7) + Hour.GetHashCode();

            return hash;
        }
    }
}