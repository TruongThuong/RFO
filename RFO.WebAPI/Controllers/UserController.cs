

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
using System.Web.Http;
using RFO.WebAPI.Models.Response;
using RFO.AspNet.Utilities.HttpActionResultBuilder;

namespace RFO.WebAPI.Controllers
{
    /// <summary>
    /// The controller takes responsibility to handle requests regardless User management
    /// </summary>
    public class UserController : AbstractController<User>
    {
        #region Fields
        
        /// <summary>
        /// The logger instance
        /// </summary>
        private static readonly ILogger Logger = LoggerManager.GetLogger(typeof(UserController).Name);

        #endregion

        #region Public actions

        /// <summary>
        /// Select specified element of specified table
        /// </summary>
        /// <param name="id">The key for identifying an element</param>
        /// <returns>Result of an action method</returns>
        [HttpGet]
        public virtual IHttpActionResult SelectByUserNameAndPassword(string username, string password)
        {
            var funcName = "SelectByUserNameAndPassword";
            Logger.DebugFormat("{0} <-- Start", funcName);
            Logger.DebugFormat("{0} - Input params: username={1}, password={2}", funcName, username, password);
            IHttpActionResult result;

            try
            {
                // Get record by identifier from database
                var records = this.UnitOfWork.UserDAO.Select(new EntityQueryArgs<User>
                {
                    StartRecordIndex = this.selectionRequestContext.StartRecordIndex,
                    NumRecordsPerPage = this.selectionRequestContext.NumRecordsPerPage,
                    OrderByExpr = this.UnitOfWork.UserDAO.BuildOrderByExpression(),
                    FilterExpr = n => n.UserName.Equals(username, StringComparison.OrdinalIgnoreCase) &&
                                      n.Password.Equals(password, StringComparison.OrdinalIgnoreCase)
                });
                var record = records.FirstOrDefault();

                // Serialize return data
                var responseContext = new SelectionResponseContext<User>
                {
                    Record = record,
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
        protected override List<User> GetRecords()
        {
            Expression<Func<User, bool>> filterExpr = null;

            if (!string.IsNullOrEmpty(this.selectionRequestContext.SearchKeyword))
            {
                filterExpr = n => n.UserName.Contains(this.selectionRequestContext.SearchKeyword);
            }

            // Get data source from database
            var dataSource = this.UnitOfWork.UserDAO.Select(new EntityQueryArgs<User>
            {
                StartRecordIndex = this.selectionRequestContext.StartRecordIndex,
                NumRecordsPerPage = this.selectionRequestContext.NumRecordsPerPage,
                OrderByExpr = this.UnitOfWork.UserDAO.BuildOrderByExpression(),
                FilterExpr = filterExpr
            });

            return dataSource;
        }

        /// <summary>
        /// Gets the record by identifier.
        /// </summary>
        /// <param name="recordId">The record identifier.</param>
        /// <returns></returns>
        protected override User GetRecordById(int recordId)
        {
            return this.UnitOfWork.UserDAO.SelectByID(recordId);
        }

        /// <summary>
        /// Gets the total items.
        /// </summary>
        /// <returns></returns>
        protected override int GetTotalRecords()
        {
            return this.UnitOfWork.UserDAO.TotalRecords;
        }

        /// <summary>
        /// Inserts new record to database
        /// </summary>
        /// <returns>Record identifier</returns>
        protected override User InsertRecord()
        {
            User user = this.updateRequestContext.Record;

            // Mark record has been inserted
            this.UnitOfWork.UserDAO.Insert(user);

            return user;
        }

        /// <summary>
        /// Updates specified record to database
        /// </summary>
        protected override User UpdateRecord()
        {
            User reqUser = this.updateRequestContext.Record;

            var user = this.UnitOfWork.UserDAO.SelectByID(reqUser.UserId);

            user.UserName = reqUser.UserName;

            user.Password = reqUser.Password;

            user.Email = reqUser.Email;

            user.IsActive = reqUser.IsActive;

            user.FullName = reqUser.FullName;

            user.Phone = reqUser.Phone;

            user.Address = reqUser.Address;

            user.AvatarFile = reqUser.AvatarFile;


            // Mark record has been updated
            this.UnitOfWork.UserDAO.Update(user);

            return user;
        }

        /// <summary>
        /// Deletes the record.
        /// </summary>
        /// <param name="recordId">The record identifier.</param>
        protected override void DeleteRecord(int recordId)
        {
            // Mark record has been deleted
            this.UnitOfWork.UserDAO.Delete(recordId);
        }

        /// <summary>
        /// Determines whether [is exist record] [the specified name].
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        protected override bool IsExistRecord(string name)
        {
            var isExist = this.UnitOfWork.UserDAO.IsExist(name);
            return isExist;
        }

        #endregion
        
    }
}

