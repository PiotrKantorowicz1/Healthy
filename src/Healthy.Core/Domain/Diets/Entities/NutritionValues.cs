using System;
using Healthy.Core.Domain.BaseClasses;

namespace Healthy.Core.Domain.Diets.Entities
{
    public class NutritionValues : ValueObject<NutritionValues>
    {
        public double EnergyValue { get; set; }
        public double Fats { get; set; }
        public double Carbo { get; set; }
        public double Sugars { get; set; }
        public double Protein { get; set; }

        public NutritionValues(double energyValue, double fats, double carbo, double sugars, double protein)
        {
            if (EnergyValue < 0)
            {
                throw new ArgumentException("EnergyValue value must be greather than 0.", nameof(EnergyValue));
            }
            if (Fats < 0)
            {
                throw new ArgumentException("Fats value must be greather than 0.", nameof(Fats));
            }
            if (Carbo < 0)
            {
                throw new ArgumentException("Carbo value must be greather than 0.", nameof(Carbo));
            }
            if (Sugars < 0)
            {
                throw new ArgumentException("Sugars value nmust be greather than 0.", nameof(Sugars));
            }
            if (Protein < 0)
            {
                throw new ArgumentException("Protein value must be greather than 0.", nameof(Protein));
            }

            EnergyValue = energyValue;
            Fats = fats;
            Carbo = carbo;
            Sugars = sugars;
            Protein = protein;
        }

         public static NutritionValues Create(double energyValue, double fats, 
            double carbo, double sugars, double protein)
            => new NutritionValues(energyValue, fats, carbo, sugars, protein);

        protected override bool EqualsCore(NutritionValues other) 
            => EnergyValue.Equals(other.EnergyValue) && Fats.Equals(other.Fats) && Carbo.Equals(other.Carbo)
                && Sugars.Equals(other.Sugars) && Protein.Equals(other.Protein); 

        protected override int GetHashCodeCore() 
        {
            var hash = 13;
            hash = (hash * 7) + EnergyValue.GetHashCode();
            hash = (hash * 7) + Fats.GetHashCode();
            hash = (hash * 7) + Carbo.GetHashCode();
            hash = (hash * 7) + Sugars.GetHashCode();
            hash = (hash * 7) + Protein.GetHashCode();

            return hash;
        }
    }
}