namespace Healthy.Core.Domain.Workouts.DomainClasses
{
    public class BodyGroup
    {
        public const string Back = "back";
        public const string Chest = "chest";
        public const string Legs = "legs";

        public static bool IsValid(string boduGroup)
        {
            if (string.IsNullOrWhiteSpace(boduGroup))
            {
                return false;
            }

            boduGroup = boduGroup.ToLowerInvariant();

            return boduGroup == Back || boduGroup == Chest
                                     || boduGroup == Legs;
        }
    }
}