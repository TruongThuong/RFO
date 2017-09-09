
using System;
using System.Configuration;
using System.Web;

namespace RFO.Model.DummyDataGenerator.Seed
{
    /// <summary>
    /// Abstract seed
    /// </summary>
    public abstract class AbstractSeed
    {
        /// <summary>
        /// The _connection string
        /// </summary>
        protected string connectionString = ConfigurationManager.ConnectionStrings["RFODbContext"].ConnectionString;

        /// <summary>
        /// Does the encode special characters.
        /// </summary>
        /// <param name="s">The s.</param>
        /// <returns></returns>
        protected string DoEncodeSpecialCharacters(string s)
        {
            return s.Replace(Environment.NewLine, string.Empty)
                    .Replace("'", HttpUtility.HtmlEncode("'"));
        }
    }
}