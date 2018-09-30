using System;
using Healthy.Core.Domain.BaseClasses;

namespace Healthy.Core.Domain.Diets.Entities
{
    public class NutritionValues : ValueObject<NutritionValues>
    {
        public double EnergyValue { get; protected set; }
        public double Fats { get; protected set; }
        public double Carbohydrates { get; protected set; }
        public double Sugars { get; protected set; }
        public double Protein { get; protected set; }

        public NutritionValues(double energyValue, double fats, double carbohydrates, double sugars, double protein)
        {
            if (EnergyValue < 0)
            {
                throw new ArgumentException("EnergyValue value must be greater than 0.", nameof(EnergyValue));
            }

            if (Fats < 0)
            {
                throw new ArgumentException("Fats value must be greater than 0.", nameof(Fats));
            }

            if (Carbohydrates < 0)
            {
                throw new ArgumentException("Carbohydrates value must be greater than 0.", nameof(Carbohydrates));
            }

            if (Sugars < 0)
            {
                throw new ArgumentException("Sugars value must be greater than 0.", nameof(Sugars));
            }

            if (Protein < 0)
            {
                throw new ArgumentException("Protein value must be greater than 0.", nameof(Protein));
            }

            EnergyValue = energyValue;
            Fats = fats;
            Carbohydrates = carbohydrates;
            Sugars = sugars;
            Protein = protein;
        }

        public static NutritionValues Create(double energyValue, double fats,
            double carbohydrates, double sugars, double protein)
            => new NutritionValues(energyValue, fats, carbohydrates, sugars, protein);

        protected override bool EqualsCore(NutritionValues other)
            => EnergyValue.Equals(other.EnergyValue) && Fats.Equals(other.Fats)
                                                     && Carbohydrates.Equals(other.Carbohydrates) &&
                                                     Sugars.Equals(other.Sugars)
                                                     && Protein.Equals(other.Protein);

        protected override int GetHashCodeCore()
        {
            var hash = 13;
            hash = (hash * 7) + EnergyValue.GetHashCode();
            hash = (hash * 7) + Fats.GetHashCode();
            hash = (hash * 7) + Carbohydrates.GetHashCode();
            hash = (hash * 7) + Sugars.GetHashCode();
            hash = (hash * 7) + Protein.GetHashCode();

            return hash;
        }
    }
}