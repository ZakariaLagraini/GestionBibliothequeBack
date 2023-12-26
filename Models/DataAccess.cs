using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GestionBiblio.Models
{
    public class DataAccess
    {
        private readonly string connectionString;

        public DataAccess(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public DataTable GetBooksData()
        {
            DataTable dataTable = new DataTable();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM livres";
                    MySqlCommand command = new MySqlCommand(query, connection);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                    adapter.Fill(dataTable);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            return dataTable;
        }

        public void SaveBook(Livre newBook)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Create SQL command for inserting a new book
                    string query = "INSERT INTO livres (id, Titre, Auteurs, AnneePublication, Genres, Etat, State) " +
                                    "VALUES (@Id, @Titre, @Auteurs, @AnneePublication, @Genres, @Etat, @State)";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        // Add parameters to the command
                        command.Parameters.AddWithValue("@Id", newBook.id);
                        command.Parameters.AddWithValue("@Titre", newBook.Titre);
                        command.Parameters.AddWithValue("@Auteurs", newBook.Auteurs);
                        command.Parameters.AddWithValue("@AnneePublication", newBook.AnneePublication);
                        command.Parameters.AddWithValue("@Genres", newBook.Genres);
                        command.Parameters.AddWithValue("@Etat", newBook.Etat);
                        command.Parameters.AddWithValue("@State", newBook.State);

                        // Execute the command
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
