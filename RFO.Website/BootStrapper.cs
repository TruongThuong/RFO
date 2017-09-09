// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BootStrapper.cs" company="TNL">
//   Copyright (c) TNL, 2016
//   All rights are reserved. Reproduction or transmission in whole or in part, in
//   any form or by any means, electronic, mechanical or otherwise, is prohibited
//   without the prior written consent of the copyright owner.
// </copyright>
// <summary>
//   Defines the BootStrapper type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.IO;
using Microsoft.Practices.ServiceLocation;
using RFO.Common.Utilities.Logging;
using System.ComponentModel.Composition.Hosting;
using Microsoft.Practices.Prism.MefExtensions;
using System.ComponentModel.Composition;

namespace RFO.Website
{
    /// <summary>
    /// Initialize for using the MEF
    /// </summary>
    public class BootStrapper
    {
        /// <summary>
        /// The logger instance
        /// </summary>
        private static readonly ILogger Logger = LoggerManager.GetLogger(typeof(BootStrapper).Name);

        /// <summary>
        /// The <see cref="CompositionContainer" /> instance
        /// </summary>
        protected CompositionContainer compositionContainer;

        /// <summary>
        /// The default constructor
        /// </summary>
        public void CreateComposition()
        {
            var funcName = "CreateComposition";
            Logger.DebugFormat("{0} <-- Start", funcName);

            if (this.compositionContainer != null)
            {
                return;
            }

            var aggregateCatalog = new AggregateCatalog();
            var basePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin");
            var patterns = new string[]
            {
                "RFO.AspNet.Utilities.dll",
                "RFO.Common.Utilities.dll"
            };

            foreach (string pattern in patterns)
            {
                aggregateCatalog.Catalogs.Add(new DirectoryCatalog(basePath, pattern));
            }

            this.compositionContainer = new CompositionContainer(aggregateCatalog);

            var mefServiceLocator = new MefServiceLocatorAdapter(this.compositionContainer);
            ServiceLocator.SetLocatorProvider(() => mefServiceLocator);

            this.compositionContainer.SatisfyImportsOnce(this);

            Logger.DebugFormat("{0} --> End", funcName);
        }
    }
}