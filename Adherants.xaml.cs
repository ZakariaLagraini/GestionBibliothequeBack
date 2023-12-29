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

namespace GestionBiblio
{
    /// <summary>
    /// Logique d'interaction pour Adherants.xaml
    /// </summary>
    public partial class Adherants : UserControl
    {
        private readonly string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=dotnet;";
        private readonly DataAccess dataAccess;

        public Adherants()
        {
            InitializeComponent();
            dataAccess = new DataAccess(connectionString);

            BindDataToGrid();
        }

        private void BindDataToGrid()
        {
            dataGridAdherants.Items.Clear();

            dataGridAdherants.ItemsSource = dataAccess.GetAdherantsData().DefaultView;

        }
        private void RefreshDataGrid()
        {

            if (dataGridAdherants != null)
            {
                dataGridAdherants.ItemsSource = dataAccess.GetAdherantsData().DefaultView;
            }
        }

          private void DeleteSelectedRows()
           {
               var selectedItems = dataGridAdherants.SelectedItems;

               if (selectedItems.Count > 0)
               {
                   List<Adherant> AdherantsToDelete = new List<Adherant>();

                   foreach (var selectedItem in selectedItems)
                   {
                       DataRowView row = selectedItem as DataRowView;

                       if (row != null)
                       {

                           int id = Convert.ToInt32(row["Id"]);
                           string nom = row["Nom"].ToString();
                           string prenom = row["Prenom"].ToString();
                           string email = row["email"].ToString();
                           
                           AdherantsToDelete.Add(new Adherant(id, nom, prenom, email));
                       }
                   }


                   dataAccess.DeleteAdherants(AdherantsToDelete);


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
               var selectedItems = dataGridAdherants.SelectedItems;

               if (selectedItems.Count == 1)
               {
                   DataRowView rowView = selectedItems[0] as DataRowView;

                   if (rowView != null)
                   {

                       int Id = Convert.ToInt32(rowView["Id"]);


                       Adherant selectedAdherant = new Adherant(
                        id: Id,
                        nom: rowView["Nom"].ToString(),
                        prenom: rowView["Prenom"].ToString(),
                        email: rowView["email"].ToString()
                        
                       );



                       ModifierAdherant updateAdherantWindow = new ModifierAdherant(selectedAdherant);


                       if (updateAdherantWindow.ShowDialog() == true)
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
               AjouterAdherant ajouterAdherantWindow = new AjouterAdherant();

               if (ajouterAdherantWindow.ShowDialog() == true)
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
                   FileName = "AdherantList",
                   DefaultExt = ".xlsx",
                   Filter = "Excel Files (*.xlsx)|*.xlsx|All files (*.*)|*.*"
               };

               bool? result = saveFileDialog.ShowDialog();

               if (result == true)
               {
                   FileInfo file = new FileInfo(saveFileDialog.FileName);

                   using (ExcelPackage package = new ExcelPackage(file))
                   {
                       ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("AdherantList");

                       // Add column headers

                       for (int i = 0; i < dataGridAdherants.Columns.Count; i++)
                       {
                           if (dataGridAdherants.Columns[i] is DataGridBoundColumn boundColumn)
                           {
                               var bindingPath = (boundColumn.Binding as Binding)?.Path.Path;
                               if (!string.IsNullOrEmpty(bindingPath))
                               {
                                   worksheet.Cells[1, i + 1].Value = bindingPath;
                               }
                           }
                       }

                       // Add data rows
                       for (int i = 0; i < dataGridAdherants.Items.Count; i++)
                       {
                           DataRowView rowView = dataGridAdherants.Items[i] as DataRowView;

                           if (rowView != null)
                           {
                               DataRow row = rowView.Row;

                               for (int j = 0; j < dataGridAdherants.Columns.Count; j++)
                               {
                                   if (dataGridAdherants.Columns[j] is DataGridBoundColumn boundColumn)
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
       
    }
}
