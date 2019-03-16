using Healthy.Core.Domain.BaseClasses;

namespace Healthy.Core.Domain.Users.Enumerations
{
    public class OneTimeSecuredOperationType : Enumeration
    {
        public static OneTimeSecuredOperationType ResetPassword = new ResetPasswordOperation();
        public static OneTimeSecuredOperationType ActivateAccount = new ActivateAccountOperation();

        public OneTimeSecuredOperationType(int id, string name) 
            : base(id, name)
        {
        }

        private class ResetPasswordOperation : OneTimeSecuredOperationType
        {
            public ResetPasswordOperation() : base(1, "reset_password") { }
        }

        private class ActivateAccountOperation : OneTimeSecuredOperationType
        {
            public ActivateAccountOperation() : base(2, "activate_account") { }
        }
    }
}
