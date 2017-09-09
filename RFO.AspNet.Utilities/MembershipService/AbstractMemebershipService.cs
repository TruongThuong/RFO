
using System;
using System.Web.Security;

namespace RFO.AspNet.Utilities.MembershipService
{
    /// <summary>
    /// Abstract membership service
    /// </summary>
    public abstract class AbstractMemebershipService : IMembershipService
    {
        #region Implementation of IMembershipService

        /// <summary>
        /// Gets a value indicating whether this instance is authenticated.
        /// </summary>
        /// <returns></returns>
        /// <value>
        ///   <c>true</c> if this instance is authenticated; otherwise, <c>false</c>.
        /// </value>
        public abstract bool IsAuthenticated();

        /// <summary>
        /// Gets the name of the current user.
        /// </summary>
        /// <returns></returns>
        /// <value>
        /// The name of the current user.
        /// </value>
        public abstract string GetCurrentUserName();

        /// <summary>
        /// Logins the specified user name.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <param name="persistCookie">if set to <c>true</c> [persist cookie].</param>
        /// <returns></returns>
        public abstract bool Login(string userName, string password, bool persistCookie = false);

        /// <summary>
        /// Logouts this instance.
        /// </summary>
        public abstract void Logout();

        /// <summary>
        /// Confirms the account.
        /// </summary>
        /// <param name="accountConfirmationToken">The account confirmation token.</param>
        /// <returns></returns>
        public abstract bool ConfirmAccount(string accountConfirmationToken);

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
        public virtual bool ValidateUser(string userName, string password)
        {
            if (String.IsNullOrEmpty(userName))
                throw new ArgumentException("Value cannot be null or empty.", "userName");
            if (String.IsNullOrEmpty(password))
                throw new ArgumentException("Value cannot be null or empty.", "password");

            return Membership.ValidateUser(userName, password);
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
        public virtual bool ChangePassword(string userName, string oldPassword, string newPassword)
        {
            if (String.IsNullOrEmpty(userName))
                throw new ArgumentException("Value cannot be null or empty.", "userName");
            if (String.IsNullOrEmpty(oldPassword))
                throw new ArgumentException("Value cannot be null or empty.", "oldPassword");
            if (String.IsNullOrEmpty(newPassword))
                throw new ArgumentException("Value cannot be null or empty.", "newPassword");

            // The underlying ChangePassword() will throw an exception rather
            // than return false in certain failure scenarios.
            try
            {
                var currentUser = Membership.GetUser(userName, true /* userIsOnline */);
                return currentUser != null && currentUser.ChangePassword(oldPassword, newPassword);
            }
            catch (ArgumentException)
            {
                return false;
            }
            catch (MembershipPasswordException)
            {
                return false;
            }
        }

        /// <summary>
        /// Changes the email.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="newEmail">The new email.</param>
        /// <returns></returns>
        public virtual bool ChangeEmail(string userName, string newEmail)
        {
            // The underlying ChangePassword() will throw an exception rather
            // than return false in certain failure scenarios.
            var result = true;
            try
            {
                var currentUser = Membership.GetUser(userName, true /* userIsOnline */);
                if (currentUser != null)
                {
                    currentUser.Email = newEmail;
                    Membership.UpdateUser(currentUser);
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
        public virtual MembershipCreateStatus CreateAccount(string userName, string password, string email, 
                                                            string role, object propValues = null, bool requireConfirmationToken = false)
        {
            if (String.IsNullOrEmpty(userName))
                throw new ArgumentException("Value cannot be null or empty.", "userName");
            if (String.IsNullOrEmpty(password))
                throw new ArgumentException("Value cannot be null or empty.", "password");
            if (String.IsNullOrEmpty(email)) throw new ArgumentException("Value cannot be null or empty.", "email");

            MembershipCreateStatus status;
            Membership.CreateUser(userName, password, email, null, null, true, null, out status);

            switch (status)
            {
                case MembershipCreateStatus.Success:
                    Roles.AddUserToRole(userName, role);
                    break;
            }

            return status;
        }

        /// <summary>
        /// Creates the o authentication account.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <param name="providerUserId">The provider user identifier.</param>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        public virtual MembershipCreateStatus CreateOAuthAccount(
            string provider, string providerUserId, string userName)
        {
            return MembershipCreateStatus.Success;
        }

        /// <summary>
        /// Deletes the user.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="deleteAllRelatedData">if set to <c>true</c> [delete all related data].</param>
        /// <returns></returns>
        public virtual bool DeleteUser(string userName, bool deleteAllRelatedData)
        {
            bool result;

            try
            {
                var roles = Roles.GetRolesForUser(userName);
                Roles.RemoveUsersFromRoles(new string[] { userName }, roles);
                result = Membership.DeleteUser(userName, deleteAllRelatedData);
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
        public virtual MembershipUserCollection GetAllUsers()
        {
            return Membership.GetAllUsers();
        }

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        public virtual MembershipUser GetUser(string userName)
        {
            return Membership.GetUser(userName, false);
        }

        #endregion
    }
}