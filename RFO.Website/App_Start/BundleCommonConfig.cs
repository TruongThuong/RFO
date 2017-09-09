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

using System.Web.Optimization;

namespace RFO.Website
{
    /// <summary>
    /// Bundle configuration
    /// </summary>
    public partial class BundleConfig
    {
        /// <summary>
        /// Registers the common bundles.
        /// </summary>
        /// <param name="bundles">The bundles.</param>
        public static void RegisterCommonBundles(BundleCollection bundles)
        {
            RegisterCommonJS(bundles);
        }

        /// <summary>
        /// Registers the common js.
        /// </summary>
        /// <param name="bundles">The bundles.</param>
        public static void RegisterCommonJS(BundleCollection bundles)
        {
            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/commonjs").Include(
                        "~/Scripts/modernizr-*",
                        "~/Scripts/common/ajaxCrud.js",
                        "~/Scripts/common/commonUtil.js",
                        "~/Scripts/common/stringUtil.js"));
        }
    }
}
