
using RFO.DAO.Args;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace RFO.DAO
{
    /// <summary>
    /// DAO interface
    /// </summary>
    public interface IDAO<T> where T : class
    {
        /// <summary>
        /// Total record of current query from database
        /// </summary>
        int TotalRecords { get; set; }

        /// <summary>
        /// Selects all.
        /// </summary>
        /// <returns></returns>
        List<T> SelectAll();

        /// <summary>
        /// Selects all.
        /// </summary>
        /// <param name="queryArgs">The query parameter.</param>
        /// <returns></returns>
        /// <exception cref="DatabaseException"></exception>
        List<T> Select(EntityQueryArgs<T> queryArgs);

        /// <summary>
        /// Selects the by filter expr.
        /// </summary>
        /// <param name="filterExpr">The filter expr.</param>
        /// <param name="includeQueryableFunc">The include queryable function.</param>
        /// <returns></returns>
        T SelectByFilterExpr(Expression<Func<T, bool>> filterExpr, Func<IQueryable<T>, IQueryable<T>> includeQueryableFunc = null);

        /// <summary>
        /// Selects the by identifier.
        /// </summary>
        /// <param name="recordId">The record identifier.</param>
        /// <param name="includeQueryableFunc">The include queryable function.</param>
        /// <returns></returns>
        T SelectByID(int recordId, Func<IQueryable<T>, IQueryable<T>> includeQueryableFunc = null);

        /// <summary>
        /// Insert a new element to corresponding table
        /// </summary>
        /// <param name="record">The record will be inserted</param>
        void Insert(T record);

        /// <summary>
        /// Delete an element in corresponding table
        /// </summary>
        /// <param name="recordId">The key for identifying an record</param>
        void Delete(int recordId);

        /// <summary>
        /// Update an specified element in corresponding table
        /// </summary>
        /// <param name="record">Specified record to be updated</param>
        void Update(T record);

        /// <summary>
        /// Determines whether or not the given specification attribute is exist.
        /// </summary>
        /// <param name="specificationAttr">The attribute for specification</param>
        /// <returns></returns>
        bool IsExist(string specificationAttr);

        /// <summary>
        /// Builds the order by expression.
        /// </summary>
        /// <returns></returns>
        Expression<Func<T, object>> BuildOrderByExpression();

        /// <summary>
        /// Detaches the specified record.
        /// </summary>
        /// <param name="record">The record.</param>
        void Detach(T record);

        /// <summary>
        /// Attaches the specified record.
        /// </summary>
        /// <param name="record">The record.</param>
        void Attach(T record);
    }
}