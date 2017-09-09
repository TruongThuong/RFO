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
using Microsoft.Ajax.Utilities;

namespace RFO.Website
{
    /// <summary>
    /// Bundle configuration
    /// </summary>
    public partial class BundleConfig
    {
        /// <summary>
        /// Registers the home bundles.
        /// </summary>
        /// <param name="bundles">The bundles.</param>
        public static void RegisterHomeBundles(BundleCollection bundles)
        {
            RegisterHomeJS(bundles);
            RegisterHomeCSS(bundles);
        }

        /// <summary>
        /// Registers the home js.
        /// </summary>
        /// <param name="bundles">The bundles.</param>
        public static void RegisterHomeJS(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/homejs").Include(
                "~/Scripts/angular.min.js",
                "~/Scripts/angular-route.min.js",
                "~/Scripts/angular-animate.min.js",
                "~/Scripts/app/app.js",
                "~/Scripts/app/custom-services/data.service.js",
                "~/Scripts/app/plugins/loading-bar.js",
                "~/Scripts/app/plugins/dirPagination.js",
                "~/Scripts/app/plugins/rzslider.js",
                "~/Scripts/app/hotel/hotel-list.controller.js",
                "~/Templates/Home/js/jquery.1.7.1.js",
                "~/Templates/Home/js/idangerous.swiper.js",
                "~/Templates/Home/js/slideInit.js",
                "~/Templates/Home/js/jquery.appear.js",
                "~/Templates/Home/js/script.js",
                "~/Templates/Home/js/owl.carousel.min.js",
                "~/Templates/Home/js/custom.select.js",
                "~/Templates/Home/js/plugins/jBlockUI/jquery.blockUI.js",
                "~/Templates/Home/js/plugins/toastr/toastr.js",
                "~/Templates/Home/js/plugins/toastr/logger.js",
                "~/Scripts/common/string.js",
                "~/Scripts/common/jquery.number.js"));
        }

        /// <summary>
        /// Registers the home CSS.
        /// </summary>
        /// <param name="bundles">The bundles.</param>
        public static void RegisterHomeCSS(BundleCollection bundles)
        {
           bundles.Add(new StyleBundle("~/bundles/homecss").Include(
                "~/Scripts/app/plugins/loading-bar.css",
                "~/Scripts/app/plugins/rzslider.css",
                "~/Templates/Home/css/style.css",
                "~/Templates/Home/css/idangerous.swiper.css",
                "~/Templates/Home/css/owl.carousel.css",
                "~/Templates/Home/css/plugins/jBlockUI/jquery.blockUI.css",
                "~/Templates/Home/css/plugins/toastr/toastr.css"));
        }
    }
}
