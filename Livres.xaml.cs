using GestionBiblio.Models;
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
        private void RefreshDataGrid()
        {

            if (dataGridBooks != null)
            {
                dataGridBooks.ItemsSource = dataAccess.GetBooksData().DefaultView;
            }
        }

        private void Button_DpiChanged(object sender, DpiChangedEventArgs e)
        {

        }

        private void AjouterButton_Click(object sender, RoutedEventArgs e)
        {
            AjouterLivre ajouterLivreWindow = new AjouterLivre();

            // Show the window as a dialog
            if (ajouterLivreWindow.ShowDialog() == true)
            {
                RefreshDataGrid();
                BindDataToGrid(); 
            }
        }

        
    }
}
