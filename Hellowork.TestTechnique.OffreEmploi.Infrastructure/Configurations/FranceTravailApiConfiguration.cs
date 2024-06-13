namespace Hellowork.TestTechnique.OffreEmploi.Infrastructure.Configurations
{
    /// <summary>
    /// Configuration france travail
    /// </summary>
    public class FranceTravailApiConfiguration
    {
        public required string OffreEmploiApiBaseUrl { get; set; }
        public required string IdentifiantClient { get; set; }
        public required string CleSecrete { get; set; }
        public required string TokenUrl { get; set; }
        public required string Scope { get; set; }
        public required string Realm { get; set; }

    }
}
