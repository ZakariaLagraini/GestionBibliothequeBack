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



    // Livres.xaml.cs ********************************************************************************************************************
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

        public DataTable GetBooksDataFind()
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


        // Adherants.xaml.cs *******************************************************************************************************************

        public DataTable GetAdherantsData()
        {
            DataTable dataTable = new DataTable();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM adherants";
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

        public void DeleteAdherants(List<Adherant> AdherantsToDelete)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    foreach (var adherant in AdherantsToDelete)
                    {

                        string deleteQuery = $"DELETE FROM adherants WHERE Id = {adherant.id}";

                        using (MySqlCommand cmd = new MySqlCommand(deleteQuery, connection))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show($"Error deleting Adherants: {ex.Message}");
            }

        }

        public void SaveAdherant(Adherant newAdherant)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();


                    string query = "INSERT INTO adherants (id, Nom, Prenom, email) " +
                                    "VALUES (@Id, @Nom, @Prenom, @email)";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {

                        command.Parameters.AddWithValue("@Id", newAdherant.id);
                        command.Parameters.AddWithValue("@Nom", newAdherant.Nom);
                        command.Parameters.AddWithValue("@Prenom", newAdherant.Prenom);
                        command.Parameters.AddWithValue("@email", newAdherant.email);
                       
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public void UpdateAdherant(Adherant updatedAdherant)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();


                    string updateQuery = @"UPDATE adherants 
                                           SET Nom = @Nom, 
                                           Prenom = @Prenom,
                                           email = @email
                                           WHERE id = @id";


                    using (MySqlCommand cmd = new MySqlCommand(updateQuery, connection))
                    {

                        cmd.Parameters.AddWithValue("@id", updatedAdherant.id);
                        cmd.Parameters.AddWithValue("@Nom", updatedAdherant.Nom);
                        cmd.Parameters.AddWithValue("@Prenom", updatedAdherant.Prenom);
                        cmd.Parameters.AddWithValue("@email", updatedAdherant.email);
                       


                        cmd.ExecuteNonQuery();
                        
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show($"Error updating adherants: {ex.Message}");
                Console.WriteLine(ex.Message.ToString());
            }
        }

        public DataTable GetAdherantDataFind(string search)
        {
            DataTable dataTable = new DataTable();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = $"SELECT * FROM adherants WHERE id LIKE '%{search}%' OR Nom LIKE '%{search}%' OR Prenom LIKE '%{search}%' OR email LIKE '%{search}%'";
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


        // Auteurs.xaml.cs *********************************************************************************************************************************************************************


        public DataTable GetAuteursData()
        {
            DataTable dataTable = new DataTable();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM auteurs";
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

        public void DeleteAuteurs(List<Auteur> AuteursToDelete)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    foreach (var auteur in AuteursToDelete)
                    {

                        string deleteQuery = $"DELETE FROM auteurs WHERE Id = {auteur.id}";

                        using (MySqlCommand cmd = new MySqlCommand(deleteQuery, connection))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show($"Error deleting Adherants: {ex.Message}");
            }

        }

        public void SaveAuteur(Auteur newAuteur)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();


                    string query = "INSERT INTO auteurs (id, Nom, Prenom, LivreId) " +
                                    "VALUES (@Id, @Nom, @Prenom, @LivreId)";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {

                        command.Parameters.AddWithValue("@Id", newAuteur.id);
                        command.Parameters.AddWithValue("@Nom", newAuteur.Nom);
                        command.Parameters.AddWithValue("@Prenom", newAuteur.Prenom);
                        command.Parameters.AddWithValue("@LivreId", newAuteur.LivresId);

                        Console.WriteLine("Executing query: " + command.CommandText);
                        command.ExecuteNonQuery();
                    }
                }
                catch (MySqlException ex)
                {
                    // Log the exception details for debugging
                    Console.WriteLine($"MySqlException: {ex.Message}");
                    Console.WriteLine($"Error code: {ex.ErrorCode}");
                    Console.WriteLine($"SQL State: {ex.SqlState}");
                    MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public void UpdateAuteur(Auteur updatedAuteur)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();


                    string updateQuery = @"UPDATE auteurs 
                                           SET Nom = @Nom, 
                                           Prenom = @Prenom,
                                           livreId = @LivreId
                                           WHERE id = @id";


                    using (MySqlCommand cmd = new MySqlCommand(updateQuery, connection))
                    {

                        cmd.Parameters.AddWithValue("@id", updatedAuteur.id);
                        cmd.Parameters.AddWithValue("@Nom", updatedAuteur.Nom);
                        cmd.Parameters.AddWithValue("@Prenom", updatedAuteur.Prenom);
                        cmd.Parameters.AddWithValue("@LivreId", updatedAuteur.LivresId);



                        cmd.ExecuteNonQuery();

                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show($"Error updating auteurs: {ex.Message}");
                Console.WriteLine(ex.Message.ToString());
            }
        }

        public DataTable GetAuteurDataFind(string search)
        {
            DataTable dataTable = new DataTable();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = $"SELECT * FROM auteurs WHERE id LIKE '%{search}%' OR Nom LIKE '%{search}%' OR Prenom LIKE '%{search}%' OR LivreId LIKE '%{search}%'";
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

        // ***********************************************************************************************************************************************************************


    }
}
