// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AbstractController.cs" company="TNL">
//   Copyright (c) TNL, 2016
//   All rights are reserved. Reproduction or transmission in whole or in part, in
//   any form or by any means, electronic, mechanical or otherwise, is prohibited
//   without the prior written consent of the copyright owner.
// </copyright>
// <summary>
//   Defines the AbstractController type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Http;
using RFO.Common.Utilities.Exceptions;
using RFO.Common.Utilities.Logging;
using RFO.DAO;
using RFO.WebAPI.Models.Request;
using RFO.MetaData;
using RFO.WebAPI.Models.Response;
using RFO.AspNet.Utilities.HttpActionResultBuilder;
using System.Net.Http;
using System.Web;
using System.IO;
using RFO.AspNet.Utilities.ServerFileHelper;
using System.Drawing;
using System.Net;
using System.Net.Http.Headers;

namespace RFO.WebAPI.Controllers
{
    public abstract class AbstractController<T> : ApiController where T : class
    {
        #region Variables

        /// <summary>
        /// The logger instance
        /// </summary>
        private static readonly ILogger Logger = LoggerManager.GetLogger(typeof(AbstractController<T>).Name);

        /// <summary>
        /// Gets the PMS unit of work.
        /// </summary>
        /// <value>
        /// The PMS unit of work.
        /// </value>
        protected readonly UnitOfWork UnitOfWork = new UnitOfWork();

        /// <summary>
        /// The selection request context
        /// </summary>
        protected SelectionRequestContext selectionRequestContext = new SelectionRequestContext
        {
            Session = 1,
            StartRecordIndex = 0,
            NumRecordsPerPage = AppConstants.MaxRecordsPerPage,
            SortColumnIndex = 0,
            SortDirection = "asc"
        };

        /// <summary>
        /// The update request context
        /// </summary>
        protected UpdateRequestContext<T> updateRequestContext = null;

        #endregion

        #region Public Methods

