using Hellowork.TestTechnique.OffreEmploi.Infrastructure.Entities;
using Newtonsoft.Json;
using System.Text;

namespace Hellowork.TestTechnique.OffreEmploi.Infrastructure.Tools
{
    /// <summary>
    /// Outils de récupération du bearer token
    /// </summary>
    public class TokenTools
    {
        /// <summary>
        /// Generation du token, sans gestion du refresh
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="clientSecret"></param>
        /// <param name="scope"></param>
        /// <returns></returns>
        internal static async Task<string> GenerateAccessTokenAsync(string url, string realm, string clientId, string clientSecret, string scope)
        {
            HttpClient client = new HttpClient();
            // URL de l'endpoint pour générer l'access token
            var tokenEndpoint = $"{url}?realm=%2F{realm}";
            // Corps de la requête
            var requestBody = new StringBuilder();
            requestBody.Append("grant_type=client_credentials");
            requestBody.Append($"&client_id={Uri.EscapeDataString(clientId)}");
            requestBody.Append($"&client_secret={Uri.EscapeDataString(clientSecret)}");
            requestBody.Append($"&scope={Uri.EscapeDataString(scope)}");
            // Préparation de la requête HTTP
            var request = new HttpRequestMessage(HttpMethod.Post, tokenEndpoint);
            request.Content = new StringContent(requestBody.ToString(), Encoding.UTF8, "application/x-www-form-urlencoded");
            // Envoi de la requête HTTP
            var response = await client.SendAsync(request);
            // Traitement de la réponse
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var accessTokenResponse = JsonConvert.DeserializeObject<AccessTokenResponse>(responseContent);
                return accessTokenResponse.access_token;
            }
            else
            {
                return null;
            }
        }
    }
}
