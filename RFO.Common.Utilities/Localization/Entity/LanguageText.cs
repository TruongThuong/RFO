
using System.Xml.Serialization;

namespace RFO.Common.Utilities.Localization.Entity
{
    /// <summary>
    /// LanguageText element
    /// </summary>
    public class LanguageText
    {
        /// <summary>
        /// Gets or sets the language code.
        /// </summary>
        /// <value>
        /// The language code.
        /// </value>
        [XmlElement]
        public string LanguageCode { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        [XmlElement]
        public string Value { get; set; }
    }
}