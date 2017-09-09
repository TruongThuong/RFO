

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
using RFO.WebAPI.Models;
using RFO.Common.Utilities.Utilities;

namespace RFO.WebAPI.Controllers
{
    /// <summary>
    /// The controller takes responsibility to handle requests regardless Table management
    /// </summary>
    public class TableOrderController : AbstractController<Table>
    {
        #region Fields

        /// <summary>
        /// The logger instance
        /// </summary>
        private static readonly ILogger Logger = LoggerManager.GetLogger(typeof(TableOrderController).Name);

        #endregion

        #region Overrides of AbstractController

        /// <summary>
        /// Gets the records.
        /// </summary>
        /// <returns></returns>
        protected override List<TableOrder> GetRecords()
        {
            var dataSource = new List<TableOrder>();

            // Get data source from database
            var tables = this.UnitOfWork.TableDAO.SelectAll();
            foreach (var table in tables)
            {
                dataSource.Add(new TableOrder
                {
                    TableId = table.TableId,
                    TableName = table.Name,
                    Status = table.Status
                });
            }
            return dataSource;
        }

        /// <summary>
        /// Gets the record by identifier.
        /// </summary>
        /// <param name="recordId">The record identifier.</param>
        /// <returns></returns>
        protected override TableOrder GetRecordById(int recordId)
        {
            return null;
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
        protected override TableOrder InsertRecord()
        {
            return null;
        }

        /// <summary>
        /// Updates specified record to database
        /// </summary>
        protected override TableOrder UpdateRecord()
        {
            TableOrder tableOrder = this.updateRequestContext.Record;

            // Update table status
            var table = this.UnitOfWork.TableDAO.SelectByID(tableOrder.TableId);
            if (table.Status != tableOrder.SelectedStatus) // Update status
            {
                table.Status = tableOrder.SelectedStatus; // Book or Order
                this.UnitOfWork.TableDAO.Update(table);
            }

            // Insert order
            var orders = this.UnitOfWork.OrderDAO.Select(new EntityQueryArgs<Order>
            {
                StartRecordIndex = 0,
                NumRecordsPerPage = 1,
                OrderByExpr = n => n.CreatedDate,
                SortDirection = "desc",
                FilterExpr = n => n.TableId.Equals(tableOrder.TableId) && n.OrderState.Name.Equals("Tiếp nhận")
            });

            Order order = null;
            if (!orders.IsNullOrEmpty()) // Order has created before
            {
                order = orders.First();
            }

            var orderStates = this.UnitOfWork.OrderStateDAO.SelectAll();
            if (!orderStates.Any())
            {
                throw new ArgumentException("Chưa thiết lập bất cứ trạng thái đơn hàng nào");
            }

            if (!tableOrder.SelectedTableOrderInfoList.IsNullOrEmpty())
            {
                if (order == null) // Order has not created yet
                {
                    order = new Order()
                    {
                        TableId = tableOrder.TableId,
                        OrderStateId = orderStates.First().OrderStateId, // Tiep nhan
                        DeliveryNote = string.Empty,
                        CreatedDate = DateTime.Now,
                    };
                    this.UnitOfWork.OrderDAO.Insert(order);
                }

                // Insert order details
                foreach (var tableOrderInfo in tableOrder.SelectedTableOrderInfoList)
                {
                    var orderDetail = new OrderDetail()
                    {
                        OrderId = order.OrderId,
                        ProductId = tableOrderInfo.ProductId,
                        Quantity = tableOrderInfo.Quantity,
                        Price = tableOrderInfo.Price,
                    };
                    this.UnitOfWork.OrderDetailDAO.Insert(orderDetail);
                }
            }

            // Update order state when table available
            if (tableOrder.SelectedStatus == (int)TableStatus.Available && order != null)
            {
                order.OrderStateId = orderStates.Last().OrderStateId;
            }

            return tableOrder;
        }

        /// <summary>
        /// Deletes the record.
        /// </summary>
        /// <param name="recordId">The record identifier.</param>
        protected override void DeleteRecord(int recordId)
        {
            // Nothing to implement
        }

        /// <summary>
        /// Determines whether [is exist record] [the specified name].
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        protected override bool IsExistRecord(string name)
        {
            return false;
        }

        #endregion

    }
}

