using Hellowork.TestTechnique.OffreEmploi.Core.Entities;

namespace Hellowork.TestTechnique.OffreEmploi.Core.Repositories
{
    /// <summary>
    /// Interface vers l'api Offre Emploi de France Travail
    /// </summary>
    public interface IOffreEmploiFranceTravailRepository
    {
        /// <summary>
        /// Récupération des offres par CodeInsee
        /// </summary>
        /// <param name="codeInsee"></param>
        /// <returns></returns>
        public IEnumerable<Offre> GetOffreByCodeInsee(string codeInsee);
    }
}
