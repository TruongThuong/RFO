

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
    /// The class is responsible for CRUD operations of Product table in database
    /// </summary>
    [Export(typeof(IProductDAO))]
    public class ProductDAO : AbstractDAO<Product>, IProductDAO
    {
        #region Fields
        
        /// <summary>
        /// The logger instance
        /// </summary>
        private static readonly ILogger Logger = LoggerManager.GetLogger(typeof(ProductDAO).Name);

        #endregion
        
        #region Constructor
        
        /// <summary>
        /// Initializes a new instance of the <see cref="ProductDAO"/> class.
        /// </summary>
        /// <param name="inDbContext">The database context.</param>
        public ProductDAO(RFODbContext inDbContext) : base(inDbContext)
        {
        }

        #endregion
        
        #region Overrides of AbstractDAO
        
        /// <summary>
        /// Builds the order by expression.
        /// </summary>
        /// <returns></returns>
        public override Expression<Func<Product, object>> BuildOrderByExpression()
        {
            return n => new { n.ProductId };
        }

        /// <summary>
        /// Builds the searching by identifier expression.
        /// </summary>
        /// <param name="recordId">The record identifier.</param>
        /// <returns></returns>
        protected override Expression<Func<Product, bool>> BuildSearchingByIdExpression(int recordId)
        {
            return n => n.ProductId.Equals(recordId);
        }

        /// <summary>
        /// Builds the existent validation expression.
        /// </summary>
        /// <param name="specificationAttr">The specification attribute.</param>
        /// <returns></returns>
        protected override Expression<Func<Product, bool>> BuildExistentValidationExpression(string specificationAttr)
        {
            Expression<Func<Product, bool>> validateExpression =
                n => n.Name.Equals(specificationAttr);

            return validateExpression;
        }

        /// <summary>
        /// Builds the includes queryable.
        /// </summary>
        /// <param name="records">The records.</param>
        /// <param name="loadReference">if set to <c>true</c> [load reference].</param>
        /// <param name="loadChilds">if set to <c>true</c> [load childs].</param>
        /// <returns></returns>
        protected override IQueryable<Product> BuildIncludesQueryable(IQueryable<Product> records,
            bool loadReference = true, bool loadChilds = false)
        {
            var result = records;
            if (loadReference)
            {
                result = result.Include(n => n.Menu);
            }
            if (loadChilds)
            {
                result = result.Include(n => n.ProductImages);
            }
            return result;
        }

        #endregion
        
    }
}

