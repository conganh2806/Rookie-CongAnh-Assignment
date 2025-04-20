namespace ECommerce.MVC.Helpers
{
    public static class NetworkHelper
    {
        public static string GetIpAddress(HttpContext context)
        {
            var ip = context.Request.Headers["X-Forwarded-For"].FirstOrDefault();

            if (string.IsNullOrEmpty(ip))
            {
                ip = context.Connection?.RemoteIpAddress?.ToString();
            }

            return string.IsNullOrEmpty(ip) ? "127.0.0.1" : ip;
        }
    }
}