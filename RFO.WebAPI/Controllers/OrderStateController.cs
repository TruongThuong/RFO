

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
    /// The controller takes responsibility to handle requests regardless OrderState management
    /// </summary>
    public class OrderStateController : AbstractController<OrderState>
    {
        #region Fields
        
        /// <summary>
        /// The logger instance
        /// </summary>
        private static readonly ILogger Logger = LoggerManager.GetLogger(typeof(OrderStateController).Name);

        #endregion
        
        #region Overrides of AbstractController
        
        /// <summary>
        /// Gets the records.
        /// </summary>
        /// <returns></returns>
        protected override List<OrderState> GetRecords()
        {
            Expression<Func<OrderState, bool>> filterExpr = null;

            if (!string.IsNullOrEmpty(this.selectionRequestContext.SearchKeyword))
            {
                filterExpr = n => n.Name.Contains(this.selectionRequestContext.SearchKeyword);
            }

            // Get data source from database
            var dataSource = this.UnitOfWork.OrderStateDAO.Select(new EntityQueryArgs<OrderState>
            {
                StartRecordIndex = this.selectionRequestContext.StartRecordIndex,
                NumRecordsPerPage = this.selectionRequestContext.NumRecordsPerPage,
                OrderByExpr = this.UnitOfWork.OrderStateDAO.BuildOrderByExpression(),
                FilterExpr = filterExpr
            });

            return dataSource;
        }

        /// <summary>
        /// Gets the record by identifier.
        /// </summary>
        /// <param name="recordId">The record identifier.</param>
        /// <returns></returns>
        protected override OrderState GetRecordById(int recordId)
        {
            return this.UnitOfWork.OrderStateDAO.SelectByID(recordId);
        }

        /// <summary>
        /// Gets the total items.
        /// </summary>
        /// <returns></returns>
        protected override int GetTotalRecords()
        {
            return this.UnitOfWork.OrderStateDAO.TotalRecords;
        }

        /// <summary>
        /// Inserts new record to database
        /// </summary>
        /// <returns>Record identifier</returns>
        protected override OrderState InsertRecord()
        {
            OrderState orderState = this.updateRequestContext.Record;

            // Mark record has been inserted
            this.UnitOfWork.OrderStateDAO.Insert(orderState);

            return orderState;
        }

        /// <summary>
        /// Updates specified record to database
        /// </summary>
        protected override OrderState UpdateRecord()
        {
            OrderState reqOrderState = this.updateRequestContext.Record;

            var orderState = this.UnitOfWork.OrderStateDAO.SelectByID(reqOrderState.OrderStateId);

            orderState.Name = reqOrderState.Name;

            orderState.BriefDescription = reqOrderState.BriefDescription;


            // Mark record has been updated
            this.UnitOfWork.OrderStateDAO.Update(orderState);

            return orderState;
        }

        /// <summary>
        /// Deletes the record.
        /// </summary>
        /// <param name="recordId">The record identifier.</param>
        protected override void DeleteRecord(int recordId)
        {
            // Mark record has been deleted
            this.UnitOfWork.OrderStateDAO.Delete(recordId);
        }

        /// <summary>
        /// Determines whether [is exist record] [the specified name].
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        protected override bool IsExistRecord(string name)
        {
            var isExist = this.UnitOfWork.OrderStateDAO.IsExist(name);
            return isExist;
        }

        #endregion
        
    }
}

