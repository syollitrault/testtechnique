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
            this.franceTravailApiConfiguration = franceTravailApiConfiguration.Value ?? 
                throw new ArgumentNullException("L'api FranceTravail n'est pas configuréee");
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Core.Entities.Offre>> GetOffreByCodeInsee(string codeInsee)
        {
            string bearerToken = await TokenTools.GenerateAccessTokenAsync(franceTravailApiConfiguration.TokenUrl, franceTravailApiConfiguration.Realm, 
                franceTravailApiConfiguration.IdentifiantClient, franceTravailApiConfiguration.CleSecrete, franceTravailApiConfiguration.Scope).ConfigureAwait(false) 
                ?? throw new OeException("Authentification FranceTravail impossible");
            var client = new Client(new HttpClient()) { BaseUrl = franceTravailApiConfiguration.OffreEmploiApiBaseUrl };
            // Tri sort=1 par "Date de création horodatée décroissante, pertinence décroissante, distance croissante, origine de l’offre" afin de prendre les nouvelles offres en premier
            var response = await client.RecupererListeOffreAsync(null, "1", null, null, null, null, null, null, null, null, null, null, null, codeInsee, 
                null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, 
                null, null, $"Bearer {bearerToken}").ConfigureAwait(false);

            return response.resultats.Select(r => new Core.Entities.Offre()
            {
                Description = r.Description,
                Entreprise = r.Entreprise?.Nom,
                Id = r.Id,
                Commune = r.LieuTravail?.Libelle,
                LienPostuler = r.Contact?.UrlPostulation,
                TypeContrat = r.TypeContrat
            });
        }
    }
}