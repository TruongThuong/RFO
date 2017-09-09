
using System.Reflection;
using System.Web.Mvc;

namespace RFO.AspNet.Utilities.Attribute
{
    /// <summary>
    /// The class is used to support overload actions in controllers
    /// </summary>
    public class RequireRequestValueAttribute : ActionMethodSelectorAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequireRequestValueAttribute"/> class.
        /// </summary>
        /// <param name="valueNames">The value names.</param>
        public RequireRequestValueAttribute(string[] valueNames)
        {
            this.ValueNames = valueNames;
        }

        /// <summary>
        /// Gets the value names.
        /// </summary>
        /// <value>
        /// The value names.
        /// </value>
        public string[] ValueNames { get; private set; }

        /// <summary>
        /// Determines whether the action method selection is valid for the specified controller context.
        /// </summary>
        /// <param name="controllerContext">The controller context.</param>
        /// <param name="methodInfo">Information about the action method.</param>
        /// <returns>
        /// true if the action method selection is valid for the specified controller context; otherwise, false.
        /// </returns>
        public override bool IsValidForRequest(ControllerContext controllerContext, MethodInfo methodInfo)
        {
            var contains = false;
            foreach (var value in this.ValueNames)
            {
                contains = controllerContext.HttpContext.Request[value] != null;
                if (!contains)
                {
                    break;
                }
            }
            return contains;
        }
    }
}