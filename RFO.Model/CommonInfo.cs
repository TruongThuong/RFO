using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RFO.Model
{
    /// <summary>
    /// CompanyName
    /// Logo
    /// Philosophy
    /// Copyright
    /// WelcomeMessage
    /// ShortAboutUs
    /// AboutUs
    /// WhyChooseUs
    /// </summary>
    public class CommonInfo
    {
        /// <summary>
        /// Gets or sets the common information identifier.
        /// </summary>
        /// <value>
        /// The common information identifier.
        /// </value>
        public int CommonInfoId { get; set; }

        /// <summary>
        /// Gets or sets the common information code.
        /// </summary>
        /// <value>
        /// The common information code.
        /// </value>
        public int CommonInfoCode { get; set; }

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
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        [Column(TypeName = "ntext")]
        [MaxLength]
        public string Description { get; set; }
    }
}