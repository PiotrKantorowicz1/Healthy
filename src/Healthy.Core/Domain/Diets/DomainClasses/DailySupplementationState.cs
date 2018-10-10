namespace Healthy.Core.Domain.Diets.DomainClasses
{
    public class DailySupplementationState
    {
        public const string Success = "success";
        public const string Failed = "failed";
        public const string Pending = "pending";
        public const string None = "none";

        public static bool IsValid(string dailySupplementationState)
        {
            if (string.IsNullOrWhiteSpace(dailySupplementationState))
            {
                return false;
            }

            dailySupplementationState = dailySupplementationState.ToLowerInvariant();

            return dailySupplementationState == Success || dailySupplementationState == Failed ||
                   dailySupplementationState == Pending || dailySupplementationState == None;
        }
    }
}