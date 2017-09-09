using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFO.WebAPI.Models.Request
{
    public class UpdateRequestContext<T> : GenericRequestContext where T : class
    {
        /// <summary>
        /// Gets or sets the record.
        /// </summary>
        /// <value>
        /// The record.
        /// </value>
        public T Record { get; set; }

        /// <summary>
        /// Gets or sets the foreign keys.
        /// </summary>
        /// <value>
        /// The foreign keys.
        /// </value>
        public Dictionary<string, string> ForeignKeys { get; set; }
    }
}