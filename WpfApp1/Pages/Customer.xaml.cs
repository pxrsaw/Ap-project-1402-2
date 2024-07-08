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
            ComplaintsListView.ItemsSource = regularUser.feedBacks;
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
        private void RefreshComplaints()
        {
            ComplaintsListView.ItemsSource = regularUser.feedBacks;
            ComplaintsListView.Items.Refresh();
        }

        private void NewComplaintButton_Click(object sender, RoutedEventArgs e)
        {
            NewComplaintPanel.Visibility = Visibility.Visible;
            NewComplaintButton.Visibility = Visibility.Collapsed;
            RefreshButton.Visibility = Visibility.Collapsed;
            ComplaintsListView.Visibility = Visibility.Collapsed;
            Label.Visibility = Visibility.Collapsed;

            string[] Names = Restaurant.AllRestaurants.Select(r => r.Name).ToArray();
            RestaurantComboBox.ItemsSource = Names;
        }
        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            var MainWindow = new MainWindow();
            MainWindow.Show();
            Close();
        }
        private void SubmitComplaintButton_Click(object sender, RoutedEventArgs e)
        {
            Restaurant Res = null;
            foreach(var RES in Restaurant.AllRestaurants)
            {
                if (RES.Name == RestaurantComboBox.SelectedItem.ToString())
                {
                    Res = RES;
                }
            }
            Restaurant selectedRestaurant = Res;
            string title = TitleTextBox.Text;
            string description = DescriptionTextBox.Text;

            FeedBack newFeedBack = new FeedBack(Res, regularUser, title, description);
           // regularUser.feedBacks.Add(newFeedBack);

            RefreshComplaints();
            TitleTextBox.Clear();
            DescriptionTextBox.Clear();
            NewComplaintPanel.Visibility = Visibility.Collapsed;

            NewComplaintPanel.Visibility = Visibility.Collapsed;
            NewComplaintButton.Visibility = Visibility.Visible;
            RefreshButton.Visibility = Visibility.Visible;
            ComplaintsListView.Visibility = Visibility.Visible;
            Label.Visibility = Visibility.Visible;
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            RefreshComplaints();
        }

        private void lvOrders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lvOrders.SelectedItem = regularUser.AllUserOrders;
        }
    }
}