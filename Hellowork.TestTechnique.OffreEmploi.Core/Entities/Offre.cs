using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hellowork.TestTechnique.OffreEmploi.Core.Entities
{
    /// <summary>
    /// Représentation locale d'une offre d'emploi
    /// </summary>
    public class Offre
    {
        public string Id { get; set; }
        public required string TypeContrat { get; set; }
        public required string Entreprise { get; set; }
        public required string Commune { get; set; }
        public required string Description { get; set; }
        public required string LienPostuler { get; set; }
    }
}
