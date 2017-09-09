
using System.Web.Security;

namespace RFO.AspNet.Utilities.MembershipService
{
    /// <summary>
    /// Membership service
    /// </summary>
    public interface IMembershipService
    {
        /// <summary>
        /// Gets a value indicating whether this instance is authenticated.
        /// </summary>
        /// <value>
        /// <c>true</c> if this instance is authenticated; otherwise, <c>false</c>.
        /// </value>
        bool IsAuthenticated();

        /// <summary>
        /// Gets the name of the current user.
        /// </summary>
        /// <value>
        /// The name of the current user.
        /// </value>
        string GetCurrentUserName();

        /// <summary>
        /// Logins the specified user name.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <param name="persistCookie">if set to <c>true</c> [persist cookie].</param>
        /// <returns></returns>
        bool Login(string userName, string password, bool persistCookie = false);

        /// <summary>
        /// Logouts this instance.
        /// </summary>
        void Logout();

        /// <summary>
        /// Confirms the account.
        /// </summary>
        /// <param name="accountConfirmationToken">The account confirmation token.</param>
        /// <returns></returns>
        bool ConfirmAccount(string accountConfirmationToken);

        /// <summary>
        /// Validates the user.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        bool ValidateUser(string userName, string password);

        /// <summary>
        /// Changes the password.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="oldPassword">The old password.</param>
        /// <param name="newPassword">The new password.</param>
        /// <returns></returns>
        bool ChangePassword(string userName, string oldPassword, string newPassword);

        /// <summary>
        /// Changes the email.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="newEmail">The new email.</param>
        /// <returns></returns>
        bool ChangeEmail(string userName, string newEmail);

        /// <summary>
        /// Creates the account.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <param name="email">The email.</param>
        /// <param name="role">The role.</param>
        /// <param name="requireConfirmationToken">if set to <c>true</c> [require confirmation token].</param>
        /// <returns></returns>
        MembershipCreateStatus CreateAccount(string userName, string password, string email, 
                                             string role, object propValues = null, bool requireConfirmationToken = false);

        /// <summary>
        /// Creates the o authentication account.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <param name="providerUserId">The provider user identifier.</param>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        MembershipCreateStatus CreateOAuthAccount(string provider, string providerUserId, string userName);

        /// <summary>
        /// Deletes the user.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="deleteAllRelatedData">if set to <c>true</c> [delete all related data].</param>
        /// <returns></returns>
        bool DeleteUser(string userName, bool deleteAllRelatedData);

        /// <summary>
        /// Gets all users.
        /// </summary>
        /// <returns></returns>
        MembershipUserCollection GetAllUsers();

        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <returns></returns>
        MembershipUser GetUser(string userName);
    }
}