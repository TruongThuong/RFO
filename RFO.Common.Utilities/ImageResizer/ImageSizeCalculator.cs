
using System;

namespace RFO.Common.Utilities.ImageResizer
{
    /// <summary>
    /// Image size calculator
    /// </summary>
    public class ImageSizeCalculator
    {
        #region Fields

        /// <summary>
        /// The original image height
        /// </summary>
        private readonly double _orgHeight;

        /// <summary>
        /// The original image width
        /// </summary>
        private readonly double _orgWidth;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageSizeCalculator"/> class.
        /// </summary>
        /// <param name="orgWidth">Width of the org.</param>
        /// <param name="orgHeight">Height of the org.</param>
        public ImageSizeCalculator(double orgWidth, double orgHeight)
        {
            _orgHeight = orgHeight;
            _orgWidth = orgWidth;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Scales image with specified new width.
        /// </summary>
        /// <param name="newWidth">The new width.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">
        /// Cannot scale up, newWidth is larger than orgWidth
        /// </exception>
        public ImageSize Scale(int newWidth)
        {
            if (newWidth <= 0)
            {
                throw new ArgumentException(string.Format("Invalid new width: {0}", newWidth));
            }

            if (newWidth > _orgWidth)
            {
                throw new ArgumentException("Cannot scale up, newWidth is larger than orgWidth");
            }

            var height = (int) (_orgHeight*(newWidth/_orgWidth));
            return new ImageSize(newWidth, height, 0, 0);
        }

        /// <summary>
        /// Scales to fit image with specified width and height
        /// </summary>
        /// <param name="newWidth">The new width.</param>
        /// <param name="newHeight">The new height.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">
        /// Cannot scale up, new size is larger than org size
        /// </exception>
        public ImageSize ScaleToFit(int newWidth, int newHeight)
        {
            if (newWidth <= 0)
            {
                throw new ArgumentException(string.Format("Invalid new width: {0}", newWidth));
            }
            if (newHeight <= 0)
            {
                throw new ArgumentException(string.Format("Invalid new height: {0}", newHeight));
            }
            if (newWidth > _orgWidth || newHeight > _orgHeight)
            {
                throw new ArgumentException("Cannot scale up, new size is larger than org size");
            }

            var num1 = newWidth/_orgWidth;
            var num2 = newHeight/_orgHeight;
            return num2 > num1
                ? new ImageSize(newWidth, (int) (_orgHeight*num1), 0, 0)
                : new ImageSize((int) (_orgWidth*num2), newHeight, 0, 0);
        }

        /// <summary>
        /// Scales to fill image with specified width and height
        /// </summary>
        /// <param name="newWidth">The new width.</param>
        /// <param name="newHeight">The new height.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">
        /// Cannot scale up, new size is larger than org size
        /// </exception>
        public ImageSize ScaleToFill(int newWidth, int newHeight)
        {
            if (newWidth <= 0)
            {
                throw new ArgumentException(string.Format("Invalid new width: {0}", newWidth));
            }
            if (newHeight <= 0)
            {
                throw new ArgumentException(string.Format("Invalid new height: {0}", newHeight));
            }
            if (newWidth > _orgWidth || newHeight > _orgHeight)
            {
                throw new ArgumentException("Cannot scale up, new size is larger than org size");
            }
            var num1 = newWidth/_orgWidth;
            var num2 = newHeight/_orgHeight;
            return num1 > num2
                ? new ImageSize(newWidth, newHeight, 0,
                    (int) Math.Abs(_orgHeight*num1 - newHeight)/2)
                : new ImageSize(newWidth, newHeight, (int) Math.Abs(_orgWidth*num2 - newWidth)/2, 0);
        }

        #endregion
    }
}