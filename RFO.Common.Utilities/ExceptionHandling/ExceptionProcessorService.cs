
using System;
using System.ComponentModel.Composition;
using Microsoft.Practices.ServiceLocation;

namespace RFO.Common.Utilities.ExceptionHandling
{
    /// <summary>
    /// Provides APIs to handle VFP exception
    /// </summary>
    public static class ExceptionProcessorService
    {
        /// <summary>
        /// Gets the exception message.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <returns></returns>
        public static string GetExceptionMessage(Exception exception)
        {
            var exceptionProcessor = ServiceLocator.Current.GetInstance<IExceptionProcessor>();
            return exceptionProcessor.GetExceptionMessage(exception);
        }
    }
}