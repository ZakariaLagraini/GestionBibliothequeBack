using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace GestionBiblio
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            // Set the EPPlus license context
            OfficeOpenXml.ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            base.OnStartup(e);
        }
    }
}
