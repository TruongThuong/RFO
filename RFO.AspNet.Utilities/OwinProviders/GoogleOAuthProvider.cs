using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin.Security.Google;

namespace HAVA.Framework.AspNet.Utilities.OwinProviders
{
    public class GoogleOAuthProvider : GoogleOAuth2AuthenticationProvider
    {
        /// <summary>
        /// Invoked whenever Google succesfully authenticates a user
        /// </summary>
        /// <param name="context">Contains information about the login session as well as the user <see cref="T:System.Security.Claims.ClaimsIdentity" />.</param>
        /// <returns>
        /// A <see cref="T:System.Threading.Tasks.Task" /> representing the completed operation.
        /// </returns>
        public override Task Authenticated(GoogleOAuth2AuthenticatedContext context)
        {
            var avatarUrl = context.User
                .SelectToken("image.url")
                .ToString()
                .Replace("sz=50", "sz=240");

            context.Identity.AddClaim(
                new Claim(OwinHelper.ClaimTypeAvatarUrl, avatarUrl));

            return base.Authenticated(context);
        }
    }
}