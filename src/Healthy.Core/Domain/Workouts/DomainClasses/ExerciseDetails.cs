using System;
using Healthy.Core.Domain.BaseClasses;

namespace Healthy.Core.Domain.Workouts.DomainClasses
{
    public class ExerciseDetails : ValueObject<ExerciseDetails>
    {
        public double Distance { get; protected set; }
        public int Repeats { get; protected set; }
        public int Series { get; protected set; }
        public int Breaks { get; protected set; }

        protected ExerciseDetails()
        {
        }

        public ExerciseDetails(double distance, int repeats, int series, int breaks)
        {
            if (distance < 0 && distance > 100000)
            {
                throw new ArgumentException("Distance can not be less than 0 and greater than 100000.",
                    nameof(distance));
            }

            if (repeats < 0 && repeats > 250)
            {
                throw new ArgumentException("Repeats can not be less than 0 and greater than 250.", nameof(repeats));
            }

            if (series < 0 && series > 100)
            {
                throw new ArgumentException("Series can not be less than 0 and greater than 100.", nameof(series));
            }

            if (breaks < 0 && breaks > 200)
            {
                throw new ArgumentException("Breaks can not be less than 0 and greater than 100000.", nameof(breaks));
            }

            Distance = distance;
            Repeats = repeats;
            Series = series;
            Breaks = breaks;
        }

        public static ExerciseDetails Create(double distance, int repeats, int series, int breaks)
            => new ExerciseDetails(distance, repeats, series, breaks);

        protected override bool EqualsCore(ExerciseDetails other) => Distance.Equals(other.Distance)
                                                                     && Repeats.Equals(other.Repeats) &&
                                                                     Series.Equals(other.Series) &&
                                                                     Breaks.Equals(other.Breaks);

        protected override int GetHashCodeCore()
        {
            var hash = 13;
            hash = (hash * 7) + Distance.GetHashCode();
            hash = (hash * 7) + Repeats.GetHashCode();
            hash = (hash * 7) + Series.GetHashCode();
            hash = (hash * 7) + Breaks.GetHashCode();

            return hash;
        }
    }
}