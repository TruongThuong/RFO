

using System;
using System.Dynamic;
using System.Web.Mvc;
using RFO.AspNet.Utilities.Attribute;
using RFO.AspNet.Utilities.ActionResultBuilder;
using RFO.Common.Utilities.Logging;

namespace RFO.Website.Areas.Admin.Controllers
{
    /// <summary>
    /// The controller takes responsibility to handle requests regardless OrderDetail management
    /// </summary>
    public class OrderDetailManagementController : AbstractAdminController
    {
        #region Override of AbstractAdminController

        /// <summary>
        /// Selects the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        [CompressContent]
        public ActionResult SelectByIDAndTableId(int id, int tableId)
        {
            ActionResult result;

            try
            {
                // Get detail view for this action
                var partialView = this.GetDetailViewTemplate(id, tableId);

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

        /// <summary>
        /// Gets the detail view template.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        private string GetDetailViewTemplate(int id, int tableId)
        {
            dynamic viewModel = new ExpandoObject();
            viewModel.OrderDetailId = id;
            viewModel.TableId = tableId;
            return this.RenderPartialViewToString("~/Areas/Admin/Views/OrderDetailManagement/_PartialDetail.cshtml", viewModel);
        }

        /// <summary>
        /// Gets the detail view template.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        protected override string GetDetailViewTemplate(int id)
        {
            return this.RenderPartialViewToString("~/Areas/Admin/Views/OrderDetailManagement/_PartialDetail.cshtml", id);
        }

        /// <summary>
        /// Gets the records view template.
        /// </summary>
        /// <returns></returns>
        protected override string GetRecordsViewTemplate()
        {
            dynamic viewModel = new ExpandoObject();
            return this.RenderPartialViewToString("~/Areas/Admin/Views/OrderDetailManagement/_PartialList.cshtml", viewModel);
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Enters the select by table identifier.
        /// </summary>
        /// <param name="tableId">The table identifier.</param>
        /// <returns></returns>
        [CompressContent]
        public virtual ActionResult EnterSelectByTableId(int tableId)
        {
            ActionResult result;

            try
            {
                // Get records view result
                var partialView = this.GetRecordsByTableIdViewResult(tableId);

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

        /// <summary>
        /// Enters the select by order identifier.
        /// </summary>
        /// <param name="orderId">The order identifier.</param>
        /// <returns></returns>
        [CompressContent]
        public virtual ActionResult EnterSelectByOrderId(int orderId)
        {
            ActionResult result;

            try
            {
                // Get records view result
                var partialView = this.GetRecordsByOrderIdViewResult(orderId);

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

        #region Select by foreign key

        /// <summary>
        /// Gets the records by table identifier view result.
        /// </summary>
        /// <param name="tableId">The table identifier.</param>
        /// <returns></returns>
        protected string GetRecordsByTableIdViewResult(int tableId)
        {
            dynamic viewModel = new ExpandoObject();
            viewModel.TableId = tableId;
            viewModel.OrderStateId = 1;
            return this.RenderPartialViewToString("~/Areas/Admin/Views/OrderDetailManagement/_PartialList.cshtml", viewModel);
        }

        /// <summary>
        /// Gets the records by order identifier view result.
        /// </summary>
        /// <param name="orderId">The order identifier.</param>
        /// <returns></returns>
        protected string GetRecordsByOrderIdViewResult(int orderId)
        {
            dynamic viewModel = new ExpandoObject();
            viewModel.OrderId = orderId;
            viewModel.OrderStateId = 1;
            return this.RenderPartialViewToString("~/Areas/Admin/Views/OrderDetailManagement/_PartialListByOrderId.cshtml", viewModel);
        }

        #endregion

    }
}

