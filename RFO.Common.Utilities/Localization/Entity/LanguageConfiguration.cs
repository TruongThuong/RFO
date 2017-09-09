
using System.Xml.Serialization;

namespace RFO.Common.Utilities.Localization.Entity
{
    /// <summary>
    /// LanguageConfiguration element
    /// </summary>
    public class LanguageConfiguration
    {
        /// <summary>
        /// Gets or sets the language entries.
        /// </summary>
        /// <value>
        /// The language entries.
        /// </value>
        [XmlArray]
        [XmlArrayItem(typeof (LanguageEntry))]
        public LanguageEntry[] LanguageEntries { get; set; }
    }
}