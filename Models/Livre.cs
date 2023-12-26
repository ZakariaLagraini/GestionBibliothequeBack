using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionBiblio.Models
{
    public class Livre
    {
        
        public int id {  get; set; }
        public string Titre { get; set; }
        public string Auteurs { get; set; }
        public int AnneePublication { get; set; }
        public string Genres { get; set; }
        public string Etat { get; set; }

        public string State { get; set; }


        public Livre(int id, string titre, string auteurs, int anneePublication, string genres, string etat, string state)
        {
            this.id = id;
            Titre = titre;
            Auteurs = auteurs;
            AnneePublication = anneePublication;
            Genres = genres;
            Etat = etat;
            State = state;
        }

    }


}
