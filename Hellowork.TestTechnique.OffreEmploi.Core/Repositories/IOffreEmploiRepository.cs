using Hellowork.TestTechnique.OffreEmploi.Core.Entities;

namespace Hellowork.TestTechnique.OffreEmploi.Core.Repositories
{
    /// <summary>
    /// Getion de la persistance des Offres d'emplois
    /// </summary>
    public interface IOffreEmploiRepository
    {
        /// <summary>
        /// Récupère toutes les offres d'emplois
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Offre>> GetAllAsync();

        /// <summary>
        /// Récupère une offre par son identifiant unique
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Offre> GetByIdAsync(string id);

        /// <summary>
        /// Ajoute une offre d'emploi
        /// </summary>
        /// <param name="offre"></param>
        /// <returns></returns>
        Task AddAsync(Offre offre);
    }
}
