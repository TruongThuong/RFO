

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
    /// The controller takes responsibility to handle requests regardless CommonInfo management
    /// </summary>
    public class CommonInfoController : AbstractController<CommonInfo>
    {
        #region Fields
        
        /// <summary>
        /// The logger instance
        /// </summary>
        private static readonly ILogger Logger = LoggerManager.GetLogger(typeof(CommonInfoController).Name);

        #endregion
        
        #region Overrides of AbstractController
        
        /// <summary>
        /// Gets the records.
        /// </summary>
        /// <returns></returns>
        protected override List<CommonInfo> GetRecords()
        {
            Expression<Func<CommonInfo, bool>> filterExpr = null;

            if (!string.IsNullOrEmpty(this.selectionRequestContext.SearchKeyword))
            {
                filterExpr = n => n.Name.Contains(this.selectionRequestContext.SearchKeyword);
            }

            // Get data source from database
            var dataSource = this.UnitOfWork.CommonInfoDAO.Select(new EntityQueryArgs<CommonInfo>
            {
                StartRecordIndex = this.selectionRequestContext.StartRecordIndex,
                NumRecordsPerPage = this.selectionRequestContext.NumRecordsPerPage,
                OrderByExpr = this.UnitOfWork.CommonInfoDAO.BuildOrderByExpression(),
                FilterExpr = filterExpr
            });

            return dataSource;
        }

        /// <summary>
        /// Gets the record by identifier.
        /// </summary>
        /// <param name="recordId">The record identifier.</param>
        /// <returns></returns>
        protected override CommonInfo GetRecordById(int recordId)
        {
            return this.UnitOfWork.CommonInfoDAO.SelectByID(recordId);
        }

        /// <summary>
        /// Gets the total items.
        /// </summary>
        /// <returns></returns>
        protected override int GetTotalRecords()
        {
            return this.UnitOfWork.CommonInfoDAO.TotalRecords;
        }

        /// <summary>
        /// Inserts new record to database
        /// </summary>
        /// <returns>Record identifier</returns>
        protected override CommonInfo InsertRecord()
        {
            CommonInfo commonInfo = this.updateRequestContext.Record;

            // Mark record has been inserted
            this.UnitOfWork.CommonInfoDAO.Insert(commonInfo);

            return commonInfo;
        }

        /// <summary>
        /// Updates specified record to database
        /// </summary>
        protected override CommonInfo UpdateRecord()
        {
            CommonInfo reqCommonInfo = this.updateRequestContext.Record;

            var commonInfo = this.UnitOfWork.CommonInfoDAO.SelectByID(reqCommonInfo.CommonInfoId);

            commonInfo.CommonInfoCode = reqCommonInfo.CommonInfoCode;

            commonInfo.Name = reqCommonInfo.Name;

            commonInfo.BriefDescription = reqCommonInfo.BriefDescription;

            commonInfo.Description = reqCommonInfo.Description;


            // Mark record has been updated
            this.UnitOfWork.CommonInfoDAO.Update(commonInfo);

            return commonInfo;
        }

        /// <summary>
        /// Deletes the record.
        /// </summary>
        /// <param name="recordId">The record identifier.</param>
        protected override void DeleteRecord(int recordId)
        {
            // Mark record has been deleted
            this.UnitOfWork.CommonInfoDAO.Delete(recordId);
        }

        /// <summary>
        /// Determines whether [is exist record] [the specified name].
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        protected override bool IsExistRecord(string name)
        {
            var isExist = this.UnitOfWork.CommonInfoDAO.IsExist(name);
            return isExist;
        }

        #endregion
        
    }
}

