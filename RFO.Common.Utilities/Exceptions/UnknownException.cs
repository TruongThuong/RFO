
using System;

namespace RFO.Common.Utilities.Exceptions
{
    /// <summary>
    /// The unknown exception.
    /// </summary>
    public class UnknownException : AbstractException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnknownException" /> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public UnknownException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnknownException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="rootCause">The root cause.</param>
        public UnknownException(string message, Exception rootCause)
            : base(message, rootCause)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnknownException" /> class.
        /// </summary>
        /// <param name="errCode">The error code.</param>
        public UnknownException(int errCode)
            : base(errCode)
        {
            this.ErrCode = errCode;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnknownException" /> class.
        /// </summary>
        /// <param name="errCode">The error code.</param>
        /// <param name="errParams">The error parameters.</param>
        public UnknownException(int errCode, params object[] errParams)
            : base(errCode, errParams)
        {
            this.ErrCode = errCode;
            this.ErrParams = errParams;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnknownException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="errCode">The error code.</param>
        public UnknownException(string message, int errCode)
            : base(message)
        {
            this.ErrCode = errCode;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnknownException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="errParams">The error parameters.</param>
        public UnknownException(string message, params object[] errParams)
            : base(message)
        {
            this.ErrParams = errParams;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnknownException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="errCode">The error code.</param>
        /// <param name="errParams">The error parameters.</param>
        public UnknownException(string message, int errCode, params object[] errParams)
            : base(message)
        {
            this.ErrCode = errCode;
            this.ErrParams = errParams;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UnknownException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="rootCause">The root cause.</param>
        /// <param name="errCode">The error code.</param>
        /// <param name="errParams">The error parameters.</param>
        public UnknownException(string message, Exception rootCause, int errCode, params object[] errParams)
            : base(message, rootCause)
        {
            this.ErrCode = errCode;
            this.ErrParams = errParams;
        }
    }
}