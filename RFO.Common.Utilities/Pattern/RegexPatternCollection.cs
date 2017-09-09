
namespace RFO.Common.Utilities.Pattern
{
    /// <summary>
    /// Provides common patterns for validation
    /// </summary>
    public static class RegexPatternCollection
    {
        /// <summary>
        /// USERNAME (only lower case characters and numeric)
        /// </summary>
        public const string USERNAME = @"^[a-z0-9_-]{3,16}$";

        /// <summary>
        /// PASSWORD (only contains letter [a-z] digits[0-9], special characters(@#$%&))
        /// </summary>
        public const string PASSWORD = @"^[a-z0-9_-]{6,18}$";

        /// <summary>
        /// Hex value
        /// </summary>
        public const string HEX_VALUE = @"^#?([a-f0-9]{6}|[a-f0-9]{3})$";

        /// <summary>
        /// SLUG (example: le-sy-tuan)
        /// </summary>
        public const string SLUG = @"^[a-z0-9-]+$";

        /// <summary>
        /// EMAIL
        /// </summary>
        public const string EMAIL = @"^([a-z0-9_\.-]+)@([\da-z\.-]+)\.([a-z\.]{2,6})$";

        /// <summary>
        /// URL (with or without http)
        /// </summary>
        public const string URL = @"^(https?:\/\/)?([\da-z\.-]+)\.([a-z\.]{2,6})([\/\w \.-]*)*\/?$";

        /// <summary>
        /// IP address
        /// </summary>
        public const string IP_ADDRESS = @"^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$";

        /// <summary>
        /// HTML tag
        /// </summary>
        public const string HTML_TAG = @"^<([a-z]+)([^<]+)*(?:>(.*)<\/\1>|\s+\/>)$";
    }
}