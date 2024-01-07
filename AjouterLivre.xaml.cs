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
    public partial class AjouterLivre : Window
    {
        private readonly string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=dotnet;";
        private readonly DataAccess dataAccess;

        public AjouterLivre()
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

                string titre = Titre.Text;
                string auteurs = Auteurs.Text;
                string description = Description.Text;
                string genres = Genres.Text;
                string cheminImage = CheminImage.Text;
                string etat = Etat.Text;
                string state = State.Text;
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

                Livre newBook = new Livre(id, titre, auteurs, description, genres, cheminImage, etat, state, dateReservation, dateExpiration);

                dataAccess.SaveBook(newBook); 

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

