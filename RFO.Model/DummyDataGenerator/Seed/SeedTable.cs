

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.Samples.EntityDataReader;
using RFO.Model.DummyDataGenerator.Constant;
using RFO.Model;
using RFO.Model.Enum;

namespace RFO.Model.DummyDataGenerator.Seed
{
    /// <summary>
    /// Seed Table
    /// </summary>
    public class SeedTable : AbstractSeed
    {
        #region Fields
        
        /// <summary>
        /// Definition a SeedTable instance
        /// </summary>
        private static SeedTable instance;

        /// <summary>
        /// Defintion a object to lock
        /// </summary>
        private static readonly object singletonLocker = new object();

        /// <summary>
        /// The Tables
        /// </summary>
        private readonly List<Table> tables = new List<Table>();

        #endregion
        
        #region Methods
        
        /// <summary>
        /// Get instance of SeedTable 
        /// </summary>
        /// <returns>SeedTable instance</returns>
        public static SeedTable Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (singletonLocker)
                    {
                        if (instance == null)
                        {
                            instance = new SeedTable();
                        }
                    }
                }
                return instance;
            }
        }

        /// <summary>
        /// The Tables
        /// </summary>
        public List<Table> Tables
        {
            get { return this.tables; }
        }

        /// <summary>
        /// Insert dummy Table data to database
        /// </summary>
        public void Seed()
        {
            // Prepare to seed
            int index = 0;
            for (int j = 0; j < 30; j++)
            {
                this.tables.Add(new Table
                {
                    TableId = (index + 1),
                    Name = string.Format("Bàn {0}", index + 1),
                    Status = (int)TableStatus.Available,
                    BriefDescription = DummyDataProvider.Instance.GetGeneratedData(DummyDataType.BRIEF_DESCRIPTION),
                    NumSeat = DummyDataProvider.Instance.GetRandomNumberLargerThanZero(11),
                });
                index++;
            }

            // Insert data to database
            using (var bulkCopy = new SqlBulkCopy(this.connectionString))
            {
                bulkCopy.DestinationTableName = "[Table]";
                bulkCopy.BulkCopyTimeout = 9999;
                bulkCopy.WriteToServer(this.tables.AsDataReader());
            }
        }

        #endregion
        
    }
}

