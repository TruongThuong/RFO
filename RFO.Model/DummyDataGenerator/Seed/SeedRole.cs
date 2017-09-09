

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.Samples.EntityDataReader;
using RFO.Model.DummyDataGenerator.Constant;
using RFO.Model;
using RFO.MetaData;

namespace RFO.Model.DummyDataGenerator.Seed
{
    /// <summary>
    /// Seed Role
    /// </summary>
    public class SeedRole : AbstractSeed
    {
        #region Fields
        
        /// <summary>
        /// Definition a SeedRole instance
        /// </summary>
        private static SeedRole instance;

        /// <summary>
        /// Defintion a object to lock
        /// </summary>
        private static readonly object singletonLocker = new object();

        /// <summary>
        /// The Roles
        /// </summary>
        private readonly List<Role> roles = new List<Role>();

        #endregion
        
        #region Methods
        
        /// <summary>
        /// Get instance of SeedRole 
        /// </summary>
        /// <returns>SeedRole instance</returns>
        public static SeedRole Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (singletonLocker)
                    {
                        if (instance == null)
                        {
                            instance = new SeedRole();
                        }
                    }
                }
                return instance;
            }
        }

        /// <summary>
        /// The Roles
        /// </summary>
        public List<Role> Roles
        {
            get { return this.roles; }
        }

        /// <summary>
        /// Insert dummy Role data to database
        /// </summary>
        public void Seed()
        {
            // Prepare to seed
            this.roles.Add(new Role
            {
                RoleId = 1,
                Name = AppConstants.AdminRole,
                BriefDescription = DummyDataProvider.Instance.GetGeneratedData(DummyDataType.BRIEF_DESCRIPTION),
            });

            this.roles.Add(new Role
            {
                RoleId = 2,
                Name = AppConstants.UserRole,
                BriefDescription = DummyDataProvider.Instance.GetGeneratedData(DummyDataType.BRIEF_DESCRIPTION),
            });

            // Insert data to database
            using (var bulkCopy = new SqlBulkCopy(this.connectionString))
            {
                bulkCopy.DestinationTableName = "[Role]";
                bulkCopy.BulkCopyTimeout = 9999;
                bulkCopy.WriteToServer(this.roles.AsDataReader());
            }
        }

        #endregion
        
    }
}

