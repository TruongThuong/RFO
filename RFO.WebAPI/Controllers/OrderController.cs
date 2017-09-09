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
    /// The controller takes responsibility to handle requests regardless Order management
    /// </summary>
    public class OrderController : AbstractController<Order>
    {
        #region Fields
        
        /// <summary>
        /// The logger instance
        /// </summary>
        private static readonly ILogger Logger = LoggerManager.GetLogger(typeof(OrderController).Name);

        #endregion
        
        #region Overrides of AbstractController
        
        /// <summary>
        /// Gets the records.
        /// </summary>
        /// <returns></returns>
        protected override List<Order> GetRecords()
        {
            Expression<Func<Order, bool>> filterExpr = null;

            if (!string.IsNullOrEmpty(this.selectionRequestContext.SearchKeyword))
            {
                // Does not have classification attribute
            }

            // Get data source from database
            var dataSource = this.UnitOfWork.OrderDAO.Select(new EntityQueryArgs<Order>
            {
                StartRecordIndex = this.selectionRequestContext.StartRecordIndex,
                NumRecordsPerPage = this.selectionRequestContext.NumRecordsPerPage,
                OrderByExpr = this.UnitOfWork.OrderDAO.BuildOrderByExpression(),
                FilterExpr = filterExpr
            });

            return dataSource;
        }

        /// <summary>
        /// Gets the record by identifier.
        /// </summary>
        /// <param name="recordId">The record identifier.</param>
        /// <returns></returns>
        protected override Order GetRecordById(int recordId)
        {
            return this.UnitOfWork.OrderDAO.SelectByID(recordId);
        }

        /// <summary>
        /// Gets the total items.
        /// </summary>
        /// <returns></returns>
        protected override int GetTotalRecords()
        {
            return this.UnitOfWork.OrderDAO.TotalRecords;
        }

        /// <summary>
        /// Inserts new record to database
        /// </summary>
        /// <returns>Record identifier</returns>
        protected override Order InsertRecord()
        {
            Order order = this.updateRequestContext.Record;

            // Mark record has been inserted
            this.UnitOfWork.OrderDAO.Insert(order);

            // Add Table reference
            order.Table = this.UnitOfWork.TableDAO.SelectByID(order.TableId);

            // Add OrderState reference
            order.OrderState = this.UnitOfWork.OrderStateDAO.SelectByID(order.OrderStateId);

            return order;
        }

        /// <summary>
        /// Updates specified record to database
        /// </summary>
        protected override Order UpdateRecord()
        {
            Order reqOrder = this.updateRequestContext.Record;

            var order = this.UnitOfWork.OrderDAO.SelectByID(reqOrder.OrderId);

            order.TableId = reqOrder.TableId;

            order.OrderStateId = reqOrder.OrderStateId;

            order.DeliveryNote = reqOrder.DeliveryNote;


            // Mark record has been updated
            this.UnitOfWork.OrderDAO.Update(order);

            return order;
        }

        /// <summary>
        /// Deletes the record.
        /// </summary>
        /// <param name="recordId">The record identifier.</param>
        protected override void DeleteRecord(int recordId)
        {
            // Mark record has been deleted
            this.UnitOfWork.OrderDAO.Delete(recordId);
        }

        /// <summary>
        /// Determines whether [is exist record] [the specified name].
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        protected override bool IsExistRecord(string name)
        {
            var isExist = this.UnitOfWork.OrderDAO.IsExist(name);
            return isExist;
        }

        #endregion
        
    }
}

