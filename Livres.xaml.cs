using GestionBiblio.Models;
using MySql.Data.MySqlClient;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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
        private readonly string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=dotnet;";
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
                        
                        int id = Convert.ToInt32(row["Id"]);
                        string title = row["Titre"].ToString();
                        string author = row["Auteurs"].ToString();
                        int annee_pub = Convert.ToInt32(row["AnneePublication"]);
                        string genres = row["Genres"].ToString();
                        string etat = row["Etat"].ToString();
                        string state = row["State"].ToString();

                        booksToDelete.Add(new Livre(id, title, author, annee_pub, genres, etat, state));
                    }
                }

               
                dataAccess.DeleteBooks(booksToDelete);

                
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
                   
                    int Id = Convert.ToInt32(rowView["Id"]);

                    
                    Livre selectedBook = new Livre(
                     id: Id,
                     titre: rowView["Titre"].ToString(),
                     auteurs: rowView["Auteurs"].ToString(),
                     anneePublication: Convert.ToInt32(rowView["AnneePublication"]),
                     genres: rowView["Genres"].ToString(),
                     etat: rowView["Etat"].ToString(),
                     state: rowView["State"].ToString()
                    );
                   

                    
                    ModifierLivre updateLivreWindow = new ModifierLivre(selectedBook);

                  
                    if (updateLivreWindow.ShowDialog() == true)
                    {   

                        
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

            if (ajouterLivreWindow.ShowDialog() == true)
            {
                RefreshDataGrid();
                BindDataToGrid(); 
            }
        }


        private void ExportToExcel_Click(object sender, RoutedEventArgs e)
        {
            ExportToExcel();
        }

        private void ExportToExcel()
        {
            var saveFileDialog = new Microsoft.Win32.SaveFileDialog
            {
                FileName = "BookList",
                DefaultExt = ".xlsx",
                Filter = "Excel Files (*.xlsx)|*.xlsx|All files (*.*)|*.*"
            };

            bool? result = saveFileDialog.ShowDialog();

            if (result == true)
            {
                FileInfo file = new FileInfo(saveFileDialog.FileName);

                using (ExcelPackage package = new ExcelPackage(file))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("BookList");

                    // Add column headers

                    for (int i = 0; i < dataGridBooks.Columns.Count; i++)
                    {
                        if (dataGridBooks.Columns[i] is DataGridBoundColumn boundColumn)
                        {
                            var bindingPath = (boundColumn.Binding as Binding)?.Path.Path;
                            if (!string.IsNullOrEmpty(bindingPath))
                            {
                                worksheet.Cells[1, i + 1].Value = bindingPath;
                            }
                        }
                    }

                    // Add data rows
                    for (int i = 0; i < dataGridBooks.Items.Count; i++)
                    {
                        DataRowView rowView = dataGridBooks.Items[i] as DataRowView;

                        if (rowView != null)
                        {
                            DataRow row = rowView.Row;

                            for (int j = 0; j < dataGridBooks.Columns.Count; j++)
                            {
                                if (dataGridBooks.Columns[j] is DataGridBoundColumn boundColumn)
                                {
                                    var bindingPath = (boundColumn.Binding as Binding)?.Path.Path;
                                    if (!string.IsNullOrEmpty(bindingPath))
                                    {
                                        worksheet.Cells[i + 2, j + 1].Value = row[bindingPath];
                                    }
                                }
                            }
                        }
                    }


                    // AutoFit columns for better readability
                    worksheet.Cells.AutoFitColumns(0);
                    worksheet.Cells.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

                    package.Save();
                }

                MessageBox.Show("Data exported successfully!");
            }
        }


        private void ImportFromExcel_Click(object sender, RoutedEventArgs e)
        {
            ImportFromExcel();
        }

        private void ImportFromExcel()
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                DefaultExt = ".xlsx",
                Filter = "Excel Files (*.xlsx)|*.xlsx|All files (*.*)|*.*"
            };

            bool? result = openFileDialog.ShowDialog();

            if (result == true)
            {
                try
                {
                    FileInfo file = new FileInfo(openFileDialog.FileName);

                    using (ExcelPackage package = new ExcelPackage(file))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets.FirstOrDefault();

                        if (worksheet != null)
                        {
                            DataTable dataTable = new DataTable();

                            // Assuming the first row in the Excel file contains column headers
                            for (int i = 1; i <= worksheet.Dimension.Columns; i++)
                            {
                                string columnName = worksheet.Cells[1, i].Text;
                                dataTable.Columns.Add(columnName);
                            }

                            // Start reading data from the second row
                            for (int row = 2; row <= worksheet.Dimension.Rows; row++)
                            {
                                DataRow dataRow = dataTable.NewRow();

                                for (int col = 1; col <= worksheet.Dimension.Columns; col++)
                                {
                                    dataRow[dataTable.Columns[col - 1].ColumnName] = worksheet.Cells[row, col].Text;
                                }

                                dataTable.Rows.Add(dataRow);
                            }

                            // Pass the DataTable to your data access method for insertion
                            dataAccess.ImportBooksData(dataTable);

                            // Refresh the data grid after import
                            RefreshDataGrid();

                            MessageBox.Show("Data imported successfully!");
                        }
                        else
                        {
                            MessageBox.Show("No worksheet found in the Excel file.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred during import: {ex.Message}");
                }
            }
        }



    }
}
