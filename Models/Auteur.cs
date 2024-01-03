using MySql.Data.MySqlClient;
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

        public void InsertIntoDatabase(MySqlConnection connection, MySqlTransaction transaction)
        {
            try
            {
                string query = "INSERT INTO auteurs (id, Nom, Prenom, LivreId) " +
                                    "VALUES (@Id, @Nom, @Prenom, @LivreId)";

                using (MySqlCommand command = new MySqlCommand(query, connection, transaction))
                {

                    command.Parameters.AddWithValue("@Id", id);
                    command.Parameters.AddWithValue("@Nom", Nom);
                    command.Parameters.AddWithValue("@Prenom", Prenom);
                    command.Parameters.AddWithValue("@LivreId", LivresId);

                    Console.WriteLine("Executing query: " + command.CommandText);
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error inserting data into database: {ex.Message}");
            }
        }
    }
}
