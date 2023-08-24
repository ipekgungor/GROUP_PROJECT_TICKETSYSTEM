using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.PeerToPeer;
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

namespace CMPE312_PROJECT_TICKETSYSTEM
{

    public partial class CheckoutPage : Window
    {
        public List<string> ttime = new List<string> { "13:40", "15:20", "18:30", "20:15", "22:25" };
        public int CId;
        public byte[] MovieImage;
        public int MId;
        public string MovieName;

        public CheckoutPage(int CId, int MId, string MovieName, string MovieType, string MovieSummary, string MovieLanguage, int MovieDuration, byte[] MovieImage)
        {
            InitializeComponent();


            this.MId = MId;
            this.CId = CId;
            this.MovieImage = MovieImage;
            this.MovieName = MovieName;
            

            var items = ttime;
            foreach (var item in items)
            {

                ComboBoxData.Items.Add(item);
            }

            MovieNameText.Content = MovieName;
            


            if (MovieImage != null)
            {
                

                // byte dizisini BitmapImage nesnesine dönüştürüyoruz
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = new MemoryStream(MovieImage);
                bitmapImage.EndInit();

                // Image kontrolünün Source özelliğine BitmapImage nesnesini atıyoruz
                ImageBox.Source = bitmapImage;
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


            string UserPassword = PasswordTextBox.Password;
            string CreditCard = CreditCardTextBox.Text;

            DateTime TicketDate;
            if (DatePickerData.SelectedDate.HasValue)
            {
                TicketDate = DatePickerData.SelectedDate.Value;
            }
            else
            {
                MessageBox.Show("Please select date.");
                return;
            }

            if (Checkbox.IsChecked == false) { MessageBox.Show("Please confirm terms and conditions."); }
            if (CreditCardTextBox.Text == "") { MessageBox.Show("Please type credit card number."); }

            SqlConnection sqlConnection;
            string connectionString = ConfigurationManager.ConnectionStrings["CMPE312_PROJECT_TICKETSYSTEM.Properties.Settings.TicketSystemDBConnectionString"].ConnectionString;
            sqlConnection = new SqlConnection(connectionString);

            string query = "SELECT UserPassword FROM Customers WHERE CId=@CId ";
            SqlCommand command = new SqlCommand(query, sqlConnection);
            command.Parameters.AddWithValue("@CId", CId);
            command.Parameters.AddWithValue("@UserPassword", UserPassword);
            sqlConnection.Open();
            string result = (string)command.ExecuteScalar();
            sqlConnection.Close();

            string Hall = "Hall 2";
            float Price = 75;
            string TicketTime =ComboBoxData.SelectedValue.ToString();
            int TId = GenerateTId();

            string InsertQuery = "INSERT INTO Tickets (TId, MId, CId, TicketDate, Hall, Price, TicketTime, MovieName ) VALUES (@TId, @MId, @CId, @TicketDate, @Hall, @Price, @TicketTime, @MovieName)";

            SqlCommand command2 = new SqlCommand(InsertQuery, sqlConnection);

            command2.Parameters.AddWithValue("@TId", TId);
            command2.Parameters.AddWithValue("@MId", MId);
            command2.Parameters.AddWithValue("@CId", CId);
            command2.Parameters.AddWithValue("@TicketDate", TicketDate);
            command2.Parameters.AddWithValue("@Hall", Hall);
            command2.Parameters.AddWithValue("@Price", Price);
            command2.Parameters.AddWithValue("@TicketTime", TicketTime);
            command2.Parameters.AddWithValue("@MovieName", MovieName);

            sqlConnection.Open();
            command2.ExecuteNonQuery();
            sqlConnection.Close();


            if (result == UserPassword)
            {
                MessageBox.Show("Payment is Completed");
                
            }
            else
            {
                MessageBox.Show("Payment is Failed !");
            }

        }

        public int GenerateTId()
        {
            string IdLength = "4";
            string TicketNumber = "";
            int TicketId;

            string allowedChars = "";
            allowedChars = "1,2,3,4,5,6,7,8,9,0";


            char[] sep = {
                ','
            };
            string[] arr = allowedChars.Split(sep);


            string IDString = "";
            string temp = "";

            Random rand = new Random();

            for (int i = 0; i < Convert.ToInt32(IdLength); i++)
            {
                temp = arr[rand.Next(0, arr.Length)];
                IDString += temp;
                TicketNumber = IDString;

            }
            TicketId = Convert.ToInt32(TicketNumber);
            return TicketId;
        }

    }


}
