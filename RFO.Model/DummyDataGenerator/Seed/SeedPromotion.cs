

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.Samples.EntityDataReader;
using RFO.Model.DummyDataGenerator.Constant;
using RFO.Model;

namespace RFO.Model.DummyDataGenerator.Seed
{
    /// <summary>
    /// Seed Promotion
    /// </summary>
    public class SeedPromotion : AbstractSeed
    {
        #region Fields
        
        /// <summary>
        /// Definition a SeedPromotion instance
        /// </summary>
        private static SeedPromotion instance;

        /// <summary>
        /// Defintion a object to lock
        /// </summary>
        private static readonly object singletonLocker = new object();

        /// <summary>
        /// The Promotions
        /// </summary>
        private readonly List<Promotion> promotions = new List<Promotion>();

        #endregion
        
        #region Methods
        
        /// <summary>
        /// Get instance of SeedPromotion 
        /// </summary>
        /// <returns>SeedPromotion instance</returns>
        public static SeedPromotion Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (singletonLocker)
                    {
                        if (instance == null)
                        {
                            instance = new SeedPromotion();
                        }
                    }
                }
                return instance;
            }
        }

        /// <summary>
        /// The Promotions
        /// </summary>
        public List<Promotion> Promotions
        {
            get { return this.promotions; }
        }

        /// <summary>
        /// Insert dummy Promotion data to database
        /// </summary>
        public void Seed()
        {
            // Prepare to seed
            for (int i = 0; i < 20; i++)
            {
                this.promotions.Add(new Promotion
                {
                    PromotionId = (i + 1),
                    ImageFile = DummyDataProvider.Instance.GetGeneratedData(DummyDataType.IMAGE),
                    Title = DummyDataProvider.Instance.GetGeneratedData(DummyDataType.TITLE),
                    BriefDescription = DummyDataProvider.Instance.GetGeneratedData(DummyDataType.BRIEF_DESCRIPTION),
                    IsActive = true,
                    IsPopular = true,
                    Description = this.DoEncodeSpecialCharacters(DummyDataProvider.Instance.GetGeneratedData(DummyDataType.HTML)),
                    CreatedDate = DateTime.Now,
                });
            }
            
            // Insert data to database
            using (var bulkCopy = new SqlBulkCopy(this.connectionString))
            {
                bulkCopy.DestinationTableName = "[Promotion]";
                bulkCopy.BulkCopyTimeout = 9999;
                bulkCopy.WriteToServer(this.promotions.AsDataReader());
            }
        }

        #endregion
        
    }
}

