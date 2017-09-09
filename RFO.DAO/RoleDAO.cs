﻿

using System;
using System.Linq;
using System.Linq.Expressions;
using System.ComponentModel.Composition;
using RFO.Model;
using RFO.Common.Utilities.Logging;

namespace RFO.DAO
{
    /// <summary>
    /// The class is responsible for CRUD operations of Role table in database
    /// </summary>
    [Export(typeof(IRoleDAO))]
    public class RoleDAO : AbstractDAO<Role>, IRoleDAO
    {
        #region Fields
        
        /// <summary>
        /// The logger instance
        /// </summary>
        private static readonly ILogger Logger = LoggerManager.GetLogger(typeof(RoleDAO).Name);

        #endregion
        
        #region Constructor
        
        /// <summary>
        /// Initializes a new instance of the <see cref="RoleDAO"/> class.
        /// </summary>
        /// <param name="inDbContext">The database context.</param>
        public RoleDAO(RFODbContext inDbContext) : base(inDbContext)
        {
        }

        #endregion
        
        #region Overrides of AbstractDAO
        
        /// <summary>
        /// Builds the order by expression.
        /// </summary>
        /// <returns></returns>
        public override Expression<Func<Role, object>> BuildOrderByExpression()
        {
            return n => new { n.RoleId };
        }

        /// <summary>
        /// Builds the searching by identifier expression.
        /// </summary>
        /// <param name="recordId">The record identifier.</param>
        /// <returns></returns>
        protected override Expression<Func<Role, bool>> BuildSearchingByIdExpression(int recordId)
        {
            return n => n.RoleId.Equals(recordId);
        }

        /// <summary>
        /// Builds the existent validation expression.
        /// </summary>
        /// <param name="specificationAttr">The specification attribute.</param>
        /// <returns></returns>
        protected override Expression<Func<Role, bool>> BuildExistentValidationExpression(string specificationAttr)
        {
            Expression<Func<Role, bool>> validateExpression =
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
        protected override IQueryable<Role> BuildIncludesQueryable(IQueryable<Role> records,
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

