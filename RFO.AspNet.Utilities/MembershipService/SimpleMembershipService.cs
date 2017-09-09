
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Web.Security;
using WebMatrix.WebData;

namespace RFO.AspNet.Utilities.MembershipService
{
    /// <summary>
    /// Membership service
    /// </summary>
    [Export(typeof (IMembershipService))]
    public class MembershipService : AbstractMemebershipService
    {
        #region Fields

        /// <summary>
        /// The provider
        /// </summary>
        private readonly SimpleMembershipProvider _provider = (SimpleMembershipProvider)Membership.Provider;

        /// <summary>
        /// The role provider
        /// </summary>
        private readonly SimpleRoleProvider _roleProvider = (SimpleRoleProvider)Roles.Provider;

        #endregion

        #region Overrides of AbstractMemebershipService

        /// <summary>
        /// Gets a value indicating whether this instance is authenticated.
        /// </summary>
        /// <returns></returns>
        /// <value>
        ///   <c>true</c> if this instance is authenticated; otherwise, <c>false</c>.
        /// </value>
        public override bool IsAuthenticated()
        {
            return WebMatrix.WebData.WebSecurity.IsAuthenticated;
        }

        /// <summary>
        /// Gets the name of the current user.
        /// </summary>
        /// <returns></returns>
        /// <value>
        /// The name of the current user.
        /// </value>
        public override string GetCurrentUserName()
        {
            return WebMatrix.WebData.WebSecurity.CurrentUserName;
        }

        /// <summary>
        /// Logins the specified user name.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <param name="persistCookie">if set to <c>true</c> [persist cookie].</param>
        /// <returns></returns>
        public override bool Login(string userName, string password, bool persistCookie = false)
        {
            return WebMatrix.WebData.WebSecurity.Login(userName, password, persistCookie);
        }

        /// <summary>
        /// Logouts this instance.
        /// </summary>
        public override void Logout()
        {
            WebMatrix.WebData.WebSecurity.Logout();
        }

        /// <summary>
        /// Confirms the account.
        /// </summary>
        /// <param name="accountConfirmationToken">The account confirmation token.</param>
        /// <returns></returns>
        public override bool ConfirmAccount(string accountConfirmationToken)
        {
            return WebMatrix.WebData.WebSecurity.ConfirmAccount(accountConfirmationToken);
        }

        /// <summary>
        /// Validates the user.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">
        /// Value cannot be null or empty.;userName
        /// or
        /// Value cannot be null or empty.;password
        /// </exception>
        public override bool ValidateUser(string userName, string password)
        {
            if (String.IsNullOrEmpty(userName))
                throw new ArgumentException("Value cannot be null or empty.", "userName");
            if (String.IsNullOrEmpty(password))
                throw new ArgumentException("Value cannot be null or empty.", "password");

            return this._provider.ValidateUser(userName, password);
        }

        /// <summary>
        /// Changes the password.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="oldPassword">The old password.</param>
        /// <param name="newPassword">The new password.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">
        /// Value cannot be null or empty.;userName
        /// or
        /// Value cannot be null or empty.;oldPassword
        /// or
        /// Value cannot be null or empty.;newPassword
        /// </exception>
        public override bool ChangePassword(string userName, string oldPassword, string newPassword)
        {
            if (String.IsNullOrEmpty(userName))
                throw new ArgumentException("Value cannot be null or empty.", "userName");
            if (String.IsNullOrEmpty(oldPassword))
                throw new ArgumentException("Value cannot be null or empty.", "oldPassword");
            if (String.IsNullOrEmpty(newPassword))
                throw new ArgumentException("Value cannot be null or empty.", "newPassword");

            return WebMatrix.WebData.WebSecurity.ChangePassword(userName, oldPassword, newPassword);
        }

        /// <summary>
        /// Changes the email.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="newEmail">The new email.</param>
        /// <returns></returns>
        public override bool ChangeEmail(string userName, string newEmail)
        {
            // The underlying ChangePassword() will throw an exception rather
            // than return false in certain failure scenarios.
            var result = true;
            try
            {
                var currentUser = this._provider.GetUser(userName, true /* userIsOnline */);
                if (currentUser != null)
                {
                    currentUser.Email = newEmail;
                    this._provider.UpdateUser(currentUser);
                }
            }
            catch (ArgumentException)
            {
                result = false;
            }
            catch (MembershipPasswordException)
            {
                result = false;
            }
            return result;
        }

