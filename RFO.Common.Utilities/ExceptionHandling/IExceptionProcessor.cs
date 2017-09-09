
using System;

namespace RFO.Common.Utilities.ExceptionHandling
{
    /// <summary>
    /// Interface for exception processor
    /// </summary>
    public interface IExceptionProcessor
    {
        /// <summary>
        /// Gets the exception message.
        /// </summary>
        /// <param name="ex">The ex.</param>
        /// <returns></returns>
        string GetExceptionMessage(Exception ex);
    }
}