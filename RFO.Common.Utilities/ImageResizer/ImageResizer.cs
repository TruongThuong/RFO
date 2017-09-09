
using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using RFO.Common.Utilities.Logging;

namespace RFO.Common.Utilities.ImageResizer
{
    /// <summary>
    /// Provides methods to resize an image.
    /// </summary>
    [Export(typeof(IImageResizer))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class ImageResizer : IImageResizer
    {
        #region Fields

        /// <summary>
        /// The logger instance
        /// </summary>
        private static readonly ILogger Logger = LoggerManager.GetLogger(typeof(ImageResizer).Name);

        /// <summary>
        /// The original bitmap
        /// </summary>
        private readonly BitmapImage _orgBitMap;

        /// <summary>
        /// The image bytes
        /// </summary>
        private byte[] _imageBytes;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageResizer"/> class.
        /// </summary>
        /// <param name="path">The path.</param>
        public ImageResizer(string path)
        {
            _imageBytes = LoadImageData(path);
            _orgBitMap = LoadBitmapImage(_imageBytes);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageResizer"/> class.
        /// </summary>
        /// <param name="imageBytes">The image bytes.</param>
        public ImageResizer(byte[] imageBytes)
        {
            _imageBytes = imageBytes;
            _orgBitMap = LoadBitmapImage(_imageBytes);
        }

        #endregion

        #region Implementation of IImageResizer

        /// <summary>
        /// Resizes image with specified width.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="encoding">The encoding.</param>
        /// <returns></returns>
        public byte[] Resize(int width, ImageEncoding encoding)
        {
            return Resize(width, 0, encoding);
        }

        /// <summary>
        /// Resizes image with specified width and height
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="encoding">The encoding.</param>
        /// <returns></returns>
        public byte[] Resize(int width, int height, ImageEncoding encoding)
        {
            return Resize(width, height, true, encoding);
        }

        /// <summary>
        /// Resizes and crops image with specified width and height
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="crop">if set to <c>true</c> [crop].</param>
        /// <param name="encoding">The encoding.</param>
        /// <returns></returns>
        public byte[] Resize(int width, int height, bool crop, ImageEncoding encoding)
        {
            if (width < 0)
            {
                throw new ArgumentException("width < 0");
            }
            if (height < 0)
            {
                throw new ArgumentException("height < 0");
            }

            if (width > _orgBitMap.PixelWidth)
            {
                width = _orgBitMap.PixelWidth;
            }
            if (height > _orgBitMap.PixelHeight)
            {
                height = _orgBitMap.PixelHeight;
            }

            var bitmapSource = (BitmapSource) null;
            if (width > 0 && height > 0 && crop)
            {
                bitmapSource = ScaleToFill(width, height);
            }
            else if (width > 0 && height > 0 && !crop)
            {
                bitmapSource = ScaleToFit(width, height);
            }
            else if (width > 0)
            {
                bitmapSource = ResizeImageByWidth(_imageBytes, width);
            }
            else if (height > 0)
            {
                bitmapSource = this.ResizeImageByHeight(_imageBytes, height);
            }
            _imageBytes = EncodeImageData(bitmapSource, encoding);

            return _imageBytes;
        }

        /// <summary>
        /// Saves to file.
        /// </summary>
        /// <param name="path">The path.</param>
        public void SaveToFile(string path)
        {
            SaveImageToFile(_imageBytes, path);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        /// <filterpriority>2</filterpriority>
        public void Dispose()
        {
            _imageBytes = null;
        }

        #endregion

        #region Help methods

        /// <summary>
        /// Scales to fill.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <returns></returns>
        private BitmapSource ScaleToFill(int width, int height)
        {
            BitmapSource source;
            ImageSize imageSize;

            var heightRatio = height/(double) _orgBitMap.PixelHeight;
            var widthRatio = width/(double) _orgBitMap.PixelWidth;

            if (heightRatio > widthRatio)
            {
                source = ResizeImageByHeight(_imageBytes, height);
                imageSize = new ImageSizeCalculator(source.PixelWidth, height).ScaleToFill(width, height);
            }
            else
            {
                source = ResizeImageByWidth(_imageBytes, width);
                imageSize = new ImageSizeCalculator(width, source.PixelHeight).ScaleToFill(width, height);
            }

            return new CroppedBitmap(source,
                new Int32Rect(imageSize.XOffset, imageSize.YOffset, imageSize.Width, imageSize.Height));
        }

        /// <summary>
        /// Scales to fit.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <returns></returns>
        private BitmapSource ScaleToFit(int width, int height)
        {
            BitmapSource result = null;

            if (_orgBitMap != null)
            {
                double heightRatio = _orgBitMap.PixelHeight/height;
                double widthRatio = _orgBitMap.PixelWidth/width;

                result = heightRatio > widthRatio
                    ? ResizeImageByHeight(_imageBytes, height)
                    : ResizeImageByWidth(_imageBytes, width);
            }

            return result;
        }

        /// <summary>
        /// Resizes the image by width.
        /// </summary>
        /// <param name="imageData">The image data.</param>
        /// <param name="width">The width.</param>
        /// <returns></returns>
        private BitmapSource ResizeImageByWidth(byte[] imageData, int width)
        {
            var bitmapImage = new BitmapImage();

            bitmapImage.BeginInit();
            bitmapImage.DecodePixelWidth = width;
            bitmapImage.StreamSource = new MemoryStream(imageData);
            bitmapImage.CreateOptions = BitmapCreateOptions.IgnoreColorProfile;
            bitmapImage.CacheOption = BitmapCacheOption.Default;
            bitmapImage.EndInit();

            return bitmapImage;
        }

        /// <summary>
        /// Resizes the image by height.
        /// </summary>
        /// <param name="imageData">The image data.</param>
        /// <param name="height">The height.</param>
        /// <returns></returns>
        private BitmapSource ResizeImageByHeight(byte[] imageData, int height)
        {
            var bitmapImage = new BitmapImage();

            bitmapImage.BeginInit();
            bitmapImage.DecodePixelHeight = height;
            bitmapImage.StreamSource = new MemoryStream(imageData);
            bitmapImage.CreateOptions = BitmapCreateOptions.IgnoreColorProfile;
            bitmapImage.CacheOption = BitmapCacheOption.Default;
            bitmapImage.EndInit();

            return bitmapImage;
        }

        /// <summary>
        /// Encodes the image data.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <param name="encoding">The encoding.</param>
        /// <returns></returns>
        private byte[] EncodeImageData(ImageSource image, ImageEncoding encoding)
        {
            byte[] buffer = null;
            var bitmapEncoder = (BitmapEncoder) null;

            switch (encoding)
            {
                case ImageEncoding.Jpg100:
                    bitmapEncoder = new JpegBitmapEncoder
                    {
                        QualityLevel = 100
                    };
                    break;
                case ImageEncoding.Jpg95:
                    bitmapEncoder = new JpegBitmapEncoder
                    {
                        QualityLevel = 95
                    };
                    break;
                case ImageEncoding.Jpg90:
                    bitmapEncoder = new JpegBitmapEncoder
                    {
                        QualityLevel = 90
                    };
                    break;
                case ImageEncoding.Jpg:
                    bitmapEncoder = new JpegBitmapEncoder();
                    break;
                case ImageEncoding.Gif:
                    bitmapEncoder = new GifBitmapEncoder();
                    break;
                case ImageEncoding.Png:
                    bitmapEncoder = new PngBitmapEncoder();
                    break;
                case ImageEncoding.Tiff:
                    bitmapEncoder = new TiffBitmapEncoder();
                    break;
                case ImageEncoding.Bmp:
                    bitmapEncoder = new BmpBitmapEncoder();
                    break;
                case ImageEncoding.Wmp:
                    bitmapEncoder = new WmpBitmapEncoder();
                    break;
            }

            if (image is BitmapSource)
            {
                using (var memoryStream = new MemoryStream())
                {
                    if (bitmapEncoder != null)
                    {
                        var bitmapFrame = BitmapFrame.Create(image as BitmapSource);
                        bitmapEncoder.Frames.Add(bitmapFrame);
                        bitmapEncoder.Save(memoryStream);
                    }
                    memoryStream.Seek(0L, SeekOrigin.Begin);
                    buffer = new byte[memoryStream.Length];
                    using (var binaryReader = new BinaryReader(memoryStream))
                    {
                        binaryReader.Read(buffer, 0, (int) memoryStream.Length);
                    }
                }
            }

            return buffer;
        }

        /// <summary>
        /// Loads the image data.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns></returns>
        private static byte[] LoadImageData(string filePath)
        {
            byte[] numArray = null;

            try
            {
                using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                using (var binaryReader = new BinaryReader(fileStream))
                {
                    numArray = binaryReader.ReadBytes((int) fileStream.Length);
                }
            }
            catch(Exception ex)
            {
                Logger.ErrorFormat("LoadImageData - Exception: {0}", ex.ToString());
            }

            return numArray;
        }

        /// <summary>
        /// Loads the bitmap image.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <returns></returns>
        private BitmapImage LoadBitmapImage(byte[] bytes)
        {
            var bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.StreamSource = new MemoryStream(bytes);
            bitmapImage.CreateOptions = BitmapCreateOptions.IgnoreColorProfile;
            bitmapImage.CacheOption = BitmapCacheOption.Default;
            bitmapImage.EndInit();
            return bitmapImage;
        }

        /// <summary>
        /// Saves the image to file.
        /// </summary>
        /// <param name="bytes">The bytes.</param>
        /// <param name="path">The path.</param>
        private void SaveImageToFile(byte[] bytes, string path)
        {
            using (var fileStream = new FileStream(path, FileMode.Create, FileAccess.Write))
            using (var binaryWriter = new BinaryWriter(fileStream))
            {
                binaryWriter.Write(bytes);
            }
        }

        #endregion
    }
}