using System.Globalization;
namespace ECommerce.Application.Common.Utilities.Exceptions
{
    public class UnAuthorizedException : Exception
    {
        public UnAuthorizedException() : base() { }
        public UnAuthorizedException(string message) : base(message) { }
        public UnAuthorizedException(string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}
