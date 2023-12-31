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
    /// Interaction logic for ModifierAuteur.xaml
    /// </summary>
    public partial class ModifierAuteur : Window
    {
        


        private Auteur existingAuteur;

        private readonly string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=dotnet;";
        private readonly DataAccess dataAccess;

        public ModifierAuteur(Auteur AuteurToUpdate = null)
        {
            InitializeComponent();

            dataAccess = new DataAccess(connectionString);

            if (AuteurToUpdate != null)
            {

                existingAuteur = AuteurToUpdate;

                PopulateFields(existingAuteur);
            }
        }

        private void PopulateFields(Auteur Auteur)
        {

            Id.Text = Auteur.id.ToString();
            Nom.Text = Auteur.Nom.ToString();
            Prenom.Text = Auteur.Prenom.ToString();
            LivreId.Text = Auteur.LivresId.ToString();

        }

        private void EnregistrerModButton_Click(object sender, RoutedEventArgs e)
        {


            Auteur updatedAuteur = new Auteur(

            id: Convert.ToInt32(Id.Text),
            nom: Nom.Text,
            prenom: Prenom.Text,
            livreid: Convert.ToInt32(LivreId.Text)

        );



            if (existingAuteur != null)
            {
                dataAccess.UpdateAuteur(updatedAuteur);
            }


            this.DialogResult = true;
        }
    }
}
    

