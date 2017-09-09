

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.Samples.EntityDataReader;
using RFO.Model.DummyDataGenerator.Constant;
using RFO.Model;

namespace RFO.Model.DummyDataGenerator.Seed
{
    /// <summary>
    /// Seed ProductImage
    /// </summary>
    public class SeedProductImage : AbstractSeed
    {
        #region Fields
        
        /// <summary>
        /// Definition a SeedProductImage instance
        /// </summary>
        private static SeedProductImage instance;

        /// <summary>
        /// Defintion a object to lock
        /// </summary>
        private static readonly object singletonLocker = new object();

        /// <summary>
        /// The ProductImages
        /// </summary>
        private readonly List<ProductImage> productImages = new List<ProductImage>();

        #endregion
        
        #region Methods
        
        /// <summary>
        /// Get instance of SeedProductImage 
        /// </summary>
        /// <returns>SeedProductImage instance</returns>
        public static SeedProductImage Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (singletonLocker)
                    {
                        if (instance == null)
                        {
                            instance = new SeedProductImage();
                        }
                    }
                }
                return instance;
            }
        }

        /// <summary>
        /// The ProductImages
        /// </summary>
        public List<ProductImage> ProductImages
        {
            get { return this.productImages; }
        }

        /// <summary>
        /// Insert dummy ProductImage data to database
        /// </summary>
        public void Seed()
        {
            // Prepare to seed
            int numFood = SeedProduct.Instance.Products.Count;
            int index = 0;
            for (int i = 0; i < numFood; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    this.productImages.Add(new ProductImage
                    {
                        ProductImageId = (i + 1),
                        ProductId = SeedProduct.Instance.Products[i].ProductId,
                        ImageFile = DummyDataProvider.Instance.GetGeneratedData(DummyDataType.IMAGE),
                        IsPresent = true,
                        IsActive = true,
                    });
                }
            }
            
            // Insert data to database
            using (var bulkCopy = new SqlBulkCopy(this.connectionString))
            {
                bulkCopy.DestinationTableName = "[ProductImage]";
                bulkCopy.BulkCopyTimeout = 9999;
                bulkCopy.WriteToServer(this.productImages.AsDataReader());
            }
        }

        #endregion
        
    }
}

