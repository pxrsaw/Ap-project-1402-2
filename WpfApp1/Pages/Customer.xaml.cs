using Microsoft.VisualBasic;
using System.Buffers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace UserManagementSystem
{
    public partial class Customer : Window
    {
        RegularUser regularUser;
        private List<FeedBack> _feedbacks;
        public Customer(RegularUser regularUser)
        {
            this.regularUser = regularUser;
            InitializeComponent();
        }

        private void ProfileButton_Click(object sender, RoutedEventArgs e)
        {
            Profile.Visibility = Visibility.Visible;
            Search.Visibility = Visibility.Collapsed;
            Order.Visibility = Visibility.Collapsed;
            Feedbacks.Visibility = Visibility.Collapsed;
        }

        private void SearchRestaurantButton_Click(object sender, RoutedEventArgs e)
        {
            Profile.Visibility = Visibility.Collapsed;
            Search.Visibility = Visibility.Visible;
            Order.Visibility = Visibility.Collapsed;
            Feedbacks.Visibility = Visibility.Collapsed;
        }

        private void OrderHistoryButton_Click(object sender, RoutedEventArgs e)
        {
            lvOrders.ItemsSource = regularUser.AllUserOrders;
            Profile.Visibility = Visibility.Collapsed;
            Search.Visibility = Visibility.Collapsed;
            Order.Visibility = Visibility.Visible;
            Feedbacks.Visibility = Visibility.Collapsed;
        }

        private void FeedbacksButton_Click(object sender, RoutedEventArgs e)
        {
            FeedbacksDataGrid.ItemsSource = regularUser.feedBacks;
            Profile.Visibility = Visibility.Collapsed;
            Search.Visibility = Visibility.Collapsed;
            Order.Visibility = Visibility.Collapsed;
            Feedbacks.Visibility = Visibility.Visible;
        }

        private void ShowDataButton_Click(object sender, RoutedEventArgs e)
        {
            //Bind the DataGrid to the information list
            //InformationDataGrid.ItemsSource = (System.Collections.IEnumerable)regularUser;
            //Make the DataGrid visible
            //InformationDataGrid.Visibility = Visibility.Visible;
            
        }

        private void EditDataButton_Click(object sender, RoutedEventArgs e)
        {
            EditDataGrid.Visibility = Visibility.Visible;
            regularUser.email = Email.Text;
            regularUser.postalCode = Postal.Text;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Load all restaurants initially
            lvRestaurants.ItemsSource = Restaurant.AllRestaurants;

            // Populate city combobox
            cbCity.ItemsSource = Restaurant.AllRestaurants.Select(r => r.city).Distinct();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Filter restaurants based on user input
            List<Restaurant> filteredRestaurants = Restaurant.AllRestaurants.Where(r =>
                (string.IsNullOrEmpty(txtRestaurantName.Text) || r.Name.Contains(txtRestaurantName.Text)) &&
                (string.IsNullOrEmpty(cbCity.SelectedValue?.ToString()) || r.city == cbCity.SelectedValue.ToString()) &&
                (string.IsNullOrEmpty(cbAcceptType.SelectedValue?.ToString()) || (cbAcceptType.SelectedValue.ToString() == "Delivery" && r.isDelivery) || (cbAcceptType.SelectedValue.ToString() == "Dine-in" && r.isDine_in)) //&&
                                                                                                                                                                                                                               //r.score >= sliderMinScore.Value
            ).ToList();

            // Update the ListView with filtered restaurants
            lvRestaurants.ItemsSource = filteredRestaurants;
        }
        private void NewFeedbackButton_Click(object sender, RoutedEventArgs e)
        {
            Restaurant res = null;
            foreach (Restaurant restaurant in Restaurant.AllRestaurants)
            {
                if (restaurant.Name.Equals(Name.Text))
                {
                    res = restaurant;
                    break;
                }
            }
            // generate a new feedback
            FeedBack newFeedbackWindow = new FeedBack(res,regularUser, Title.Text, Description.Text);
            //regularUser.feedBacks.Add(newFeedbackWindow);

            // Refresh the feedbacks data grid
            FeedbacksDataGrid.ItemsSource = regularUser.feedBacks;
        }

        private void FeedbacksDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FeedbacksDataGrid.SelectedItem = regularUser.feedBacks;
        }
        private void lvOrders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lvOrders.SelectedItem = regularUser.AllUserOrders;
        }
    }
}