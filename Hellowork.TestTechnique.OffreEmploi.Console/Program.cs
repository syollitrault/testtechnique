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
                    // Attente resultats pour les statistiques
                    offreEmploiService.GetOffreEmploiFranceTravail("35238").Wait(); // Rennes
                    offreEmploiService.GetOffreEmploiFranceTravail("75101").Wait(); // Paris
                    offreEmploiService.GetOffreEmploiFranceTravail("75102").Wait(); // Paris
                    offreEmploiService.GetOffreEmploiFranceTravail("75103").Wait(); // Paris
                    offreEmploiService.GetOffreEmploiFranceTravail("75104").Wait(); // Paris
                    offreEmploiService.GetOffreEmploiFranceTravail("75105").Wait(); // Paris
                    offreEmploiService.GetOffreEmploiFranceTravail("75106").Wait(); // Paris
                    offreEmploiService.GetOffreEmploiFranceTravail("75107").Wait(); // Paris
                    offreEmploiService.GetOffreEmploiFranceTravail("75108").Wait(); // Paris
                    offreEmploiService.GetOffreEmploiFranceTravail("75109").Wait(); // Paris
                    offreEmploiService.GetOffreEmploiFranceTravail("75110").Wait(); // Paris
                    offreEmploiService.GetOffreEmploiFranceTravail("75111").Wait(); // Paris
                    offreEmploiService.GetOffreEmploiFranceTravail("75112").Wait(); // Paris
                    offreEmploiService.GetOffreEmploiFranceTravail("75113").Wait(); // Paris
                    offreEmploiService.GetOffreEmploiFranceTravail("75114").Wait(); // Paris
                    offreEmploiService.GetOffreEmploiFranceTravail("75115").Wait(); // Paris
                    offreEmploiService.GetOffreEmploiFranceTravail("75116").Wait(); // Paris
                    offreEmploiService.GetOffreEmploiFranceTravail("75117").Wait(); // Paris
                    offreEmploiService.GetOffreEmploiFranceTravail("75118").Wait(); // Paris
                    offreEmploiService.GetOffreEmploiFranceTravail("75119").Wait(); // Paris
                    offreEmploiService.GetOffreEmploiFranceTravail("75120").Wait(); // Paris
                    offreEmploiService.GetOffreEmploiFranceTravail("33063").Wait(); // Bordeaux

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
                    System.Console.WriteLine("Statistiques des Communes:");
                    foreach (var entry in statistiques.Commune)
                    {
                        System.Console.WriteLine($"{entry.Key}: {entry.Value}");
                    }
                }
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