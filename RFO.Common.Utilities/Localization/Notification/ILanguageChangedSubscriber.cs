
namespace RFO.Common.Utilities.Localization.Notification
{
    /// <summary>
    /// Implements this interface so that the classes enable to know the posting status
    /// </summary>
    public interface ILanguageChangedSubscriber
    {
        /// <summary>
        /// Receives the language changed.
        /// </summary>
        void ReceiveLanguageChanged();
    }
}