        /// <summary>
        /// Selects all.
        /// </summary>
        /// <returns>IHttpActionResult</returns>
        [HttpGet]
        public virtual IHttpActionResult SelectAll()
        {
            var funcName = "SelectAll";
            Logger.DebugFormat("{0} <-- Start", funcName);
            IHttpActionResult result;

            try
            {
                var records = this.GetRecords();
                var totalRecords = this.GetTotalRecords();

                // Customize records if neccessary
                records = this.CustomizeRecords(records, ref totalRecords);

                // Serialize return data
                var responseContext = new SelectionResponseContext<T>
                {
                    Session = this.selectionRequestContext.Session,
                    NumTotalRecords = totalRecords,
                    Records = records,
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

        /// <summary>
        /// Selects the specified request context.
        /// </summary>
        /// <returns>IHttpActionResult</returns>
        [HttpPost]
        public virtual IHttpActionResult Select(SelectionRequestContext requestContext)
        {
            var funcName = "Select";
            Logger.DebugFormat("{0} <-- Start", funcName);
            IHttpActionResult result;

            try
            {
                if (requestContext == null)
                {
                    throw new ArgumentException("Could not get request context");
                }

                // Keeps input request for using later
                this.selectionRequestContext = requestContext;

                // Get datasource from database
                var records = this.GetRecords();
                var totalRecords = this.GetTotalRecords();

                // Customize records if neccessary
                records = this.CustomizeRecords(records, ref totalRecords);

                // Serialize return data
                var responseContext = new SelectionResponseContext<T>
                {
                    Session = this.selectionRequestContext.Session,
                    NumTotalRecords = totalRecords,
                    Records = records,
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

        /// <summary>
        /// Select specified element of specified table
        /// </summary>
        /// <param name="id">The key for identifying an element</param>
        /// <returns>Result of an action method</returns>
        [HttpGet]
        public virtual IHttpActionResult SelectByID(int id)
        {
            var funcName = "SelectByID";
            Logger.DebugFormat("{0} <-- Start", funcName);
            Logger.DebugFormat("{0} - Input params: id={1}", funcName, id);
            IHttpActionResult result;

            try
            {
                // Get record by identifier from database
                var record = this.GetRecordById(id);

                // Serialize return data
                var responseContext = new SelectionResponseContext<T>
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

        /// <summary>
        /// Inserts the specified request contexts.
        /// </summary>
        /// <param name="requestContext">The request context.</param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult Insert(UpdateRequestContext<T> requestContext)
        {
            var funcName = "Insert";
            Logger.DebugFormat("{0} <-- Start", funcName);
            IHttpActionResult result;

            try
            {
                if (requestContext == null)
                {
                    throw new ArgumentException("Could not get request context");
                }

                // Keeps input request for using later
                this.updateRequestContext = requestContext;

                // Insert record to database
                var record = this.InsertRecord();

                // Commit changed
                this.UnitOfWork.SaveChanges((exception) =>
                {
                    throw new DatabaseException((int)DatabaseErrorCode.Insert, exception);
                });

                // Build action result
                var responseContext = new UpdateResponseContext<T>
                {
                    Record = record,
                    Result = true,
                    Description = "Dữ liệu đã được thêm"
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

        /// <summary>
        /// Updates the specified request context.
        /// </summary>
        /// <param name="requestContext">The request context.</param>
        /// <returns></returns>
        [HttpPost]
        public virtual IHttpActionResult Update(UpdateRequestContext<T> requestContext)
        {
            var funcName = "Update";
            Logger.DebugFormat("{0} <-- Start", funcName);
            IHttpActionResult result;

            try
            {
                if (requestContext == null)
                {
                    throw new ArgumentException("Could not get request context");
                }

                // Keeps input request for using later
                this.updateRequestContext = requestContext;

                // Update record to database
                var record = this.UpdateRecord();

                // Commit changed
                this.UnitOfWork.SaveChanges((exception) =>
                {
                    throw new DatabaseException((int)DatabaseErrorCode.Update, exception);
                });

                // Build action result
                var responseContext = new UpdateResponseContext<T>
                {
                    Record = record,
                    Result = true,
                    Description = "Dữ liệu đã được cập nhật"
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

        /// <summary>
        /// The action to delete specified record in corresponding table
        /// </summary>
        /// <param name="id">The key for identifying the element will be deleted</param>
        /// <returns>Result of an action method</returns>
        [HttpGet]
        public IHttpActionResult Delete(int id)
        {
            var funcName = "Delete";
            Logger.DebugFormat("{0} <-- Start", funcName);
            IHttpActionResult result;

            try
            {
                // Delete ProductCategory item in database
                this.DeleteRecord(id);

                // Commit changed
                this.UnitOfWork.SaveChanges((exception) =>
                {
                    throw new DatabaseException((int)DatabaseErrorCode.Delete, exception);
                });

                // Build action result
                var responseContext = new UpdateResponseContext<T>
                {
                    Result = true,
                    Description = "Dữ liệu đã được xóa"
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

        /// <summary>
        /// Determines whether the specified specification attribute is exist.
        /// </summary>
        /// <param name="name">The specification attribute.</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult IsExist(string name)
        {
            var funcName = "IsExist";
            Logger.DebugFormat("{0} <-- Start", funcName);
            Logger.DebugFormat("{0} - input params: Name={0}", name);
            IHttpActionResult result;

            try
            {
                // True: exist - False: otherwise
                var isExist = this.IsExistRecord(name);
                result = HttpActionResultBuilder.BuildJsonContentResult(this.Request, isExist);
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("{0} - Exception: {1}", funcName, ex);
                result = HttpActionResultBuilder.BuildExceptionResult(this.Request, ex);
            }

            Logger.DebugFormat("{0} --> End", funcName);
            return result;
        }

        /// <summary>
        /// Determines whether the specified specification attribute is exist.
        /// </summary>
        /// <param name="name">The specification attribute.</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult IsNotExist(string name)
        {
            var funcName = "IsNotExist";
            Logger.DebugFormat("{0} <-- Start", funcName);
            Logger.DebugFormat("{0} - input params: Name={0}", name);
            IHttpActionResult result;

            try
            {
                // True: not exist - False: otherwise
                var isExist = !this.IsExistRecord(name);
                result = HttpActionResultBuilder.BuildJsonContentResult(this.Request, isExist);
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("{0} - Exception: {1}", funcName, ex);
                result = HttpActionResultBuilder.BuildExceptionResult(this.Request, ex);
            }

            Logger.DebugFormat("{0} --> End", funcName);
            return result;
        }

        /// <summary>
        /// Gets the image.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <returns></returns>
        [HttpGet]
        public virtual HttpResponseMessage GetImage(string filename, int width = 0, int height = 0)
        {
            var funcName = "GetImage";
            Logger.DebugFormat("{0} <-- Start", funcName);
            Logger.DebugFormat("{0} - filename=[{1}], width={2}, height={3}", new object[] { funcName, filename, width, height });

            HttpResponseMessage result = null;
            try
            {
                var useDefaultImage = false;
                if (!string.IsNullOrEmpty(filename))
                {
                    var filepath = Path.Combine(HttpContext.Current.Server.MapPath(AppConstants.ImageDirName), filename);
                    if (File.Exists(filepath))
                    {
                        result = this.GetImageResponseMessage(filepath, width, height);
                    }
                    else // Not found image file
                    {
                        useDefaultImage = true;
                    }
                }
                else // Empty image file
                {
                    useDefaultImage = true;
                }

                if (useDefaultImage) // Using default image
                {
                    var noImageFile = HttpContext.Current.Server.MapPath(AppConstants.NoImageFileName);
                    result = this.GetImageResponseMessage(noImageFile, width, height);
                }
            }
            catch (Exception ex)
            {
                Logger.WarnFormat("{0} - Exception: {1}", funcName, ex);
                result = new HttpResponseMessage(HttpStatusCode.NoContent);
            }

            Logger.DebugFormat("{0} --> End", funcName);
            return result;
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Gets the records.
        /// </summary>
        /// <returns></returns>
        protected abstract List<T> GetRecords();

        /// <summary>
        /// Gets the record by identifier.
        /// </summary>
        /// <param name="recordId">The record identifier.</param>
        /// <returns></returns>
        protected abstract T GetRecordById(int recordId);

        /// <summary>
        /// Gets the total items.
        /// </summary>
        /// <returns></returns>
        protected abstract int GetTotalRecords();

        /// <summary>
        /// Updates the record.
        /// </summary>
        /// <returns></returns>
        protected abstract T UpdateRecord();

        /// <summary>
        /// Inserts the record.
        /// </summary>
        /// <returns>Record identifier</returns>
        protected abstract T InsertRecord();

        /// <summary>
        /// Deletes the record.
        /// </summary>
        /// <param name="recordId">The record identifier.</param>
        protected abstract void DeleteRecord(int recordId);

        /// <summary>
        /// Determines whether [is exist record] [the specified name].
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        protected abstract bool IsExistRecord(string name);

        /// <summary>
        /// Customizes the records.
        /// </summary>
        /// <param name="records">The records.</param>
        /// <param name="totalRecords">The total records.</param>
        /// <returns></returns>
        protected virtual List<T> CustomizeRecords(List<T> records, ref int totalRecords)
        {
            return records;
        }

        /// <summary>
        /// Gets the image response message.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <returns></returns>
        protected virtual HttpResponseMessage GetImageResponseMessage(string filePath, int width = 0, int height = 0)
        {
            HttpResponseMessage result;
            var fileInfo = new FileInfo(filePath);
            var resizedFilePath = ServerFileHelper.GetResizedImagePath(filePath, width, height);

            byte[] resizedImageByteArray;
            using (Image resizedImage = Image.FromFile(resizedFilePath))
            {
                resizedImageByteArray = ServerFileHelper.ConvertImageToByteArray(resizedImage, fileInfo.Extension);
            }

            if (resizedImageByteArray != null && resizedImageByteArray.Length > 0)
            {
                result = new HttpResponseMessage(HttpStatusCode.OK);
                MemoryStream dataStream = new MemoryStream(resizedImageByteArray);
                result.Content = new StreamContent(dataStream);
                result.Content.Headers.ContentType = new MediaTypeHeaderValue(ServerFileHelper.ConvertImageExtToMediaTypeHeaderValue(fileInfo.Extension));
            }
            else // Failed to read image
            {
                result = new HttpResponseMessage(HttpStatusCode.NoContent);
            }
            return result;
        }

        #endregion
    }
}