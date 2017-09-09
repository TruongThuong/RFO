
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace RFO.Model
{
    public class Order
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Order"/> class.
        /// </summary>
        public Order()
        {
            this.OrderDetails = new HashSet<OrderDetail>();
        }

        /// <summary>
        /// Gets or sets the order identifier.
        /// </summary>
        /// <value>
        /// The order identifier.
        /// </value>
        public int OrderId { get; set; }

        /// <summary>
        /// Gets or sets the table identifier.
        /// </summary>
        /// <value>
        /// The table identifier.
        /// </value>
        public int TableId { get; set; }

        /// <summary>
        /// Gets or sets the order state identifier.
        /// </summary>
        /// <value>
        /// The order state identifier.
        /// </value>
        public int OrderStateId { get; set; }

        /// <summary>
        /// Gets or sets the delivery note.
        /// </summary>
        /// <value>
        /// The delivery note.
        /// </value>
        public string DeliveryNote { get; set; }

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        /// <value>
        /// The created date.
        /// </value>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the table.
        /// </summary>
        /// <value>
        /// The table.
        /// </value>
        public Table Table { get; set; }

        /// <summary>
        /// Gets or sets the state of the order.
        /// </summary>
        /// <value>
        /// The state of the order.
        /// </value>
        public virtual OrderState OrderState { get; set; }

        /// <summary>
        /// Gets or sets the order details.
        /// </summary>
        /// <value>
        /// The order details.
        /// </value>
        [JsonIgnore]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}