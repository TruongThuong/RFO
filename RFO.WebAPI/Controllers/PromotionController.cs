

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
    /// The controller takes responsibility to handle requests regardless Promotion management
    /// </summary>
    public class PromotionController : AbstractController<Promotion>
    {
        #region Fields
        
        /// <summary>
        /// The logger instance
        /// </summary>
        private static readonly ILogger Logger = LoggerManager.GetLogger(typeof(PromotionController).Name);

        #endregion
        
        #region Overrides of AbstractController
        
        /// <summary>
        /// Gets the records.
        /// </summary>
        /// <returns></returns>
        protected override List<Promotion> GetRecords()
        {
            Expression<Func<Promotion, bool>> filterExpr = null;

            if (!string.IsNullOrEmpty(this.selectionRequestContext.SearchKeyword))
            {
                filterExpr = n => n.Title.Contains(this.selectionRequestContext.SearchKeyword);
            }

            // Get data source from database
            var dataSource = this.UnitOfWork.PromotionDAO.Select(new EntityQueryArgs<Promotion>
            {
                StartRecordIndex = this.selectionRequestContext.StartRecordIndex,
                NumRecordsPerPage = this.selectionRequestContext.NumRecordsPerPage,
                OrderByExpr = this.UnitOfWork.PromotionDAO.BuildOrderByExpression(),
                FilterExpr = filterExpr
            });

            return dataSource;
        }

        /// <summary>
        /// Gets the record by identifier.
        /// </summary>
        /// <param name="recordId">The record identifier.</param>
        /// <returns></returns>
        protected override Promotion GetRecordById(int recordId)
        {
            return this.UnitOfWork.PromotionDAO.SelectByID(recordId);
        }

        /// <summary>
        /// Gets the total items.
        /// </summary>
        /// <returns></returns>
        protected override int GetTotalRecords()
        {
            return this.UnitOfWork.PromotionDAO.TotalRecords;
        }

        /// <summary>
        /// Inserts new record to database
        /// </summary>
        /// <returns>Record identifier</returns>
        protected override Promotion InsertRecord()
        {
            Promotion promotion = this.updateRequestContext.Record;

            // Mark record has been inserted
            this.UnitOfWork.PromotionDAO.Insert(promotion);

            return promotion;
        }

        /// <summary>
        /// Updates specified record to database
        /// </summary>
        protected override Promotion UpdateRecord()
        {
            Promotion reqPromotion = this.updateRequestContext.Record;

            var promotion = this.UnitOfWork.PromotionDAO.SelectByID(reqPromotion.PromotionId);

            promotion.ImageFile = reqPromotion.ImageFile;

            promotion.Title = reqPromotion.Title;

            promotion.BriefDescription = reqPromotion.BriefDescription;

            promotion.IsActive = reqPromotion.IsActive;

            promotion.IsPopular = reqPromotion.IsPopular;

            promotion.Description = reqPromotion.Description;


            // Mark record has been updated
            this.UnitOfWork.PromotionDAO.Update(promotion);

            return promotion;
        }

        /// <summary>
        /// Deletes the record.
        /// </summary>
        /// <param name="recordId">The record identifier.</param>
        protected override void DeleteRecord(int recordId)
        {
            // Mark record has been deleted
            this.UnitOfWork.PromotionDAO.Delete(recordId);
        }

        /// <summary>
        /// Determines whether [is exist record] [the specified name].
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        protected override bool IsExistRecord(string name)
        {
            var isExist = this.UnitOfWork.PromotionDAO.IsExist(name);
            return isExist;
        }

        #endregion
        
    }
}

