using RestSharp;
using RestSharp.Deserializers;

namespace Acme.Billing.Infrastructure.RestHelper
{
    /// <summary>
    /// A helper for making REST API calls.
    /// </summary>
    public static class RestClientHelper
    {
        /// <summary>
        /// Make a GET to <c>baseUrl</c> and <c>requestPath</c> and return the result content.
        /// </summary>
        /// <param name="baseUrl"></param>
        /// <param name="requestPath"></param>
        /// <returns></returns>
        public static string GetJson(string baseUrl, string requestPath)
        {
            RestClient client = new RestClient(baseUrl);
            client.AddHandler("application/json", new JsonDeserializer());

            RestRequest request = new RestRequest(requestPath) { Method = Method.GET };
            IRestResponse result = client.Get(request);

            return result.Content;
        }
    }
}
