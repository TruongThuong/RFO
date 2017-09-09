
using System;
using RFO.Model;
using RFO.Common.Utilities.Logging;

namespace RFO.DAO
{

    public class UnitOfWork : IDisposable
    {
        #region Variables
        

        /// <summary>
        /// The logger instance
        /// </summary>
        private static readonly ILogger Logger = LoggerManager.GetLogger(typeof(UnitOfWork).Name);

        /// <summary>
        /// The database context
        /// </summary>
        private readonly RFODbContext dbContext = new RFODbContext();

        /// <summary>
        /// The disposed
        /// </summary>
        private bool disposed = false;

                /// <summary>
        /// The CommonInfo DAO
        /// </summary>
        private ICommonInfoDAO commonInfoDAO = null;

        /// <summary>
        /// The Menu DAO
        /// </summary>
        private IMenuDAO menuDAO = null;

        /// <summary>
        /// The Product DAO
        /// </summary>
        private IProductDAO productDAO = null;

        /// <summary>
        /// The OrderDetail DAO
        /// </summary>
        private IOrderDetailDAO orderDetailDAO = null;

        /// <summary>
        /// The Order DAO
        /// </summary>
        private IOrderDAO orderDAO = null;

        /// <summary>
        /// The OrderState DAO
        /// </summary>
        private IOrderStateDAO orderStateDAO = null;

        /// <summary>
        /// The Table DAO
        /// </summary>
        private ITableDAO tableDAO = null;

        /// <summary>
        /// The ProductImage DAO
        /// </summary>
        private IProductImageDAO productImageDAO = null;

        /// <summary>
        /// The Promotion DAO
        /// </summary>
        private IPromotionDAO promotionDAO = null;

        /// <summary>
        /// The User DAO
        /// </summary>
        private IUserDAO userDAO = null;


          
        
        #endregion
        
        #region Properties
        

                /// <summary>
        /// Gets the CommonInfo DAO.
        /// </summary>
        /// <value>
        /// The CommonInfo DAO.
        /// </value>
        public ICommonInfoDAO CommonInfoDAO
        {
            get
            {
                if (this.commonInfoDAO == null)
                {
                    this.commonInfoDAO = new CommonInfoDAO(dbContext);
                }
                return this.commonInfoDAO;
            }
        }

        /// <summary>
        /// Gets the Menu DAO.
        /// </summary>
        /// <value>
        /// The Menu DAO.
        /// </value>
        public IMenuDAO MenuDAO
        {
            get
            {
                if (this.menuDAO == null)
                {
                    this.menuDAO = new MenuDAO(dbContext);
                }
                return this.menuDAO;
            }
        }

        /// <summary>
        /// Gets the Product DAO.
        /// </summary>
        /// <value>
        /// The Product DAO.
        /// </value>
        public IProductDAO ProductDAO
        {
            get
            {
                if (this.productDAO == null)
                {
                    this.productDAO = new ProductDAO(dbContext);
                }
                return this.productDAO;
            }
        }

        /// <summary>
        /// Gets the OrderDetail DAO.
        /// </summary>
        /// <value>
        /// The OrderDetail DAO.
        /// </value>
        public IOrderDetailDAO OrderDetailDAO
        {
            get
            {
                if (this.orderDetailDAO == null)
                {
                    this.orderDetailDAO = new OrderDetailDAO(dbContext);
                }
                return this.orderDetailDAO;
            }
        }

        /// <summary>
        /// Gets the Order DAO.
        /// </summary>
        /// <value>
        /// The Order DAO.
        /// </value>
        public IOrderDAO OrderDAO
        {
            get
            {
                if (this.orderDAO == null)
                {
                    this.orderDAO = new OrderDAO(dbContext);
                }
                return this.orderDAO;
            }
        }

        /// <summary>
        /// Gets the OrderState DAO.
        /// </summary>
        /// <value>
        /// The OrderState DAO.
        /// </value>
        public IOrderStateDAO OrderStateDAO
        {
            get
            {
                if (this.orderStateDAO == null)
                {
                    this.orderStateDAO = new OrderStateDAO(dbContext);
                }
                return this.orderStateDAO;
            }
        }

        /// <summary>
        /// Gets the Table DAO.
        /// </summary>
        /// <value>
        /// The Table DAO.
        /// </value>
        public ITableDAO TableDAO
        {
            get
            {
                if (this.tableDAO == null)
                {
                    this.tableDAO = new TableDAO(dbContext);
                }
                return this.tableDAO;
            }
        }

        /// <summary>
        /// Gets the ProductImage DAO.
        /// </summary>
        /// <value>
        /// The ProductImage DAO.
        /// </value>
        public IProductImageDAO ProductImageDAO
        {
            get
            {
                if (this.productImageDAO == null)
                {
                    this.productImageDAO = new ProductImageDAO(dbContext);
                }
                return this.productImageDAO;
            }
        }

        /// <summary>
        /// Gets the Promotion DAO.
        /// </summary>
        /// <value>
        /// The Promotion DAO.
        /// </value>
        public IPromotionDAO PromotionDAO
        {
            get
            {
                if (this.promotionDAO == null)
                {
                    this.promotionDAO = new PromotionDAO(dbContext);
                }
                return this.promotionDAO;
            }
        }

        /// <summary>
        /// Gets the User DAO.
        /// </summary>
        /// <value>
        /// The User DAO.
        /// </value>
        public IUserDAO UserDAO
        {
            get
            {
                if (this.userDAO == null)
                {
                    this.userDAO = new UserDAO(dbContext);
                }
                return this.userDAO;
            }
        }


          
        
        #endregion
        
        #region Methods
        

        /// <summary>
        /// Saves the changes.
        /// </summary>
        /// <param name="handleExceptionCallbacFunc">The handle exception callbac function.</param>
        public void SaveChanges(Action<Exception> handleExceptionCallbacFunc)
        {
            var funcName = "SaveChanges";
            Logger.DebugFormat("{0} <-- Start", funcName);

            try
            {
                this.dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("{0} - Exception: {1}", funcName, ex);
                handleExceptionCallbacFunc.Invoke(ex);
            }

            Logger.DebugFormat("{0} --> End", funcName);
        }
        
        
        #endregion
        
        #region Implementation of IDisposable
        

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    dbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
          
        
        #endregion
        
    }
}
