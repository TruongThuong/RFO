
using Microsoft.Practices.ServiceLocation;
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using RFO.AspNet.Utilities.ServerFileHelper;
using RFO.Common.Utilities.ImageResizer;
using RFO.Common.Utilities.Logging;

namespace RFO.AspNet.Utilities.MVCExtension
{
    /// <summary>
    /// The class is used to send contents of a file to the response.
    /// </summary>
    public class ImageResult : FilePathResult
    {
        #region Constants

        /// <summary>
        /// The image directory
        /// </summary>
        private const string IMAGE_DIR = "image";

        #endregion

        #region Fields

        /// <summary>
        /// The logger instance
        /// </summary>
        public static readonly ILogger Logger = LoggerManager.GetLogger(typeof(ImageResult).Name);

        /// <summary>
        /// The object for synchronization
        /// </summary>
        private static readonly object syncLock = new object();

        /// <summary>
        /// The file path
        /// </summary>
        private readonly string filePath;

        /// <summary>
        /// The image height
        /// </summary>
        private readonly int height;

        /// <summary>
        /// The image width
        /// </summary>
        private readonly int width;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageResult" /> class.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public ImageResult(string filePath, int width = 0, int height = 0) :
            base(filePath, string.Format("{0}/{1}", IMAGE_DIR, Path.GetExtension(filePath)))
        {
            this.filePath = filePath;
            this.width = width;
            this.height = height;
        }

        #endregion

        #region Overrides of FilePathResult

        /// <summary>
        /// Writes the file to the response.
        /// </summary>
        /// <param name="response">The response.</param>
        protected override void WriteFile(HttpResponseBase response)
        {
            lock (syncLock)
            {
                var resizedFilePath = ServerFileHelper.ServerFileHelper.GetResizedImagePath(this.filePath, this.width, this.height);
                response.SetDefaultImageHeaders(resizedFilePath);
                this.WriteFileToResponse(resizedFilePath, response);
            }
        }

        #endregion

        #region Help methods
        
        /// <summary>
        /// Writes the file to response.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="response">The response.</param>
        private void WriteFileToResponse(string filePath, HttpResponseBase response)
        {
            try
            {
                using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    const int bufferLength = 65536;
                    var buffer = new byte[bufferLength];

                    while (true)
                    {
                        var bytesRead = fs.Read(buffer, 0, bufferLength);

                        if (bytesRead == 0)
                        {
                            break;
                        }

                        response.OutputStream.Write(buffer, 0, bytesRead);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("Failed to write file to response - error: {0}", ex.ToString());
            }
        }

        #endregion
    }
}