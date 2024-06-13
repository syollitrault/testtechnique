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
            var offres = offreEmploiFranceTravailRepository.GetOffreByCodeInsee(codeInsee);
            foreach (var offre in offres)
            {
                // Todo : Vérifier l'unicité par l'ID
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
                // Compter les types de contrat
                if (statistiques.TypeContrat.ContainsKey(offre.TypeContrat))
                {
                    statistiques.TypeContrat[offre.TypeContrat]++;
                }
                else
                {
                    statistiques.TypeContrat[offre.TypeContrat] = 1;
                }

                // Compter les entreprises
                if (statistiques.Entreprise.ContainsKey(offre.Entreprise))
                {
                    statistiques.Entreprise[offre.Entreprise]++;
                }
                else
                {
                    statistiques.Entreprise[offre.Entreprise] = 1;
                }

                // Compter les pays
                if (statistiques.Pays.ContainsKey(offre.Pays))
                {
                    statistiques.Pays[offre.Pays]++;
                }
                else
                {
                    statistiques.Pays[offre.Pays] = 1;
                }
            }

            return statistiques;
        }
    }
}
