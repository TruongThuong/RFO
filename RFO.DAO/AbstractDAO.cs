
using System.Collections.Generic;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Entity;
using RFO.DAO.Args;
using RFO.MetaData;
using RFO.Common.Utilities.Logging;
using RFO.Model;
using RFO.Common.Utilities.Exceptions;

namespace RFO.DAO
{
    /// <summary>
    /// Abstract class for DAOs
    /// </summary>
    public abstract class AbstractDAO<T> : IDAO<T> where T : class
    {
        #region Variables
        
        /// <summary>
        /// The logger instance
        /// </summary>
        private static readonly ILogger Logger = LoggerManager.GetLogger(typeof(AbstractDAO<T>).Name);

        /// <summary>
        /// The dbcontext
        /// </summary>
        protected RFODbContext dbContext;

        /// <summary>
        /// The database set
        /// </summary>
        protected DbSet<T> dbSet;

        /// <summary>
        /// The synchronize lock
        /// </summary>
        private static readonly object syncLock = new object();

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="AbstractRepository{T}"/> class.
        /// </summary>
        /// <param name="inDbContext">The database context.</param>
        protected AbstractDAO(RFODbContext inDbContext)
        {
            this.dbContext = inDbContext;
            this.dbSet = this.dbContext.Set<T>();
        }

        #endregion

        #region Implementation of IDAO

        /// <summary>
        /// Total record of current query from database
        /// </summary>
        public int TotalRecords { get; set; }

        /// <summary>
        /// Selects all.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="DatabaseException"></exception>
        public virtual List<T> SelectAll()
        {
            var funcName = "SelectAll";
            Logger.DebugFormat("{0}...", funcName);
            List<T> result;

            try
            {
                var queryArgs = new EntityQueryArgs<T>
                {
                    StartRecordIndex = 0,
                    NumRecordsPerPage = AppConstants.MaxRecordsPerPage,
                    OrderByExpr = this.BuildOrderByExpression()
                };
                result = this.GetRecords(queryArgs);
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("{0} - Exception: {1}", funcName, ex.ToString());
                throw new DatabaseException((int)DatabaseErrorCode.Load, ex);
            }

            Logger.DebugFormat("{0}...DONE", funcName);
            return result;
        }

        /// <summary>
        /// Selects all.
        /// </summary>
        /// <param name="queryArgs">The query parameter.</param>
        /// <returns></returns>
        /// <exception cref="DatabaseException"></exception>
        public virtual List<T> Select(EntityQueryArgs<T> queryArgs)
        {
            var funcName = "Select";
            Logger.DebugFormat("{0}...", funcName);
            List<T> result;

            try
            {
                result = this.GetRecords(queryArgs);
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("{0} - Exception: {1}", funcName, ex.ToString());
                throw new DatabaseException((int)DatabaseErrorCode.Load, ex);
            }

            Logger.DebugFormat("{0}...DONE", funcName);
            return result;
        }

        /// <summary>
        /// Selects the by filter expr.
        /// </summary>
        /// <param name="filterExpr">The filter expr.</param>
        /// <param name="includeQueryableFunc">The include queryable function.</param>
        /// <returns></returns>
        /// <exception cref="DatabaseException"></exception>
        public T SelectByFilterExpr(Expression<Func<T, bool>> filterExpr, Func<IQueryable<T>, IQueryable<T>> includeQueryableFunc = null)
        {
            var funcName = "SelectByFilterExpr";
            Logger.DebugFormat("{0}...", funcName);
            T result;

            try
            {
                // Cast DbSet to IQueryable to get included informations
                var records = this.dbSet as IQueryable<T>;

                records = includeQueryableFunc == null
                    ? this.BuildIncludesQueryable(records)
                    : includeQueryableFunc.Invoke(records);

                // Using built expression to find specified record in database
                result = records.FirstOrDefault(filterExpr);
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("{0} - Exception: {1}", funcName, ex.ToString());
                throw new DatabaseException((int)DatabaseErrorCode.Load, ex);
            }

            Logger.DebugFormat("{0}...DONE", funcName);
            return result;
        }

