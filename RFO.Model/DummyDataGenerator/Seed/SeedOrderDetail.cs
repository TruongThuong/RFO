

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.Samples.EntityDataReader;
using RFO.Model.DummyDataGenerator.Constant;
using RFO.Model;

namespace RFO.Model.DummyDataGenerator.Seed
{
    /// <summary>
    /// Seed OrderDetail
    /// </summary>
    public class SeedOrderDetail : AbstractSeed
    {
        #region Fields
        
        /// <summary>
        /// Definition a SeedOrderDetail instance
        /// </summary>
        private static SeedOrderDetail instance;

        /// <summary>
        /// Defintion a object to lock
        /// </summary>
        private static readonly object singletonLocker = new object();

        /// <summary>
        /// The OrderDetails
        /// </summary>
        private readonly List<OrderDetail> orderDetails = new List<OrderDetail>();

        #endregion
        
        #region Methods
        
        /// <summary>
        /// Get instance of SeedOrderDetail 
        /// </summary>
        /// <returns>SeedOrderDetail instance</returns>
        public static SeedOrderDetail Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (singletonLocker)
                    {
                        if (instance == null)
                        {
                            instance = new SeedOrderDetail();
                        }
                    }
                }
                return instance;
            }
        }

        /// <summary>
        /// The OrderDetails
        /// </summary>
        public List<OrderDetail> OrderDetails
        {
            get { return this.orderDetails; }
        }

        /// <summary>
        /// Insert dummy OrderDetail data to database
        /// </summary>
        public void Seed()
        {
            //// Prepare to seed
            //for (int i = 0; i < 1000; i++)
            //{
            //    this.orderDetails.Add(new OrderDetail
            //    {
            //        OrderDetailId = (i + 1),
            //        OrderId = DummyDataProvider.Instance.GetRandomNumberLargerThanZero(21),
            //        ProductId = DummyDataProvider.Instance.GetRandomNumberLargerThanZero(21),
            //        Quantity = DummyDataProvider.Instance.GetRandomNumberLargerThanZero(11),
            //        Price = long.Parse(DummyDataProvider.Instance.GetGeneratedData(DummyDataType.NUMBER)),
            //    });
            //}
            
            //// Insert data to database
            //using (var bulkCopy = new SqlBulkCopy(this.connectionString))
            //{
            //    bulkCopy.DestinationTableName = "[OrderDetail]";
            //    bulkCopy.BulkCopyTimeout = 9999;
            //    bulkCopy.WriteToServer(this.orderDetails.AsDataReader());
            //}
        }

        #endregion
        
    }
}

