

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
using RFO.Common.Utilities.Utilities;

namespace RFO.WebAPI.Controllers
{
    /// <summary>
    /// The controller takes responsibility to handle requests regardless OrderDetail management
    /// </summary>
    public class OrderDetailController : AbstractController<OrderDetail>
    {
        #region Fields
        
        /// <summary>
        /// The logger instance
        /// </summary>
        private static readonly ILogger Logger = LoggerManager.GetLogger(typeof(OrderDetailController).Name);

        #endregion
        
        #region Overrides of AbstractController
        
        /// <summary>
        /// Gets the records.
        /// </summary>
        /// <returns></returns>
        protected override List<OrderDetail> GetRecords()
        {
            Expression<Func<OrderDetail, bool>> filterExpr = n => true;

            int tableId;
            if (this.selectionRequestContext.SearchForeignKeys.ContainsKey("TableIds") &&
                int.TryParse(this.selectionRequestContext.SearchForeignKeys["TableIds"], out tableId))
            {
                filterExpr = filterExpr.And(n => n.Order.TableId.Equals(tableId));
            }

            int orderId;
            if (this.selectionRequestContext.SearchForeignKeys.ContainsKey("OrderIds") &&
                int.TryParse(this.selectionRequestContext.SearchForeignKeys["OrderIds"], out orderId))
            {
                filterExpr = filterExpr.And(n => n.Order.OrderId.Equals(orderId));
            }

            int orderStateId;
            if (this.selectionRequestContext.SearchForeignKeys.ContainsKey("OrderStateIds") &&
                int.TryParse(this.selectionRequestContext.SearchForeignKeys["OrderStateIds"], out orderStateId))
            {
                filterExpr = filterExpr.And(n => n.Order.OrderStateId.Equals(orderStateId));
            }

            // Get data source from database
            var dataSource = this.UnitOfWork.OrderDetailDAO.Select(new EntityQueryArgs<OrderDetail>
            {
                StartRecordIndex = this.selectionRequestContext.StartRecordIndex,
                NumRecordsPerPage = this.selectionRequestContext.NumRecordsPerPage,
                OrderByExpr = this.UnitOfWork.OrderDetailDAO.BuildOrderByExpression(),
                FilterExpr = filterExpr
            });

            return dataSource;
        }

        /// <summary>
        /// Gets the record by identifier.
        /// </summary>
        /// <param name="recordId">The record identifier.</param>
        /// <returns></returns>
        protected override OrderDetail GetRecordById(int recordId)
        {
            return this.UnitOfWork.OrderDetailDAO.SelectByID(recordId);
        }

        /// <summary>
        /// Gets the total items.
        /// </summary>
        /// <returns></returns>
        protected override int GetTotalRecords()
        {
            return this.UnitOfWork.OrderDetailDAO.TotalRecords;
        }

        /// <summary>
        /// Inserts new record to database
        /// </summary>
        /// <returns>Record identifier</returns>
        protected override OrderDetail InsertRecord()
        {
            OrderDetail reqOrderDetail = this.updateRequestContext.Record;

            /* STEP 1: Update table status */
            var tableId = reqOrderDetail.TableId;
            var table = this.UnitOfWork.TableDAO.SelectByID(tableId);
            if (table.Status == (int)TableStatus.Available) // Current available
            {
                table.Status = (int)TableStatus.Using; // Book or Order
                this.UnitOfWork.TableDAO.Update(table);
            }

            /* STEP 2: Get latest order with receiving state of current table, it will be created if not exist */
            var orders = this.UnitOfWork.OrderDAO.Select(new EntityQueryArgs<Order>
            {
                StartRecordIndex = 0,
                NumRecordsPerPage = 1,
                OrderByExpr = this.UnitOfWork.OrderDAO.BuildOrderByExpression(),
                SortDirection = "desc",
                FilterExpr = n => n.TableId.Equals(table.TableId) && n.OrderState.Name.Equals("Accept")
            });

            Order order = null;
            if (!orders.IsNullOrEmpty()) // Order has created before
            {
                order = orders.First();
            }

            if (order == null) // Order has not created yet
            {
                var orderStates = this.UnitOfWork.OrderStateDAO.SelectAll();
                if (!orderStates.Any())
                {
                    throw new ArgumentException("Chưa thiết lập bất cứ trạng thái đơn hàng nào");
                }

                order = new Order()
                {
                    TableId = tableId,
                    OrderStateId = orderStates.First().OrderStateId, // Tiep nhan
                    DeliveryNote = string.Empty,
                    CreatedDate = DateTime.Now,
                };
                this.UnitOfWork.OrderDAO.Insert(order);
            }

            /* STEP 3: Insert order details */
            var product = this.UnitOfWork.ProductDAO.SelectByID(reqOrderDetail.ProductId);
            var orderDetail = new OrderDetail()
            {
                OrderId = order.OrderId,
                ProductId = reqOrderDetail.ProductId,
                Quantity = reqOrderDetail.Quantity,
                Price = product.Price,
            };

            product.Total -= orderDetail.Quantity;
            this.UnitOfWork.OrderDetailDAO.Insert(orderDetail);
            this.UnitOfWork.ProductDAO.Update(product);

            return orderDetail;
        }

        /// <summary>
        /// Updates specified record to database
        /// </summary>
        protected override OrderDetail UpdateRecord()
        {
            OrderDetail reqOrderDetail = this.updateRequestContext.Record;

            var orderDetail = this.UnitOfWork.OrderDetailDAO.SelectByID(reqOrderDetail.OrderDetailId);
            var product = this.UnitOfWork.ProductDAO.SelectByID(orderDetail.ProductId);

            product.Total += orderDetail.Quantity;

            orderDetail.Quantity = reqOrderDetail.Quantity;
            orderDetail.ProductId = reqOrderDetail.ProductId;

            if (product.ProductId != orderDetail.ProductId)
            {
                this.UnitOfWork.ProductDAO.Update(product);
                product = this.UnitOfWork.ProductDAO.SelectByID(orderDetail.ProductId);
                product.Total -= orderDetail.Quantity;
                this.UnitOfWork.ProductDAO.Update(product);
            }
            else
            {
                product.Total -= orderDetail.Quantity;
                this.UnitOfWork.ProductDAO.Update(product);
            }           

            // Mark record has been updated
            this.UnitOfWork.OrderDetailDAO.Update(orderDetail);

            return orderDetail;
        }

        /// <summary>
        /// Deletes the record.
        /// </summary>
        /// <param name="recordId">The record identifier.</param>
        protected override void DeleteRecord(int recordId)
        {

            var orderDetail = this.UnitOfWork.OrderDetailDAO.SelectByID(recordId);
            var product = this.UnitOfWork.ProductDAO.SelectByID(orderDetail.ProductId);
            product.Total += orderDetail.Quantity;

            // Mark record has been deleted
            this.UnitOfWork.OrderDetailDAO.Delete(recordId);
            this.UnitOfWork.ProductDAO.Update(product);
        }

        /// <summary>
        /// Determines whether [is exist record] [the specified name].
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        protected override bool IsExistRecord(string name)
        {
            var isExist = this.UnitOfWork.OrderDetailDAO.IsExist(name);
            return isExist;
        }

        #endregion
        
    }
}

