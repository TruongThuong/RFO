

using System;
using System.Linq;
using System.Linq.Expressions;
using System.ComponentModel.Composition;
using RFO.Model;
using RFO.Common.Utilities.Logging;

namespace RFO.DAO
{
    /// <summary>
    /// The class is responsible for CRUD operations of Menu table in database
    /// </summary>
    [Export(typeof(IMenuDAO))]
    public class MenuDAO : AbstractDAO<Menu>, IMenuDAO
    {
        #region Fields
        
        /// <summary>
        /// The logger instance
        /// </summary>
        private static readonly ILogger Logger = LoggerManager.GetLogger(typeof(MenuDAO).Name);

        #endregion
        
        #region Constructor
        
        /// <summary>
        /// Initializes a new instance of the <see cref="MenuDAO"/> class.
        /// </summary>
        /// <param name="inDbContext">The database context.</param>
        public MenuDAO(RFODbContext inDbContext) : base(inDbContext)
        {
        }

        #endregion
        
        #region Overrides of AbstractDAO
        
        /// <summary>
        /// Builds the order by expression.
        /// </summary>
        /// <returns></returns>
        public override Expression<Func<Menu, object>> BuildOrderByExpression()
        {
            return n => new { n.MenuId };
        }

        /// <summary>
        /// Builds the searching by identifier expression.
        /// </summary>
        /// <param name="recordId">The record identifier.</param>
        /// <returns></returns>
        protected override Expression<Func<Menu, bool>> BuildSearchingByIdExpression(int recordId)
        {
            return n => n.MenuId.Equals(recordId);
        }

        /// <summary>
        /// Builds the existent validation expression.
        /// </summary>
        /// <param name="specificationAttr">The specification attribute.</param>
        /// <returns></returns>
        protected override Expression<Func<Menu, bool>> BuildExistentValidationExpression(string specificationAttr)
        {
            Expression<Func<Menu, bool>> validateExpression =
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
        protected override IQueryable<Menu> BuildIncludesQueryable(IQueryable<Menu> records,
            bool loadReference = true, bool loadChilds = false)
        {
            var result = records;
            if (loadReference)
            {
                
            }
            if (loadChilds)
            {
                
            }
            return result;
        }

        #endregion
        
    }
}

