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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Security.Policy;
using System.Security.Claims;
using System.Security.Cryptography;

namespace CMPE312_PROJECT_TICKETSYSTEM
{
  
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            
            InitializeComponent();
           
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {

            // Veritabanı bağlantısını aç
            SqlConnection sqlConnection;
            string connectionString = ConfigurationManager.ConnectionStrings["CMPE312_PROJECT_TICKETSYSTEM.Properties.Settings.TicketSystemDBConnectionString"].ConnectionString;
            sqlConnection = new SqlConnection(connectionString);
            
            
            
            string UserName = UnameTextbox.Text;
            string UserPassword = PasswordTextbox.Password;


            // kullancı adı ve şifreyi onaylamak
            string query = "SELECT UserPassword FROM Customers WHERE UserName=@UserName";
            SqlCommand command = new SqlCommand(query, sqlConnection);
            // Parametreleri ayarla
            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@UserPassword", UserPassword);



            // CId yi çekmek için
            string query2 = "SELECT CId FROM Customers WHERE UserName=@UserName ";
            SqlCommand command2 = new SqlCommand(query2, sqlConnection);
            command2.Parameters.AddWithValue("@UserName", UserName);


            // Bağlantıyı aç
            sqlConnection.Open();

            // Sorguyu çalıştır ve sonucları al
            var result =(string)command.ExecuteScalar();
            int CId = (int)command2.ExecuteScalar();


            // Bağlantıyı kapat
            sqlConnection.Close();

            
            if (result == UserPassword )
            {
                // Login başarılı
                
                MoviesPage MoviesPage = new MoviesPage(CId);
                MoviesPage.Show();
                MessageBox.Show("Login is Successful");
                this.Close();
                
            }
            else
            {
                // Login başarısız
                MessageBox.Show("Login is Failed !");
            }

        }


    }
}