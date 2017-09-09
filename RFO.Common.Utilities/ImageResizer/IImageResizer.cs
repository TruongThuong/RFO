using System;

namespace RFO.Common.Utilities.ImageResizer
{
    /// <summary>
    /// Provides methods to resize an image.
    /// </summary>
    public interface IImageResizer : IDisposable
    {
        /// <summary>
        /// Resizes image with specified width.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="encoding">The encoding.</param>
        /// <returns></returns>
        byte[] Resize(int width, ImageEncoding encoding);

        /// <summary>
        /// Resizes image with specified width and height
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="encoding">The encoding.</param>
        /// <returns></returns>
        byte[] Resize(int width, int height, ImageEncoding encoding);

        /// <summary>
        /// Resizes and crops image with specified width and height
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="crop">if set to <c>true</c> [crop].</param>
        /// <param name="encoding">The encoding.</param>
        /// <returns></returns>
        byte[] Resize(int width, int height, bool crop, ImageEncoding encoding);

        /// <summary>
        /// Saves to file.
        /// </summary>
        /// <param name="path">The path.</param>
        void SaveToFile(string path);
    }
}