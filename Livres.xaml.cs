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
            dataGridBooks.Items.Clear();

            // Set the ItemsSource
            dataGridBooks.ItemsSource = dataAccess.GetBooksData().DefaultView;
        
    }
        private void RefreshDataGrid()
        {

            if (dataGridBooks != null)
            {
                dataGridBooks.ItemsSource = dataAccess.GetBooksData().DefaultView;
            }
        }

        private void DeleteSelectedRows()
        {
            var selectedItems = dataGridBooks.SelectedItems;

            if (selectedItems.Count > 0)
            {
                List<Livre> booksToDelete = new List<Livre>();

                foreach (var selectedItem in selectedItems)
                {
                    DataRowView row = selectedItem as DataRowView;

                    if (row != null)
                    {
                        // Extract the necessary data from the row or use the corresponding data structure (Book class)
                        // For example:
                        int id = Convert.ToInt32(row["Id"]);
                        string title = row["Titre"].ToString();
                        string author = row["Auteurs"].ToString();
                        int annee_pub = Convert.ToInt32(row["AnneePublication"]);
                        string genres = row["Genres"].ToString();
                        string etat = row["Etat"].ToString();
                        string state = row["State"].ToString();

                        // Create a Book object and add it to the list of books to delete
                        booksToDelete.Add(new Livre(id, title, author, annee_pub, genres, etat, state));
                    }
                }

                // Perform the deletion in the database
                dataAccess.DeleteBooks(booksToDelete);

                // Refresh the DataGrid
                RefreshDataGrid();
            }
            else
            {
                MessageBox.Show("No rows selected.");
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            DeleteSelectedRows();
        }

        private void UpdateSelectedRow()
        {
            var selectedItems = dataGridBooks.SelectedItems;

            if (selectedItems.Count == 1)
            {
                DataRowView rowView = selectedItems[0] as DataRowView;

                if (rowView != null)
                {
                    // Assuming there's an "Id" column in your DataTable
                    int Id = Convert.ToInt32(rowView["Id"]);

                    // Create a Book object with existing data
                    Livre selectedBook = new Livre(
                     id: Id,
                     titre: rowView["Titre"].ToString(),
                     auteurs: rowView["Auteurs"].ToString(),
                     anneePublication: Convert.ToInt32(rowView["AnneePublication"]),
                     genres: rowView["Genres"].ToString(),
                     etat: rowView["Etat"].ToString(),
                     state: rowView["State"].ToString()
                    );
                   

                    // Open the update window and pass the selected book
                    ModifierLivre updateLivreWindow = new ModifierLivre(selectedBook);

                    // Show the window as a dialog
                    if (updateLivreWindow.ShowDialog() == true)
                    {   

                        // Refresh the DataGrid
                        RefreshDataGrid();
                    }
                }
            }
            else
            {
                MessageBox.Show("Select one row to update.");
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateSelectedRow();
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
