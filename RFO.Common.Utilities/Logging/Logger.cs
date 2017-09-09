
using System;
using System.IO;
using log4net;
using log4net.Config;

namespace RFO.Common.Utilities.Logging
{
    /// <summary>
    /// Implement ILogger interface.
    /// </summary>
    internal sealed class Logger : ILogger
    {
        #region Entries

        /// <summary>
        /// Logger for writing log messages to target.
        /// </summary>
        private readonly ILog logger;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="loggerName">Name of  Logger</param>
        /// <param name="configFile">Configuration file</param>
        internal Logger(string loggerName, string configFile)
        {
            InitConfig(configFile);
            logger = LogManager.GetLogger(loggerName);
        }

        /// <summary>
        /// Constructor with default configuration file.
        /// </summary>
        /// <param name="loggerName">Name of  Logger</param>
        internal Logger(string loggerName)
        {
            InitConfig(null);
            logger = LogManager.GetLogger(loggerName);
        }

        #endregion

        #region Initialize

        public void SetContext(string contextName, string contextValue)
        {
            ThreadContext.Stacks[contextName].Push(contextValue);
        }

        /// <summary>
        /// Initialize Logger configuration.
        /// </summary>
        /// <param name="xmlConfigFile">XML Xonfiguration file</param>
        internal static void InitConfig(string xmlConfigFile)
        {
            try
            {
                if (string.IsNullOrEmpty(xmlConfigFile))
                {
                    XmlConfigurator.Configure();
                }
                else
                {
                    var configInfo = new FileInfo(xmlConfigFile);
                    if (configInfo.Length > 0) // Throw exceprion if file does not exist.
                    {
                        XmlConfigurator.Configure(configInfo);
                    }
                }
            }
            catch
            {
                throw new ApplicationException("Fail to initialize Logger");
            }
        }

        #endregion

        #region Debug Level

        /// <summary>
        /// Check if DEBUG leve is enable.
        /// </summary>
        /// <returns></returns>
        public bool IsDebugEnabled()
        {
            return logger.IsDebugEnabled;
        }

        /// <summary>
        /// Log a message object with the Debug level.
        /// </summary>
        /// <param name="message">The message object to log</param>
        public void Debug(object message)
        {
            if (logger.IsDebugEnabled)
            {
                logger.Debug(message);
            }
        }

        /// <summary>
        /// Log a message object with the Debug level including
        /// the stack trace of the Exception passed as a parameter.
        /// </summary>
        /// <param name="message">The message object to log</param>
        /// <param name="exception">The exception to log, including its stack trace</param>
        public void Debug(object message, Exception exception)
        {
            if (logger.IsDebugEnabled)
            {
                logger.Debug(message, exception);
            }
        }

        /// <summary>
        /// Logs a formatted message string with the Debug level.
        /// </summary>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="arg0">An Object to format</param>
        public void DebugFormat(string format, object arg0)
        {
            if (logger.IsDebugEnabled)
            {
                logger.DebugFormat(format, arg0);
            }
        }

        /// <summary>
        /// Logs a formatted message string with the Debug level.
        /// </summary>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="arg0">The first object to format</param>
        /// <param name="arg1">The second object to format</param>
        public void DebugFormat(string format, object arg0, object arg1)
        {
            if (logger.IsDebugEnabled)
            {
                logger.DebugFormat(format, arg0, arg1);
            }
        }

        /// <summary>
        /// Logs a formatted message string with the Debug level.
        /// </summary>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="arg0">The first object to format</param>
        /// <param name="arg1">The second object to format</param>
        /// <param name="arg2">The third object to format</param>
        public void DebugFormat(string format, object arg0, object arg1, object arg2)
        {
            if (logger.IsDebugEnabled)
            {
                logger.DebugFormat(format, arg0, arg1, arg2);
            }
        }

        /// <summary>
        /// Logs a formatted message string with the Debug level.
        /// </summary>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="args">An Object array containing zero or more objects to format</param>
        public void DebugFormat(string format, object[] args)
        {
            if (logger.IsDebugEnabled)
            {
                logger.DebugFormat(format, args);
            }
        }

        /// <summary>
        /// Logs a formatted message string with the Debug level.
        /// </summary>
        /// <param name="provider">An IFormatProvider that supplies culture-specific formatting information</param>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="args">An Object array containing zero or more objects to format</param>
        public void DebugFormat(IFormatProvider provider, string format, object[] args)
        {
            if (logger.IsDebugEnabled)
            {
                logger.DebugFormat(provider, format, args);
            }
        }

        #endregion

        #region Info Level

        /// <summary>
        /// Log a message object with the Info level.
        /// </summary>
        /// <param name="message">The message object to log</param>
        public void Info(object message)
        {
            if (logger.IsInfoEnabled)
            {
                logger.Info(message);
            }
        }

        /// <summary>
        /// Log a message object with the Info level including
        /// the stack trace of the Exception passed as a parameter.
        /// </summary>
        /// <param name="message">The message object to log</param>
        /// <param name="exception">The exception to log, including its stack trace</param>
        public void Info(object message, Exception exception)
        {
            if (logger.IsInfoEnabled)
            {
                logger.Info(message, exception);
            }
        }

