using Hellowork.TestTechnique.OffreEmploi.Core.Entities;
using Hellowork.TestTechnique.OffreEmploi.Core.Repositories;

namespace Hellowork.TestTechnique.OffreEmploi.Core.Business.Impl
{
    /// <summary>
    /// Domaine Offre Emploi
    /// </summary>
    public class OffreEmploiService : IOffreEmploiService
    {
        private readonly IOffreEmploiFranceTravailRepository offreEmploiFranceTravailRepository;
        private readonly IOffreEmploiRepository offreEmploiRepository;

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="offreEmploiFranceTravailRepository"></param>
        /// <param name="offreEmploiRepository"></param>
        public OffreEmploiService(IOffreEmploiFranceTravailRepository offreEmploiFranceTravailRepository, IOffreEmploiRepository offreEmploiRepository)
        {
            this.offreEmploiFranceTravailRepository = offreEmploiFranceTravailRepository;
            this.offreEmploiRepository=offreEmploiRepository;
        }

        /// <summary>
        /// Récupère les offres d'emploi en provenance de FranceTravail et les injecte en BDD
        /// </summary>
        /// <param name="codeInsee"></param>
        /// <returns></returns>
        public async Task GetOffreEmploiFranceTravail(string codeInsee)
        {
            var offres = await offreEmploiFranceTravailRepository.GetOffreByCodeInsee(codeInsee).ConfigureAwait(false);
            foreach (var offre in offres)
            {
                var existingOffre = await offreEmploiRepository.GetByIdAsync(offre.Id).ConfigureAwait(false);
                if (existingOffre != null)
                {
                    // Si on arrive dans les offres déja présente en local, on arrête la mise à jour
                    break;
                }
                await offreEmploiRepository.AddAsync(offre).ConfigureAwait(false);

            }
        }

        /// <summary>
        /// Calcul les statistiques des offres d'emplois sur la bdd locale
        /// </summary>
        /// <returns></returns>
        public async Task<Statistique> ComputeStatistiques()
        {
            var statistiques = new Statistique();
            var offres = await offreEmploiRepository.GetAllAsync().ConfigureAwait(false);

            foreach (var offre in offres)
            {
                string typeContrat = offre.TypeContrat ?? "Inconnu";
                string entreprise = offre.Entreprise ?? "Inconnue";
                string commune = offre.Commune ?? "Inconnue";

                // Compter les types de contrat
                if (statistiques.TypeContrat.ContainsKey(typeContrat))
                {
                    statistiques.TypeContrat[typeContrat]++;
                }
                else
                {
                    statistiques.TypeContrat[typeContrat] = 1;
                }

                // Compter les entreprises
                if (statistiques.Entreprise.ContainsKey(entreprise))
                {
                    statistiques.Entreprise[entreprise]++;
                }
                else
                {
                    statistiques.Entreprise[entreprise] = 1;
                }

                // Compter les pays
                if (statistiques.Commune.ContainsKey(commune))
                {
                    statistiques.Commune[commune]++;
                }
                else
                {
                    statistiques.Commune[commune] = 1;
                }
            }

            // Trier les statistiques par ordre décroissant
            statistiques.TypeContrat = statistiques.TypeContrat.OrderByDescending(pair => pair.Value)
                                                                         .ToDictionary(pair => pair.Key, pair => pair.Value);
            statistiques.Entreprise = statistiques.Entreprise.OrderByDescending(pair => pair.Value)
                                                                       .ToDictionary(pair => pair.Key, pair => pair.Value);
            statistiques.Commune = statistiques.Commune.OrderByDescending(pair => pair.Value)
                                                           .ToDictionary(pair => pair.Key, pair => pair.Value);

            return statistiques;
        }
    }
}
