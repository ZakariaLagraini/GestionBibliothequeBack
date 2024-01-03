using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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

        public void InsertIntoDatabase(MySqlConnection connection, MySqlTransaction transaction)
        {
            try
            {
                
                    string query = "INSERT INTO adherants (id, Nom, Prenom, email) " +
                    "VALUES (@Id, @Nom, @Prenom, @email)";

                using (MySqlCommand command = new MySqlCommand(query, connection,transaction))
                {

                    command.Parameters.AddWithValue("@Id", id);
                    command.Parameters.AddWithValue("@Nom", Nom);
                    command.Parameters.AddWithValue("@Prenom", Prenom);
                    command.Parameters.AddWithValue("@email", email);

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