        /// <summary>
        /// Logs a formatted message string with the Info level.
        /// </summary>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="arg0">An Object to format</param>
        public void InfoFormat(string format, object arg0)
        {
            if (logger.IsInfoEnabled)
            {
                logger.InfoFormat(format, arg0);
            }
        }

        /// <summary>
        /// Logs a formatted message string with the Info level.
        /// </summary>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="arg0">The first object to format</param>
        /// <param name="arg1">The second object to format</param>
        public void InfoFormat(string format, object arg0, object arg1)
        {
            if (logger.IsInfoEnabled)
            {
                logger.InfoFormat(format, arg0, arg1);
            }
        }

        /// <summary>
        /// Logs a formatted message string with the Info level.
        /// </summary>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="arg0">The first object to format</param>
        /// <param name="arg1">The second object to format</param>
        /// <param name="arg2">The third object to format</param>
        public void InfoFormat(string format, object arg0, object arg1, object arg2)
        {
            if (logger.IsInfoEnabled)
            {
                logger.InfoFormat(format, arg0, arg1, arg2);
            }
        }

        /// <summary>
        /// Logs a formatted message string with the Info level.
        /// </summary>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="args">An Object array containing zero or more objects to format</param>
        public void InfoFormat(string format, object[] args)
        {
            if (logger.IsInfoEnabled)
            {
                logger.InfoFormat(format, args);
            }
        }

        /// <summary>
        /// Logs a formatted message string with the Info level.
        /// </summary>
        /// <param name="provider">An IFormatProvider that supplies culture-specific formatting information</param>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="args">An Object array containing zero or more objects to format</param>
        public void InfoFormat(IFormatProvider provider, string format, object[] args)
        {
            if (logger.IsInfoEnabled)
            {
                logger.InfoFormat(provider, format, args);
            }
        }

        #endregion

        #region Warn Level

        /// <summary>
        /// Log a message object with the Warn level.
        /// </summary>
        /// <param name="message">The message object to log</param>
        public void Warn(object message)
        {
            if (logger.IsWarnEnabled)
            {
                logger.Warn(message);
            }
        }

        /// <summary>
        /// Log a message object with the Warn level including
        /// the stack trace of the Exception passed as a parameter.
        /// </summary>
        /// <param name="message">The message object to log</param>
        /// <param name="exception">The exception to log, including its stack trace</param>
        public void Warn(object message, Exception exception)
        {
            if (logger.IsWarnEnabled)
            {
                logger.Warn(message, exception);
            }
        }

        /// <summary>
        /// Logs a formatted message string with the Warn level.
        /// </summary>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="arg0">An Object to fformat</param>
        public void WarnFormat(string format, object arg0)
        {
            if (logger.IsWarnEnabled)
            {
                logger.WarnFormat(format, arg0);
            }
        }

        /// <summary>
        /// Logs a formatted message string with the Warn level.
        /// </summary>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="arg0">The first object to format</param>
        /// <param name="arg1">The second object to format</param>
        public void WarnFormat(string format, object arg0, object arg1)
        {
            if (logger.IsWarnEnabled)
            {
                logger.WarnFormat(format, arg0, arg1);
            }
        }

        /// <summary>
        /// Logs a formatted message string with the Warn level.
        /// </summary>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="arg0">The first object to format</param>
        /// <param name="arg1">The second object to format</param>
        /// <param name="arg2">The third object to format</param>
        public void WarnFormat(string format, object arg0, object arg1, object arg2)
        {
            if (logger.IsWarnEnabled)
            {
                logger.WarnFormat(format, arg0, arg1, arg2);
            }
        }

        /// <summary>
        /// Logs a formatted message string with the Warn level.
        /// </summary>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="args">An Object array containing zero or more objects to format</param>
        public void WarnFormat(string format, object[] args)
        {
            if (logger.IsWarnEnabled)
            {
                logger.WarnFormat(format, args);
            }
        }

        /// <summary>
        /// Logs a formatted message string with the Warn level.
        /// </summary>
        /// <param name="provider">An IFormatProvider that supplies culture-specific formatting information</param>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="args">An Object array containing zero or more objects to format</param>
        public void WarnFormat(IFormatProvider provider, string format, object[] args)
        {
            if (logger.IsWarnEnabled)
            {
                logger.WarnFormat(provider, format, args);
            }
        }

        #endregion

        #region Error Level

        /// <summary>
        /// Log a message object with the Error level.
        /// </summary>
        /// <param name="message">The message object to log</param>
        public void Error(object message)
        {
            if (logger.IsErrorEnabled)
            {
                logger.Error(message);
            }
        }

        /// <summary>
        /// Log a message object with the Error level including
        /// the stack trace of the Exception passed as a parameter.
        /// </summary>
        /// <param name="message">The message object to log</param>
        /// <param name="exception">The exception to log, including its stack trace</param>
        public void Error(object message, Exception exception)
        {
            if (logger.IsErrorEnabled)
            {
                logger.Error(message, exception);
            }
        }

