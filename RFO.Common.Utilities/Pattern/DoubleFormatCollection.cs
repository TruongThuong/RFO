
namespace RFO.Common.Utilities.Pattern
{
    /// <summary>
    /// Provides common patterns for double format
    /// </summary>
    public static class DoubleFormatCollection
    {
        /// <summary>
        /// <para>12345.67 -> 12,345.7</para>
        /// <para>12345.4  -> 12,345.4</para>
        /// <para>12345.0  -> 12,345</para>
        /// </summary>
        public const string FORMAT_1 = "0,0.###";
    }
}