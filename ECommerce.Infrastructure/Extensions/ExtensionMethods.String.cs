using Microsoft.AspNetCore.Mvc.Rendering;

namespace ECommerce.Infrastructure.Extensions
{
    public static partial class ExtensionMethods
    { 
        public static string CombineWithSpace(this string str, string other)
        {
            if (string.IsNullOrWhiteSpace(str))
                return other;
            if (string.IsNullOrWhiteSpace(other))
                return str;

            return string.Concat(str, " ", other);
        }

        // public static void GenerateSlug(this string name, out string slug)
        // { 
        //     //slug = 
        // }
    }
}