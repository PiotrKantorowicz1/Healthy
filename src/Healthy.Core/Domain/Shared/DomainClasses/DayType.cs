namespace Healthy.Core.Domain.Shared.DomainClasses
{
    public class DayType
    {
        public const string Training = "traning";
        public const string NonTraining = "nontraining";

        public static bool IsValid(string dayType)
        {
            if (string.IsNullOrWhiteSpace(dayType))
            {
                return false;
            }

            dayType = dayType.ToLowerInvariant();

            return dayType == Training || dayType == NonTraining;
        }
    }
}