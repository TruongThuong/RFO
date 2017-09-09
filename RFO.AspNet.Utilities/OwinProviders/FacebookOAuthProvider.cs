
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin.Security.Facebook;

namespace HAVA.Framework.AspNet.Utilities.OwinProviders
{
    public class FacebookOAuthProvider : FacebookAuthenticationProvider
    {
        private const string ApiBaseUrl = "https://graph.facebook.com";

        /// <summary>
        /// Invoked whenever Facebook succesfully authenticates a user
        /// </summary>
        /// <param name="context">Contains information about the login session as well as the user <see cref="T:System.Security.Claims.ClaimsIdentity" />.</param>
        /// <returns>
        /// A <see cref="T:System.Threading.Tasks.Task" /> representing the completed operation.
        /// </returns>
        public override Task Authenticated(FacebookAuthenticatedContext context)
        {
            var avatarUrl = GetAvatarUrl(context.User.GetValue("id").ToString(), 240);
            context.Identity.AddClaim(
                new Claim(OwinHelper.ClaimTypeAvatarUrl, avatarUrl));

            return base.Authenticated(context);
        }

        /// <summary>
        /// Gets the avatar URL.
        /// </summary>
        /// <param name="facebookUserId">The facebook user identifier.</param>
        /// <param name="size">The size.</param>
        /// <returns></returns>
        public static string GetAvatarUrl(string facebookUserId, int size)
        {
            return string.Format("{0}/{1}/picture?width={2}&height={2}",
                ApiBaseUrl,
                facebookUserId,
                size);
        }
    }
}