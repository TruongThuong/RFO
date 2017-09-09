

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web;
using Microsoft.Practices.ServiceLocation;
using RFO.AspNet.Utilities.Attribute;
using RFO.AspNet.Utilities.ServerFileHelper;
using RFO.Common.Utilities.ExpressionHelper;
using RFO.Common.Utilities.Logging;
using RFO.Common.Utilities.Pattern;
using RFO.Common.Utilities.Exceptions;
using RFO.Model;
using RFO.DAO.Args;
using RFO.MetaData;

namespace RFO.WebAPI.Controllers
{
    /// <summary>
    /// The controller takes responsibility to handle requests regardless ProductImage management
    /// </summary>
    public class ProductImageController : AbstractImageController<ProductImage>
    {
        #region Fields
        
        /// <summary>
        /// The logger instance
        /// </summary>
        private static readonly ILogger Logger = LoggerManager.GetLogger(typeof(ProductImageController).Name);

        #endregion
        
        #region Overrides of AbstractController
        
        /// <summary>
        /// Gets the records.
        /// </summary>
        /// <returns></returns>
        protected override List<ProductImage> GetRecords()
        {
            Expression<Func<ProductImage, bool>> filterExpr = null;

            int productId;
            if (this.selectionRequestContext.SearchForeignKeys.ContainsKey("ProductIds") &&
                int.TryParse(this.selectionRequestContext.SearchForeignKeys["ProductIds"], out productId))
            {
                filterExpr = n => n.ProductId.Equals(productId);
            }

            // Get data source from database
            var dataSource = this.UnitOfWork.ProductImageDAO.Select(new EntityQueryArgs<ProductImage>
            {
                StartRecordIndex = this.selectionRequestContext.StartRecordIndex,
                NumRecordsPerPage = this.selectionRequestContext.NumRecordsPerPage,
                OrderByExpr = this.UnitOfWork.ProductImageDAO.BuildOrderByExpression(),
                FilterExpr = filterExpr
            });

            return dataSource;
        }

        /// <summary>
        /// Gets the record by identifier.
        /// </summary>
        /// <param name="recordId">The record identifier.</param>
        /// <returns></returns>
        protected override ProductImage GetRecordById(int recordId)
        {
            return this.UnitOfWork.ProductImageDAO.SelectByID(recordId);
        }

        /// <summary>
        /// Gets the total items.
        /// </summary>
        /// <returns></returns>
        protected override int GetTotalRecords()
        {
            return this.UnitOfWork.ProductImageDAO.TotalRecords;
        }

        /// <summary>
        /// Inserts new record to database
        /// </summary>
        /// <returns>Record identifier</returns>
        protected override ProductImage InsertRecord()
        {
            ProductImage productImage = this.updateRequestContext.Record;

            // Mark record has been inserted
            this.UnitOfWork.ProductImageDAO.Insert(productImage);

            // Add Product reference
            productImage.Product = this.UnitOfWork.ProductDAO.SelectByID(productImage.ProductId);

            return productImage;
        }

        /// <summary>
        /// Updates specified record to database
        /// </summary>
        protected override ProductImage UpdateRecord()
        {
            ProductImage reqProductImage = this.updateRequestContext.Record;

            var productImage = this.UnitOfWork.ProductImageDAO.SelectByID(reqProductImage.ProductImageId);

            productImage.ProductId = reqProductImage.ProductId;

            //productImage.ImageFile = reqProductImage.ImageFile;

            productImage.IsPresent = reqProductImage.IsPresent;

            productImage.IsActive = reqProductImage.IsActive;


            // Mark record has been updated
            this.UnitOfWork.ProductImageDAO.Update(productImage);

            return productImage;
        }

        /// <summary>
        /// Deletes the record.
        /// </summary>
        /// <param name="recordId">The record identifier.</param>
        protected override void DeleteRecord(int recordId)
        {
            var productImage = this.UnitOfWork.ProductImageDAO.SelectByID(recordId);
            if (productImage != null)
            {
                var oldFilePath = System.IO.Path.Combine(HttpContext.Current.Server.MapPath(
                    AppConstants.ImageDirName), productImage.ImageFile);
                if (!ServerFileHelper.DeleteFile(oldFilePath))
                {
                    throw new BusinessException("Không thể xóa hình ảnh");
                }
                
                // Mark record has been deleted
                this.UnitOfWork.ProductImageDAO.Delete(recordId);
            }
        }

        /// <summary>
        /// Determines whether [is exist record] [the specified name].
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        protected override bool IsExistRecord(string name)
        {
            var isExist = this.UnitOfWork.ProductImageDAO.IsExist(name);
            return isExist;
        }

        #endregion
        
        #region Upload ProductImage
        
        /// <summary>
        /// Pres the process uploading.
        /// </summary>
        /// <param name="formData">The form data.</param>
        /// <returns>Owner Id</returns>
        protected override string GetImageOwnerId(NameValueCollection formData)
        {
            var productId = formData["ProductId"];
            if (!string.IsNullOrEmpty(productId))
            {
                Logger.DebugFormat("GetImageOwnerId - productId={0}", productId);
            }
            return productId;
        }

        /// <summary>
        /// Posts the process uploading.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="ownerId">The owner identifier.</param>
        protected override void PostProcessUploading(string fileName, string ownerId)
        {
            var funcName = "PostProcessUploading";
            Logger.DebugFormat("{0} <-- Start", funcName);
            Logger.DebugFormat("{0} - fileName={1}, OwnerId={2}", funcName, fileName, ownerId);

            // Mark image has been inserted
            var productImage = new ProductImage
            {
                ImageFile = fileName,
                IsActive = true,
                IsPresent = false,
                ProductId = int.Parse(ownerId),
            };
            this.UnitOfWork.ProductImageDAO.Insert(productImage);

            Logger.DebugFormat("{0} --> End", funcName);
        }

        #endregion
        
    }
}

