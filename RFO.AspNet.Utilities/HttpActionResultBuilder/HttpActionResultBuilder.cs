
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Microsoft.Practices.ServiceLocation;
using Newtonsoft.Json;
using RFO.AspNet.Utilities.Converter;
using RFO.Common.Utilities.ExceptionHandling;
using System.Web.Http;
using RFO.AspNet.Utilities.MVCExtension;
using RFO.Common.Utilities.Logging;
using System.Net.Http;

namespace RFO.AspNet.Utilities.HttpActionResultBuilder
{
    /// <summary>
    /// Implements APIs to build ActionResult
    /// </summary>
    public static class HttpActionResultBuilder
    {
        #region Fields

        /// <summary>
        /// The logger instance
        /// </summary>
        private static readonly ILogger Logger = LoggerManager.GetLogger(typeof(HttpActionResultBuilder).Name);

        #endregion

        #region Implementations of IActionResultBuilder

        /// <summary>
        /// Builds the exception result.
        /// </summary>
        /// <param name="httpRequest">The HTTP request.</param>
        /// <param name="exception">The exception.</param>
        /// <returns></returns>
        public static IHttpActionResult BuildExceptionResult(HttpRequestMessage httpRequest, Exception exception)
        {
            var funcName = "BuildExceptionResult";
            IHttpActionResult result = null;
            try
            {
                var combineResults = new
                {
                    Result = false,
                    Description = ExceptionProcessorService.GetExceptionMessage(exception)
                };
                // Create Json object using Newton Json
                result = BuildJsonContentResult(httpRequest, combineResults);
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("{0} - Exception: {1}", funcName, ex.ToString());
            }
            return result;
        }

        /// <summary>
        /// Builds the json content result.
        /// </summary>
        /// <param name="httpRequest">The HTTP request.</param>
        /// <param name="rawObject">The raw object.</param>
        /// <returns></returns>
        public static IHttpActionResult BuildJsonContentResult(HttpRequestMessage httpRequest, object rawObject)
        {
            var funcName = "BuildJsonContentResult";
            IHttpActionResult result = null;
            try
            {
                // Setting for Json serialization
                var settings = new JsonSerializerSettings();
                settings.Converters.Add(new EFPropertyConverter());

                // Serialize .NET object to Json
                var json = JsonConvert.SerializeObject(rawObject, settings);

                // Call ExecuteAsync to create an HttpResponseMessage, then convert to an HTTP response message.
                result = new HttpActionResult(json, httpRequest);
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("{0} - Exception: {1}", funcName, ex.ToString());
            }
            return result;
        }

        #endregion
    }
}