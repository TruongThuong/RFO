
namespace RFO.Common.Utilities.ImageResizer
{
    /// <summary>
    /// The class is used to store size of Image after finished resizing
    /// </summary>
    public class ImageSize
    {
        #region Fields

        /// <summary>
        /// The image height
        /// </summary>
        private readonly int _height;

        /// <summary>
        /// The image width
        /// </summary>
        private readonly int _width;

        /// <summary>
        /// The image X offset
        /// </summary>
        private readonly int _xOffset;

        /// <summary>
        /// The image Y offset
        /// </summary>
        private readonly int _yOffset;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the width.
        /// </summary>
        /// <value>
        /// The width.
        /// </value>
        public int Width
        {
            get { return _width; }
        }

        /// <summary>
        /// Gets the height.
        /// </summary>
        /// <value>
        /// The height.
        /// </value>
        public int Height
        {
            get { return _height; }
        }

        /// <summary>
        /// Gets the x offset.
        /// </summary>
        /// <value>
        /// The x offset.
        /// </value>
        public int XOffset
        {
            get { return _xOffset; }
        }

        /// <summary>
        /// Gets the y offset.
        /// </summary>
        /// <value>
        /// The y offset.
        /// </value>
        public int YOffset
        {
            get { return _yOffset; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ImageSize"/> class.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="xOffset">The x offset.</param>
        /// <param name="yOffset">The y offset.</param>
        public ImageSize(int width, int height, int xOffset, int yOffset)
        {
            _width = width;
            _height = height;
            _xOffset = xOffset;
            _yOffset = yOffset;
        }

        #endregion
    }
}