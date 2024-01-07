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
    /// Interaction logic for ModifierLivresRes.xaml
    /// </summary>
    public partial class ModifierLivresRes : Window
    {
        private LivresRe existingBook; 

        private readonly string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=dotnet;";
        private readonly DataAccess dataAccess;

        public ModifierLivresRes(LivresRe bookToUpdate = null)
        {
            InitializeComponent();

            dataAccess = new DataAccess(connectionString);

            if (bookToUpdate != null)
            {
                
                existingBook = bookToUpdate;
                
                PopulateFields(existingBook);
            }
        }

        private void PopulateFields(LivresRe LivresRe)
        {
           
            Id.Text = LivresRe.id.ToString();
            LivreId.Text = LivresRe.id.ToString();
            DateReservation.Text = LivresRe.DateReservation.ToString();
            DateExpiration.Text = LivresRe.DateExpiration.ToString();
            AdherantsId.Text = LivresRe.AdherantsId.ToString();

        }

        private void EnregistrerModButton_Click(object sender, RoutedEventArgs e)
        {

           
            LivresRe updatedBook = new LivresRe(

            id: Convert.ToInt32(Id.Text),
            livreId: Convert.ToInt32(LivreId.Text),           
            dateReservation: Convert.ToDateTime(DateReservation.Text),
            dateExpiration: Convert.ToDateTime(DateExpiration.Text),
            adherantsId: Convert.ToInt32(AdherantsId.Text)
        );


     
            if (existingBook != null)
            {
                dataAccess.UpdateLivresRe(updatedBook);
            }
           

            this.DialogResult = true;
        }
    }

}
