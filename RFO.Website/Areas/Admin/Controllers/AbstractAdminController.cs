
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RFO.AspNet.Utilities.Attribute;
using RFO.AspNet.Utilities.ControllerBase;
using RFO.AspNet.Utilities.ActionResultBuilder;

namespace RFO.Website.Areas.Admin.Controllers
{
    public abstract class AbstractAdminController : AbstractController
    {
        #region Public methods

        /// <summary>
        /// Enter index page
        /// </summary>
        /// <returns>Result of an action method</returns>
        [CompressContent]
        public virtual ActionResult Index()
        {
            return this.View("Index");
        }

        /// <summary>
        /// Selects the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [CompressContent]
        public virtual ActionResult SelectByID(int id)
        {
            ActionResult result;

            try
            {
                // Get detail view for this action
                var partialView = this.GetDetailViewTemplate(id);

                // Serialize datasource to Json
                var responseContext = new 
                {
                    Data = partialView,
                    Result = true,
                    Description = string.Empty
                };
                result = ActionResultBuilder.BuildJsonContentResult(responseContext);
            }
            catch (Exception ex)
            {
                result = ActionResultBuilder.BuildExceptionResult(ex);
            }

            return result;
        }

        #endregion

        #region Protected methods

        /// <summary>
        /// Gets the detail view template.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        protected abstract string GetDetailViewTemplate(int id);

        /// <summary>
        /// Gets the records view template.
        /// </summary>
        /// <returns></returns>
        protected abstract string GetRecordsViewTemplate();

        #endregion
    }
}