using Newtonsoft.Json;
using System.Collections.Generic;

namespace RFO.Model
{
    public class Role
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Role"/> class.
        /// </summary>
        public Role()
        {
            this.Users = new HashSet<User>();
        }

        /// <summary>
        /// Gets or sets the role identifier.
        /// </summary>
        /// <value>
        /// The role identifier.
        /// </value>
        public int RoleId { get; set; }

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
        /// Gets or sets the user roles.
        /// </summary>
        /// <value>
        /// The user roles.
        /// </value>
        [JsonIgnore]
        public virtual ICollection<User> Users { get; set; }
    }
}
