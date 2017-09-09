
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Caching;

namespace RFO.AspNet.Utilities.MVCExtension
{
    /// <summary>
    /// Provides extention methods for HttpResponse
    /// </summary>
    public static class HttpResponseExtension
    {
        /// <summary>
        /// Sets the default image headers.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <param name="fileName">Name of the file.</param>
        public static void SetDefaultImageHeaders(this HttpResponseBase response, string fileName)
        {
            response.Cache.SetETag(CalculateMD5Hash(fileName));
            response.Cache.SetCacheability(HttpCacheability.Public);
            response.Cache.SetExpires(Cache.NoAbsoluteExpiration);
            response.Cache.SetLastModifiedFromFileDependencies();
        }

        /// <summary>
        /// Calculates the m d5 hash.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns></returns>
        public static string CalculateMD5Hash(string input)
        {
            // step 1, calculate MD5 hash from input
            var md5 = MD5.Create();
            var inputBytes = Encoding.ASCII.GetBytes(input);
            var hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            var sb = new StringBuilder();
            for (var i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }

            return sb.ToString();
        }
    }
}