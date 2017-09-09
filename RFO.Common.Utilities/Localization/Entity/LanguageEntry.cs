
using System.Xml.Serialization;

namespace RFO.Common.Utilities.Localization.Entity
{
    /// <summary>
    /// LanguageEntry element
    /// </summary>
    public class LanguageEntry
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [XmlElement]
        public string ID { get; set; }

        /// <summary>
        /// Gets or sets the default text.
        /// </summary>
        /// <value>
        /// The default text.
        /// </value>
        [XmlElement]
        public string DefaultText { get; set; }

        /// <summary>
        /// Gets or sets the language texts.
        /// </summary>
        /// <value>
        /// The language texts.
        /// </value>
        [XmlArray]
        [XmlArrayItem(typeof (LanguageText))]
        public LanguageText[] LanguageTexts { get; set; }
    }
}