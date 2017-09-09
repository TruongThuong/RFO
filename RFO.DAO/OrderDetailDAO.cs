

using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.ComponentModel.Composition;
using RFO.Model;
using RFO.Common.Utilities.Logging;

namespace RFO.DAO
{
    /// <summary>
    /// The class is responsible for CRUD operations of OrderDetail table in database
    /// </summary>
    [Export(typeof(IOrderDetailDAO))]
    public class OrderDetailDAO : AbstractDAO<OrderDetail>, IOrderDetailDAO
    {
        #region Fields
        
        /// <summary>
        /// The logger instance
        /// </summary>
        private static readonly ILogger Logger = LoggerManager.GetLogger(typeof(OrderDetailDAO).Name);

        #endregion
        
        #region Constructor
        
        /// <summary>
        /// Initializes a new instance of the <see cref="OrderDetailDAO"/> class.
        /// </summary>
        /// <param name="inDbContext">The database context.</param>
        public OrderDetailDAO(RFODbContext inDbContext) : base(inDbContext)
        {
        }

        #endregion
        
        #region Overrides of AbstractDAO
        
        /// <summary>
        /// Builds the order by expression.
        /// </summary>
        /// <returns></returns>
        public override Expression<Func<OrderDetail, object>> BuildOrderByExpression()
        {
            return n => new { n.OrderDetailId };
        }

        /// <summary>
        /// Builds the searching by identifier expression.
        /// </summary>
        /// <param name="recordId">The record identifier.</param>
        /// <returns></returns>
        protected override Expression<Func<OrderDetail, bool>> BuildSearchingByIdExpression(int recordId)
        {
            return n => n.OrderDetailId.Equals(recordId);
        }

        /// <summary>
        /// Builds the existent validation expression.
        /// </summary>
        /// <param name="specificationAttr">The specification attribute.</param>
        /// <returns></returns>
        protected override Expression<Func<OrderDetail, bool>> BuildExistentValidationExpression(string specificationAttr)
        {
            return null;
        }

        /// <summary>
        /// Builds the includes queryable.
        /// </summary>
        /// <param name="records">The records.</param>
        /// <param name="loadReference">if set to <c>true</c> [load reference].</param>
        /// <param name="loadChilds">if set to <c>true</c> [load childs].</param>
        /// <returns></returns>
        protected override IQueryable<OrderDetail> BuildIncludesQueryable(IQueryable<OrderDetail> records,
            bool loadReference = true, bool loadChilds = false)
        {
            var result = records;
            if (loadReference)
            {
                result = result.Include(n => n.Order).Include(n => n.Order.Table).Include(n => n.Order.OrderState).Include(n => n.Product).Include(n => n.Product.Menu);
            }
            if (loadChilds)
            {
                
            }
            return result;
        }

        #endregion
        
    }
}

