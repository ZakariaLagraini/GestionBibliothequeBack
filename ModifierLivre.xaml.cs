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
    /// <summary>
    /// Interaction logic for ModifierLivre.xaml
    /// </summary>
    public partial class ModifierLivre : Window
    {
        private Livre existingBook; 

        private readonly string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=zaratapa;database=dotnet;";
        private readonly DataAccess dataAccess;

        public ModifierLivre(Livre bookToUpdate = null)
        {
            InitializeComponent();

            dataAccess = new DataAccess(connectionString);

            if (bookToUpdate != null)
            {
                
                existingBook = bookToUpdate;
                
                PopulateFields(existingBook);
            }
        }

        private void PopulateFields(Livre Livre)
        {
           
            Id.Text = Livre.id.ToString();
            Titre.Text = Livre.Titre;
            Auteurs.Text = Livre.Auteurs;
            Annee_pub.Text = Livre.AnneePublication.ToString();
            Genres.Text = Livre.Genres;
            Etat.Text = Livre.Etat;
            State.Text = Livre.State;
        }

        private void EnregistrerModButton_Click(object sender, RoutedEventArgs e)
        {

           
            Livre updatedBook = new Livre(

            id: Convert.ToInt32(Id.Text),
            titre: Titre.Text,
            auteurs: Auteurs.Text,
            anneePublication: Convert.ToInt32(Annee_pub.Text),
            genres: Genres.Text,
            etat: Etat.Text,
            state: State.Text
        );


     
            if (existingBook != null)
            {
                dataAccess.UpdateBook(updatedBook);
            }
           

            this.DialogResult = true;
        }
    }

}
