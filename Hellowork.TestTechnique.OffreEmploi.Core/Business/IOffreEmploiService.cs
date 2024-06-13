using Hellowork.TestTechnique.OffreEmploi.Core.Entities;

namespace Hellowork.TestTechnique.OffreEmploi.Core.Business
{
    public interface IOffreEmploiService
    {
        Task<Statistique> ComputeStatistiques();
        Task GetOffreEmploiFranceTravail(string codeInsee);
    }
}
