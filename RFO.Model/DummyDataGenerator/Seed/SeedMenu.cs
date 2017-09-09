

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.Samples.EntityDataReader;
using RFO.Model.DummyDataGenerator.Constant;
using RFO.Model;

namespace RFO.Model.DummyDataGenerator.Seed
{
    /// <summary>
    /// Seed Menu
    /// </summary>
    public class SeedMenu : AbstractSeed
    {
        #region Fields
        
        /// <summary>
        /// Definition a SeedMenu instance
        /// </summary>
        private static SeedMenu instance;

        /// <summary>
        /// Defintion a object to lock
        /// </summary>
        private static readonly object singletonLocker = new object();

        /// <summary>
        /// The Menus
        /// </summary>
        private readonly List<Menu> menus = new List<Menu>();

        #endregion
        
        #region Methods
        
        /// <summary>
        /// Get instance of SeedMenu 
        /// </summary>
        /// <returns>SeedMenu instance</returns>
        public static SeedMenu Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (singletonLocker)
                    {
                        if (instance == null)
                        {
                            instance = new SeedMenu();
                        }
                    }
                }
                return instance;
            }
        }

        /// <summary>
        /// The Menus
        /// </summary>
        public List<Menu> Menus
        {
            get { return this.menus; }
        }

        /// <summary>
        /// Insert dummy Menu data to database
        /// </summary>
        public void Seed()
        {
            // Prepare to seed
            int numFoodCategory = DummyDataProvider.Instance.GetNumItem(DummyDataType.MENU);
            for (int i = 0; i < numFoodCategory; i++)
            {
                this.menus.Add(new Menu
                {
                    MenuId = i + 1,
                    Name = DummyDataProvider.Instance.GetGeneratedData(DummyDataType.MENU, false, i),
                    BriefDescription = DummyDataProvider.Instance.GetGeneratedData(DummyDataType.BRIEF_DESCRIPTION),
                    IsActive = true,
                    OrderIndex = i + 1,
                });
            }
            
            // Insert data to database
            using (var bulkCopy = new SqlBulkCopy(this.connectionString))
            {
                bulkCopy.DestinationTableName = "[Menu]";
                bulkCopy.BulkCopyTimeout = 9999;
                bulkCopy.WriteToServer(this.menus.AsDataReader());
            }
        }

        #endregion
        
    }
}

