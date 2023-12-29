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

                    
                    string query = "INSERT INTO livres (id, Titre, Auteurs, AnneePublication, Genres, Etat, State) " +
                                    "VALUES (@Id, @Titre, @Auteurs, @AnneePublication, @Genres, @Etat, @State)";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        
                        command.Parameters.AddWithValue("@Id", newBook.id);
                        command.Parameters.AddWithValue("@Titre", newBook.Titre);
                        command.Parameters.AddWithValue("@Auteurs", newBook.Auteurs);
                        command.Parameters.AddWithValue("@AnneePublication", newBook.AnneePublication);
                        command.Parameters.AddWithValue("@Genres", newBook.Genres);
                        command.Parameters.AddWithValue("@Etat", newBook.Etat);
                        command.Parameters.AddWithValue("@State", newBook.State);

                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public void DeleteBooks(List<Livre> booksToDelete)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    foreach (var livre in booksToDelete)
                    {
                        
                        string deleteQuery = $"DELETE FROM livres WHERE Id = {livre.id}";

                        using (MySqlCommand cmd = new MySqlCommand(deleteQuery, connection))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
               
                MessageBox.Show($"Error deleting books: {ex.Message}");
            }
        
    }

        public void UpdateBook(Livre updatedBook)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    
                    string updateQuery = @"UPDATE livres 
                                       SET Titre = @Titre, 
                                           Auteurs = @Auteurs,
                                           AnneePublication = @AnneePublication,
                                           Genres = @Genres,
                                           Etat = @Etat,
                                           State = @State
                                           WHERE id = @id";
                                           

                    using (MySqlCommand cmd = new MySqlCommand(updateQuery, connection))
                    {
                     
                        cmd.Parameters.AddWithValue("@Id", updatedBook.id);
                        cmd.Parameters.AddWithValue("@Titre", updatedBook.Titre);
                        cmd.Parameters.AddWithValue("@Auteurs", updatedBook.Auteurs);
                        cmd.Parameters.AddWithValue("@AnneePublication", updatedBook.AnneePublication);
                        cmd.Parameters.AddWithValue("@Genres", updatedBook.Genres);
                        cmd.Parameters.AddWithValue("@Etat", updatedBook.Etat);
                        cmd.Parameters.AddWithValue("@State", updatedBook.State);

                        
                        cmd.ExecuteNonQuery();
                        Console.WriteLine(updatedBook.Genres.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                
                MessageBox.Show($"Error updating book: {ex.Message}");
                Console.WriteLine(ex.Message.ToString());
            }
        }


    }
}
