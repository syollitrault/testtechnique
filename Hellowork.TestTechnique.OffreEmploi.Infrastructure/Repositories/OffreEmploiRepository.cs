using Hellowork.TestTechnique.OffreEmploi.Core.Entities;
using Hellowork.TestTechnique.OffreEmploi.Core.Repositories;
using Hellowork.TestTechnique.OffreEmploi.Infrastructure.Configurations;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Hellowork.TestTechnique.OffreEmploi.Infrastructure.Repositories
{
    /// <inheritdoc/>
    public class OffreEmploiRepository : IOffreEmploiRepository
    {
        private readonly MongoDbConfiguration mongoDbConfiguration;
        private readonly IMongoCollection<Offre> offreEmplois;

        public OffreEmploiRepository(IOptions<MongoDbConfiguration> mongoDbConfiguration)
        {
            this.mongoDbConfiguration = mongoDbConfiguration.Value ?? throw new ArgumentNullException("La base de données Mongo n'est pas configuréee");
            var client = new MongoClient(this.mongoDbConfiguration.ConnectionString);
            var database = client.GetDatabase(this.mongoDbConfiguration.DatabaseName);
            offreEmplois = database.GetCollection<Offre>("Offre");
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Offre>> GetAllAsync()
        {
            return await offreEmplois.Find(offre => true).ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<Offre> GetByIdAsync(string id)
        {
            return await offreEmplois.Find(offre => offre.Id == id).FirstOrDefaultAsync();
        }

        /// <inheritdoc/>
        public async Task AddAsync(Offre offre)
        {
            await offreEmplois.InsertOneAsync(offre);
        }

        /// <inheritdoc/>
        public async Task UpdateAsync(Offre offre)
        {
            await offreEmplois.ReplaceOneAsync(o => o.Id == offre.Id, offre);
        }

        /// <inheritdoc/>
        public async Task DeleteAsync(string id)
        {
            await offreEmplois.DeleteOneAsync(offre => offre.Id == id);
        }
    }
}