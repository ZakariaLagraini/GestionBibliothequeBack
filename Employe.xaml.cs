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
    /// Logique d'interaction pour Employe.xaml
    /// </summary>
    public partial class Employe : UserControl
    {
        private readonly string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=dotnet;";
        private readonly DataAccess dataAccess;

        public Employe()
        {
            InitializeComponent();
            dataAccess = new DataAccess(connectionString);

            BindDataToGrid();
        }

        private void BindDataToGrid()
        {
            dataGridEmploye.Items.Clear();

            dataGridEmploye.ItemsSource = dataAccess.GetEmployeData().DefaultView;

        }

        private void BindDataToGridFind(string search)
        {
            DataView dv = dataAccess.GetEmployeDataFind(search).DefaultView;

            // Instead of clearing Items and setting ItemsSource directly, you can modify the existing DataView
            dataGridEmploye.ItemsSource = dv;

            // Optionally, you can call Refresh to force the grid to update its UI
            dataGridEmploye.Items.Refresh();
        }


        private void RefreshDataGrid()
        {

            if (dataGridEmploye != null)
            {
                dataGridEmploye.ItemsSource = dataAccess.GetEmployeData().DefaultView;
            }
        }

        private void RefreshDataGridFind(string search)
        {

            if (dataGridEmploye != null)
            {
                dataGridEmploye.ItemsSource = dataAccess.GetEmployeDataFind(search).DefaultView;
            }
        }

        private void DeleteSelectedRows()
        {
            var selectedItems = dataGridEmploye.SelectedItems;

            if (selectedItems.Count > 0)
            {
                List<Employe> EmployeToDelete = new List<Employe>();

                foreach (var selectedItem in selectedItems)
                {
                    DataRowView row = selectedItem as DataRowView;

                    if (row != null)
                    {

                        int id = Convert.ToInt32(row["Id"]);
                        string nom = row["Nom"].ToString();
                        string prenom = row["Prenom"].ToString();
                        string email = row["email"].ToString();

                        EmployeToDelete.Add(new Employe(id, nom, prenom, email));
                    }
                }


                dataAccess.DeleteEmploye(EmployeToDelete);


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
            var selectedItems = dataGridEmploye.SelectedItems;

            if (selectedItems.Count == 1)
            {
                DataRowView rowView = selectedItems[0] as DataRowView;

                if (rowView != null)
                {

                    int Id = Convert.ToInt32(rowView["Id"]);


                    Employe selectedEmploye = new Employe(
                     id: Id,
                     nom: rowView["Nom"].ToString(),
                     prenom: rowView["Prenom"].ToString(),
                     email: rowView["email"].ToString()

                    );



                    ModifierEmploye updateEmployeWindow = new ModifierEmploye(selectedEmploye);


                    if (updateEmployeWindow.ShowDialog() == true)
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
            AjouterEmploye ajouterEmployeWindow = new AjouterEmploye();

            if (ajouterEmployeWindow.ShowDialog() == true)
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
                FileName = "EmployeList",
                DefaultExt = ".xlsx",
                Filter = "Excel Files (*.xlsx)|*.xlsx|All files (*.*)|*.*"
            };

            bool? result = saveFileDialog.ShowDialog();

            if (result == true)
            {
                FileInfo file = new FileInfo(saveFileDialog.FileName);

                using (ExcelPackage package = new ExcelPackage(file))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("EmployeList");

                    // Add column headers

                    for (int i = 0; i < dataGridEmploye.Columns.Count; i++)
                    {
                        if (dataGridEmploye.Columns[i] is DataGridBoundColumn boundColumn)
                        {
                            var bindingPath = (boundColumn.Binding as Binding)?.Path.Path;
                            if (!string.IsNullOrEmpty(bindingPath))
                            {
                                worksheet.Cells[1, i + 1].Value = bindingPath;
                            }
                        }
                    }

                    // Add data rows
                    for (int i = 0; i < dataGridEmploye.Items.Count; i++)
                    {
                        DataRowView rowView = dataGridEmploye.Items[i] as DataRowView;

                        if (rowView != null)
                        {
                            DataRow row = rowView.Row;

                            for (int j = 0; j < dataGridEmploye.Columns.Count; j++)
                            {
                                if (dataGridEmploye.Columns[j] is DataGridBoundColumn boundColumn)
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


    }
}
