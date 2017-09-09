

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.Samples.EntityDataReader;
using RFO.Model.DummyDataGenerator.Constant;
using RFO.Model;
using RFO.AspNet.Utilities.MembershipService;
using RFO.MetaData;
using System.Data.Entity.Migrations;

namespace RFO.Model.DummyDataGenerator.Seed
{
    /// <summary>
    /// Seed User
    /// </summary>
    public class SeedUser : AbstractSeed
    {
        #region Fields
        
        /// <summary>
        /// Definition a SeedUser instance
        /// </summary>
        private static SeedUser instance;

        /// <summary>
        /// Defintion a object to lock
        /// </summary>
        private static readonly object singletonLocker = new object();

        /// <summary>
        /// The Users
        /// </summary>
        private readonly List<User> users = new List<User>();

        #endregion
        
        #region Methods
        
        /// <summary>
        /// Get instance of SeedUser 
        /// </summary>
        /// <returns>SeedUser instance</returns>
        public static SeedUser Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (singletonLocker)
                    {
                        if (instance == null)
                        {
                            instance = new SeedUser();
                        }
                    }
                }
                return instance;
            }
        }

        /// <summary>
        /// The Users
        /// </summary>
        public List<User> Users
        {
            get { return this.users; }
        }

        /// <summary>
        /// Insert dummy User data to database
        /// </summary>
        public void Seed(RFODbContext context)
        {
            var memberShipUsers = new List<MembershipUserInfo>();

            // Prepare to seed
            for (int i = 0; i < 10; i++)
            {
                var user = new User
                {
                    UserId = (i + 1),
                    UserName = (i == 0 ? "admin" : string.Format("user{0}", i + 1)),
                    Password = "123",
                    Email = DummyDataProvider.Instance.GetGeneratedData(DummyDataType.EMAIL),
                    IsActive = true,
                    FullName = DummyDataProvider.Instance.GetGeneratedData(DummyDataType.SIMPLE_DATA),
                    Phone = DummyDataProvider.Instance.GetGeneratedData(DummyDataType.PHONE),
                    Address = DummyDataProvider.Instance.GetGeneratedData(DummyDataType.ADDRESS),
                    AvatarFile = DummyDataProvider.Instance.GetGeneratedData(DummyDataType.IMAGE),
                };
                this.users.Add(user);

                memberShipUsers.Add(new MembershipUserInfo
                {
                    UserName = user.UserName,
                    Password = user.Password,
                    Email = user.Email,
                    Role = (i == 0 ? AppConstants.AdminRole: AppConstants.EmployeeRole)
                });
            }

            // Create membership users
            SimpleMembershipInitializer<RFODbContext>.CreateUsers(memberShipUsers);

            this.users.ForEach(n => context.Users.AddOrUpdate(n));

            //// Insert data to database
            //using (var bulkCopy = new SqlBulkCopy(this.connectionString))
            //{
            //    bulkCopy.DestinationTableName = "[User]";
            //    bulkCopy.BulkCopyTimeout = 9999;
            //    bulkCopy.WriteToServer(this.users.AsDataReader());
            //}
        }

        #endregion
        
    }
}

