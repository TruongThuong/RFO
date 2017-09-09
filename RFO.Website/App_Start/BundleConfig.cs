// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BundleConfig.cs" company="TNL">
//   Copyright (c) TNL, 2016
//   All rights are reserved. Reproduction or transmission in whole or in part, in
//   any form or by any means, electronic, mechanical or otherwise, is prohibited
//   without the prior written consent of the copyright owner.
// </copyright>
// <summary>
//   Defines the BundleConfig type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Web;
using System.Web.Optimization;
using RFO.Common.Utilities.Logging;

namespace RFO.Website
{
    public partial class BundleConfig
    {
        /// <summary>
        /// The logger instance
        /// </summary>
        private static readonly ILogger Logger = LoggerManager.GetLogger(typeof(BundleConfig).Name);

        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            Logger.Info("RegisterBundles <-- Start");

            try
            {
                Logger.Info("RegisterAdminBundles <-- Start");
                RegisterAdminBundles(bundles);
                Logger.Info("RegisterAdminBundles --> End");

                Logger.Info("RegisterHomeBundles <-- Start");
                RegisterHomeBundles(bundles);
                Logger.Info("RegisterHomeBundles --> End");
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("RegisterBundles - Exception: [{0}]", ex);
                throw;
            }

            Logger.Info("RegisterBundles --> End");
        }
    }
}
