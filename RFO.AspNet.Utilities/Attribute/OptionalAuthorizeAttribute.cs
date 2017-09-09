
using System.Web;
using System.Web.Mvc;

namespace RFO.AspNet.Utilities.Attribute
{
    /// <summary>
    /// The custom authorisation attribute inheriting from the standard AuthorizeAttribute
    /// <para>
    /// with an optional bool parameter to specify whether authorisation is required or not.
    /// </para>
    /// </summary>
    public class OptionalAuthorizeAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// The authorize
        /// </summary>
        private readonly bool _authorize;

        /// <summary>
        /// Initializes a new instance of the <see cref="OptionalAuthorizeAttribute" /> class.
        /// </summary>
        public OptionalAuthorizeAttribute()
        {
            this._authorize = true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OptionalAuthorizeAttribute" /> class.
        /// </summary>
        /// <param name="authorize">if set to <c>true</c> [authorize].</param>
        public OptionalAuthorizeAttribute(bool authorize)
        {
            this._authorize = authorize;
        }

        /// <summary>
        /// When overridden, provides an entry point for custom authorization checks.
        /// </summary>
        /// <param name="httpContext">
        /// The HTTP context, which encapsulates all HTTP-specific information about an individual HTTP
        /// request.
        /// </param>
        /// <returns>
        /// true if the user is authorized; otherwise, false.
        /// </returns>
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return !this._authorize || base.AuthorizeCore(httpContext);
        }
    }
}