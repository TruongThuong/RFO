
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading;
using Microsoft.Practices.ServiceLocation;
using RFO.Common.Utilities.Localization.Entity;
using RFO.Common.Utilities.XMLHelper;

namespace RFO.Common.Utilities.Localization
{
    /// <summary>
    /// The class is used to translate message defined in XML file
    /// </summary>
    public class XmlTranslationProvider : ITranslationProvider
    {
        #region Fields

        /// <summary>
        /// The distionary that is used to store messages of all supported language
        /// Key: language code
        /// Value: messages dictionary
        /// </summary>
        private readonly Dictionary<string, StringDictionary> _languageDictionary =
            new Dictionary<string, StringDictionary>();

        /// <summary>
        /// The _languageFilePath
        /// </summary>
        private readonly string _languageFilePath;

        /// <summary>
        /// The _allLanguages
        /// </summary>
        private IList<string> _allLanguages;

        /// <summary>
        /// Gets the current language code.
        /// </summary>
        /// <value>
        /// The current language code.
        /// </value>
        private static string CurrentLanguageCode
        {
            get { return Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName; }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the available languages.
        /// </summary>
        /// <value>The available languages.</value>
        public IList<string> AllLanguages
        {
            get { return this._allLanguages; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Create an instane of XmlTranslationProvider with specified Data file
        /// </summary>
        /// <param name="languageFilePath">File contains language messages</param>
        public XmlTranslationProvider(string languageFilePath)
        {
            this._languageFilePath = languageFilePath;
        }

        #endregion

        #region Implementation of ITranslationProvider

        /// <summary>
        /// Initialize XML language translator
        /// </summary>
        public void Initialize()
        {
            // Create language model
            var languageConfiguration = XmlHelper<LanguageConfiguration>.LoadFromFile(this._languageFilePath);
            var entries = languageConfiguration.LanguageEntries;
            var id2Languages = entries.ToDictionary(x => x.ID, x => x.LanguageTexts);

            foreach (var languageEntry in id2Languages)
            {
                var msgId = languageEntry.Key;
                foreach (var languageText in languageEntry.Value)
                {
                    var language = languageText.LanguageCode;
                    var msg = languageText.Value;
                    StringDictionary dict;
                    if (!this._languageDictionary.TryGetValue(language, out dict))
                    {
                        dict = new StringDictionary();
                        this._languageDictionary[language] = dict;
                    }

                    dict[msgId] = msg;
                }
            }

            this._allLanguages = this._languageDictionary.Keys.Select(x => x).ToList();
        }

        /// <summary>
        /// Translates the specified key.
        /// </summary>
        /// <param name="languageCode">Language code</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public object Translate(string languageCode, string key)
        {
            return this._languageDictionary[languageCode][key];
        }

        #endregion
    }
}