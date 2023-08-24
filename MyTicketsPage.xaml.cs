using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Sockets;
using System.Security.Claims;
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
using System.Windows.Navigation;

namespace CMPE312_PROJECT_TICKETSYSTEM
{

    public partial class MyTicketsPage : Window
    {
        public int CId;
        public List<string> ttime = new List<string> { "13:40", "15:20", "18:30", "20:15", "22:25" };
        public MyTicketsPage(int CId)
        {
            InitializeComponent();


            SqlConnection sqlConnection;
            string connectionString = ConfigurationManager.ConnectionStrings["CMPE312_PROJECT_TICKETSYSTEM.Properties.Settings.TicketSystemDBConnectionString"].ConnectionString;
            sqlConnection = new SqlConnection(connectionString);

            string query = "SELECT * FROM Tickets WHERE CId=@CId ";
            SqlCommand command = new SqlCommand(query, sqlConnection);

            command.Parameters.AddWithValue("@CId", CId);

            sqlConnection.Open();
            SqlDataReader reader = command.ExecuteReader();

            List<Tickets> tickets = new List<Tickets>();

            while (reader.Read())
            {
                int TId = (int)reader["TId"];
                int MId = (int)reader["MId"];
                string Hall = reader["Hall"].ToString();
                string MovieName = reader["MovieName"].ToString();
                string TicketTime = (string)reader["TicketTime"];
                DateTime  TicketDate = (DateTime)reader["TicketDate"];
                float Price = Convert.ToSingle(reader["Price"]);
                

                Tickets ticket = new Tickets(TId,CId,MovieName,TicketDate,TicketTime,Hall,Price);
                tickets.Add(ticket);


            }
            sqlConnection.Close();

            TicketDataGrid.ItemsSource = tickets;
            this.CId= CId;

            var items = ttime;
            foreach (var item in items)
            {

                ComboBoxData.Items.Add(item);
            }

        }


        // Biletleri silme
        private void DeleteTicketButton_Click(object sender, RoutedEventArgs e)
        {
            Tickets selectedTicket = ((FrameworkElement)sender).DataContext as Tickets;
            if (selectedTicket != null)
            {
                // Seçilen biletin silme işlemini gerçekleştir
                int TId = selectedTicket.TId;

                SqlConnection sqlConnection;
                string connectionString = ConfigurationManager.ConnectionStrings["CMPE312_PROJECT_TICKETSYSTEM.Properties.Settings.TicketSystemDBConnectionString"].ConnectionString;
                sqlConnection = new SqlConnection(connectionString);

                string query = "DELETE FROM Tickets WHERE TId=@TId ";
                SqlCommand command = new SqlCommand(query, sqlConnection);

                command.Parameters.AddWithValue("@TId", TId);

                sqlConnection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                MessageBox.Show("Ticket deleted successfully. Please refresh the page.");
                sqlConnection.Close();
            }
        }


        private void UpdateTicketButton_Click(object sender, RoutedEventArgs e)
        {
            Tickets selectedTicket = ((FrameworkElement)sender).DataContext as Tickets;
            
            ComboBoxData.Visibility = Visibility.Visible;
            string TicketTime = ComboBoxData.SelectedValue.ToString();


            if (selectedTicket != null)
            {
                int TId = selectedTicket.TId;               
                SqlConnection sqlConnection;
                string connectionString = ConfigurationManager.ConnectionStrings["CMPE312_PROJECT_TICKETSYSTEM.Properties.Settings.TicketSystemDBConnectionString"].ConnectionString;
                sqlConnection = new SqlConnection(connectionString);

                string query = "UPDATE Tickets SET  TicketTime = @TicketTime WHERE TId = @TId";

                SqlCommand command = new SqlCommand(query, sqlConnection);

                command.Parameters.AddWithValue("@TicketTime", TicketTime);
                command.Parameters.AddWithValue("@TId", TId);

                sqlConnection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                sqlConnection.Close();

                MessageBox.Show("Ticket updated successfully. Please refresh the page.");
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            MoviesPage MoviesPage = new MoviesPage(CId);
            MoviesPage.Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            MyTicketsPage MyTicketsPage = new MyTicketsPage(CId);
            MyTicketsPage.Show();
            this.Close();

        }
    }
}
