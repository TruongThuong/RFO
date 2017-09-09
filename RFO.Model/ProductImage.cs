using System;

namespace RFO.Model
{
    public class ProductImage
    {
        /// <summary>
        /// Gets or sets the product image identifier.
        /// </summary>
        /// <value>
        /// The product image identifier.
        /// </value>
        public int ProductImageId { get; set; }

        /// <summary>
        /// Gets or sets the product identifier.
        /// </summary>
        /// <value>
        /// The product identifier.
        /// </value>
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets the image file.
        /// </summary>
        /// <value>
        /// The image file.
        /// </value>
        public string ImageFile { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is present.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is present; otherwise, <c>false</c>.
        /// </value>
        public bool IsPresent { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is active; otherwise, <c>false</c>.
        /// </value>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets the product.
        /// </summary>
        /// <value>
        /// The product.
        /// </value>
        public virtual Product Product { get; set; }
    }
}