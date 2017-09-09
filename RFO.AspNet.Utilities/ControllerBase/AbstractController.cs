
using System.IO;
using System.Web.Mvc;
using Microsoft.Practices.ServiceLocation;
using RFO.AspNet.Utilities.ActionResultBuilder;

namespace RFO.AspNet.Utilities.ControllerBase
{
    /// <summary>
    /// The abstract controller
    /// </summary>
    public abstract class AbstractController : Controller
    {
        #region Protected methods

        /// <summary>
        /// RenderPartialView without model
        /// </summary>
        /// <param name="viewName"></param>
        /// <returns></returns>
        protected string RenderPartialViewToString(string viewName)
        {
            return this.RenderPartialViewToString(viewName, null);
        }

        /// <summary>
        /// Render a partial view and convert its content to string
        /// </summary>
        /// <param name="viewName">The name of partial view</param>
        /// <param name="model">Data Model</param>
        protected string RenderPartialViewToString(string viewName, object model)
        {
            this.ViewData.Model = model;
            string result;

            if (string.IsNullOrEmpty(viewName))
            {
                viewName = this.ControllerContext.RouteData.GetRequiredString("action");
            }

            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(this.ControllerContext, viewName);
                var viewContext = new ViewContext(this.ControllerContext, viewResult.View, this.ViewData, this.TempData,
                    sw);
                viewResult.View.Render(viewContext, sw);
                result = sw.GetStringBuilder().ToString();
            }

            return result;
        }

        #endregion
    }
}