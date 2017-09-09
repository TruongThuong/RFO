using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFO.WebAPI.Models.Response
{
    public class GenericResponseContext
    {
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="SelectionResponseContext"/> is result.
        /// </summary>
        /// <value>
        ///   <c>true</c> if result; otherwise, <c>false</c>.
        /// </value>
        public bool Result { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }
    }
}