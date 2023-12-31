using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionBiblio.Models
{
    public class Auteur
    {
        public int id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public int LivresId { get; set; }


        public Auteur(int id, string nom, string prenom, int livreid)
        {
            this.id = id;
            Nom = nom;
            Prenom = prenom;
            this.LivresId = livreid;
        }
    }
}
