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
        public string Description { get; set; }
        public string Genres { get; set; }

        public string CheminImage { get; set; }
        public string Etat { get; set; }

        public string State { get; set; }

        public DateTime DateReservation { get; set; }

        public DateTime DateExpiration { get; set; }


        public Livre(int id, string titre, string auteurs, string description, string genres, string cheminImage, string etat, string state, DateTime dateReservation, DateTime dateExpiration)
        {
            this.id = id;
            Titre = titre;
            Auteurs = auteurs;
            Description = description;
            Genres = genres;
            CheminImage = cheminImage;
            Etat = etat;
            State = state;
            DateReservation = dateReservation;
            DateExpiration = dateExpiration;
        }

        public void InsertIntoDatabase(MySqlConnection connection, MySqlTransaction transaction)
        {
            try
            {
                using (MySqlCommand cmd = new MySqlCommand("INSERT INTO produits (id, Nom, Auteurs, Description, Genre, CheminImage, Etat, State, DateReservation, DateExpiration) VALUES (@id, @Nom, @Auteurs, @Description, @Genre, @CheminImage, @Etat, @State, @DateReservation, @DateExpiration)", connection, transaction))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@Nom", Titre);
                    cmd.Parameters.AddWithValue("@Auteurs", Auteurs);
                    cmd.Parameters.AddWithValue("@Description", Description);
                    cmd.Parameters.AddWithValue("@Genre", Genres);
                    cmd.Parameters.AddWithValue("@CheminImage", CheminImage);
                    cmd.Parameters.AddWithValue("@Etat", Etat);
                    cmd.Parameters.AddWithValue("@State", State);
                    cmd.Parameters.AddWithValue("@DateReservation", DateReservation);
                    cmd.Parameters.AddWithValue("@DateExpiration", DateExpiration);

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
