
using System.Collections.Generic;
using System.ComponentModel.Composition;
using RFO.Common.Utilities.Localization.Notification;
using RFO.Common.Utilities.Logging;

namespace RFO.Common.Utilities.Localization
{
    /// <summary>
    /// The class is used to manage translation
    /// </summary>
    [Export(typeof(ITranslationManager))]
    [Export(typeof(ILanguageChangedPublisher))]
    public class TranslationManager : ITranslationManager, ILanguageChangedPublisher
    {
        #region Fields

        /// <summary>
        /// The logger instance
        /// </summary>
        public static readonly ILogger Logger = LoggerManager.GetLogger(typeof(TranslationManager).Name);

        /// <summary>
        /// Provides properties and methods for translation
        /// </summary>
        private ITranslationProvider _translationProvider;

        /// <summary>
        /// The subscribers
        /// </summary>
        private readonly List<ILanguageChangedSubscriber> _subscribers = new List<ILanguageChangedSubscriber>();

        #endregion

        #region Implementation of ITranslationManager

        private string _currentLanguage = "vi";

        /// <summary>
        /// Get current language
        /// </summary>
        public string CurrentLanguage
        {
            get { return this._currentLanguage; }
            set
            {
                if (value != this._currentLanguage)
                {
                    this._currentLanguage = value;
                    this.NotifyLanguageChangedToSubscribers();
                }
            }
        }

        /// <summary>
        /// Gets all languages.
        /// </summary>
        /// <value>
        /// All languages.
        /// </value>
        public IList<string> AllLanguages
        {
            get
            {
                return this._translationProvider != null ?
                    this._translationProvider.AllLanguages : new List<string>();
            }
        }

        /// <summary>
        /// Sets the translation provider.
        /// </summary>
        /// <param name="translationProvider">The translation provider.</param>
        public void SetTranslationProvider(ITranslationProvider translationProvider)
        {
            this._translationProvider = translationProvider;
        }

        /// <summary>
        /// Execute language translate
        /// </summary>
        /// <param name="key">From key to message has been translate</param>
        /// <returns>Translated message</returns>
        public object Translate(string key)
        {
            object result = null;

            if (this._translationProvider != null)
            {
                if (key != null)
                {
                    result = string.Format("!{0}!", key);
                    var translatedValue = this._translationProvider.Translate(this._currentLanguage, key);
                    if (translatedValue != null)
                    {
                        result = translatedValue;
                    }
                }
            }
            else
            {
                Logger.Error("The translation provider is not exist");
            }

            return result;
        }

        /// <summary>
        /// Execute language translate
        /// </summary>
        /// <param name="key">From key to message has been translate</param>
        /// <param name="args">The parameter list for translation</param>
        /// <returns>Translated message</returns>
        public object Translate(string key, params object[] args)
        {
            object result = null;

            if (this._translationProvider != null)
            {
                if (key != null)
                {
                    result = string.Format("!{0}!", key);

                    var translatedValue = this._translationProvider.Translate(this._currentLanguage, key);
                    if (translatedValue != null)
                    {
                        result = string.Format((string) translatedValue, args);
                    }
                }
            }
            else
            {
                Logger.Error("The translation provider is not exist");
            }

            return result;
        }

        #endregion

        #region Implementation of IForumPosterPublisher

        /// <summary>
        /// Subscribes the specified subscriber.
        /// </summary>
        /// <param name="subscriber">The subscriber.</param>
        public void Subscribe(ILanguageChangedSubscriber subscriber)
        {
            this._subscribers.Add(subscriber);
        }

        /// <summary>
        /// Uns the subscribe.
        /// </summary>
        /// <param name="subscriber">The subscriber.</param>
        public void UnSubscribe(ILanguageChangedSubscriber subscriber)
        {
            var index = this._subscribers.IndexOf(subscriber);
            if (index != -1)
            {
                this._subscribers.Remove(subscriber);
            }
        }

        /// <summary>
        /// Notifies the subscribers.
        /// </summary>
        public void NotifyLanguageChangedToSubscribers()
        {
            foreach (var subscriber in this._subscribers)
            {
                subscriber.ReceiveLanguageChanged();
            }
        }

        #endregion
    }
}