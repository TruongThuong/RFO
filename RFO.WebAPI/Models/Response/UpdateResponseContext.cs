using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFO.WebAPI.Models.Response
{
    public class UpdateResponseContext<T> : GenericResponseContext where T : class
    {
        /// <summary>
        /// Gets or sets the record.
        /// </summary>
        /// <value>
        /// The out record.
        /// </value>
        public T Record { get; set; }
    }
}