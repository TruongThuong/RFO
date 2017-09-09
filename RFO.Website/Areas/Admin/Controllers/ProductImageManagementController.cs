

using System;
using System.Dynamic;
using System.Web.Mvc;
using RFO.AspNet.Utilities.Attribute;
using RFO.AspNet.Utilities.ActionResultBuilder;
using RFO.Common.Utilities.Logging;

namespace RFO.Website.Areas.Admin.Controllers
{
    /// <summary>
    /// The controller takes responsibility to handle requests regardless ProductImage management
    /// </summary>
    public class ProductImageManagementController : AbstractAdminController
    {
        #region Override of AbstractAdminController
        
        /// <summary>
        /// Gets the detail view template.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        protected override string GetDetailViewTemplate(int id)
        {
            return this.RenderPartialViewToString("~/Areas/Admin/Views/ProductImageManagement/_PartialDetail.cshtml", id);
        }

        /// <summary>
        /// Gets the records view template.
        /// </summary>
        /// <returns></returns>
        protected override string GetRecordsViewTemplate()
        {
            dynamic viewModel = new ExpandoObject();
            return this.RenderPartialViewToString("~/Areas/Admin/Views/ProductImageManagement/_PartialList.cshtml", viewModel);
        }

        #endregion
        
        #region Public methods
        
        /// <summary>
        /// Select all of elements in specified table except specified identifier
        /// </summary>
        /// <para name="productId">The Product identifier</para>
        /// <returns>Result of an action method</returns>
        [CompressContent]
        public virtual ActionResult EnterSelectByProductId(int productId)
        {
            ActionResult result;

            try
            {
                // Get records view result
                var partialView = this.GetRecordsByProductIdViewResult(productId);

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
        /// Enters the upload productImage image.
        /// </summary>
        /// <param name="productId">The productImage identifier.</param>
        /// <returns></returns>
        [CompressContent]
        public virtual ActionResult EnterUploadProductImage(int productId)
        {
            ActionResult result;

            try
            {
                // Get upload image view
                var partialView = this.GetUploadProductImageViewResult(productId);

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
        /// Gets the records by ProductId view result.
        /// </summary>
        /// <para name="productId">The Product identifier</para>
        /// <returns></returns>
        protected string GetRecordsByProductIdViewResult(int productId)
        {
            dynamic viewModel = new ExpandoObject();
            viewModel.ProductId = productId;
            return this.RenderPartialViewToString("~/Areas/Admin/Views/ProductImageManagement/_PartialList.cshtml", viewModel);
        }

        /// <summary>
        /// Gets the upload productImage image view result.
        /// </summary>
        /// <param name="productId">The productImage identifier.</param>
        /// <returns></returns>
        protected string GetUploadProductImageViewResult(int productId)
        {
            dynamic viewModel = new ExpandoObject();
            viewModel.ProductId = productId;
            return this.RenderPartialViewToString("~/Areas/Admin/Views/ProductImageManagement/_PartialUploadImage.cshtml", viewModel);
        }

        #endregion
        
    }
}

