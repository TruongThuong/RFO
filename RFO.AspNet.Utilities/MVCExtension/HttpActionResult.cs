
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace RFO.AspNet.Utilities.MVCExtension
{
    public class HttpActionResult : IHttpActionResult
    {
        #region Variables

        /// <summary>
        /// The content
        /// </summary>
        private readonly string content;

        /// <summary>
        /// The request
        /// </summary>
        private readonly HttpRequestMessage httpRequest;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the content.
        /// </summary>
        /// <value>
        /// The content.
        /// </value>
        public string Content
        {
            get { return this.content; }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpActionResult"/> class.
        /// </summary>
        /// <param name="inContent">Content of the in.</param>
        /// <param name="inRequest">The in request.</param>
        public HttpActionResult(string inContent, HttpRequestMessage inRequest)
        {
            this.content = inContent;
            this.httpRequest = inRequest;
        }

        #endregion

        #region Override of IHttpActionResult

        /// <summary>
        /// Creates an <see cref="T:System.Net.Http.HttpResponseMessage" /> asynchronously.
        /// </summary>
        /// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
        /// <returns>
        /// A task that, when completed, contains the <see cref="T:System.Net.Http.HttpResponseMessage" />.
        /// </returns>
        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage()
            {
                Content = new StringContent(this.content),
                RequestMessage = this.httpRequest
            };
            return Task.FromResult(response);
        }

        #endregion
    }
}
