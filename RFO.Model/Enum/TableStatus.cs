using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFO.Model.Enum
{
    public enum TableStatus
    {
        /// <summary>
        /// The lunch money
        /// </summary>
        [Description("Empty")]
        Available = 1,

        /// <summary>
        /// The gasoline
        /// </summary>
        [Description("Reserved")]
        Booked,

        /// <summary>
        /// The gasoline
        /// </summary>
        [Description("Full")]
        Using,
    }
}
