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
    /// Interaction logic for ModifierAdherant.xaml
    /// </summary>
    public partial class ModifierAdherant : Window
    {
        private Adherant existingAdherant; 

        private readonly string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=dotnet;";
        private readonly DataAccess dataAccess;

        public ModifierAdherant(Adherant AdherantToUpdate = null)
        {
            InitializeComponent();

            dataAccess = new DataAccess(connectionString);

            if (AdherantToUpdate != null)
            {
                
                existingAdherant = AdherantToUpdate;
                
                PopulateFields(existingAdherant);
            }
        }

        private void PopulateFields(Adherant Adherant)
        {
           
            Id.Text = Adherant.id.ToString();
            Nom.Text = Adherant.Nom.ToString();
            Prenom.Text = Adherant.Prenom.ToString();
            email.Text = Adherant.email.ToString();
            Password.Text = Adherant.password.ToString();
            
        }

        private void EnregistrerModButton_Click(object sender, RoutedEventArgs e)
        {

           
            Adherant updatedAdherant = new Adherant(

            id: Convert.ToInt32(Id.Text),
            nom: Nom.Text,
            prenom: Prenom.Text,
            email: email.Text,
            password: Password.Text
           
        );


     
            if (existingAdherant != null)
            {
                dataAccess.UpdateAdherant(updatedAdherant);
            }
           

            this.DialogResult = true;
        }
    }

}
