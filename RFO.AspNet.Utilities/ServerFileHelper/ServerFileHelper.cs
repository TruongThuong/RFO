
using System;
using System.ComponentModel.Composition;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using RFO.Common.Utilities.ImageResizer;
using RFO.Common.Utilities.Logging;

namespace RFO.AspNet.Utilities.ServerFileHelper
{
    /// <summary>
    /// Utitlies for file operations
    /// </summary>
    public static class ServerFileHelper
    {
        #region Constants

        /// <summary>
        /// The image resized directory
        /// </summary>
        private const string IMAGE_RESIZED_DIR = "resized";

        #endregion

        #region Fields

        /// <summary>
        /// The logger instance
        /// </summary>
        public static readonly ILogger Logger = LoggerManager.GetLogger(typeof (ServerFileHelper).Name);

        #endregion

        #region Implementation of IServerFileHelper

        /// <summary>
        /// The method for handling upload a file to server
        /// </summary>
        /// <param name="file">
        /// The file will be uploaded to server
        /// </param>
        /// <param name="dirPath">
        /// The directory which the file will be stored in
        /// </param>
        /// <param name="fileName">
        /// FileName will be created based on DateTime.Now
        /// as binary string and uploaded file extension
        /// </param>
        public static bool UploadFile(HttpPostedFileBase file, string dirPath, ref string fileName)
        {
            var result = true;
            try
            {
                var fileInfo = new FileInfo(file.FileName);
                fileName = DateTime.Now.ToBinary() + fileInfo.Extension;
                var fullPath = Path.Combine(dirPath, fileName);
                file.SaveAs(fullPath);
            }
            catch (Exception ex)
            {
                result = false;
                Logger.ErrorFormat("UploadFile: {0}", ex.ToString());
            }
            return result;
        }

        /// <summary>
        /// The method for handling upload a file to server
        /// </summary>
        /// <param name="file">
        /// The file will be uploaded to server
        /// </param>
        /// <param name="storagePath">
        /// The directory which the file will be stored in
        /// </param>
        /// <param name="fileName">
        /// FileName will be created based on DateTime.Now
        /// as binary string and uploaded file extension
        /// </param>
        public static bool UploadFile(HttpPostedFile file, string storagePath, ref string fileName)
        {
            var result = true;
            try
            {
                var fileInfo = new FileInfo(file.FileName);
                fileName = DateTime.Now.ToBinary() + fileInfo.Extension;
                var fullPath = Path.Combine(storagePath, fileName);
                file.SaveAs(fullPath);
            }
            catch (Exception ex)
            {
                result = false;
                Logger.ErrorFormat("UploadFile: {0}", ex.ToString());
            }
            return result;
        }

        /// <summary>
        /// The method for handling upload a file to server
        /// </summary>
        /// <param name="file">
        /// The file will be uploaded to server
        /// </param>
        /// <param name="maximumWidth">
        /// Maximum width
        /// </param>
        /// <param name="storagePath">
        /// The directory which the file will be stored in
        /// </param>
        /// <param name="fileName">
        /// fileName will be created based on DateTime.Now
        /// as binary string and uploaded file extension
        /// </param>
        public static bool UploadAndResizeFile(HttpPostedFileBase file, int maximumWidth,
            string storagePath, ref string fileName)
        {
            var result = true;
            try
            {
                // Get storage upload path
                var fileInfo = new FileInfo(file.FileName);
                fileName = DateTime.Now.ToBinary() + fileInfo.Extension;
                var fullStoragePath = Path.Combine(storagePath, fileName);

                // Get to be uploaded image width
                using (Image sourceImage = Image.FromStream(file.InputStream))
                {
                    var uploadImageWidth = sourceImage.Width;

                    if (uploadImageWidth > maximumWidth)
                    {
                        var fileByteArray = ConvertImageToByteArray(sourceImage, fileInfo.Extension);
                        var imageResizer = new ImageResizer(fileByteArray);
                        imageResizer.Resize(maximumWidth, ImageEncoding.Jpg90);
                        imageResizer.SaveToFile(fullStoragePath);
                        imageResizer.Dispose();
                    }
                    else
                    {
                        file.SaveAs(fullStoragePath);
                    }
                }
            }
            catch (Exception ex)
            {
                result = false;
                Logger.ErrorFormat("UploadAndResizeFile: {0}", ex.ToString());
            }
            return result;
        }

        /// <summary>
        /// The method for handling upload a file to server
        /// </summary>
        /// <param name="file">
        /// The file will be uploaded to server
        /// </param>
        /// <param name="maximumWidth">
        /// Maximum width
        /// </param>
        /// <param name="storagePath">
        /// The directory which the file will be stored in
        /// </param>
        /// <param name="fileName">
        /// fileName will be created based on DateTime.Now
        /// as binary string and uploaded file extension
        /// </param>
        public static bool UploadAndResizeFile(HttpPostedFile file, int maximumWidth,
            string storagePath, ref string fileName)
        {
            var result = true;
            try
            {
                // Get storage upload path
                var fileInfo = new FileInfo(file.FileName);
                fileName = DateTime.Now.ToBinary() + fileInfo.Extension;
                var fullStoragePath = Path.Combine(storagePath, fileName);

                // Get to be uploaded image width
                using (Image sourceImage = Image.FromStream(file.InputStream))
                {
                    var uploadImageWidth = sourceImage.Width;

                    if (uploadImageWidth > maximumWidth)
                    {
                        var fileByteArray = ConvertImageToByteArray(sourceImage, fileInfo.Extension);
                        var imageResizer = new ImageResizer(fileByteArray);
                        imageResizer.Resize(maximumWidth, ImageEncoding.Jpg90);
                        imageResizer.SaveToFile(fullStoragePath);
                        imageResizer.Dispose();
                    }
                    else
                    {
                        file.SaveAs(fullStoragePath);
                    }
                }
            }
            catch (Exception ex)
            {
                result = false;
                Logger.ErrorFormat("UploadAndResizeFile: {0}", ex.ToString());
            }
            return result;
        }

