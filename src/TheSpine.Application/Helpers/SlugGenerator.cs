using System.Text.RegularExpressions;

namespace TheSpine.Application.Helpers
{
    public static class SlugGenerator
    {
        public static string GenerateSlug(string route)
        {
            if (string.IsNullOrWhiteSpace(route))
            {
                return string.Empty;
            }

            // Remove non-letter characters (replace with hyphens)
            route = Regex.Replace(route, @"[^a-zA-Z0-9\s-]", string.Empty);

            // Replace whitespace with hyphens
            route = Regex.Replace(route, @"\s+", "-").Trim();

            // Remove leading and trailing hyphens
            route = route.Trim('-');

            // Convert to lowercase
            route = route.ToLower();

            return route;
        }
    }
}
