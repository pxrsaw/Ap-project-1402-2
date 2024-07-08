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
using System.Xml;

namespace UserManagementSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class RestaurantPage : Window
    {
        private Restaurant _restaurant;

        public RestaurantPage(Restaurant restaurant)
        {
            InitializeComponent();
            _restaurant = restaurant;
            LoadRestaurantData();
        }

        private void LoadRestaurantData()
        {
            // Bind the restaurant data to the UI elements
            NameLabel.Content = _restaurant.Name;
            LoadMenu();
            LoadOrders();
            LoadReviews();
        }

        private void LoadMenu()
        {
            // Load the menu items based on the selected restaurant
            MenuListView.ItemsSource = _restaurant.Menu;
        }

        private void LoadOrders()
        {
            // Load the orders for the selected restaurant
            OrderListView.ItemsSource = _restaurant.Orders;
        }

        private void LoadReviews()
        {
            // Load the reviews for the selected restaurant
            ReviewListView.ItemsSource = _restaurant.Reviews;
        }

        private void PlaceOrderButton_Click(object sender, RoutedEventArgs e)
        {
            // Process the order
            // ...
        }

        private void SubmitReviewButton_Click(object sender, RoutedEventArgs e)
        {
            // Get the score and comment from the UI
            int score = (int)ScoreSlider.Value;
            string comment = CommentTextBox.Text;

            // Create a new review
            Review newReview = new Review(_restaurant, score, comment);

            // Add the review to the restaurant
            _restaurant.Reviews.Add(newReview);

            // Refresh the ReviewListView
            ReviewListView.Items.Refresh();
        }

        private void ReserveButton_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to the reservation page
            // ...
        }

        private void OrderButton_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to the order page
            // ...
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            // Navigate back to the search page
            this.Close();
        }
    }

}