        /// <summary>
        /// The method for deleting a file in server
        /// </summary>
        /// <param name="filePath">The file will be deleted in server</param>
        public static bool DeleteFile(string filePath)
        {
            var result = true;
            try
            {
                var file = new FileInfo(filePath);
                if (file.Exists)
                {
                    var fileDir = file.Directory;
                    if (fileDir != null)
                    {
                        RemoveFileInFolders(fileDir, file.Name);
                    }
                }
            }
            catch (Exception ex)
            {
                result = false;
                Logger.ErrorFormat("DeleteFile: {0}", ex.ToString());
            }
            return result;
        }

        /// <summary>
        /// Remove files with name in folders
        /// </summary>
        /// <param name="fileDir"></param>
        /// <param name="fileDelete"> </param>
        public static void RemoveFileInFolders(DirectoryInfo fileDir, string fileDelete)
        {
            if (fileDir != null)
            {
                var subDirs = fileDir.GetDirectories();
                foreach (DirectoryInfo directoryInfo in subDirs)
                {
                    RemoveFileInFolders(directoryInfo, fileDelete);
                }
                var fileInfo = fileDir.GetFiles().FirstOrDefault(
                    n => fileDelete.Equals(n.Name, StringComparison.OrdinalIgnoreCase));
                if (fileInfo != null)
                {
                    fileInfo.Delete();
                }
            }
        }

        /// <summary>
        /// Convert image to byte array
        /// </summary>
        /// <param name="image">Image instance</param>
        /// <param name="imageExtension">Image extension</param>
        /// <returns>Byte array</returns>
        public static byte[] ConvertImageToByteArray(Image image, string imageExtension)
        {
            byte[] result;
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    var imageFormat = ConvertImageExtToImageFormat(imageExtension);
                    image.Save(ms, imageFormat);
                    result = ms.ToArray();
                } 
            }
            catch (Exception)
            {
                result = new byte[0];
            }
            return result;
        }

        /// <summary>
        /// Convert from image extension string to image format
        /// </summary>
        /// <param name="imageExtension">Image extension string</param>
        /// <returns>Image format</returns>
        public static ImageFormat ConvertImageExtToImageFormat(string imageExtension)
        {
            var result = ImageFormat.Jpeg;
            imageExtension = imageExtension.ToLower();

            switch (imageExtension)
            {
                case ".jpeg":
                case ".jpg":
                    result = ImageFormat.Jpeg;
                    break;

                case ".png":
                    result = ImageFormat.Png;
                    break;

                case ".bmp":
                    result = ImageFormat.Bmp;
                    break;

                case ".gif":
                    result = ImageFormat.Gif;
                    break;
            }

            return result;
        }

        /// <summary>
        /// Converts the image ext to media type header value.
        /// </summary>
        /// <param name="imageExtension">The image extension.</param>
        /// <returns></returns>
        public static string ConvertImageExtToMediaTypeHeaderValue(string imageExtension)
        {
            var result = "image/jpeg";
            imageExtension = imageExtension.ToLower();

            switch (imageExtension)
            {
                case ".jpeg":
                case ".jpg":
                    result = "image/jpeg";
                    break;

                case ".png":
                    result = "image/png";
                    break;

                case ".bmp":
                    result = "image/bmp";
                    break;

                case ".gif":
                    result = "image/gif";
                    break;
            }

            return result;
        }

        /// <summary>
        /// Gets the resized image path.
        /// </summary>
        /// <param name="filepath">The filepath.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <returns></returns>
        public static string GetResizedImagePath(string filepath, int width, int height)
        {
            var resizedPath = filepath;
            try
            {
                if (width > 0 || height > 0)
                {
                    resizedPath = GetPathForResizedImage(filepath, width, height);

                    if (!Directory.Exists(resizedPath))
                    {
                        var fileInfo = new FileInfo(resizedPath);
                        if (!string.IsNullOrEmpty(fileInfo.DirectoryName))
                        {
                            Directory.CreateDirectory(fileInfo.DirectoryName);
                        }
                    }

                    if (!File.Exists(resizedPath))
                    {
                        var imageResizer = new ImageResizer(filepath);
                        if (width > 0 && height > 0)
                        {
                            imageResizer.Resize(width, height, ImageEncoding.Jpg90);
                        }
                        else if (width > 0)
                        {
                            imageResizer.Resize(width, ImageEncoding.Jpg90);
                        }
                        imageResizer.SaveToFile(resizedPath);
                        imageResizer.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("Failed to get resized image path - error: {0}", ex.ToString());
            }

            return resizedPath;
        }

        /// <summary>
        /// Gets the path for resized image.
        /// </summary>
        /// <param name="orgPath">The org path.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <returns></returns>
        public static string GetPathForResizedImage(string orgPath, int width = 0, int height = 0)
        {
            var result = string.Empty;

            var fileInfo = new FileInfo(orgPath);
            if (!string.IsNullOrEmpty(fileInfo.DirectoryName) && !string.IsNullOrEmpty(Path.GetFileName(orgPath)))
            {
                result = Path.Combine(fileInfo.DirectoryName, IMAGE_RESIZED_DIR, width + "x" + height,
                    Path.GetFileName(orgPath));
            }

            return result;
        }

        #endregion
    }
}