        /// <summary>
        /// Selects the by identifier.
        /// </summary>
        /// <param name="recordId">The record identifier.</param>
        /// <param name="includeQueryableFunc">The include queryable function.</param>
        /// <returns></returns>
        /// <exception cref="DatabaseException"></exception>
        public virtual T SelectByID(int recordId, Func<IQueryable<T>, IQueryable<T>> includeQueryableFunc = null)
        {
            var funcName = "SelectByID";
            Logger.DebugFormat("{0}...", funcName);
            Logger.DebugFormat("{0} - Input params: {1}", funcName, recordId);
            T result;

            try
            {
                // Build expression to find specified record
                var isMatch = this.BuildSearchingByIdExpression(recordId);

                // Cast DbSet to IQueryable to get included informations
                var records = this.dbSet as IQueryable<T>;

                records = includeQueryableFunc == null
                    ? this.BuildIncludesQueryable(records)
                    : includeQueryableFunc.Invoke(records);

                // Using built expression to find specified record in database
                result = records.FirstOrDefault(isMatch);
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("{0} - Exception: {1}", funcName, ex.ToString());
                throw new DatabaseException((int)DatabaseErrorCode.Load, ex);
            }

            Logger.DebugFormat("{0}...DONE", funcName);
            return result;
        }

        /// <summary>
        /// Insert a new element to corresponding table
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="record">The record will be inserted</param>
        public virtual void Insert(T record)
        {
            var funcName = "Insert";
            Logger.DebugFormat("{0}...", funcName);

            try
            {
                this.dbSet.Add(record);
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("{0} - Exception: {1}", funcName, ex.ToString());
                throw new DatabaseException((int)DatabaseErrorCode.Insert, ex);
            }

            Logger.Debug("Insert...DONE");
        }

        /// <summary>
        /// Delete an element in corresponding table
        /// </summary>
        /// <param name="recordId">The key for identifying an record</param>
        public virtual void Delete(int recordId)
        {
            var funcName = "Delete";
            Logger.DebugFormat("{0}...", funcName);
            Logger.DebugFormat("Input params: recordId = {0}", recordId);

            try
            {
                var foundItem = dbSet.Find(recordId);
                if (foundItem != null)
                {
                    if (this.dbContext.Entry(foundItem).State == EntityState.Detached)
                    {
                        dbSet.Attach(foundItem);
                    }
                    dbSet.Remove(foundItem);
                }
                else
                {
                    var errMsg = string.Format("There is no record with specified ID={0}", recordId);
                    Logger.ErrorFormat("{0} - Exception: {1}", funcName, errMsg);
                    throw new DatabaseException(errMsg);
                }
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("{0} - Exception: {1}", funcName, ex.ToString());
                throw new DatabaseException((int)DatabaseErrorCode.Delete, ex);
            }

            Logger.DebugFormat("{0}...DONE", funcName);
        }

        /// <summary>
        /// Update an specified element in corresponding table
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="record">Specified record to be updated</param>
        public virtual void Update(T record)
        {
            var funcName = "Update";
            Logger.DebugFormat("{0}...", funcName);

            try
            {
                this.dbSet.Attach(record);
                this.dbContext.Entry(record).State = EntityState.Modified;
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("{0} - Exception: {1}", funcName, ex.ToString());
                throw new DatabaseException((int)DatabaseErrorCode.Update, ex);
            }

            Logger.DebugFormat("{0}...DONE", funcName);
        }

        /// <summary>
        /// Determines whether or not the given specification attribute is exist.
        /// </summary>
        /// <param name="specificationAttr">The attribute for specification</param>
        /// <returns></returns>
        public virtual bool IsExist(string specificationAttr)
        {
            var funcName = "IsExist";
            Logger.DebugFormat("{0}...", funcName);
            Logger.DebugFormat("{0} - Input params: specificationAttr={1}", funcName, specificationAttr);

            bool result;

            try
            {
                // Build expression for checking the existence of specification attribute
                var isMatch = this.BuildExistentValidationExpression(specificationAttr);

                // Checking the existence based on expression
                result = this.dbSet.Count(isMatch) > 1;
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("{0} - Exception: {1}", funcName, ex.ToString());
                throw new DatabaseException((int)DatabaseErrorCode.Update, ex);
            }

            Logger.DebugFormat("{0} - Result=[{1}]", funcName, result);
            Logger.DebugFormat("{0}...DONE", funcName);
            return result;
        }

