

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.Samples.EntityDataReader;
using RFO.Model.DummyDataGenerator.Constant;
using RFO.Model;

namespace RFO.Model.DummyDataGenerator.Seed
{
    /// <summary>
    /// Seed Order
    /// </summary>
    public class SeedOrder : AbstractSeed
    {
        #region Fields
        
        /// <summary>
        /// Definition a SeedOrder instance
        /// </summary>
        private static SeedOrder instance;

        /// <summary>
        /// Defintion a object to lock
        /// </summary>
        private static readonly object singletonLocker = new object();

        /// <summary>
        /// The Orders
        /// </summary>
        private readonly List<Order> orders = new List<Order>();

        #endregion
        
        #region Methods
        
        /// <summary>
        /// Get instance of SeedOrder 
        /// </summary>
        /// <returns>SeedOrder instance</returns>
        public static SeedOrder Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (singletonLocker)
                    {
                        if (instance == null)
                        {
                            instance = new SeedOrder();
                        }
                    }
                }
                return instance;
            }
        }

        /// <summary>
        /// The Orders
        /// </summary>
        public List<Order> Orders
        {
            get { return this.orders; }
        }

        /// <summary>
        /// Insert dummy Order data to database
        /// </summary>
        public void Seed()
        {
            //// Prepare to seed
            //for (int i = 0; i < 1000; i++)
            //{
            //    this.orders.Add(new Order
            //    {
            //        OrderId = (i + 1),
            //        TableId = DummyDataProvider.Instance.GetRandomNumberLargerThanZero(21),
            //        OrderStateId = DummyDataProvider.Instance.GetRandomNumberLargerThanZero(21),
            //        DeliveryNote = DummyDataProvider.Instance.GetGeneratedData(DummyDataType.SIMPLE_DATA),
            //        CreatedDate = DateTime.Now,
            //    });
            //}
            
            //// Insert data to database
            //using (var bulkCopy = new SqlBulkCopy(this.connectionString))
            //{
            //    bulkCopy.DestinationTableName = "[Order]";
            //    bulkCopy.BulkCopyTimeout = 9999;
            //    bulkCopy.WriteToServer(this.orders.AsDataReader());
            //}
        }

        #endregion
        
    }
}

