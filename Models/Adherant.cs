using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionBiblio.Models
{
    public class Adherant
    {
        
        public int id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string email { get; set; }


        public Adherant(int id, string nom, string prenom, string email)
        {
            this.id = id;
            Nom = nom;
            Prenom = prenom;
            this.email = email;
        }


    }
}
