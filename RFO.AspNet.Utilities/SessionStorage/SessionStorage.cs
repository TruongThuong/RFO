
using System.ComponentModel.Composition;
using System.Web;

namespace RFO.AspNet.Utilities.SessionStorage
{
    /// <summary>
    /// The class is used to store a value in Session
    /// </summary>
    public class SessionStorage
    {
        #region Fields

        /// <summary>
        /// The synchronous lock object
        /// </summary>
        private static readonly object _syncLock = new object();

        #endregion

        #region Implementation of ISessionStorage

        /// <summary>
        /// Deletes the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        public static void Delete(string key)
        {
            lock (_syncLock)
            {
                HttpContext.Current.Session.Remove(key);
            }
        }

        /// <summary>
        /// Determines whether the specified key is exist.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public static bool IsExist(string key)
        {
            bool result;
            
            lock (_syncLock)
            {
                result = HttpContext.Current.Session[key] != null;
            }

            return result;
        }

        /// <summary>
        /// Gets the specified key.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        public static T Get<T>(string key)
        {
            T result;

            lock (_syncLock)
            {
                result = (T)HttpContext.Current.Session[key];
            }

            return result;
        }

        /// <summary>
        /// Stores the specified key.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public static void Store<T>(string key, T value)
        {
            lock (_syncLock)
            {
                HttpContext.Current.Session[key] = value;
            }
        }

        #endregion
    }
}