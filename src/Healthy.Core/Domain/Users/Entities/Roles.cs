namespace Healthy.Core.Domain.Users.Entities
{
    public static class Roles
    {
        public const string User = "user";
        public const string Editor = "editor";
        public const string Admin = "admin";
        public const string Owner = "owner";

        public static bool IsValid(string role)
        {
            if (string.IsNullOrWhiteSpace(role))
            {
                return false;
            }
            role = role.ToLowerInvariant();

            return role == User || role == Admin || role == Editor 
                || role == Owner;
        }
    }
}
