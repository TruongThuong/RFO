
namespace RFO.Common.Utilities.Localization.Notification
{
    /// <summary>
    /// Notify posting status to subscribers
    /// </summary>
    public interface ILanguageChangedPublisher
    {
        /// <summary>
        /// Subscribes the specified subscriber.
        /// </summary>
        /// <param name="subscriber">The subscriber.</param>
        void Subscribe(ILanguageChangedSubscriber subscriber);

        /// <summary>
        /// Unsubscribe.
        /// </summary>
        /// <param name="subscriber">The subscriber.</param>
        void UnSubscribe(ILanguageChangedSubscriber subscriber);

        /// <summary>
        /// Notifies the subscribers.
        /// </summary>
        void NotifyLanguageChangedToSubscribers();
    }
}