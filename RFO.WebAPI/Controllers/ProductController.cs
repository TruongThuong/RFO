

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
    /// The controller takes responsibility to handle requests regardless Product management
    /// </summary>
    public class ProductController : AbstractController<Product>
    {
        #region Fields
        
        /// <summary>
        /// The logger instance
        /// </summary>
        private static readonly ILogger Logger = LoggerManager.GetLogger(typeof(ProductController).Name);

        #endregion
        
        #region Overrides of AbstractController
        
        /// <summary>
        /// Gets the records.
        /// </summary>
        /// <returns></returns>
        protected override List<Product> GetRecords()
        {
            Expression<Func<Product, bool>> filterExpr = null;

            if (!string.IsNullOrEmpty(this.selectionRequestContext.SearchKeyword))
            {
                filterExpr = n => n.Name.Contains(this.selectionRequestContext.SearchKeyword);
            }

            // Get data source from database
            var dataSource = this.UnitOfWork.ProductDAO.Select(new EntityQueryArgs<Product>
            {
                StartRecordIndex = this.selectionRequestContext.StartRecordIndex,
                NumRecordsPerPage = this.selectionRequestContext.NumRecordsPerPage,
                OrderByExpr = this.UnitOfWork.ProductDAO.BuildOrderByExpression(),
                FilterExpr = filterExpr
            });

            return dataSource;
        }

        /// <summary>
        /// Gets the record by identifier.
        /// </summary>
        /// <param name="recordId">The record identifier.</param>
        /// <returns></returns>
        protected override Product GetRecordById(int recordId)
        {
            return this.UnitOfWork.ProductDAO.SelectByID(recordId);
        }

        /// <summary>
        /// Gets the total items.
        /// </summary>
        /// <returns></returns>
        protected override int GetTotalRecords()
        {
            return this.UnitOfWork.ProductDAO.TotalRecords;
        }

        /// <summary>
        /// Inserts new record to database
        /// </summary>
        /// <returns>Record identifier</returns>
        protected override Product InsertRecord()
        {
            Product product = this.updateRequestContext.Record;

            // Mark record has been inserted
            this.UnitOfWork.ProductDAO.Insert(product);

            // Add Menu reference
            product.Menu = this.UnitOfWork.MenuDAO.SelectByID(product.MenuId);

            return product;
        }

        /// <summary>
        /// Updates specified record to database
        /// </summary>
        protected override Product UpdateRecord()
        {
            Product reqProduct = this.updateRequestContext.Record;

            var product = this.UnitOfWork.ProductDAO.SelectByID(reqProduct.ProductId);

            product.Name = reqProduct.Name;

            product.MenuId = reqProduct.MenuId;

            product.IsAvailable = reqProduct.IsAvailable;

            product.Price = reqProduct.Price;

            product.BriefDescription = reqProduct.BriefDescription;

            product.IsActive = reqProduct.IsActive;

            product.IsPopular = reqProduct.IsPopular;

            product.IsBestSeller = reqProduct.IsBestSeller;

            product.Description = reqProduct.Description;

            product.Remark = reqProduct.Remark;

            product.Total = reqProduct.Total;


            // Mark record has been updated
            this.UnitOfWork.ProductDAO.Update(product);

            return product;
        }

        /// <summary>
        /// Deletes the record.
        /// </summary>
        /// <param name="recordId">The record identifier.</param>
        protected override void DeleteRecord(int recordId)
        {
            // Mark record has been deleted
            this.UnitOfWork.ProductDAO.Delete(recordId);
        }

        /// <summary>
        /// Determines whether [is exist record] [the specified name].
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        protected override bool IsExistRecord(string name)
        {
            var isExist = this.UnitOfWork.ProductDAO.IsExist(name);
            return isExist;
        }

        #endregion
        
    }
}

