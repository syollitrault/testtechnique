namespace Hellowork.TestTechnique.OffreEmploi.Infrastructure.FranceTravailClient.OffreEmploi
{
    public class RecupererListeOffreResult
    {
        public required IEnumerable<Offre> resultats { get; set; }
        public required object filtresPossibles { get; set; }
    }
}
