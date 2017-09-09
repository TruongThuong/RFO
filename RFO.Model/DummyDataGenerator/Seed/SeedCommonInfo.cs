

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
    /// Seed CommonInfo
    /// </summary>
    public class SeedCommonInfo : AbstractSeed
    {
        #region Fields
        
        /// <summary>
        /// Definition a SeedCommonInfo instance
        /// </summary>
        private static SeedCommonInfo instance;

        /// <summary>
        /// Defintion a object to lock
        /// </summary>
        private static readonly object singletonLocker = new object();

        /// <summary>
        /// The CommonInfoes
        /// </summary>
        private readonly List<CommonInfo> commonInfoes = new List<CommonInfo>();

        #endregion
        
        #region Methods
        
        /// <summary>
        /// Get instance of SeedCommonInfo 
        /// </summary>
        /// <returns>SeedCommonInfo instance</returns>
        public static SeedCommonInfo Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (singletonLocker)
                    {
                        if (instance == null)
                        {
                            instance = new SeedCommonInfo();
                        }
                    }
                }
                return instance;
            }
        }

        /// <summary>
        /// The CommonInfoes
        /// </summary>
        public List<CommonInfo> CommonInfoes
        {
            get { return this.commonInfoes; }
        }

        /// <summary>
        /// Insert dummy CommonInfo data to database
        /// </summary>
        public void Seed()
        {
            // CompanyName
            var commonInfo = new CommonInfo
            {
                CommonInfoCode = (int)CommonInfoCode.RestaurantName,
                Name = "RestaurantName",
                BriefDescription = DummyDataProvider.Instance.GetGeneratedData(DummyDataType.BRIEF_DESCRIPTION)
            };
            this.commonInfoes.Add(commonInfo);

            // Slogun
            commonInfo = new CommonInfo
            {
                CommonInfoCode = (int)CommonInfoCode.Slogun,
                Name = "Slogun",
                BriefDescription = DummyDataProvider.Instance.GetGeneratedData(DummyDataType.BRIEF_DESCRIPTION)
            };
            this.commonInfoes.Add(commonInfo);

            // SlogunDescription
            commonInfo = new CommonInfo
            {
                CommonInfoCode = (int)CommonInfoCode.SlogunDescription,
                Name = "SlogunDescription",
                BriefDescription = DummyDataProvider.Instance.GetGeneratedData(DummyDataType.BRIEF_DESCRIPTION)
            };
            this.commonInfoes.Add(commonInfo);

            // CopyRight
            commonInfo = new CommonInfo
            {
                CommonInfoCode = (int)CommonInfoCode.CopyRight,
                Name = "CopyRight",
                BriefDescription = "Copyright © 2017 Co., Ltd. All rights reserved"
            };
            this.commonInfoes.Add(commonInfo);

            // AboutUs
            commonInfo = new CommonInfo
            {
                CommonInfoCode = (int)CommonInfoCode.AboutUs,
                Name = "AboutUs",
                Description = this.DoEncodeSpecialCharacters(DummyDataProvider.Instance.GetGeneratedData(DummyDataType.HTML)),
            };
            this.commonInfoes.Add(commonInfo);

            // Insert data to database
            using (var bulkCopy = new SqlBulkCopy(this.connectionString))
            {
                bulkCopy.DestinationTableName = "[CommonInfo]";
                bulkCopy.BulkCopyTimeout = 9999;
                bulkCopy.WriteToServer(this.commonInfoes.AsDataReader());
            }
        }

        #endregion
        
    }
}

