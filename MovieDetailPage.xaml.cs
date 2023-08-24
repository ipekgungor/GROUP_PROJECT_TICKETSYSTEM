using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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

    public partial class MovieDetailPage : Window
    {
        private int CId;
        private int MId;
        private string MovieName;
        private string MovieType;
        private string MovieSummary;
        private string MovieLanguage;
        private int MovieDuration;
        private byte[] MovieImage;


        public MovieDetailPage(int CId, int MId, string MovieName, string MovieType, string MovieSummary, string MovieLanguage, int MovieDuration, byte[] MovieImage)
        {
            InitializeComponent();
            this.CId= CId;
            this.MId = MId;
            this.MovieName = MovieName;
            this.MovieType = MovieType;
            this.MovieSummary = MovieSummary;
            this.MovieLanguage = MovieLanguage;
            this.MovieDuration = MovieDuration;
            this.MovieImage = MovieImage;

            MovieNameText.Content = MovieName;
            MovieTypeText.Content = MovieType;
            MovieSummaryText.Text = MovieSummary;
            MovieLanguageText.Content = MovieLanguage;
            MovieDurationText.Content = MovieDuration;


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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MoviesPage MoviesPage = new MoviesPage(CId);
            MoviesPage.Show();
            this.Close();
        }

        private void CheckoutButton_Click(object sender, RoutedEventArgs e)
        {
            CheckoutPage CheckoutPage = new CheckoutPage(CId, MId, MovieName, MovieType, MovieSummary, MovieLanguage, MovieDuration, MovieImage);
            CheckoutPage.Show();
            this.Close();
        }
    }
}