        /// <summary>
        /// Detaches the specified record.
        /// </summary>
        /// <param name="record">The record.</param>
        /// <exception cref="DatabaseException"></exception>
        public virtual void Detach(T record)
        {
            var funcName = "Detach";
            Logger.DebugFormat("{0}...", funcName);

            try
            {
                this.dbContext.Entry(record).State = EntityState.Detached;
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("{0} - Exception: {1}", funcName, ex.ToString());
                throw new DatabaseException((int)DatabaseErrorCode.Update, ex);
            }

            Logger.DebugFormat("{0}...DONE", funcName);
        }

        /// <summary>
        /// Attaches the specified record.
        /// </summary>
        /// <param name="record">The record.</param>
        /// <exception cref="DatabaseException"></exception>
        public virtual void Attach(T record)
        {
            var funcName = "Attach";
            Logger.DebugFormat("{0}...", funcName);

            try
            {
                this.dbSet.Attach(record);
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("{0} - Exception: {1}", funcName, ex.ToString());
                throw new DatabaseException((int)DatabaseErrorCode.Update, ex);
            }

            Logger.DebugFormat("{0}...DONE", funcName);
        }

        /// <summary>
        /// Builds the order by expression.
        /// </summary>
        /// <returns></returns>
        public abstract Expression<Func<T, object>> BuildOrderByExpression();

        #endregion

        #region Protected methods

        /// <summary>
        /// Gets the records.
        /// </summary>
        /// <param name="queryArgs">The parameters.</param>
        /// <returns></returns>
        protected virtual List<T> GetRecords(EntityQueryArgs<T> queryArgs)
        {
            lock (syncLock)
            {
                var funcName = "GetRecords";
                Logger.DebugFormat("{0}...", funcName);
                Logger.DebugFormat("{0} - Input parameters: {1}", funcName, queryArgs.ToString());
                List<T> result;

                // Trace SQL generated by the entity framework
                this.dbContext.Database.Log = s => Logger.Debug(Environment.NewLine + "\t" + s);

                // Build query to select records (maybe by foreign key)
                var records = (queryArgs.FilterExpr != null ? dbSet.Where(queryArgs.FilterExpr) : dbSet);

                // Execute query to count total records (maybe by foreign key) from database
                this.TotalRecords = records.Count();

                // User already sort
                if (queryArgs.OrderByExpr != null)
                {
                    // Append order by expression into built query
                    records = queryArgs.SortDirection.Equals("asc")
                        ? records.OrderBy(queryArgs.OrderByExpr)
                        : records.OrderByDescending(queryArgs.OrderByExpr);
                }

                // Append includes queryable to join reference tables into built query
                records = queryArgs.IncludeQueryableFunc == null
                    ? this.BuildIncludesQueryable(records)
                    : queryArgs.IncludeQueryableFunc.Invoke(records);

                // Execute built query to select records from database
                result = records.Skip(queryArgs.StartRecordIndex).Take(queryArgs.NumRecordsPerPage).ToList();

                Logger.DebugFormat("{0}...DONE", funcName);
                return result;
            }
        }

        /// <summary>
        /// Builds the searching by identifier expression.
        /// </summary>
        /// <param name="recordId">The record identifier.</param>
        /// <returns></returns>
        protected abstract Expression<Func<T, bool>> BuildSearchingByIdExpression(int recordId);

        /// <summary>
        /// Builds the existent validation expression.
        /// </summary>
        /// <param name="specificationAttr">The specification attribute.</param>
        /// <returns></returns>
        protected abstract Expression<Func<T, bool>> BuildExistentValidationExpression(string specificationAttr);

        /// <summary>
        /// Builds the includes queryable.
        /// </summary>
        /// <param name="records">The records.</param>
        /// <param name="loadReference">if set to <c>true</c> [load reference].</param>
        /// <param name="loadChilds">if set to <c>true</c> [load childs].</param>
        /// <returns></returns>
        protected abstract IQueryable<T> BuildIncludesQueryable(IQueryable<T> records, bool loadReference = true, bool loadChilds = false);

        #endregion
    }
}