using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
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

namespace CMPE312_PROJECT_TICKETSYSTEM
{

    
    public partial class MoviesPage : Window
    {
      
        private int CId;


        public void ShowDetail(int ParameterMId)
        {
            SqlConnection sqlConnection;
            string connectionString = ConfigurationManager.ConnectionStrings["CMPE312_PROJECT_TICKETSYSTEM.Properties.Settings.TicketSystemDBConnectionString"].ConnectionString;
            sqlConnection = new SqlConnection(connectionString);

            int MId = ParameterMId;

            string query = "SELECT * FROM Movies WHERE MId = @MId";
            SqlCommand command = new SqlCommand(query, sqlConnection);

            command.Parameters.AddWithValue("@MId", MId);

            sqlConnection.Open();

            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                // Verileri oku
                int MovieId = reader.GetInt32(0);
                string MovieName = reader.GetString(1);
                string MovieType = reader.GetString(2);
                string MovieSummary = reader.GetString(3);
                string Movielanguage = reader.GetString(4);
                int MovieDuration = reader.GetInt32(5);
                
                byte[] MovieImage = (byte[])reader["MovieImage"];
                // Detay sayfasına verileri aktar
                MovieDetailPage MovieDetailPage = new MovieDetailPage(CId, MId, MovieName, MovieType, MovieSummary, Movielanguage, MovieDuration, MovieImage);

                MovieDetailPage.Show(); // Detay sayfasını göster
                this.Close();
            }



            sqlConnection.Close(); // bağlantıyı kapat
        }

       


        public MoviesPage(int CId)
        {
            InitializeComponent();
            this.CId = CId;
          


        }

        private void mov1_Click(object sender, RoutedEventArgs e)
        {
            ShowDetail(100);
   
        }

        private void mov2_Click(object sender, RoutedEventArgs e)
        {
            ShowDetail(102);
        }

        private void mov3_Click(object sender, RoutedEventArgs e)
        {
            ShowDetail(104);
        }

        private void mov4_Click(object sender, RoutedEventArgs e)
        {
            ShowDetail(105);
        }

        private void mov5_Click(object sender, RoutedEventArgs e)
        {
            ShowDetail(106);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           MyTicketsPage MyTicketsPage = new MyTicketsPage(CId);

            MyTicketsPage.Show(); 
            this.Close();
        }
    }
}
