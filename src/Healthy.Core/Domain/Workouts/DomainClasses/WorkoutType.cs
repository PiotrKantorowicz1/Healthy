namespace Healthy.Core.Domain.Workouts.DomainClasses
{
    public class WorkoutType
    {
        public const string Jogging = "Jogging";
        public const string Gym = "Gym";
        public const string Other = "Other";

        public static bool IsValid(string workoutType)
        {
            if (string.IsNullOrWhiteSpace(workoutType))
            {
                return false;
            }

            workoutType = workoutType.ToLowerInvariant();

            return workoutType == Jogging || workoutType == Gym
                                          || workoutType == Other;
        }
    }
}