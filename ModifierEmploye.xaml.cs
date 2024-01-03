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
    /// Interaction logic for ModifierEmploye.xaml
    /// </summary>
    public partial class ModifierEmploye : Window
    {
        private Employe existingEmploye;

        private readonly string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=dotnet;";
        private readonly DataAccess dataAccess;

        public ModifierEmploye(Employe EmployeToUpdate = null)
        {
            InitializeComponent();

            dataAccess = new DataAccess(connectionString);

            if (EmployeToUpdate != null)
            {

                existingEmploye = EmployeToUpdate;

                PopulateFields(existingEmploye);
            }
        }

        private void PopulateFields(Employe Employe)
        {

            Id.Text = Employe.id.ToString();
            Nom.Text = Employe.Nom.ToString();
            Prenom.Text = Employe.Prenom.ToString();
            email.Text = Employe.email.ToString();

        }

        private void EnregistrerModButton_Click(object sender, RoutedEventArgs e)
        {


            Employe updatedEmploye = new Employe(

            id: Convert.ToInt32(Id.Text),
            nom: Nom.Text,
            prenom: Prenom.Text,
            email: email.Text

        );



            if (existingEmploye != null)
            {
                dataAccess.UpdateEmploye(updatedEmploye);
            }


            this.DialogResult = true;
        }
    }

}
