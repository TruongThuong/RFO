using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RFO.WebAPI.Models.Request
{
    public class SelectionRequestContext : GenericRequestContext
    {
        /// <summary>
        /// Gets or sets the session.
        /// </summary>
        /// <value>
        /// The session.
        /// </value>
        public int Session { get; set; }

        /// <summary>
        /// Gets or sets the start index of the record.
        /// </summary>
        /// <value>
        /// The start index of the record.
        /// </value>
        public int StartRecordIndex { get; set; }

        /// <summary>
        /// Gets or sets the number records per page.
        /// </summary>
        /// <value>
        /// The number records per page.
        /// </value>
        public int NumRecordsPerPage { get; set; }

        /// <summary>
        /// Gets or sets the index of the sort column.
        /// </summary>
        /// <value>
        /// The index of the sort column.
        /// </value>
        public int SortColumnIndex { get; set; }

        /// <summary>
        /// Gets or sets the sort direction.
        /// </summary>
        /// <value>
        /// The sort direction.
        /// </value>
        public string SortDirection { get; set; }

        /// <summary>
        /// Gets or sets the search keyword.
        /// </summary>
        /// <value>
        /// The search keyword.
        /// </value>
        public string SearchKeyword { get; set; }

        /// <summary>
        /// Gets or sets the search foreign keys.
        /// </summary>
        /// <value>
        /// The search foreign keys.
        /// </value>
        public Dictionary<string, string> SearchForeignKeys { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectionRequestContext"/> class.
        /// </summary>
        public SelectionRequestContext()
        {
            this.Session = -1;
            this.StartRecordIndex = 0;
            this.NumRecordsPerPage = 12;
            this.SortColumnIndex = 0;
            this.SearchKeyword = string.Empty;
            this.SortDirection = "asc";
            this.SearchForeignKeys = new Dictionary<string, string>();
        }
    }
}