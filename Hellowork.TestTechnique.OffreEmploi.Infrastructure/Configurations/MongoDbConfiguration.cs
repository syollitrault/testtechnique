namespace Hellowork.TestTechnique.OffreEmploi.Infrastructure.Configurations
{
    /// <summary>
    /// Configuration de la bbase de données locale mongo
    /// </summary>
    public class MongoDbConfiguration
    {
        public required string ConnectionString { get; set; }
        public required string DatabaseName { get; set; }

    }
}
