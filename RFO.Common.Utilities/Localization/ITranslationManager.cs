
using System.Collections.Generic;
using System.Globalization;

namespace RFO.Common.Utilities.Localization
{
    /// <summary>
    /// Provide APIs to support the localization
    /// </summary>
    public interface ITranslationManager
    {
        /// <summary>
        /// Gets or sets the current language.
        /// </summary>
        /// <value>
        /// The current language.
        /// </value>
        string CurrentLanguage { get; set; }

        /// <summary>
        /// Gets all languages.
        /// </summary>
        /// <value>
        /// All languages.
        /// </value>
        IList<string> AllLanguages { get; }

        /// <summary>
        /// Sets the translation provider.
        /// </summary>
        /// <param name="translationProvider">The translation provider.</param>
        void SetTranslationProvider(ITranslationProvider translationProvider);

        /// <summary>
        /// Execute language translate
        /// </summary>
        /// <param name="key">From key to message has been translate</param>
        /// <returns>Translated message</returns>
        object Translate(string key);

        /// <summary>
        /// Execute language translate
        /// </summary>
        /// <param name="key">From key to message has been translate</param>
        /// <param name="args">The parameter list for translation</param>
        /// <returns>Translated message</returns>
        object Translate(string key, params object[] args);
    }
}