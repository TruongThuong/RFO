

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.Samples.EntityDataReader;
using RFO.Model.DummyDataGenerator.Constant;
using RFO.Model;

namespace RFO.Model.DummyDataGenerator.Seed
{
    /// <summary>
    /// Seed Region
    /// </summary>
    public class SeedRegion : AbstractSeed
    {
        #region Fields
        
        /// <summary>
        /// Definition a SeedRegion instance
        /// </summary>
        private static SeedRegion instance;

        /// <summary>
        /// Defintion a object to lock
        /// </summary>
        private static readonly object singletonLocker = new object();

        /// <summary>
        /// The Regions
        /// </summary>
        private readonly List<Region> regions = new List<Region>();

        #endregion
        
        #region Methods
        
        /// <summary>
        /// Get instance of SeedRegion 
        /// </summary>
        /// <returns>SeedRegion instance</returns>
        public static SeedRegion Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (singletonLocker)
                    {
                        if (instance == null)
                        {
                            instance = new SeedRegion();
                        }
                    }
                }
                return instance;
            }
        }

        /// <summary>
        /// The Regions
        /// </summary>
        public List<Region> Regions
        {
            get { return this.regions; }
        }

        /// <summary>
        /// Insert dummy Region data to database
        /// </summary>
        public void Seed()
        {
            // Prepare to seed
            this.regions.Add(new Region
            {
                RegionId = 1,
                Name = "Lầu 1",
                BriefDescription = DummyDataProvider.Instance.GetGeneratedData(DummyDataType.BRIEF_DESCRIPTION),
            });

            this.regions.Add(new Region
            {
                RegionId = 2,
                Name = "Lầu 2",
                BriefDescription = DummyDataProvider.Instance.GetGeneratedData(DummyDataType.BRIEF_DESCRIPTION),
            });

            this.regions.Add(new Region
            {
                RegionId = 3,
                Name = "Lầu 3",
                BriefDescription = DummyDataProvider.Instance.GetGeneratedData(DummyDataType.BRIEF_DESCRIPTION),
            });

            // Insert data to database
            using (var bulkCopy = new SqlBulkCopy(this.connectionString))
            {
                bulkCopy.DestinationTableName = "[Region]";
                bulkCopy.BulkCopyTimeout = 9999;
                bulkCopy.WriteToServer(this.regions.AsDataReader());
            }
        }

        #endregion
        
    }
}

