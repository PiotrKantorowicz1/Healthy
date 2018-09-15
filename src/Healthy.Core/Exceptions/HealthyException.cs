using System;

namespace Healthy.Core.Exceptions
{
    public abstract class HealthyException : Exception
    {
        public string Code { get; }

        protected HealthyException()
        {
        }

        protected HealthyException(string code)
        {
            Code = code;
        }

        protected HealthyException(string message, params object[] args) : this(string.Empty, message, args)
        {
        }

        protected HealthyException(string code, string message, params object[] args) : this(null, code, message, args)
        {
        }

        protected HealthyException(Exception innerException, string message, params object[] args)
            : this(innerException, string.Empty, message, args)
        {
        }

        protected HealthyException(Exception innerException, string code, string message, params object[] args)
            : base(string.Format(message, args), innerException)
        {
            Code = code;
        }
    }
}