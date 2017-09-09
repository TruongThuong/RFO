using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.Design;
using System.Linq;
using Newtonsoft.Json;

namespace RFO.Model
{
    public class Product
    {
        public Product()
        {
            this.OrderDetails = new HashSet<OrderDetail>();
            this.ProductImages = new HashSet<ProductImage>();
        }

        /// <summary>
        /// Gets or sets the product identifier.
        /// </summary>
        /// <value>
        /// The product identifier.
        /// </value>
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the menu identifier.
        /// </summary>
        /// <value>
        /// The menu identifier.
        /// </value>
        public int MenuId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is available.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is available; otherwise, <c>false</c>.
        /// </value>
        public bool IsAvailable { get; set; }

        /// <summary>
        /// Gets or sets the price.
        /// </summary>
        /// <value>
        /// The price.
        /// </value>
        public long Price { get; set; }

        /// <summary>
        /// Gets or sets the brief description.
        /// </summary>
        /// <value>
        /// The brief description.
        /// </value>
        public string BriefDescription { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is active; otherwise, <c>false</c>.
        /// </value>
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is popular.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is popular; otherwise, <c>false</c>.
        /// </value>
        public bool IsPopular { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is best seller.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is best seller; otherwise, <c>false</c>.
        /// </value>
        public bool IsBestSeller { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        [Column(TypeName = "ntext")]
        [MaxLength]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the remark.
        /// </summary>
        /// <value>
        /// The remark.
        /// </value>
        public string Remark { get; set; }

        public int Total { get; set; }

        /// <summary>
        /// Gets the present image.
        /// </summary>
        /// <value>
        /// The present image.
        /// </value>
        [JsonIgnore]
        public ProductImage PresentImage
        {
            get
            {
                ProductImage result = null;
                if (this.ProductImages.Any())
                {
                    var productImage = this.ProductImages.FirstOrDefault(n => n.IsPresent);
                    result = productImage ?? this.ProductImages.FirstOrDefault();
                }
                return result;
            }
        }

        /// <summary>
        /// Gets the present image file.
        /// </summary>
        /// <value>
        /// The present image file.
        /// </value>
        [NotMapped]
        public string PresentImageFile
        {
            get
            {
                var result = string.Empty;
                try
                {
                    if (this.ProductImages.Any())
                    {
                        var productImage = this.ProductImages.FirstOrDefault(n => n.IsPresent);
                        if (productImage != null)
                        {
                            result = productImage.ImageFile;
                        }
                        else // Not found Present image
                        {
                            productImage = this.ProductImages.FirstOrDefault();
                            if (productImage != null)
                            {
                                result = productImage.ImageFile;
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    // Just catching exception to prevent error
                }
                return result;
            }
        }

        /// <summary>
        /// Gets or sets the menu.
        /// </summary>
        /// <value>
        /// The menu.
        /// </value>
        public virtual Menu Menu { get; set; }

        /// <summary>
        /// Gets or sets the order details.
        /// </summary>
        /// <value>
        /// The order details.
        /// </value>
        [JsonIgnore]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        /// <summary>
        /// Gets or sets the product images.
        /// </summary>
        /// <value>
        /// The product images.
        /// </value>
        [JsonIgnore]
        public virtual ICollection<ProductImage> ProductImages { get; set; }
    }
}