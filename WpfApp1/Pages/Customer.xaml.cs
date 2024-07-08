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
        private List<FeedBack> _feedbacks { get; set; }
        public Customer(RegularUser regularUser)
        {
            this.regularUser = regularUser;
            InitializeComponent();
        }

        private void ProfileButton_Click(object sender, RoutedEventArgs e)
        {
            string[] Names = Restaurant.AllRestaurants.Select(r => r.Name).ToArray();
            ComboRes.ItemsSource = Names;
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
        { // Bind the ListView to the user information
            ProfileListView.ItemsSource = new List<object> { new { Email = regularUser.email, PostalCode = regularUser.postalCode,
                PhoneNumber = regularUser.phoneNumber } };
        }
        private void EditDataButton_Click(object sender, RoutedEventArgs e) 
        { 
            ComboRes.Visibility = Visibility.Collapsed;
            ResLab.Visibility = Visibility.Collapsed;
            LabSer.Visibility = Visibility.Collapsed;
            Gold.Visibility = Visibility.Collapsed;
            Silver.Visibility = Visibility.Collapsed;
            Bronze.Visibility = Visibility.Collapsed;
            Laabel.Visibility = Visibility.Collapsed;
            ProfileListView.Visibility = Visibility.Collapsed;
            EditDataButton.Visibility = Visibility.Collapsed;
            EditDataGrid.Visibility = Visibility.Visible;
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e) 
        { 
            regularUser.email = Email.Text; regularUser.postalCode = Postal.Text; 
            EditDataGrid.Visibility = Visibility.Collapsed;
            ComboRes.Visibility = Visibility.Visible;
            ResLab.Visibility = Visibility.Visible;
            LabSer.Visibility = Visibility.Visible;
            Gold.Visibility = Visibility.Visible;
            Silver.Visibility = Visibility.Visible;
            Bronze.Visibility = Visibility.Visible;
            Laabel.Visibility = Visibility.Visible;
            ProfileListView.Visibility = Visibility.Visible;
            EditDataButton.Visibility = Visibility.Visible;
            EditDataGrid.Visibility = Visibility.Collapsed;
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
            //List<Restaurant> filteredRestaurants = Restaurant.AllRestaurants.Where(r =>
            //    (string.IsNullOrEmpty(txtRestaurantName.Text) || r.Name.Contains(txtRestaurantName.Text)) &&
            //    (string.IsNullOrEmpty(cbCity.SelectedValue?.ToString()) || r.city == cbCity.SelectedValue.ToString()) &&
            //    (string.IsNullOrEmpty(cbAcceptType.SelectedValue?.ToString()) || (cbAcceptType.SelectedValue.ToString() == "Delivery" && r.isDelivery) || (cbAcceptType.SelectedValue.ToString() == "Dine-in" && r.isDine_in)) //&&
            //                                                                                                                                                                                                                   //r.score >= sliderMinScore.Value
            //).ToList();

            // Update the ListView with filtered restaurants
            List<Restaurant> filteredRestaurants=new List<Restaurant>(Restaurant.AllRestaurants);
            if(!string.IsNullOrWhiteSpace(txtRestaurantName.Text))
            {
                filteredRestaurants = RegularUser.SearchByName(filteredRestaurants, txtRestaurantName.Text);
            }
            //MessageBox.Show(cbCity.SelectedValue?.ToString());
            if (!string.IsNullOrEmpty(cbCity.SelectedValue?.ToString()))
            {
                //filteredRestaurants = RegularUser.SearchByCity(filteredRestaurants,cbCity.SelectedValue?.ToString());
                //MessageBox.Show(cbCity.sele);
                if(cbCity.SelectedIndex==1)
                {
                    filteredRestaurants = RegularUser.SearchByCity(filteredRestaurants,"tehran");

                }
                if(cbCity.SelectedIndex==2)
                {
                    filteredRestaurants = RegularUser.SearchByCity(filteredRestaurants, "shiraz");
                }
                if(cbCity.SelectedIndex==3) 
                {
                    filteredRestaurants = RegularUser.SearchByCity(filteredRestaurants, "mashhad");
                }
                if (cbCity.SelectedIndex == 4)
                {
                    filteredRestaurants = RegularUser.SearchByCity(filteredRestaurants, "esfehan");
                }
            }
            if(!string.IsNullOrEmpty(cbAcceptType.SelectedValue?.ToString()))
            {
                //MessageBox.Show(cbAcceptType.SelectedValue.ToString());
                if(cbAcceptType.SelectedIndex==0)
                {
                    filteredRestaurants = RegularUser.SearchByDelivery(filteredRestaurants);
                    //MessageBox.Show("hi");
                }
                if (cbAcceptType.SelectedIndex==1)
                {
                    filteredRestaurants = RegularUser.SearchByDineIn(filteredRestaurants);
                }
                if(cbAcceptType.SelectedIndex==3)
                {
                    filteredRestaurants = RegularUser.SearchByDineIn(filteredRestaurants);
                    filteredRestaurants = RegularUser.SearchByDelivery(filteredRestaurants);
                }
            }
            filteredRestaurants = RegularUser.SearchByScore(sliderMinScore.Value, filteredRestaurants);
            //MessageBox.Show(Restaurant.AllRestaurants[0].restaurantScore.ToString());
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

        private void Gold_Click(object sender, RoutedEventArgs e)
        {

        }
        private void lvRestaurants_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Get the selected restaurant from the ListView
            Restaurant selectedRestaurant = (Restaurant)lvRestaurants.SelectedItem;

            // Navigate to the Restaurant page and bind the selected restaurant
            NavigateToRestaurantPage(selectedRestaurant);
        }
        private void NavigateToRestaurantPage(Restaurant restaurant)
        {
            // Create a new instance of the Restaurant page and pass the selected restaurant
            UserManagementSystem.RestaurantPage restaurantPage = new UserManagementSystem.RestaurantPage(restaurant);
            //MessageBox.Show("hi");
            restaurantPage.Show();
            RestaurantPage.OrderDic = new Dictionary<int, Food>();
        }
        private void Silver_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Bronze_Click(object sender, RoutedEventArgs e)
        {

        }
        private void lvRestaurants_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lvOrders.SelectedItem = regularUser.AllUserOrders;
        }
    }
}