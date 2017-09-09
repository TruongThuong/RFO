// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MvcApplication.cs" company="TNL">
//   Copyright (c) TNL, 2016
//   All rights are reserved. Reproduction or transmission in whole or in part, in
//   any form or by any means, electronic, mechanical or otherwise, is prohibited
//   without the prior written consent of the copyright owner.
// </copyright>
// <summary>
//   Defines the MvcApplication type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using RFO.Common.Utilities.Localization;
using RFO.Common.Utilities.Logging;
using RFO.Common.Utilities.PathHelper;

namespace RFO.Website
{
    public class MvcApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// The logger instance
        /// </summary>
        private static readonly ILogger Logger = LoggerManager.GetLogger(typeof(MvcApplication).Name);

        /// <summary>
        /// Handle application starting
        /// </summary>
        protected void Application_Start()
        {
            Logger.Info("Application_Start <-- start");

            try
            {
                // Create compositions for using MEF
                var bootstrapper = new BootStrapper();
                bootstrapper.CreateComposition();

                AreaRegistration.RegisterAllAreas();
                FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
                RouteConfig.RegisterRoutes(RouteTable.Routes);
                BundleConfig.RegisterBundles(BundleTable.Bundles);
            }
            catch(Exception ex)
            {
                throw new Exception("Application_Start - Exception: Could not start application", ex);
            }

            Logger.Info("Application_Start --> End");
        }

        /// <summary>
        /// Handles the Start event of the Session control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Session_Start(object sender, EventArgs e)
        {
            Logger.Info("Session_Start <-- start");

            try
            {
                this.InitLocalization();
            }
            catch(Exception ex)
            {
                throw new Exception("Application_Start - Exception: Could not start session", ex);
            }

            Logger.Info("Session_Start --> End");
        }

        /// <summary>
        /// Initializes the localization.
        /// </summary>
        private void InitLocalization()
        {
            Logger.Info("InitLocalization <-- start");

            // First determines whether the TranslationManager has been already stored in Session
            if (this.Session["TranslationManager"] == null)
            {
                this.Session["TranslationManager"] = this.CreateTranslationManager();
            }

            const string selectLanguage = "vi";
            var translationManager = (ITranslationManager)this.Session["TranslationManager"];
            translationManager.CurrentLanguage = selectLanguage;

            // Store selected culture in cookie
            var culture = new HttpCookie("culture", selectLanguage)
            {
                Expires = DateTime.Now.AddYears(10)
            };
            Response.Cookies.Add(culture);

            Logger.Info("InitLocalization --> End");
        }

        /// <summary>
        /// Creates the translation manager.
        /// </summary>
        /// <returns></returns>
        private TranslationManager CreateTranslationManager()
        {
            var lang = PathHelper.GetAppPath(@"Configurations\Languages.xml");
            var transProvider = new XmlTranslationProvider(lang);
            transProvider.Initialize();

            var translationManager = new TranslationManager();
            translationManager.SetTranslationProvider(transProvider);

            return translationManager;
        }
    }
}
