
using RFO.MetaData;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace RFO.DAO.Args
{
    public class EntityQueryArgs<T> where T : class
    {
        /// <summary>
        /// Gets or sets the record start.
        /// </summary>
        /// <value>
        /// The record start.
        /// </value>
        public int StartRecordIndex { get; set; }

        /// <summary>
        /// Gets or sets the number records per page.
        /// Optional
        /// </summary>
        /// <value>
        /// The number records per page.
        /// </value>
        public int NumRecordsPerPage { get; set; }

        /// <summary>
        /// Gets or sets the order direction.
        /// Optional: asc/desc, default is asc
        /// </summary>
        /// <value>
        /// The order direction: asc/desc
        /// </value>
        public string SortDirection { get; set; }

        /// <summary>
        /// Gets or sets the order by expr.
        /// Optional
        /// </summary>
        /// <value>
        /// The order by expr.
        /// </value>
        public Expression<Func<T, object>> OrderByExpr { get; set; }

        /// <summary>
        /// Gets or sets the filter expr.
        /// Optional
        /// </summary>
        /// <value>
        /// The filter expr.
        /// </value>
        public Expression<Func<T, bool>> FilterExpr { get; set; }

        /// <summary>
        /// Gets or sets the include queryable function.
        /// Optional
        /// </summary>
        /// <value>
        /// The include queryable function.
        /// </value>
        public Func<IQueryable<T>, IQueryable<T>> IncludeQueryableFunc { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityQueryArgs{T}"/> class.
        /// </summary>
        public EntityQueryArgs()
        {
            this.StartRecordIndex = 0;
            this.NumRecordsPerPage = AppConstants.NumRecordPerPage;
            this.SortDirection = "asc";
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return $"StartRecordIndex={this.StartRecordIndex}, NumRecordsPerPage={this.NumRecordsPerPage}, " +
                    $"OrderDirection={this.SortDirection}, FilterExpr={(this.FilterExpr == null ? string.Empty : this.FilterExpr.ToString())}, " +
                    $"OrderByExpr={(this.OrderByExpr == null ? string.Empty : this.OrderByExpr.ToString())}, " +
                    $"IncludeQueryableFunc={this.IncludeQueryableFunc != null}";
        }
    }
}
