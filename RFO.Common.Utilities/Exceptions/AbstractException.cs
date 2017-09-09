
using System;

namespace RFO.Common.Utilities.Exceptions
{
    /// <summary>
    /// The abstract exception.
    /// </summary>
    public abstract class AbstractException : Exception
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets ErrParams.
        /// </summary>
        public object[] ErrParams { get; set; }

        /// <summary>
        /// Gets or sets the error code.
        /// </summary>
        /// <value>
        /// The error code.
        /// </value>
        public int ErrCode { get; set; }

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        protected AbstractException(string message)
            : base(message)
        {
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="rootCause">The root cause.</param>
        protected AbstractException(string message, Exception rootCause)
            : base(message, rootCause)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractException"/> class.
        /// </summary>
        /// <param name="errCode">The error code.</param>
        protected AbstractException(int errCode)
        {
            this.ErrCode = errCode;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractException"/> class.
        /// </summary>
        /// <param name="errCode">The error code.</param>
        /// <param name="errParams">The error parameters.</param>
        protected AbstractException(int errCode, params object[] errParams)
        {
            this.ErrCode = errCode;
            this.ErrParams = errParams;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="errCode">The error code.</param>
        protected AbstractException(string message, int errCode)
            : base(message)
        {
            this.ErrCode = errCode;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="errParams">The error parameters.</param>
        protected AbstractException(string message, params object[] errParams)
            : base(message)
        {
            this.ErrParams = errParams;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="errCode">The error code.</param>
        /// <param name="errParams">The error parameters.</param>
        protected AbstractException(string message, int errCode, params object[] errParams)
            : base(message)
        {
            this.ErrCode = errCode;
            this.ErrParams = errParams;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="rootCause">The root cause.</param>
        /// <param name="errCode">The error code.</param>
        /// <param name="errParams">The error parameters.</param>
        protected AbstractException(string message, Exception rootCause, int errCode, params object[] errParams)
            : base(message, rootCause)
        {
            this.ErrCode = errCode;
            ErrParams = errParams;
        }

        #endregion
    }
}