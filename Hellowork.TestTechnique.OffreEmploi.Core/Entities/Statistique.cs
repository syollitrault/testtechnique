namespace Hellowork.TestTechnique.OffreEmploi.Core.Entities
{
    /// <summary>
    /// Permet de stocker les statistiques calculer afin de les exploiter
    /// </summary>
    public class Statistique
    {
        public Dictionary<string, int> TypeContrat { get; set; }
        public Dictionary<string, int> Entreprise { get; set; }
        public Dictionary<string, int> Commune { get; set; }

        public Statistique()
        {
            TypeContrat = new Dictionary<string, int>();
            Entreprise = new Dictionary<string, int>();
            Commune = new Dictionary<string, int>();
        }
    }
}
