namespace Hellowork.TestTechnique.OffreEmploi.Infrastructure.Entities
{
    /// <summary>
    /// Retour de l'authentification oauth2 de france travail
    /// </summary>
    public class AccessTokenResponse
    {
        public required int expires_in { get; set; }
        public required string token_type { get; set; }
        public required string access_token { get; set; }
        public required string scope { get; set; }
    }
}
