using Healthy.Core.Domain.BaseClasses;

namespace Healthy.Core.Domain.Users.Enumerations
{
    public class Roles : Enumeration
    {
        public static Roles User = new UserRole();
        public static Roles Editor = new EditorRole();
        public static Roles Admin = new AdminRole();
        public static Roles Owner = new OwnerRole();

        public Roles(int id, string name)
            : base(id, name)
        {
        }

        private class UserRole : Roles
        {
            public UserRole() : base(1, "user") { }
        }

        private class EditorRole : Roles
        {
            public EditorRole() : base(2, "editor") { }
        }

        private class AdminRole : Roles
        {
            public AdminRole() : base(3, "admin") { }
        }

        private class OwnerRole : Roles
        {
            public OwnerRole() : base(4, "owner") { }
        }
    }
}
