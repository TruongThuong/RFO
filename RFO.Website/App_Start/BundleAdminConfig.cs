
using System.Web.Optimization;

namespace RFO.Website
{
    /// <summary>
    /// Bundle configuration
    /// </summary>
    public partial class BundleConfig
    {
        /// <summary>
        /// Registers the admin bundles.
        /// </summary>
        /// <param name="bundles">The bundles.</param>
        public static void RegisterAdminBundles(BundleCollection bundles)
        {
            RegisterAdminJS(bundles);
            RegisterAdminCSS(bundles);
        }

        /// <summary>
        /// Registers the admin js.
        /// </summary>
        /// <param name="bundles">The bundles.</param>
        public static void RegisterAdminJS(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/adminjs").Include(
                "~/Scripts/modernizr-*",
                "~/Scripts/jquery-1.11.1.min.js",
                "~/Scripts/jquery.unobtrusive*",
                "~/Scripts/jquery.validate*",
                "~/Scripts/bootstrap.js",
                "~/Scripts/bootstrap-ckeditor-modal-fix.js",
                "~/Scripts/common/string.js",
                "~/Scripts/common/commonUtil.js",
                "~/Scripts/common/stringUtil.js",
                "~/Scripts/common/dialogManager.js",
                "~/Scripts/common/ajaxCrud.js",
                "~/Scripts/common/jDataTableWapper.js",
                "~/Templates/Admin/js/eakroko.js",
                "~/Templates/Admin/js/application.min.js",
                "~/Templates/Admin/js/plugins/nicescroll/jquery.nicescroll.min.js",
                "~/Templates/Admin/js/plugins/jquery-ui/jquery.ui.core.min.js",
                "~/Templates/Admin/js/plugins/jquery-ui/jquery.ui.widget.min.js",
                "~/Templates/Admin/js/plugins/jquery-ui/jquery.ui.mouse.min.js",
                "~/Templates/Admin/js/plugins/jquery-ui/jquery.ui.draggable.min.js",
                "~/Templates/Admin/js/plugins/jquery-ui/jquery.ui.resizable.min.js",
                "~/Templates/Admin/js/plugins/jquery-ui/jquery.ui.sortable.min.js",
                "~/Templates/Admin/js/plugins/datepicker/bootstrap-datepicker.js",
                "~/Templates/Admin/js/plugins/bootbox/bootbox.js",
                "~/Templates/Admin/js/plugins/imagesLoaded/jquery.imagesloaded.min.js",
                "~/Templates/Admin/js/plugins/chosen/chosen.jquery.min.js",
                "~/Templates/Admin/js/plugins/select2/select2.js",
                "~/Templates/Admin/js/plugins/colorbox/jquery.colorbox-min.js",
                "~/Templates/Admin/js/plugins/icheck/jquery.icheck.min.js",
                "~/Templates/Admin/js/plugins/datatable/jquery.dataTables.js",
                "~/Templates/Admin/js/plugins/datatable/fnSetFilteringDelay.js",
                "~/Templates/Admin/js/plugins/datatable/TableTools.min.js",
                "~/Templates/Admin/js/plugins/datatable/ColReorderWithResize.js",
                "~/Templates/Admin/js/plugins/datatable/ColVis.min.js",
                "~/Templates/Admin/js/plugins/datatable/jquery.dataTables.columnFilter.js",
                "~/Templates/Admin/js/plugins/datatable/jquery.dataTables.grouping.js",
                "~/Templates/Admin/js/plugins/tbltree/jquery.tbltree.js",
                "~/Templates/Admin/js/plugins/jBlockUI/jquery.blockUI.js",
                "~/Templates/Admin/js/plugins/toastr/toastr.js",
                "~/Templates/Admin/js/plugins/toastr/logger.js",
                "~/Templates/Admin/js/plugins/ckeditor/ckeditor.js",
                "~/Templates/Admin/js/plugins/jLoader/jLoader.js",
                "~/Templates/Admin/js/plugins/jPubsub/jPubsub.js",
                "~/Templates/Admin/js/plugins/jNumber/jquery.number.js",
                "~/Templates/Admin/js/plugins/tagsinput/jquery.tagsinput.min.js",
                "~/Templates/Admin/js/plugins/jDateFormat/dateFormat.js",
                "~/Templates/Admin/js/plugins/jDateFormat/jquery.dateFormat.js",
                "~/Templates/Admin/js/plugins/jRating/jRating.js",
                "~/Templates/Admin/js/plugins/jKnockout/knockout-3.3.0.js",
                "~/Templates/Admin/js/plugins/fileupload/bootstrap-fileupload.js",
                "~/Templates/Admin/js/plugins/fileupload/ajaxfileupload.js",
                "~/Templates/Admin/js/plugins/colorpicker/bootstrap-colorpicker.js"));
        }

        /// <summary>
        /// Registers the admin CSS.
        /// </summary>
        /// <param name="bundles">The bundles.</param>
        public static void RegisterAdminCSS(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/bundles/fonts").Include(
                "~/Templates/Admin/font/font-awesome.min.css"));

            bundles.Add(new StyleBundle("~/bundles/admincss").Include(
                "~/Templates/Admin/css/bootstrap.min.css",
                "~/Templates/Admin/css/bootstrap-responsive.min.css",
                "~/Templates/Admin/css/style.css",
                "~/Templates/Admin/css/themes.css",
                "~/Templates/Admin/css/plugins/jquery-ui/smoothness/jquery-ui.css",
                "~/Templates/Admin/css/plugins/jquery-ui/smoothness/jquery.ui.theme.css",
                "~/Templates/Admin/css/plugins/pageguide/pageguide.css",
                "~/Templates/Admin/css/plugins/fullcalendar/fullcalendar.css",
                "~/Templates/Admin/css/plugins/fullcalendar/fullcalendar.print.css",
                "~/Templates/Admin/css/plugins/chosen/chosen.css",
                "~/Templates/Admin/css/plugins/select2/select2.css",
                "~/Templates/Admin/css/plugins/colorbox/colorbox.css",
                "~/Templates/Admin/css/plugins/icheck/all.css",
                "~/Templates/Admin/css/plugins/icheck/square/_all.css",
                "~/Templates/Admin/css/plugins/datatable/TableTools.css",
                "~/Templates/Admin/css/plugins/datepicker/datepicker.css",
                "~/Templates/Admin/css/plugins/tagsinput/jquery.tagsinput.css",
                "~/Templates/Admin/css/plugins/jQuestionMark/jQuestionMark.css",
                "~/Templates/Admin/css/plugins/colorpicker/colorpicker.css",
                "~/Templates/Admin/css/plugins/jLoader/jLoader.css",
                "~/Templates/Admin/css/plugins/tbltree/jquery.tbltree.css",
                "~/Templates/Admin/css/plugins/jBlockUI/jquery.blockUI.css",
                "~/Templates/Admin/css/plugins/toastr/toastr.css",
                "~/Templates/Admin/css/plugins/jRating/jRating.css"));
        }
    }
}
