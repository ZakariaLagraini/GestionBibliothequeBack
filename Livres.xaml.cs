using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GestionBiblio
{
    /// <summary>
    /// Interaction logic for Livres.xaml
    /// </summary>
    /// 
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
                        string query = "SELECT Titre, Auteurs, AnneePublication, Genres, Etat FROM livres";
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
        }
    

    public partial class Livres : UserControl
    {
        private readonly string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=zaratapa;database=dotnet;";
        private readonly DataAccess dataAccess;

        public Livres()
        {
            InitializeComponent();
            dataAccess = new DataAccess(connectionString);

            BindDataToGrid();
        }

        private void BindDataToGrid()
        {
            dataGridBooks.ItemsSource = dataAccess.GetBooksData().DefaultView;
        }
    }
}
