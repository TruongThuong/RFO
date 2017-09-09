
using System;

namespace RFO.Common.Utilities.Exceptions
{
    /// <summary>
    /// The settign config exception.
    /// This exception is used for failed case when system is reading configuration.
    /// </summary>
    public class ConfigurationException : AbstractException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationException" /> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public ConfigurationException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="rootCause">The root cause.</param>
        public ConfigurationException(string message, Exception rootCause)
            : base(message, rootCause)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationException" /> class.
        /// </summary>
        /// <param name="errCode">The error code.</param>
        public ConfigurationException(int errCode)
            : base(errCode)
        {
            this.ErrCode = errCode;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationException" /> class.
        /// </summary>
        /// <param name="errCode">The error code.</param>
        /// <param name="errParams">The error parameters.</param>
        public ConfigurationException(int errCode, params object[] errParams)
            : base(errCode, errParams)
        {
            this.ErrCode = errCode;
            this.ErrParams = errParams;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="errCode">The error code.</param>
        public ConfigurationException(string message, int errCode)
            : base(message)
        {
            this.ErrCode = errCode;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="errParams">The error parameters.</param>
        public ConfigurationException(string message, params object[] errParams)
            : base(message)
        {
            this.ErrParams = errParams;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="errCode">The error code.</param>
        /// <param name="errParams">The error parameters.</param>
        public ConfigurationException(string message, int errCode, params object[] errParams)
            : base(message)
        {
            this.ErrCode = errCode;
            this.ErrParams = errParams;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="rootCause">The root cause.</param>
        /// <param name="errCode">The error code.</param>
        /// <param name="errParams">The error parameters.</param>
        public ConfigurationException(string message, Exception rootCause, int errCode, params object[] errParams)
            : base(message, rootCause)
        {
            this.ErrCode = errCode;
            this.ErrParams = errParams;
        }
    }

    /// <summary>
    /// Database error code
    /// </summary>
    public enum ConfigurationErrorCode
    {
        /// <summary>
        /// The updating error code
        /// </summary>
        Update = 0,

        /// <summary>
        /// The loading error code
        /// </summary>
        Load
    }
}