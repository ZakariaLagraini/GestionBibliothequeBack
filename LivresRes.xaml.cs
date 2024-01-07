using GestionBiblio.Models;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;

namespace GestionBiblio
{
    /// <summary>
    /// Logique d'interaction pour LivresRes.xaml
    /// </summary>
    public partial class LivresRes : UserControl
    {
        private readonly string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=dotnet;";
        private readonly DataAccess dataAccess;

        public LivresRes()
        {
            InitializeComponent();
            dataAccess = new DataAccess(connectionString);

            BindDataToGrid();
        }

        private void BindDataToGrid()
        {
            dataGridLivresRes.Items.Clear();

            dataGridLivresRes.ItemsSource = dataAccess.GetLivresResData().DefaultView;

        }

        private void BindDataToGridFind(string search)
        {
            DataView dv = dataAccess.GetLivresResDataFind(search).DefaultView;

            // Instead of clearing Items and setting ItemsSource directly, you can modify the existing DataView
            dataGridLivresRes.ItemsSource = dv;

            // Optionally, you can call Refresh to force the grid to update its UI
            dataGridLivresRes.Items.Refresh();
        }


        private void RefreshDataGrid()
        {

            if (dataGridLivresRes != null)
            {
                dataGridLivresRes.ItemsSource = dataAccess.GetLivresResData().DefaultView;
            }
        }

        private void RefreshDataGridFind(string search)
        {

            if (dataGridLivresRes != null)
            {
                dataGridLivresRes.ItemsSource = dataAccess.GetLivresResDataFind(search).DefaultView;
            }
        }

        private void DeleteSelectedRows()
        {
            var selectedItems = dataGridLivresRes.SelectedItems;

            if (selectedItems.Count > 0)
            {
                List<LivresRe> LivresResToDelete = new List<LivresRe>();

                foreach (var selectedItem in selectedItems)
                {
                    DataRowView row = selectedItem as DataRowView;

                    if (row != null)
                    {

                        int id = Convert.ToInt32(row["Id"]);
                        int livreId = Convert.ToInt32(row["id_produit"]);
                        DateTime dateReservation = Convert.ToDateTime(row["DateReservation"]);
                        DateTime dateExpiration = Convert.ToDateTime(row["DateExpiration"]);
                        int adherantsId = Convert.ToInt32(row["id_user"]);

                        LivresResToDelete.Add(new LivresRe(id, livreId, dateReservation, dateExpiration, adherantsId));
                    }
                }


                dataAccess.DeleteLivresRes(LivresResToDelete);


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

        //*****************************************************************************************************************
        private void UpdateSelectedRow()
        {
            var selectedItems = dataGridLivresRes.SelectedItems;

            if (selectedItems.Count == 1)
            {
                DataRowView rowView = selectedItems[0] as DataRowView;

                if (rowView != null)
                {

                    int Id = Convert.ToInt32(rowView["Id"]);


                    LivresRe selectedLivresRe = new LivresRe(
                     id: Id,
                     livreId: Convert.ToInt32(rowView["id_produit"]),
                     dateReservation: Convert.ToDateTime(rowView["DateReservation"]),
                     dateExpiration: Convert.ToDateTime(rowView["DateExpiration"]),
                     adherantsId: Convert.ToInt32(rowView["id_user"])

                    );



                    ModifierLivresRes updateLivresReWindow = new ModifierLivresRes(selectedLivresRe);


                    if (updateLivresReWindow.ShowDialog() == true)
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
            AjouterLivresRes ajouterLivresReWindow = new AjouterLivresRes();

            if (ajouterLivresReWindow.ShowDialog() == true)
            {
                RefreshDataGrid();
                BindDataToGrid();
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Find(object sender, RoutedEventArgs e)
        {
            string searchTerm = SearchBox.Text; // Assuming textBox is the name of your TextBox control

            if (!string.IsNullOrEmpty(searchTerm))
            {
                BindDataToGridFind(searchTerm);
                RefreshDataGridFind(searchTerm);
            }
            else
            {
                // If the search term is empty, refresh the grid with the original data
                RefreshDataGrid();
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
                FileName = "LivresReList",
                DefaultExt = ".xlsx",
                Filter = "Excel Files (*.xlsx)|*.xlsx|All files (*.*)|*.*"
            };

            bool? result = saveFileDialog.ShowDialog();

            if (result == true)
            {
                FileInfo file = new FileInfo(saveFileDialog.FileName);

                using (ExcelPackage package = new ExcelPackage(file))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("LivresReList");

                    // Add column headers

                    for (int i = 0; i < dataGridLivresRes.Columns.Count; i++)
                    {
                        if (dataGridLivresRes.Columns[i] is DataGridBoundColumn boundColumn)
                        {
                            var bindingPath = (boundColumn.Binding as Binding)?.Path.Path;
                            if (!string.IsNullOrEmpty(bindingPath))
                            {
                                worksheet.Cells[1, i + 1].Value = bindingPath;
                            }
                        }
                    }

                    // Add data rows
                    for (int i = 0; i < dataGridLivresRes.Items.Count; i++)
                    {
                        DataRowView rowView = dataGridLivresRes.Items[i] as DataRowView;

                        if (rowView != null)
                        {
                            DataRow row = rowView.Row;

                            for (int j = 0; j < dataGridLivresRes.Columns.Count; j++)
                            {
                                if (dataGridLivresRes.Columns[j] is DataGridBoundColumn boundColumn)
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
                            dataAccess.ImportLivresResData(dataTable);

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
