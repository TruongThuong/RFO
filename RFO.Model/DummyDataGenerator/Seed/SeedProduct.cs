

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.Samples.EntityDataReader;
using RFO.Model.DummyDataGenerator.Constant;
using RFO.Model;

namespace RFO.Model.DummyDataGenerator.Seed
{
    /// <summary>
    /// Seed Product
    /// </summary>
    public class SeedProduct : AbstractSeed
    {
        #region Fields
        
        /// <summary>
        /// Definition a SeedProduct instance
        /// </summary>
        private static SeedProduct instance;

        /// <summary>
        /// Defintion a object to lock
        /// </summary>
        private static readonly object singletonLocker = new object();

        /// <summary>
        /// The Products
        /// </summary>
        private readonly List<Product> products = new List<Product>();

        #endregion
        
        #region Methods
        
        /// <summary>
        /// Get instance of SeedProduct 
        /// </summary>
        /// <returns>SeedProduct instance</returns>
        public static SeedProduct Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (singletonLocker)
                    {
                        if (instance == null)
                        {
                            instance = new SeedProduct();
                        }
                    }
                }
                return instance;
            }
        }

        /// <summary>
        /// The Products
        /// </summary>
        public List<Product> Products
        {
            get { return this.products; }
        }

        /// <summary>
        /// Insert dummy Product data to database
        /// </summary>
        public void Seed()
        {
            // Prepare to seed
            int numFoodCategory = SeedMenu.Instance.Menus.Count;
            int numFood = DummyDataProvider.Instance.GetNumItem(DummyDataType.FOOD);
            int index = 0;
            for (int i = 0; i < numFoodCategory; i++)
            {
                for (int j = 0; j < numFood / numFoodCategory; j++)
                {
                    this.products.Add(new Product
                    {
                        ProductId = index + 1,
                        Name = DummyDataProvider.Instance.GetGeneratedData(DummyDataType.FOOD, false, index),
                        MenuId = SeedMenu.Instance.Menus[i].MenuId,
                        IsAvailable = true,
                        Price = long.Parse(DummyDataProvider.Instance.GetGeneratedData(DummyDataType.NUMBER)),
                        BriefDescription = DummyDataProvider.Instance.GetGeneratedData(DummyDataType.BRIEF_DESCRIPTION),
                        IsActive = true,
                        IsPopular = true,
                        IsBestSeller = true,
                        Description = this.DoEncodeSpecialCharacters(DummyDataProvider.Instance.GetGeneratedData(DummyDataType.HTML)),
                        Remark = DummyDataProvider.Instance.GetGeneratedData(DummyDataType.SIMPLE_DATA),
                    });
                    index++;
                }
            }
            
            
            // Insert data to database
            using (var bulkCopy = new SqlBulkCopy(this.connectionString))
            {
                bulkCopy.DestinationTableName = "[Product]";
                bulkCopy.BulkCopyTimeout = 9999;
                bulkCopy.WriteToServer(this.products.AsDataReader());
            }
        }

        #endregion
        
    }
}

