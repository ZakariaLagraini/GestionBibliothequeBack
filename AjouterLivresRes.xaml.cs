using GestionBiblio.Models;
using System;
using System.Collections.Generic;
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
    public partial class AjouterLivresRes : Window
    {
        private readonly string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=dotnet;";
        private readonly DataAccess dataAccess;

        public AjouterLivresRes()
        {
            InitializeComponent();
            dataAccess = new DataAccess(connectionString);
        }

        private void EnregistrerButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (!int.TryParse(Id.Text, out int id))
                {
                    MessageBox.Show("Invalid ID. Please enter a valid integer.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (!int.TryParse(LivreId.Text, out int Livreid))
                {
                    MessageBox.Show("Invalid ID. Please enter a valid integer.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (!DateTime.TryParse(DateReservation.Text, out DateTime dateReservation))
                {
                    MessageBox.Show("Invalid Date. Please enter a valid Date.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (!DateTime.TryParse(DateExpiration.Text, out DateTime dateExpiration))
                {
                    MessageBox.Show("Invalid Date. Please enter a valid Date.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (!int.TryParse(AdherantsId.Text, out int adherantsid))
                {
                    MessageBox.Show("Invalid ID. Please enter a valid integer.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                LivresRe newBook = new LivresRe(id, Livreid,dateReservation, dateExpiration, adherantsid);

                dataAccess.SaveLivresRe(newBook);

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

