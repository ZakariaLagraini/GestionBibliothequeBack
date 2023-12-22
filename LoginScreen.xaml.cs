using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using MySql.Data.MySqlClient;

namespace GestionBiblio
{
    /// <summary>
    /// Interaction logic for LoginScreen.xaml
    /// </summary>
    public partial class LoginScreen : Window
    {
        public LoginScreen()
        {
            InitializeComponent();
        }

        private void Button_Submit(object sender, RoutedEventArgs e)
        {
<<<<<<< HEAD
            String connString = "datasource=127.0.0.1;port=3306;username=root;password=zaratapa;database=dotnet;";
=======
            String connString = "datasource=127.0.0.1;port=3306;username=root;password=;database=dotnet;";
>>>>>>> f71f2491a62642524ce848e69a451fa5f69c91cd
            MySqlConnection sqlconn = new MySqlConnection(connString);
            try
            {
                if (sqlconn.State == ConnectionState.Closed)
                    sqlconn.Open();
                String query = "SELECT COUNT(1) from admin WHERE Nom=@Nom AND Password=@Password";
                MySqlCommand sqlCmd = new MySqlCommand(query, sqlconn);
                sqlCmd.CommandType = CommandType.Text;
                sqlCmd.Parameters.AddWithValue("@Nom", Username.Text);
                sqlCmd.Parameters.AddWithValue("@Password", Password.Password);
                int count = Convert.ToInt32(sqlCmd.ExecuteScalar());
                if(count == 1)
                {
                    MainWindow d = new MainWindow();
                    d.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Username or password is incorrect. ");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sqlconn.Close();
            }
        
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Window parentWindow = Window.GetWindow(this);

            // Fermez la fenêtre
            if (parentWindow != null)
            {
                parentWindow.Close();
            }
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            Window parentWindow = Window.GetWindow(this);

            // Minimisez la fenêtre
            if (parentWindow != null)
            {
                parentWindow.WindowState = WindowState.Minimized;
            }
        }
    }
}
