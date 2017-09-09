
using System.Collections.Generic;
using System.Globalization;

namespace RFO.Common.Utilities.Localization
{
    /// <summary>
    /// Provides properties and methods for translation
    /// </summary>
    public interface ITranslationProvider
    {
        /// <summary>
        /// Gets the available languages.
        /// </summary>
        /// <value>The available languages.</value>
        IList<string> AllLanguages { get; }

        /// <summary>
        /// Initialize XML language translator
        /// </summary>
        void Initialize();

        /// <summary>
        /// Translates the specified key.
        /// </summary>
        /// <param name="languageCode">Language code</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        object Translate(string languageCode, string key);
    }
}