using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : UserControl
    {

        private const string ConnectionString = "datasource=127.0.0.1;port=3306;username=root;password=zaratapa;database=dotnet;"; 

        public Dashboard()
        {
            InitializeComponent();
            UpdateAdherentCount();
            UpdateLivresCount();
            UpdateLivresResCount();
            UpdateEmployeCount();
        }

        private void UpdateAdherentCount()
        {
            int adherentCount = GetAdherentCount();
            AdherentCountTextBlock.Text = adherentCount.ToString();
        }

        private int GetAdherentCount()
        {
            int count = 0;

            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();

                    
                    string query = "SELECT COUNT(*) FROM adherants";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        object result = command.ExecuteScalar();

                        if (result != null && int.TryParse(result.ToString(), out int parsedCount))
                        {
                            count = parsedCount;
                        }
                        
                    }
                }
                catch (Exception ex)
                {
                    
                    Console.WriteLine(ex.Message);
                }
            }

            return count;
        }

       

      
        private void UpdateLivresCount()
        {
            int adherentCount = GetLivresCount();
            LivresCountedTextBlock.Text = adherentCount.ToString();
        }

        private int GetLivresCount()
        {
            int count = 0;

            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();


                    string query = "SELECT COUNT(*) FROM livres where State=1";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        object result = command.ExecuteScalar();

                        if (result != null && int.TryParse(result.ToString(), out int parsedCount))
                        {
                            count = parsedCount;
                        }

                    }
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }
            }

            return count;
        }

        private void UpdateEmployeCount()
        {
            int adherentCount = GetEmployeCount();
            EmployeCountTextBlock.Text = adherentCount.ToString();
        }

        private int GetEmployeCount()
        {
            int count = 0;

            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();


                    string query = "SELECT COUNT(*) FROM livres";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        object result = command.ExecuteScalar();

                        if (result != null && int.TryParse(result.ToString(), out int parsedCount))
                        {
                            count = parsedCount;
                        }

                    }
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }
            }

            return count;
        }

        private void UpdateLivresResCount()
        {
            int adherentCount = GetLivresResCount();
            LivresResCountTextBlock.Text = adherentCount.ToString();
        }

        private int GetLivresResCount()
        {
            int count = 0;

            using (MySqlConnection connection = new MySqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();


                    string query = "SELECT COUNT(*) FROM livres where State=0";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        object result = command.ExecuteScalar();

                        if (result != null && int.TryParse(result.ToString(), out int parsedCount))
                        {
                            count = parsedCount;
                        }

                    }
                }
                catch (Exception ex)
                {

                    Console.WriteLine(ex.Message);
                }
            }

            return count;
        }
    }
}
