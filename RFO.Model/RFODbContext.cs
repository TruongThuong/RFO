
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace RFO.Model
{
    public class RFODbContext : DbContext
    {
        public RFODbContext() : base("name=RFODbContext")
        {
        }

        /// <summary>
        /// Gets or sets the users.
        /// </summary>
        /// <value>
        /// The users.
        /// </value>
        public virtual DbSet<User> Users { get; set; }

        /// <summary>
        /// Gets or sets the tables.
        /// </summary>
        /// <value>
        /// The tables.
        /// </value>
        public virtual DbSet<Table> Tables { get; set; }

        /// <summary>
        /// Gets or sets the order states.
        /// </summary>
        /// <value>
        /// The order states.
        /// </value>
        public virtual DbSet<OrderState> OrderStates { get; set; }

        /// <summary>
        /// Gets or sets the orders.
        /// </summary>
        /// <value>
        /// The orders.
        /// </value>
        public virtual DbSet<Order> Orders { get; set; }

        /// <summary>
        /// Gets or sets the order details.
        /// </summary>
        /// <value>
        /// The order details.
        /// </value>
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }

        /// <summary>
        /// Gets or sets the menus.
        /// </summary>
        /// <value>
        /// The menus.
        /// </value>
        public virtual DbSet<Menu> Menus { get; set; }

        /// <summary>
        /// Gets or sets the products.
        /// </summary>
        /// <value>
        /// The products.
        /// </value>
        public virtual DbSet<Product> Products { get; set; }

        /// <summary>
        /// Gets or sets the product images.
        /// </summary>
        /// <value>
        /// The product images.
        /// </value>
        public virtual DbSet<ProductImage> ProductImages { get; set; }

        /// <summary>
        /// Gets or sets the promotions.
        /// </summary>
        /// <value>
        /// The promotions.
        /// </value>
        public virtual DbSet<Promotion> Promotions { get; set; }

        /// <summary>
        /// Gets or sets the common infoes.
        /// </summary>
        /// <value>
        /// The common infoes.
        /// </value>
        public virtual DbSet<CommonInfo> CommonInfoes { get; set; }

        /// <summary>
        /// This method is called when the model for a derived context has been initialized, but
        /// before the model has been locked down and used to initialize the context.  The default
        /// implementation of this method does nothing, but it can be overridden in a derived class
        /// such that the model can be further configured before it is locked down.
        /// </summary>
        /// <param name="modelBuilder">The builder that defines the model for the context being created.</param>
        /// <remarks>
        /// Typically, this method is called only once when the first instance of a derived context
        /// is created.  The model for that context is then cached and is for all further instances of
        /// the context in the app domain.  This caching can be disabled by setting the ModelCaching
        /// property on the given ModelBuidler, but note that this can seriously degrade performance.
        /// More control over caching is provided through use of the DbModelBuilder and DbContextFactory
        /// classes directly.
        /// </remarks>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Remove the convention to set the table name to be a pluralized version of the entity type name.
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}