
using System;

namespace RFO.Common.Utilities.Exceptions
{
    /// <summary>
    /// The database exception
    /// </summary>
    public class DatabaseException : AbstractException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public DatabaseException(string message)
            : base(message)
        {
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="rootCause">The root cause.</param>
        public DatabaseException(string message, Exception rootCause)
            : base(message, rootCause)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseException"/> class.
        /// </summary>
        /// <param name="errCode">The error code.</param>
        public DatabaseException(int errCode)
            : base(errCode)
        {
            this.ErrCode = errCode;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseException"/> class.
        /// </summary>
        /// <param name="errCode">The error code.</param>
        /// <param name="errParams">The error parameters.</param>
        public DatabaseException(int errCode, params object[] errParams)
            : base(errCode, errParams)
        {
            this.ErrCode = errCode;
            this.ErrParams = errParams;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="errCode">The error code.</param>
        public DatabaseException(string message, int errCode)
            : base(message)
        {
            this.ErrCode = errCode;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="errParams">The error parameters.</param>
        public DatabaseException(string message, params object[] errParams)
            : base(message)
        {
            this.ErrParams = errParams;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="errCode">The error code.</param>
        /// <param name="errParams">The error parameters.</param>
        public DatabaseException(string message, int errCode, params object[] errParams)
            : base(message)
        {
            this.ErrCode = errCode;
            this.ErrParams = errParams;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="rootCause">The root cause.</param>
        /// <param name="errCode">The error code.</param>
        /// <param name="errParams">The error parameters.</param>
        public DatabaseException(string message, Exception rootCause, int errCode, params object[] errParams)
            : base(message, rootCause)
        {
            this.ErrCode = errCode;
            ErrParams = errParams;
        }
    }

    /// <summary>
    /// Database error code
    /// </summary>
    public enum DatabaseErrorCode
    {
        /// <summary>
        /// The inserting error code
        /// </summary>
        Insert = 0,

        /// <summary>
        /// The updating error code
        /// </summary>
        Update,

        /// <summary>
        /// The deleting error code
        /// </summary>
        Delete,

        /// <summary>
        /// The selecting error code
        /// </summary>
        Load,

        /// <summary>
        /// The upload image
        /// </summary>
        UploadImage
    }
}