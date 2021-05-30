using System.Net.Http;

namespace Cadastro.Extenssions
{
    internal static class HttpClientExtenssions
    {
        public static HttpClient AddJWT(this HttpClient httpClient, string token)
        {
            httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            return httpClient;
        }
    }
}