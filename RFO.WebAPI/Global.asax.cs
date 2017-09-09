using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using RFO.AspNet.Utilities.MembershipService;
using RFO.Common.Utilities.Logging;
using RFO.Model;

namespace RFO.WebAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// The logger instance
        /// </summary>
        private static readonly ILogger Logger = LoggerManager.GetLogger(typeof(WebApiApplication).Name);

        /// <summary>
        /// Applications the start.
        /// </summary>
        protected void Application_Start()
        {
            var funcName = "Application_Start";
            Logger.DebugFormat("{0} <-- Start", funcName);

            GlobalConfiguration.Configure(WebApiConfig.Register);

            // Add this code line to fix following bug:
            // System.InvalidOperationException: The model backing the 'DBEntities' 
            // context has changed since the database was created. 
            // Consider using Code First Migrations to update the database 
            // (http://go.microsoft.com/fwlink/?LinkId=238269).
            Database.SetInitializer<RFODbContext>(null);
            SimpleMembershipInitializer<RFODbContext>.Initialize();

            // Create compositions for using MEF
            var bootstrapper = new BootStrapper();
            bootstrapper.CreateComposition();

            Logger.DebugFormat("{0} --> End", funcName);
        }

        /// <summary>
        /// Applications the begin request.
        /// </summary>
        protected void Application_BeginRequest()
        {
            if (Request.Headers.AllKeys.Contains("Origin") && Request.HttpMethod == "OPTIONS")
            {
                Response.Flush();
            }
        }
    }
}
