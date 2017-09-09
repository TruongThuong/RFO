
using System;

namespace RFO.Common.Utilities.Logging
{
    /// <summary>
    /// The ILogger interface is used by 's components to log messages
    /// into the log4net framework.
    /// </summary>
    public interface ILogger
    {
        #region Context

        /// <summary>
        /// Set context for logger
        /// </summary>
        void SetContext(string contextName, string contextValue);

        #endregion

        #region Debug Level

        /// <summary>
        /// Check if DEBUG leve is enable.
        /// </summary>
        /// <returns></returns>
        bool IsDebugEnabled();

        /// <summary>
        /// Log a message object with the Debug level.
        /// </summary>
        /// <param name="message">The message object to log</param>
        void Debug(object message);

        /// <summary>
        /// Log a message object with the Debug level including
        /// the stack trace of the Exception passed as a parameter.
        /// </summary>
        /// <param name="message">The message object to log</param>
        /// <param name="exception">The exception to log, including its stack trace</param>
        void Debug(object message, Exception exception);

        /// <summary>
        /// Logs a formatted message string with the Debug level.
        /// </summary>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="arg0">An Object to format</param>
        void DebugFormat(string format, object arg0);

        /// <summary>
        /// Logs a formatted message string with the Debug level.
        /// </summary>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="arg0">The first object to format</param>
        /// <param name="arg1">The second object to format</param>
        void DebugFormat(string format, object arg0, object arg1);

        /// <summary>
        /// Logs a formatted message string with the Debug level.
        /// </summary>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="arg0">The first object to format</param>
        /// <param name="arg1">The second object to format</param>
        /// <param name="arg2">The third object to format</param>
        void DebugFormat(string format, object arg0, object arg1, object arg2);

        /// <summary>
        /// Logs a formatted message string with the Debug level.
        /// </summary>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="args">An Object array containing zero or more objects to format</param>
        void DebugFormat(string format, object[] args);

        /// <summary>
        /// Logs a formatted message string with the Debug level.
        /// </summary>
        /// <param name="provider">An IFormatProvider that supplies culture-specific formatting information</param>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="args">An Object array containing zero or more objects to format</param>
        void DebugFormat(IFormatProvider provider, string format, object[] args);

        #endregion

        #region Info Level

        /// <summary>
        /// Log a message object with the Info level.
        /// </summary>
        /// <param name="message">The message object to log</param>
        void Info(object message);

        /// <summary>
        /// Log a message object with the Info level including
        /// the stack trace of the Exception passed as a parameter.
        /// </summary>
        /// <param name="message">The message object to log</param>
        /// <param name="exception">The exception to log, including its stack trace</param>
        void Info(object message, Exception exception);

        /// <summary>
        /// Logs a formatted message string with the Info level.
        /// </summary>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="arg0">An object to format</param>
        void InfoFormat(string format, object arg0);

        /// <summary>
        /// Logs a formatted message string with the Info level.
        /// </summary>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="arg0">The first object to format</param>
        /// <param name="arg1">The second object to format</param>
        void InfoFormat(string format, object arg0, object arg1);

        /// <summary>
        /// Logs a formatted message string with the Info level.
        /// </summary>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="arg0">The first object to format</param>
        /// <param name="arg1">The second object to format</param>
        /// <param name="arg2">The third object to format</param>
        void InfoFormat(string format, object arg0, object arg1, object arg2);

        /// <summary>
        /// Logs a formatted message string with the Info level.
        /// </summary>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="args">An Object array containing zero or more objects to format</param>
        void InfoFormat(string format, object[] args);

        /// <summary>
        /// Logs a formatted message string with the Info level.
        /// </summary>
        /// <param name="provider">An IFormatProvider that supplies culture-specific formatting information</param>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="args">An Object array containing zero or more objects to format</param>
        void InfoFormat(IFormatProvider provider, string format, object[] args);

        #endregion

        #region Warn Level

        /// <summary>
        /// Log a message object with the Warn level.
        /// </summary>
        /// <param name="message">The message object to log</param>
        void Warn(object message);

        /// <summary>
        /// Log a message object with the Warn level including
        /// the stack trace of the Exception passed as a parameter.
        /// </summary>
        /// <param name="message">The message object to log</param>
        /// <param name="exception">The exception to log, including its stack trace</param>
        void Warn(object message, Exception exception);

        /// <summary>
        /// Logs a formatted message string with the Warn level.
        /// </summary>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="arg0">An Object to format</param>
        void WarnFormat(string format, object arg0);

