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
                    string query = "SELECT * FROM produits";
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
                    string query = "SELECT * FROM produits";
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

                    
                    string query = "INSERT INTO produits (id, Nom, Auteurs, Description, Genre, CheminImage, Etat, State, DateReservation, DateExpiration) " +
                                    "VALUES (@Id, @Titre, @Auteurs, @Description, @Genres, @CheminImage, @Etat, @State, @DateReservation, @DateExpiration)";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {

                        command.Parameters.AddWithValue("@Id", newBook.id);
                        command.Parameters.AddWithValue("@Titre", newBook.Titre);
                        command.Parameters.AddWithValue("@Auteurs", newBook.Auteurs);
                        command.Parameters.AddWithValue("@Description", newBook.Description);
                        command.Parameters.AddWithValue("@Genres", newBook.Genres);
                        command.Parameters.AddWithValue("@CheminImage", newBook.CheminImage);
                        command.Parameters.AddWithValue("@Etat", newBook.Etat);
                        command.Parameters.AddWithValue("@State", newBook.State);
                        command.Parameters.AddWithValue("@DateReservation", newBook.DateReservation);
                        command.Parameters.AddWithValue("@DateExpiration", newBook.DateExpiration);

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
                        
                        string deleteQuery = $"DELETE FROM produits WHERE Id = {livre.id}";

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

                    
                    string updateQuery = @"UPDATE produits 
                                       SET Nom = @Nom, 
                                           Auteurs = @Auteurs,
                                           Description = @Description,
                                           Genre = @Genre,
                                           CheminImage = @CheminImage,
                                           Etat = @Etat,
                                           State = @State,
                                           DateReservation = @DateReservation,
                                           DateExpiration = @DateExpiration
                                           WHERE id = @id";
                                           

                    using (MySqlCommand cmd = new MySqlCommand(updateQuery, connection))
                    {
                     
                        cmd.Parameters.AddWithValue("@Id", updatedBook.id);
                        cmd.Parameters.AddWithValue("@Nom", updatedBook.Titre);
                        cmd.Parameters.AddWithValue("@Auteurs", updatedBook.Auteurs);
                        cmd.Parameters.AddWithValue("@Description", updatedBook.Description);
                        cmd.Parameters.AddWithValue("@Genre", updatedBook.Genres);
                        cmd.Parameters.AddWithValue("@CheminImage", updatedBook.CheminImage);
                        cmd.Parameters.AddWithValue("@Etat", updatedBook.Etat);
                        cmd.Parameters.AddWithValue("@State", updatedBook.State);
                        cmd.Parameters.AddWithValue("@DateReservation", updatedBook.DateReservation);
                        cmd.Parameters.AddWithValue("@DateExpiration", updatedBook.DateExpiration);


                        cmd.ExecuteNonQuery();
                        Console.WriteLine(updatedBook.Genres.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                
                
                Console.WriteLine(ex.Message.ToString());
            }
        }

        public void ImportBooksData(DataTable dataTable)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                using (MySqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        foreach (DataRow row in dataTable.Rows)
                        {
                            int id = Convert.ToInt32(row["Id"]);
                            string title = row["Nom"].ToString();
                            string author = row["Auteurs"].ToString();
                            string description = row["Description"].ToString();
                            string genres = row["Genre"].ToString();
                            string cheminImage = row["CheminImage"].ToString();
                            string etat = row["Etat"].ToString();
                            string state = row["State"].ToString();
                            DateTime dateReservation = Convert.ToDateTime(row["DateReservation"]);
                            DateTime dateExpiration = Convert.ToDateTime(row["DateExpiration"]);

                            // Assuming your Livre class has a method to insert data into the database
                            Livre livre = new Livre(id,title, author, description, genres, cheminImage, etat, state, dateReservation, dateExpiration);
                            livre.InsertIntoDatabase(connection, transaction); // Implement this method in your Livre class
                        }

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception($"Error importing data: {ex.Message}");
                    }
                }
            }
        }



        // LivresRes.xaml.cs **********************************************************************************************************************

        public DataTable GetLivresResData()
        {
            DataTable dataTable = new DataTable();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM livrereserve";
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

        public void DeleteLivresRes(List<LivresRe> LivresResToDelete)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    foreach (var LivresRe in LivresResToDelete)
                    {

                        string deleteQuery = $"DELETE FROM livrereserve WHERE Id = {LivresRe.id}";

                        using (MySqlCommand cmd = new MySqlCommand(deleteQuery, connection))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show($"Error deleting LivresRes: {ex.Message}");
            }

        }

        public void SaveLivresRe(LivresRe newLivresRe)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();


                    string query = "INSERT INTO livrereserve (id, id_produit, DateReservation, DateExpiration, id_user) " +
                                    "VALUES (@Id, @LivreId, @DateReservation, @DateExpiration, @AdherantsId)";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {

                        command.Parameters.AddWithValue("@id", newLivresRe.id);
                        command.Parameters.AddWithValue("@LivreId", newLivresRe.LivreId);
                        command.Parameters.AddWithValue("@DateReservation", newLivresRe.DateReservation);
                        command.Parameters.AddWithValue("@DateExpiration", newLivresRe.DateExpiration);
                        command.Parameters.AddWithValue("@AdherantsId", newLivresRe.AdherantsId);

                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public void UpdateLivresRe(LivresRe updatedLivresRe)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();


                    string updateQuery = @"UPDATE livrereserve 
                                           SET id_produit = @LivreId, 
                                           DateReservation = @DateReservation,
                                           DateExpiration = @DateExpiration,
                                           id_user = @AdherantsId
                                           WHERE id = @id";


                    using (MySqlCommand cmd = new MySqlCommand(updateQuery, connection))
                    {

                        cmd.Parameters.AddWithValue("@id", updatedLivresRe.id);
                        cmd.Parameters.AddWithValue("@LivreId", updatedLivresRe.LivreId);
                        cmd.Parameters.AddWithValue("@DateReservation", updatedLivresRe.DateReservation);
                        cmd.Parameters.AddWithValue("@DateExpiration", updatedLivresRe.DateExpiration);
                        cmd.Parameters.AddWithValue("@AdherantsId", updatedLivresRe.AdherantsId);



                        cmd.ExecuteNonQuery();

                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show($"Error updating LivresRes: {ex.Message}");
                Console.WriteLine(ex.Message.ToString());
            }
        }

        public DataTable GetLivresResDataFind(string search)
        {
            DataTable dataTable = new DataTable();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = $"SELECT * FROM Livrereserve WHERE id LIKE '%{search}%' OR id_produit LIKE '%{search}%' OR DateReservation LIKE '%{search}%' OR DateExpiration LIKE '%{search}%'";
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

        public void ImportLivresResData(DataTable dataTable)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                using (MySqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        foreach (DataRow row in dataTable.Rows)
                        {
                            int id = Convert.ToInt32(row["Id"]);
                            int livreId = Convert.ToInt32(row["id_produit"]);
                            DateTime dateReservation = Convert.ToDateTime(row["DateReservation"]);
                            DateTime dateExpiration = Convert.ToDateTime(row["DateExpiration"]);
                            int adherantsId = Convert.ToInt32(row["id_user"]);



                            // Assuming your Livre class has a method to insert data into the database
                            LivresRe LivresRe = new LivresRe(id, livreId, dateReservation, dateExpiration, adherantsId);
                            LivresRe.InsertIntoDatabase(connection, transaction); // Implement this method in your Livre class
                        }

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception($"Error importing data: {ex.Message}");
                    }
                }
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
                    string query = "SELECT * FROM user";
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

                        string deleteQuery = $"DELETE FROM user WHERE Id = {adherant.id}";

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


                    string query = "INSERT INTO user (id, Nom, Prenom, email, Password) " +
                                    "VALUES (@Id, @Nom, @Prenom, @email, @Password)";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {

                        command.Parameters.AddWithValue("@Id", newAdherant.id);
                        command.Parameters.AddWithValue("@Nom", newAdherant.Nom);
                        command.Parameters.AddWithValue("@Prenom", newAdherant.Prenom);
                        command.Parameters.AddWithValue("@email", newAdherant.email);
                        command.Parameters.AddWithValue("@Password", newAdherant.password);
                       
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


                    string updateQuery = @"UPDATE user 
                                           SET Nom = @Nom, 
                                           Prenom = @Prenom,
                                           email = @email,
                                           Password = @Password
                                           WHERE id = @id";


                    using (MySqlCommand cmd = new MySqlCommand(updateQuery, connection))
                    {

                        cmd.Parameters.AddWithValue("@id", updatedAdherant.id);
                        cmd.Parameters.AddWithValue("@Nom", updatedAdherant.Nom);
                        cmd.Parameters.AddWithValue("@Prenom", updatedAdherant.Prenom);
                        cmd.Parameters.AddWithValue("@email", updatedAdherant.email);
                        cmd.Parameters.AddWithValue("@Password", updatedAdherant.password);



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
                    string query = $"SELECT * FROM user WHERE id LIKE '%{search}%' OR Nom LIKE '%{search}%' OR Prenom LIKE '%{search}%' OR email LIKE '%{search}%'";
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

        public void ImportAdherantsData(DataTable dataTable)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                using (MySqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        foreach (DataRow row in dataTable.Rows)
                        {
                            int id = Convert.ToInt32(row["Id"]);
                            string nom = row["Nom"].ToString();
                            string prenom = row["Prenom"].ToString();
                            string email = row["email"].ToString();
                            string password = row["Password"].ToString();

                            // Assuming your Livre class has a method to insert data into the database
                            Adherant adherant = new Adherant(id, nom, prenom, email, password);
                            adherant.InsertIntoDatabase(connection, transaction); // Implement this method in your Livre class
                        }

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception($"Error importing data: {ex.Message}");
                    }
                }
            }
        }

        // Employes.xaml.cs *******************************************************************************************************************************************

        public DataTable GetEmployesData()
        {
            DataTable dataTable = new DataTable();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM Employee";
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

        public void DeleteEmployes(List<Employe> EmployesToDelete)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();

                    foreach (var Employe in EmployesToDelete)
                    {

                        string deleteQuery = $"DELETE FROM Employee WHERE Id = {Employe.id}";

                        using (MySqlCommand cmd = new MySqlCommand(deleteQuery, connection))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show($"Error deleting Employee: {ex.Message}");
            }

        }

        public void SaveEmploye(Employe newEmploye)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();


                    string query = "INSERT INTO Employee (id, Nom, Prenom, email) " +
                                    "VALUES (@Id, @Nom, @Prenom, @email)";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {

                        command.Parameters.AddWithValue("@Id", newEmploye.id);
                        command.Parameters.AddWithValue("@Nom", newEmploye.Nom);
                        command.Parameters.AddWithValue("@Prenom", newEmploye.Prenom);
                        command.Parameters.AddWithValue("@email", newEmploye.email);

                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public void UpdateEmploye(Employe updatedEmploye)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();


                    string updateQuery = @"UPDATE Employee
                                           SET Nom = @Nom, 
                                           Prenom = @Prenom,
                                           email = @email
                                           WHERE id = @id";


                    using (MySqlCommand cmd = new MySqlCommand(updateQuery, connection))
                    {

                        cmd.Parameters.AddWithValue("@id", updatedEmploye.id);
                        cmd.Parameters.AddWithValue("@Nom", updatedEmploye.Nom);
                        cmd.Parameters.AddWithValue("@Prenom", updatedEmploye.Prenom);
                        cmd.Parameters.AddWithValue("@email", updatedEmploye.email);



                        cmd.ExecuteNonQuery();

                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show($"Error updating Employee: {ex.Message}");
                Console.WriteLine(ex.Message.ToString());
            }
        }

        public DataTable GetEmployeDataFind(string search)
        {
            DataTable dataTable = new DataTable();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = $"SELECT * FROM Employee WHERE id LIKE '%{search}%' OR Nom LIKE '%{search}%' OR Prenom LIKE '%{search}%' OR email LIKE '%{search}%'";
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

        public void ImportEmployesData(DataTable dataTable)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                using (MySqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        foreach (DataRow row in dataTable.Rows)
                        {
                            int id = Convert.ToInt32(row["Id"]);
                            string nom = row["Nom"].ToString();
                            string prenom = row["Prenom"].ToString();
                            string email = row["email"].ToString();

                            
                            Employe employe = new Employe(id, nom, prenom, email);
                            employe.InsertIntoDatabase(connection, transaction); // Implement this method in your Livre class
                        }

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception($"Error importing data: {ex.Message}");
                    }
                }
            }
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



        public void ImportAuteursData(DataTable dataTable)
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                using (MySqlTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        foreach (DataRow row in dataTable.Rows)
                        {
                            int id = Convert.ToInt32(row["Id"]);
                            string nom = row["Nom"].ToString();
                            string prenom = row["Prenom"].ToString();
                            int livreid = Convert.ToInt32(row["Livreid"]);

                            // Assuming your Livre class has a method to insert data into the database
                            Auteur auteur = new Auteur(id, nom, prenom, livreid);
                            auteur.InsertIntoDatabase(connection, transaction); // Implement this method in your Livre class
                        }

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception($"Error importing data: {ex.Message}");
                    }
                }
            }
        }

        // ***********************************************************************************************************************************************************************


    }
}
