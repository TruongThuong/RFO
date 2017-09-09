using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFO.WebAPI.Models.Response
{
    public class SelectionResponseContext<T> : GenericResponseContext where T : class
    {
        /// <summary>
        /// Gets or sets the session.
        /// </summary>
        /// <value>
        /// The session.
        /// </value>
        public int Session { get; set; }

        /// <summary>
        /// Gets or sets the number total records.
        /// </summary>
        /// <value>
        /// The number total records.
        /// </value>
        public int NumTotalRecords { get; set; }

        /// <summary>
        /// Gets or sets the records.
        /// </summary>
        /// <value>
        /// The records.
        /// </value>
        public List<T> Records { get; set; }

        /// <summary>
        /// Gets or sets the record.
        /// </summary>
        /// <value>
        /// The record.
        /// </value>
        public T Record { get; set; }
    }
}