

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.Samples.EntityDataReader;
using RFO.Model.DummyDataGenerator.Constant;
using RFO.Model;

namespace RFO.Model.DummyDataGenerator.Seed
{
    /// <summary>
    /// Seed OrderState
    /// </summary>
    public class SeedOrderState : AbstractSeed
    {
        #region Fields
        
        /// <summary>
        /// Definition a SeedOrderState instance
        /// </summary>
        private static SeedOrderState instance;

        /// <summary>
        /// Defintion a object to lock
        /// </summary>
        private static readonly object singletonLocker = new object();

        /// <summary>
        /// The OrderStates
        /// </summary>
        private readonly List<OrderState> orderStates = new List<OrderState>();

        #endregion
        
        #region Methods
        
        /// <summary>
        /// Get instance of SeedOrderState 
        /// </summary>
        /// <returns>SeedOrderState instance</returns>
        public static SeedOrderState Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (singletonLocker)
                    {
                        if (instance == null)
                        {
                            instance = new SeedOrderState();
                        }
                    }
                }
                return instance;
            }
        }

        /// <summary>
        /// The OrderStates
        /// </summary>
        public List<OrderState> OrderStates
        {
            get { return this.orderStates; }
        }

        /// <summary>
        /// Insert dummy OrderState data to database
        /// </summary>
        public void Seed()
        {
            // Prepare to seed
            this.orderStates.Add(new OrderState
            {
                OrderStateId = 1,
                Name = "Tiếp nhận",
                BriefDescription = DummyDataProvider.Instance.GetGeneratedData(DummyDataType.BRIEF_DESCRIPTION),
            });

            this.orderStates.Add(new OrderState
            {
                OrderStateId = 2,
                Name = "Chưa thanh toán",
                BriefDescription = DummyDataProvider.Instance.GetGeneratedData(DummyDataType.BRIEF_DESCRIPTION),
            });

            this.orderStates.Add(new OrderState
            {
                OrderStateId = 3,
                Name = "Đã thanh toán",
                BriefDescription = DummyDataProvider.Instance.GetGeneratedData(DummyDataType.BRIEF_DESCRIPTION),
            });

            // Insert data to database
            using (var bulkCopy = new SqlBulkCopy(this.connectionString))
            {
                bulkCopy.DestinationTableName = "[OrderState]";
                bulkCopy.BulkCopyTimeout = 9999;
                bulkCopy.WriteToServer(this.orderStates.AsDataReader());
            }
        }

        #endregion
        
    }
}