        /// <summary>
        /// Logs a formatted message string with the Warn level.
        /// </summary>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="arg0">The first object to format</param>
        /// <param name="arg1">The second object to format</param>
        void WarnFormat(string format, object arg0, object arg1);

        /// <summary>
        /// Logs a formatted message string with the Warn level.
        /// </summary>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="arg0">The first object to format</param>
        /// <param name="arg1">The second object to format</param>
        /// <param name="arg2">The third object to format</param>
        void WarnFormat(string format, object arg0, object arg1, object arg2);

        /// <summary>
        /// Logs a formatted message string with the Warn level.
        /// </summary>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="args">An Object array containing zero or more objects to format</param>
        void WarnFormat(string format, object[] args);

        /// <summary>
        /// Logs a formatted message string with the Warn level.
        /// </summary>
        /// <param name="provider">An IFormatProvider that supplies culture-specific formatting information</param>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="args">An Object array containing zero or more objects to format</param>
        void WarnFormat(IFormatProvider provider, string format, object[] args);

        #endregion

        #region Error Level

        /// <summary>
        /// Log a message object with the Error level.
        /// </summary>
        /// <param name="message">The message object to log</param>
        void Error(object message);

        /// <summary>
        /// Log a message object with the Error level including
        /// the stack trace of the Exception passed as a parameter.
        /// </summary>
        /// <param name="message">The message object to log</param>
        /// <param name="exception">The exception to log, including its stack trace</param>
        void Error(object message, Exception exception);

        /// <summary>
        /// Logs a formatted message string with the Error level.
        /// </summary>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="arg0">An Object to format</param>
        void ErrorFormat(string format, object arg0);

        /// <summary>
        /// Logs a formatted message string with the Error level.
        /// </summary>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="arg0">The first object to format</param>
        /// <param name="arg1">The second object to format</param>
        void ErrorFormat(string format, object arg0, object arg1);

        /// <summary>
        /// Logs a formatted message string with the Error level.
        /// </summary>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="arg0">The first object to format</param>
        /// <param name="arg1">The second object to format</param>
        /// <param name="arg2">The third object to format</param>
        void ErrorFormat(string format, object arg0, object arg1, object arg2);

        /// <summary>
        /// Logs a formatted message string with the Error level.
        /// </summary>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="args">An Object array containing zero or more objects to format</param>
        void ErrorFormat(string format, object[] args);

        /// <summary>
        /// Logs a formatted message string with the Error level.
        /// </summary>
        /// <param name="provider">An IFormatProvider that supplies culture-specific formatting information</param>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="args">An Object array containing zero or more objects to format</param>
        void ErrorFormat(IFormatProvider provider, string format, object[] args);

        /// <summary>
        /// Logs a formatted message string with the Error level.
        /// </summary>
        /// <param name="msg">A String containing error description</param>
        /// <param name="ex">An exception object containing error</param>
        void ErrorFormat(string msg, Exception ex);

        #endregion

        #region Fatal Level

        /// <summary>
        /// Log a message object with the Fatal level.
        /// </summary>
        /// <param name="message">The message object to log</param>
        void Fatal(object message);

        /// <summary>
        /// Log a message object with the Fatal level including
        /// the stack trace of the Exception passed as a parameter.
        /// </summary>
        /// <param name="message">The message object to log</param>
        /// <param name="exception">The exception to log, including its stack trace</param>
        void Fatal(object message, Exception exception);

        /// <summary>
        /// Logs a formatted message string with the Fatal level.
        /// </summary>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="arg0">An Object to format</param>
        void FatalFormat(string format, object arg0);

        /// <summary>
        /// Logs a formatted message string with the Fatal level.
        /// </summary>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="arg0">The first object to format</param>
        /// <param name="arg1">The second object to format</param>
        void FatalFormat(string format, object arg0, object arg1);

        /// <summary>
        /// Logs a formatted message string with the Fatal level.
        /// </summary>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="arg0">The first object to format</param>
        /// <param name="arg1">The second object to format</param>
        /// <param name="arg2">The third object to format</param>
        void FatalFormat(string format, object arg0, object arg1, object arg2);

        /// <summary>
        /// Logs a formatted message string with the Fatal level.
        /// </summary>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="args">An Object array containing zero or more objects to format</param>
        void FatalFormat(string format, object[] args);

        /// <summary>
        /// Logs a formatted message string with the Fatal level.
        /// </summary>
        /// <param name="provider">An IFormatProvider that supplies culture-specific formatting information</param>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="args">An Object array containing zero or more objects to format</param>
        void FatalFormat(IFormatProvider provider, string format, object[] args);

        #endregion
    }
}