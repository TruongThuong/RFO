using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;
using RFO.Common.Utilities.Exceptions;
using RFO.Common.Utilities.Logging;
using RFO.WebAPI.Models;
using RFO.MetaData;
using RFO.AspNet.Utilities.ServerFileHelper;

namespace RFO.WebAPI.Controllers
{
    public abstract class AbstractImageController<T> : AbstractController<T> where T : class
    {
        #region Fields

        /// <summary>
        /// The logger instance
        /// </summary>
        private static readonly ILogger Logger = LoggerManager.GetLogger(typeof(AbstractImageController<T>).Name);

        /// <summary>
        /// The image extensions
        /// </summary>
        private readonly List<string> ImageExtensions = new List<string>
        {
            ".JPG", ".JPE", ".BMP", ".GIF", ".PNG"
        };

        /// <summary>
        /// Storage root path
        /// </summary>
        private string StorageRootPath
        {
            get
            {
                //Path should always end with '/'
                return HttpContext.Current.Server.MapPath(AppConstants.ImageDirName);
            }
        }

        /// <summary>
        /// The js script serializer
        /// </summary>
        private readonly JavaScriptSerializer JSScriptSerializer = new JavaScriptSerializer
        {
            MaxJsonLength = 41943040
        };

        #endregion

        #region Methods

        /// <summary>
        /// Uploads the files.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public virtual HttpResponseMessage UploadFiles()
        {
            var funcName = "UploadFiles";
            Logger.DebugFormat("{0} <-- Start", funcName);
            HttpResponseMessage result = null;

            try
            {
                var statuses = new List<FileStatus>();
                var ownerId = this.GetImageOwnerId(HttpContext.Current.Request.Form);

                if (!string.IsNullOrEmpty(ownerId))
                {
                    for (var i = 0; i < HttpContext.Current.Request.Files.Count; i++)
                    {
                        var file = HttpContext.Current.Request.Files[i];
                        var refFileName = string.Empty;
                        bool uploadResult;

                        if (ImageExtensions.Contains(Path.GetExtension(file.FileName).ToUpper())) // Uploaded file is image
                        {
                            // Decrease image width to less than 1024 before uploading
                            uploadResult = ServerFileHelper.UploadAndResizeFile(file,
                                AppConstants.MaximumImageWidth, this.StorageRootPath, ref refFileName);
                        }
                        else // pdf or zip file
                        {
                            uploadResult = ServerFileHelper.UploadFile(file, this.StorageRootPath, ref refFileName);
                        }

                        if (uploadResult) // Upload successful
                        {
                            this.PostProcessUploading(refFileName, ownerId); // Update image info to DB
                            statuses.Add(new FileStatus(refFileName, file.ContentLength));
                        }
                    }

                    // Save uploaded image info to DB
                    this.UnitOfWork.SaveChanges((exception) =>
                    {
                        // Deleted all uploaded files
                        for (var i = 0; i < HttpContext.Current.Request.Files.Count; i++)
                        {
                            var file = HttpContext.Current.Request.Files[i];
                            var filePath = Path.Combine(this.StorageRootPath, file.FileName);
                            ServerFileHelper.DeleteFile(filePath);
                        }
                        throw new DatabaseException((int)DatabaseErrorCode.UploadImage, exception);
                    });

                    // Write upload status
                    this.WriteJsonIframeSafe(HttpContext.Current, statuses);
                    result = new HttpResponseMessage(HttpStatusCode.OK);
                }
                else // Owner is empty
                {
                    result = new HttpResponseMessage(HttpStatusCode.NotImplemented);
                }
            }
            catch (Exception ex)
            {
                Logger.WarnFormat("{0} - Exception: {1}", funcName, ex);
                result = new HttpResponseMessage(HttpStatusCode.NotImplemented);
            }

            Logger.DebugFormat("{0} --> End", funcName);
            return result;
        }

        #endregion

        #region Internal methods

        /// <summary>
        /// Pres the process uploading.
        /// </summary>
        /// <param name="formData">The form data.</param>
        /// <returns>Owner Id</returns>
        protected abstract string GetImageOwnerId(NameValueCollection formData);

        /// <summary>
        /// Posts the process uploading.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="ownerId">The owner identifier.</param>
        protected abstract void PostProcessUploading(string fileName, string ownerId);

        /// <summary>
        /// Writes the json iframe safe.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="statuses">The statuses.</param>
        protected virtual void WriteJsonIframeSafe(HttpContext context, List<FileStatus> statuses)
        {
            context.Response.AddHeader("Vary", "Accept");
            try
            {
                context.Response.ContentType =
                    context.Request["HTTP_ACCEPT"].Contains("application/json")
                        ? "application/json"
                        : "text/plain";
            }
            catch
            {
                context.Response.ContentType = "text/plain";
            }

            // Serialize .NET object to Json
            var json = this.JSScriptSerializer.Serialize(statuses.ToArray());
            context.Response.Write(json);
        }

        #endregion
    }
}
