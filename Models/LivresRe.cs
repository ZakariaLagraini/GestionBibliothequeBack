using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionBiblio.Models
{
    public class LivresRe
    {
        public int id;
        public int? LivreId;
        public DateTime? DateReservation;
        public DateTime? DateExpiration;
        public int? AdherantsId;

        public LivresRe(int id, int livreId, DateTime dateReservation, DateTime dateExpiration, int adherantsId)
        {
            this.id = id;
            LivreId = livreId;
            DateReservation = dateReservation;
            DateExpiration = dateExpiration;
            AdherantsId = adherantsId;
        }
        public void InsertIntoDatabase(MySqlConnection connection, MySqlTransaction transaction)
        {
            try
            {
                using (MySqlCommand cmd = new MySqlCommand("INSERT INTO livrereserve (id,id_produit, DateReservation, DateExpiration, id_user) VALUES (@id, @LivreId, @DateReservation, @DateExpiration, @AdherantsId)", connection, transaction))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@LivreId", LivreId);
                    cmd.Parameters.AddWithValue("@DateReservation", DateReservation);
                    cmd.Parameters.AddWithValue("@DateExpiration", DateExpiration);
                    cmd.Parameters.AddWithValue("@AdherantsId", AdherantsId);


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
