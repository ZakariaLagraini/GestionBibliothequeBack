using MySql.Data.MySqlClient;
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

        public void InsertIntoDatabase(MySqlConnection connection, MySqlTransaction transaction)
        {
            try
            {
                using (MySqlCommand cmd = new MySqlCommand("INSERT INTO Livres (id, Titre, Auteurs, AnneePublication, Genres, Etat, State) VALUES (@id, @Titre, @Auteurs, @AnneePublication, @Genres, @Etat, @State)", connection, transaction))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@Titre", Titre);
                    cmd.Parameters.AddWithValue("@Auteurs", Auteurs);
                    cmd.Parameters.AddWithValue("@AnneePublication", AnneePublication);
                    cmd.Parameters.AddWithValue("@Genres", Genres);
                    cmd.Parameters.AddWithValue("@Etat", Etat);
                    cmd.Parameters.AddWithValue("@State", State);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error inserting data into database: {ex.Message}");
            }
        }

    }

   

       
}
