
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Web.Security;
using RFO.Common.Utilities.Logging;
using WebMatrix.WebData;

namespace RFO.AspNet.Utilities.MembershipService
{
    /// <summary>
    /// Simple Membership initializer
    /// </summary>
    public class SimpleMembershipInitializer<T> where T : DbContext
    {
        #region Fields

        /// <summary>
        /// The logger
        /// </summary>
        private static readonly ILogger Logger = LoggerManager.GetLogger(typeof(SimpleMembershipInitializer<T>).Name);

        #endregion

        #region Public methods

        /// <summary>
        /// Initializes the specified database connection name.
        /// </summary>
        public static void Initialize()
        {
            Logger.Info("Initialize <-- Start");

            try
            {
                if (!WebSecurity.Initialized)
                {
                    SqlConnection.ClearAllPools(); 
                    var contextTypeName = typeof (T).Name;

                    // Connect to database
                    WebMatrix.WebData.WebSecurity.InitializeDatabaseConnection(
                        contextTypeName, "User", "UserId", "UserName", autoCreateTables: true);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            Logger.Info("Initialize --> End");
        }

        /// <summary>
        /// Creates the roles.
        /// </summary>
        public static void CreateRoles(string[] roleNames)
        {
            Logger.Debug("CreateRoles <-- Start");

            var roles = (SimpleRoleProvider)Roles.Provider;

            foreach (var roleName in roleNames)
            {
                if (!roles.RoleExists(roleName))
                {
                    Roles.CreateRole(roleName);
                }
            }
            
            Logger.Debug("CreateRoles --> End");
        }

        /// <summary>
        /// Creates the users.
        /// </summary>
        public static void CreateUsers(List<MembershipUserInfo> users)
        {
            Logger.Debug("CreateUsers <-- Start");

            IMembershipService membership = new MembershipService();

            foreach (var membershipUser in users)
            {
                if (membership.GetUser(membershipUser.UserName) == null)
                {
                    membership.CreateAccount(membershipUser.UserName, membershipUser.Password,
                                             membershipUser.Email, membershipUser.Role, membershipUser.PropValues);
                }
            }

            Logger.Debug("CreateUsers --> End");
        }

        #endregion
    }
}