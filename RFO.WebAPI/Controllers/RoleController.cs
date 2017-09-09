

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
    /// The controller takes responsibility to handle requests regardless Role management
    /// </summary>
    public class RoleController : AbstractController<Role>
    {
        #region Fields
        
        /// <summary>
        /// The logger instance
        /// </summary>
        private static readonly ILogger Logger = LoggerManager.GetLogger(typeof(RoleController).Name);

        #endregion
        
        #region Overrides of AbstractController
        
        /// <summary>
        /// Gets the records.
        /// </summary>
        /// <returns></returns>
        protected override List<Role> GetRecords()
        {
            Expression<Func<Role, bool>> filterExpr = null;

            if (!string.IsNullOrEmpty(this.selectionRequestContext.SearchKeyword))
            {
                filterExpr = n => n.Name.Contains(this.selectionRequestContext.SearchKeyword);
            }

            // Get data source from database
            var dataSource = this.UnitOfWork.RoleDAO.Select(new EntityQueryArgs<Role>
            {
                StartRecordIndex = this.selectionRequestContext.StartRecordIndex,
                NumRecordsPerPage = this.selectionRequestContext.NumRecordsPerPage,
                OrderByExpr = this.UnitOfWork.RoleDAO.BuildOrderByExpression(),
                FilterExpr = filterExpr
            });

            return dataSource;
        }

        /// <summary>
        /// Gets the record by identifier.
        /// </summary>
        /// <param name="recordId">The record identifier.</param>
        /// <returns></returns>
        protected override Role GetRecordById(int recordId)
        {
            return this.UnitOfWork.RoleDAO.SelectByID(recordId);
        }

        /// <summary>
        /// Gets the total items.
        /// </summary>
        /// <returns></returns>
        protected override int GetTotalRecords()
        {
            return this.UnitOfWork.RoleDAO.TotalRecords;
        }

        /// <summary>
        /// Inserts new record to database
        /// </summary>
        /// <returns>Record identifier</returns>
        protected override Role InsertRecord()
        {
            Role role = this.updateRequestContext.Record;

            // Mark record has been inserted
            this.UnitOfWork.RoleDAO.Insert(role);

            return role;
        }

        /// <summary>
        /// Updates specified record to database
        /// </summary>
        protected override Role UpdateRecord()
        {
            Role reqRole = this.updateRequestContext.Record;

            var role = this.UnitOfWork.RoleDAO.SelectByID(reqRole.RoleId);

            role.Name = reqRole.Name;

            role.BriefDescription = reqRole.BriefDescription;


            // Mark record has been updated
            this.UnitOfWork.RoleDAO.Update(role);

            return role;
        }

        /// <summary>
        /// Deletes the record.
        /// </summary>
        /// <param name="recordId">The record identifier.</param>
        protected override void DeleteRecord(int recordId)
        {
            // Mark record has been deleted
            this.UnitOfWork.RoleDAO.Delete(recordId);
        }

        /// <summary>
        /// Determines whether [is exist record] [the specified name].
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        protected override bool IsExistRecord(string name)
        {
            var isExist = this.UnitOfWork.RoleDAO.IsExist(name);
            return isExist;
        }

        #endregion
        
    }
}

