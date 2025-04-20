using System.Globalization;

namespace ECommerce.Application.Common.Utilities.Exceptions
{
    public class DatabaseException : Exception
    {
        public DatabaseException() : base() { }
        public DatabaseException(string message) : base(message) { }
        public DatabaseException(string message, params object[] args)
            : base(String.Format(CultureInfo.CurrentCulture, message, args))
        {
        }
    }
}