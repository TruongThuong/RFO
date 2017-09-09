
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;

namespace RFO.Model
{
    public class OrderState
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OrderState"/> class.
        /// </summary>
        public OrderState()
        {
            this.Orders = new HashSet<Order>();
        }

        /// <summary>
        /// Gets or sets the order state identifier.
        /// </summary>
        /// <value>
        /// The order state identifier.
        /// </value>
        public int OrderStateId { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the brief description.
        /// </summary>
        /// <value>
        /// The brief description.
        /// </value>
        public string BriefDescription { get; set; }

        /// <summary>
        /// Gets or sets the orders.
        /// </summary>
        /// <value>
        /// The orders.
        /// </value>
        [JsonIgnore]
        public virtual ICollection<Order> Orders { get; set; }
    }
}