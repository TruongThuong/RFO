

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
using RFO.Model.Enum;
using System.Web.Http;
using RFO.WebAPI.Models.Response;
using RFO.AspNet.Utilities.HttpActionResultBuilder;
using RFO.Common.Utilities.Utilities;

namespace RFO.WebAPI.Controllers
{
    /// <summary>
    /// The controller takes responsibility to handle requests regardless Table management
    /// </summary>
    public class TableController : AbstractController<Table>
    {
        #region Fields
        
        /// <summary>
        /// The logger instance
        /// </summary>
        private static readonly ILogger Logger = LoggerManager.GetLogger(typeof(TableController).Name);

        #endregion

        #region Public methods

        /// <summary>
        /// Select specified element of specified table
        /// </summary>
        /// <param name="id">The key for identifying an element</param>
        /// <returns>Result of an action method</returns>
        [HttpGet]
        public virtual IHttpActionResult Checkout(int id)
        {
            var funcName = "Checkout";
            Logger.DebugFormat("{0} <-- Start", funcName);
            Logger.DebugFormat("{0} - Input params: id={1}", funcName, id);
            IHttpActionResult result;

            try
            {
                /* STEP 1: Update table available */
                var table = this.GetRecordById(id);
                table.Status = (int)TableStatus.Available;
                this.UnitOfWork.TableDAO.Update(table);

                /* STEP 2: Get latest order with receiving state of current table, it will be created if not exist */
                var orderStates = this.UnitOfWork.OrderStateDAO.SelectAll();
                if (!orderStates.Any())
                {
                    throw new ArgumentException("Chưa thiết lập bất cứ trạng thái đơn hàng nào");
                }

                var orders = this.UnitOfWork.OrderDAO.Select(new EntityQueryArgs<Order>
                {
                    StartRecordIndex = 0,
                    NumRecordsPerPage = 1,
                    OrderByExpr = this.UnitOfWork.OrderDAO.BuildOrderByExpression(),
                    SortDirection = "desc",
                    FilterExpr = n => n.TableId.Equals(table.TableId) && n.OrderState.Name.Equals("Accept")
                });

                Order order = null;
                if (orders.IsNullOrEmpty()) // Order has created before
                {
                    throw new BusinessException($"Could not find order with receiving state of table [{table.TableId}]");
                }
                order = orders.First();
                order.OrderStateId = orderStates.Last().OrderStateId;
                this.UnitOfWork.OrderDAO.Update(order);

                // Commit changed
                this.UnitOfWork.SaveChanges((exception) =>
                {
                    throw new DatabaseException((int)DatabaseErrorCode.Insert, exception);
                });

                // Serialize return data
                var responseContext = new SelectionResponseContext<Table>
                {
                    Record = table,
                    Result = true,
                    Description = string.Empty
                };
                result = HttpActionResultBuilder.BuildJsonContentResult(this.Request, responseContext);
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("{0} - Exception: {1}", funcName, ex);
                result = HttpActionResultBuilder.BuildExceptionResult(this.Request, ex);
            }

            Logger.DebugFormat("{0} --> End", funcName);
            return result;
        }

        #endregion

        #region Overrides of AbstractController

        /// <summary>
        /// Gets the records.
        /// </summary>
        /// <returns></returns>
        protected override List<Table> GetRecords()
        {
            Expression<Func<Table, bool>> filterExpr = null;

            if (!string.IsNullOrEmpty(this.selectionRequestContext.SearchKeyword))
            {
                filterExpr = n => n.Name.Contains(this.selectionRequestContext.SearchKeyword);
            }

            // Get data source from database
            var dataSource = this.UnitOfWork.TableDAO.Select(new EntityQueryArgs<Table>
            {
                StartRecordIndex = this.selectionRequestContext.StartRecordIndex,
                NumRecordsPerPage = this.selectionRequestContext.NumRecordsPerPage,
                OrderByExpr = this.UnitOfWork.TableDAO.BuildOrderByExpression(),
                FilterExpr = filterExpr
            });

            return dataSource;
        }

        /// <summary>
        /// Gets the record by identifier.
        /// </summary>
        /// <param name="recordId">The record identifier.</param>
        /// <returns></returns>
        protected override Table GetRecordById(int recordId)
        {
            return this.UnitOfWork.TableDAO.SelectByID(recordId);
        }

        /// <summary>
        /// Gets the total items.
        /// </summary>
        /// <returns></returns>
        protected override int GetTotalRecords()
        {
            return this.UnitOfWork.TableDAO.TotalRecords;
        }

        /// <summary>
        /// Inserts new record to database
        /// </summary>
        /// <returns>Record identifier</returns>
        protected override Table InsertRecord()
        {
            Table table = this.updateRequestContext.Record;
            table.Status = (int)TableStatus.Available;

            // Mark record has been inserted
            this.UnitOfWork.TableDAO.Insert(table);

            return table;
        }

        /// <summary>
        /// Updates specified record to database
        /// </summary>
        protected override Table UpdateRecord()
        {
            Table reqTable = this.updateRequestContext.Record;

            var table = this.UnitOfWork.TableDAO.SelectByID(reqTable.TableId);

            table.Name = reqTable.Name;

            table.BriefDescription = reqTable.BriefDescription;

            table.NumSeat = reqTable.NumSeat;


            // Mark record has been updated
            this.UnitOfWork.TableDAO.Update(table);

            return table;
        }

        /// <summary>
        /// Deletes the record.
        /// </summary>
        /// <param name="recordId">The record identifier.</param>
        protected override void DeleteRecord(int recordId)
        {
            // Mark record has been deleted
            this.UnitOfWork.TableDAO.Delete(recordId);
        }

        /// <summary>
        /// Determines whether [is exist record] [the specified name].
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        protected override bool IsExistRecord(string name)
        {
            var isExist = this.UnitOfWork.TableDAO.IsExist(name);
            return isExist;
        }

        #endregion
        
    }
}

