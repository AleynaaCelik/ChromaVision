using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ChromaVision.Core.Exceptions
{
    public static class StringExtensions
    {
        public static bool IsValidHexColor(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return false;

            // Remove # if present
            if (input.StartsWith("#"))
                input = input.Substring(1);

            // Check if it's a valid hex color (3 or 6 characters)
            return Regex.IsMatch(input, "^([0-9A-Fa-f]{3}|[0-9A-Fa-f]{6})$");
        }

        public static string EnsureHexFormat(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return "#000000";

            input = input.Trim();

            if (!input.StartsWith("#"))
                input = "#" + input;

            // If it's in #RGB format, convert to #RRGGBB
            if (input.Length == 4)
            {
                input = "#" + input[1] + input[1] + input[2] + input[2] + input[3] + input[3];
            }

            // Return default black if invalid
            if (!Regex.IsMatch(input.Substring(1), "^([0-9A-Fa-f]{6})$"))
                return "#000000";

            return input.ToUpper();
        }
    }
}