        /// <summary>
        /// Logs a formatted message string with the Error level.
        /// </summary>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="arg0">An Object to format</param>
        public void ErrorFormat(string format, object arg0)
        {
            if (logger.IsErrorEnabled)
            {
                logger.ErrorFormat(format, arg0);
            }
        }

        /// <summary>
        /// Logs a formatted message string with the Error level.
        /// </summary>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="arg0">The first object to format</param>
        /// <param name="arg1">The second object to format</param>
        public void ErrorFormat(string format, object arg0, object arg1)
        {
            if (logger.IsErrorEnabled)
            {
                logger.ErrorFormat(format, arg0, arg1);
            }
        }

        /// <summary>
        /// Logs a formatted message string with the Error level.
        /// </summary>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="arg0">The first object to format</param>
        /// <param name="arg1">The second object to format</param>
        /// <param name="arg2">The third object to format</param>
        public void ErrorFormat(string format, object arg0, object arg1, object arg2)
        {
            if (logger.IsErrorEnabled)
            {
                logger.ErrorFormat(format, arg0, arg1, arg2);
            }
        }

        /// <summary>
        /// Logs a formatted message string with the Error level.
        /// </summary>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="args">An Object array containing zero or more objects to format</param>
        public void ErrorFormat(string format, object[] args)
        {
            if (logger.IsErrorEnabled)
            {
                logger.ErrorFormat(format, args);
            }
        }

        /// <summary>
        /// Logs a formatted message string with the Error level.
        /// </summary>
        /// <param name="provider">An IFormatProvider that supplies culture-specific formatting information</param>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="args">An Object array containing zero or more objects to format</param>
        public void ErrorFormat(IFormatProvider provider, string format, object[] args)
        {
            if (logger.IsErrorEnabled)
            {
                logger.ErrorFormat(provider, format, args);
            }
        }

        /// <summary>
        /// Logs a formatted message string with the Error level.
        /// </summary>
        /// <param name="msg">A String containing error description</param>
        /// <param name="ex">An exception object containing error</param>
        public void ErrorFormat(string msg, Exception ex)
        {
            // Null ex param
            if (ex == null)
            {
                Error(msg);
                return;
            }

            logger.Error(msg, ex);
        }

        #endregion

        #region Fatal Level

        /// <summary>
        /// Log a message object with the Fatal level.
        /// </summary>
        /// <param name="message">The message object to log</param>
        public void Fatal(object message)
        {
            if (logger.IsFatalEnabled)
            {
                logger.Fatal(message);
            }
        }

        /// <summary>
        /// Log a message object with the Fatal level including
        /// the stack trace of the Exception passed as a parameter.
        /// </summary>
        /// <param name="message">The message object to log</param>
        /// <param name="exception">The exception to log, including its stack trace</param>
        public void Fatal(object message, Exception exception)
        {
            if (logger.IsFatalEnabled)
            {
                logger.Fatal(message, exception);
            }
        }

        /// <summary>
        /// Logs a formatted message string with the Fatal level.
        /// </summary>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="arg0">An Object to format</param>
        public void FatalFormat(string format, object arg0)
        {
            if (logger.IsFatalEnabled)
            {
                logger.FatalFormat(format, arg0);
            }
        }

        /// <summary>
        /// Logs a formatted message string with the Fatal level.
        /// </summary>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="arg0">The first object to format</param>
        /// <param name="arg1">The second object to format</param>
        public void FatalFormat(string format, object arg0, object arg1)
        {
            if (logger.IsFatalEnabled)
            {
                logger.FatalFormat(format, arg0, arg1);
            }
        }

        /// <summary>
        /// Logs a formatted message string with the Fatal level.
        /// </summary>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="arg0">The first object to format</param>
        /// <param name="arg1">The second object to format</param>
        /// <param name="arg2">The third object to format</param>
        public void FatalFormat(string format, object arg0, object arg1, object arg2)
        {
            if (logger.IsFatalEnabled)
            {
                logger.FatalFormat(format, arg0, arg1, arg2);
            }
        }

        /// <summary>
        /// Logs a formatted message string with the Fatal level.
        /// </summary>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="args">An Object array containing zero or more objects to format</param>
        public void FatalFormat(string format, object[] args)
        {
            if (logger.IsFatalEnabled)
            {
                logger.FatalFormat(format, args);
            }
        }

        /// <summary>
        /// Logs a formatted message string with the Fatal level.
        /// </summary>
        /// <param name="provider">An IFormatProvider that supplies culture-specific formatting information</param>
        /// <param name="format">A String containing zero or more format items</param>
        /// <param name="args">An Object array containing zero or more objects to format</param>
        public void FatalFormat(IFormatProvider provider, string format, object[] args)
        {
            if (logger.IsFatalEnabled)
            {
                logger.FatalFormat(provider, format, args);
            }
        }

        #endregion
    }
}