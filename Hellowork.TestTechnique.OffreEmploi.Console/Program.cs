using Hellowork.TestTechnique.OffreEmploi.Core.Business;
using Hellowork.TestTechnique.OffreEmploi.Core.Business.Impl;
using Hellowork.TestTechnique.OffreEmploi.Core.Repositories;
using Hellowork.TestTechnique.OffreEmploi.Infrastructure.Configurations;
using Hellowork.TestTechnique.OffreEmploi.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Hellowork.TestTechnique.OffreEmploi.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var host = CreateHostBuilder(args).Build();

                // Utilisation des services configurés
                using (var scope = host.Services.CreateScope())
                {
                    var services = scope.ServiceProvider;

                    var test = services.GetRequiredService<IOptions<FranceTravailApiConfiguration>>().Value;

                    // Peupler la base de données
                    var offreEmploiService = services.GetRequiredService<IOffreEmploiService>();
                    offreEmploiService.GetOffreEmploiFranceTravail("35238").Wait(); // Test pour Rennes / Attente resultats pour les statistiques

                    // Calculer et afficher le rapport
                    var statistiques = offreEmploiService.ComputeStatistiques().Result;
                    System.Console.WriteLine("Statistiques des Types de Contrat:");
                    foreach (var entry in statistiques.TypeContrat)
                    {
                        System.Console.WriteLine($"{entry.Key}: {entry.Value}");
                    }
                    System.Console.WriteLine(string.Empty);
                    System.Console.WriteLine("Statistiques des Entreprises:");
                    foreach (var entry in statistiques.Entreprise)
                    {
                        System.Console.WriteLine($"{entry.Key}: {entry.Value}");
                    }
                    System.Console.WriteLine(string.Empty);
                    System.Console.WriteLine("Statistiques des Pays:");
                    foreach (var entry in statistiques.Pays)
                    {
                        System.Console.WriteLine($"{entry.Key}: {entry.Value}");
                    }
                }
                System.Console.ReadLine();
            }
            catch (Exception e)
            {
                System.Console.WriteLine($"Erreur dans le programme : {e.Message}");
            }
        }

        /// <summary>
        /// Ajout des configurations et des injections de dependances
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.SetBasePath(Directory.GetCurrentDirectory());
                config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
                config.AddCommandLine(args);
            })
            .ConfigureServices((context, services) =>
            {
                // Configuration des services
                services.AddScoped<IOffreEmploiService, OffreEmploiService>();

                // Configuration des repositories
                services.AddScoped<IOffreEmploiFranceTravailRepository, OffreEmploiFranceTravailRepository>();
                services.AddSingleton<IOffreEmploiRepository, OffreEmploiRepository>();

                // Configuration
                services.Configure<FranceTravailApiConfiguration>(context.Configuration.GetSection(nameof(FranceTravailApiConfiguration)));
                services.Configure<MongoDbConfiguration>(context.Configuration.GetSection(nameof(MongoDbConfiguration)));
            });
    }
}