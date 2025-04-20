using ECommerce.Application.DTOs.Common;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;

namespace ECommerce.MVC.Helpers
{
    public static class HtmlMediaUrlHelper
    {
        private static MinioSettings Settings(IHtmlHelper htmlHelper)
        {
            var serviceProvider = htmlHelper.ViewContext.HttpContext.RequestServices;
            return serviceProvider.GetRequiredService<IOptions<MinioSettings>>().Value;
        }

        public static string BuildMediaUrl(this IHtmlHelper htmlHelper, string s3Key)
        {
            var settings = Settings(htmlHelper);
            var protocol = settings.UseSSL ? "https" : "http";
            return $"{protocol}://{settings.Endpoint}/{settings.BucketName}/{s3Key}";
        }
    }
}