        /// <summary>
        /// Creates the account.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <param name="email">The email.</param>
        /// <param name="role">The role.</param>
        /// <param name="requireConfirmationToken">if set to <c>true</c> [require confirmation token].</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentException">
        /// Value cannot be null or empty.;userName
        /// or
        /// Value cannot be null or empty.;password
        /// or
        /// Value cannot be null or empty.;email
        /// </exception>
        public override MembershipCreateStatus CreateAccount(string userName, string password, string email, 
                                                             string role, object propValues = null, bool requireConfirmationToken = false)
        {
            if (String.IsNullOrEmpty(userName))
            {
                throw new ArgumentException("Value cannot be null or empty.", "userName");
            }

            if (String.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Value cannot be null or empty.", "password");
            }

            if (String.IsNullOrEmpty(email))
            {
                throw new ArgumentException("Value cannot be null or empty.", "email");
            }

            var result = MembershipCreateStatus.Success;

            try
            {
                if (WebMatrix.WebData.WebSecurity.UserExists(userName))
                {
                    result = MembershipCreateStatus.DuplicateUserName;
                }
                else
                {
                    if(propValues == null)
                    {
                        WebMatrix.WebData.WebSecurity.CreateUserAndAccount(
                            userName, password, new { Email = email, IsActive = true }, requireConfirmationToken);
                    }
                    else // Use default values
                    {
                        WebMatrix.WebData.WebSecurity.CreateUserAndAccount(
                            userName, password, propValues, requireConfirmationToken);
                    }
                    

                    // Add role for user
                    Roles.AddUserToRole(userName, role);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("CreateAccount() --- function error: {0}", ex);
            }

            return result;
        }

        /// <summary>
        /// Creates the o authentication account.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <param name="providerUserId">The provider user identifier.</param>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        public override MembershipCreateStatus CreateOAuthAccount(
            string provider, string providerUserId, string userName)
        {
            if (String.IsNullOrEmpty(provider))
            {
                throw new ArgumentException("Value cannot be null or empty.", "provider");
            }

            if (String.IsNullOrEmpty(providerUserId))
            {
                throw new ArgumentException("Value cannot be null or empty.", "providerUserId");
            }

            if (String.IsNullOrEmpty(userName))
            {
                throw new ArgumentException("Value cannot be null or empty.", "userName");
            }

            try
            {
                this._provider.CreateOrUpdateOAuthAccount(provider, providerUserId, userName);
            }
            catch (Exception ex)
            {
                throw new Exception("CreateAccount() --- function error: {0}", ex);
            }
            
            return MembershipCreateStatus.Success;
        }

        /// <summary>
        /// Deletes the user.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="deleteAllRelatedData">if set to <c>true</c> [delete all related data].</param>
        /// <returns></returns>
        public override bool DeleteUser(string userName, bool deleteAllRelatedData)
        {
            var result = true;

            try
            {
                var roles = this._roleProvider.GetRolesForUser(userName);
                this._roleProvider.RemoveUsersFromRoles(new string[] { userName }, roles);
                this._provider.DeleteAccount(userName);
                this._provider.DeleteUser(userName, true);
            }
            catch
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <returns></returns>
        public override MembershipUserCollection GetAllUsers()
        {
            var allRoles = this._roleProvider.GetAllRoles();
            var usernames = new List<string>();

            foreach (var role in allRoles)
            {
                usernames.AddRange(_roleProvider.GetUsersInRole(role));
            }

            var users = new MembershipUserCollection();
            foreach (var username in usernames)
            {
                var user = this._provider.GetUser(username, false);
                if (user != null)
                {
                    users.Add(user);
                }
            }

            return users;
        }

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        public override MembershipUser GetUser(string userName)
        {
            return this._provider.GetUser(userName, false);
        }

        #endregion
    }
}