
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Microsoft.Practices.ServiceLocation;
using Newtonsoft.Json;
using RFO.Common.Utilities.ExceptionHandling;
using RFO.AspNet.Utilities.Converter;
using RFO.Common.Utilities.Logging;

namespace RFO.AspNet.Utilities.ActionResultBuilder
{
    /// <summary>
    /// Implements APIs to build ActionResult
    /// </summary>
    public class ActionResultBuilder
    {
        #region Fields

        /// <summary>
        /// The logger instance
        /// </summary>
        private static readonly ILogger Logger = LoggerManager.GetLogger(typeof(ActionResultBuilder).Name);

        #endregion

        #region Implementations of IActionResultBuilder

        /// <summary>
        /// Builds the action result.
        /// </summary>
        /// <param name="exception">The ex.</param>
        /// <returns></returns>
        public static ActionResult BuildExceptionResult(Exception exception)
        {
            var funcName = "BuildExceptionResult";
            ActionResult result = null;
            try
            {
                var combineResults = new
                {
                    Result = false,
                    Description = ExceptionProcessorService.GetExceptionMessage(exception)
                };
                result = BuildJsonContentResult(combineResults);
            }
            catch(Exception ex)
            {
                Logger.ErrorFormat("{0} - Exception: {1}", funcName, ex.ToString());
            }
            return result;
        }

        /// <summary>
        /// Builds the json content result.
        /// </summary>
        /// <param name="rawObject">The raw object.</param>
        /// <returns></returns>
        public static ActionResult BuildJsonContentResult(object rawObject)
        {
            var funcName = "BuildJsonContentResult";
            ActionResult result = null;
            try
            {
                // Setting for Json serialization
                var settings = new JsonSerializerSettings();
                settings.Converters.Add(new EFPropertyConverter());

                // Serialize .NET object to Json
                var json = JsonConvert.SerializeObject(rawObject, settings);
                result = new ContentResult
                {
                    Content = json,
                    ContentType = "application/json"
                };
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