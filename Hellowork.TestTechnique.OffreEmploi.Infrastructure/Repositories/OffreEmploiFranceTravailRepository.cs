using Hellowork.TestTechnique.OffreEmploi.Core;
using Hellowork.TestTechnique.OffreEmploi.Core.Repositories;
using Hellowork.TestTechnique.OffreEmploi.Infrastructure.Configurations;
using Hellowork.TestTechnique.OffreEmploi.Infrastructure.FranceTravailClient.OffreEmploi;
using Hellowork.TestTechnique.OffreEmploi.Infrastructure.Tools;
using Microsoft.Extensions.Options;

namespace Hellowork.TestTechnique.OffreEmploi.Infrastructure.Repositories
{
    /// <inheritdoc/>
    public class OffreEmploiFranceTravailRepository : IOffreEmploiFranceTravailRepository
    {
        private FranceTravailApiConfiguration franceTravailApiConfiguration;

        public OffreEmploiFranceTravailRepository(IOptions<FranceTravailApiConfiguration> franceTravailApiConfiguration)
        {
            this.franceTravailApiConfiguration = franceTravailApiConfiguration.Value ?? throw new ArgumentNullException("L'api FranceTravail n'est pas configuréee");
        }

        /// <inheritdoc/>
        public IEnumerable<Core.Entities.Offre> GetOffreByCodeInsee(string codeInsee)
        {
            string bearerToken = TokenTools.GenerateAccessTokenAsync(franceTravailApiConfiguration.TokenUrl, franceTravailApiConfiguration.Realm, franceTravailApiConfiguration.IdentifiantClient, franceTravailApiConfiguration.CleSecrete, franceTravailApiConfiguration.Scope).Result ?? throw new OeException("Authentification FranceTravail impossible");
            var client = new Client(new HttpClient()) { BaseUrl = franceTravailApiConfiguration.OffreEmploiApiBaseUrl };
            //var response = client.RecupererListeOffreAsync("0-50", null, null, null, null, null, null, null, null, null, null, null, null, codeInsee, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, $"Bearer {bearerToken}").Result;
            // Pb de documentation, il n'existe pas de retour 206 contrairement au réél. La désérialisation est érroné, il faut récrire le client
            
            // List pour la démo
            return new List<Core.Entities.Offre>() { new Core.Entities.Offre
            {
                Id = Guid.NewGuid().ToString(),
                Description = "lorem ipsum",
                Entreprise = "Entreprise Dumont",
                LienPostuler = "http://nouveaujob.com/postuler",
                Pays = "France",
                TypeContrat = "CDI"
            },new Core.Entities.Offre
            {
                Id = Guid.NewGuid().ToString(),
                Description = "lorem ipsum",
                Entreprise = "Conserto",
                LienPostuler = "http://nouveaujob.com/postuler",
                Pays = "France",
                TypeContrat = "CDI"
            },new Core.Entities.Offre
            {
                Id = Guid.NewGuid().ToString(),
                Description = "lorem ipsum",
                Entreprise = "HelloWork",
                LienPostuler = "http://nouveaujob.com/postuler",
                Pays = "Bretagne",
                TypeContrat = "CDD"
            } };
        }
    }
}