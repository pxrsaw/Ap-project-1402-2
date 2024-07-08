using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;

namespace UserManagementSystem
{
    public partial class AdminPanelWindow : Window
    {
        adm admin;
        public AdminPanelWindow(adm admin)
        {
            InitializeComponent();
            this.admin = admin;
        }

        private void RegisterRestaurantButton_Click(object sender, RoutedEventArgs e)
        {
            restaurant.Visibility = Visibility.Visible;
            SearchRes.Visibility = Visibility.Collapsed;
            SearchReports.Visibility = Visibility.Collapsed;
            Unreviewed.Visibility = Visibility.Collapsed;
            AllReports.Visibility = Visibility.Collapsed;
            Respond.Visibility = Visibility.Collapsed;

        }

        private void SearchRestaurantButton_Click(object sender, RoutedEventArgs e)
        {
            restaurant.Visibility = Visibility.Collapsed;
            SearchReports.Visibility = Visibility.Collapsed;
            SearchRes.Visibility = Visibility.Visible;
            Unreviewed.Visibility= Visibility.Collapsed;
            AllReports.Visibility = Visibility.Collapsed;
            Respond.Visibility = Visibility.Collapsed;
        }
        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            var MainWindow = new MainWindow();
            MainWindow.Show();
            Close();
        }
        private void UnreviewedReportsButton_Click(object sender, RoutedEventArgs e)
        {
            var unreviewedFeedbacks = adm.AllFeedBacks.Where(f => !f.isAnswered).OrderByDescending(f => f.Feedbackuser.AllUserOrders.LastOrDefault()?.orderDate ?? DateTime.MinValue);
            UnreviewedFeedbacksGrid.ItemsSource = unreviewedFeedbacks;
            Unreviewed.Visibility = Visibility.Visible;
            restaurant.Visibility= Visibility.Collapsed;
            SearchRes.Visibility= Visibility.Collapsed;
            SearchReports.Visibility= Visibility.Collapsed;
            AllReports.Visibility = Visibility.Collapsed;
            Respond.Visibility = Visibility.Collapsed;
        }
        private void SearchComplaintsButton_Click(object sender, RoutedEventArgs e)
        {
            SearchReports.Visibility = Visibility.Visible;
            restaurant.Visibility = Visibility.Collapsed;
            SearchRes.Visibility = Visibility.Collapsed;
            AllReports.Visibility = Visibility.Collapsed;
            Respond.Visibility = Visibility.Collapsed;
            Unreviewed.Visibility = Visibility.Collapsed;
        }
        private void RespondToComplaintsButton_Click(object sender, RoutedEventArgs e)
        {
            var unreviewedComplaints = adm.AllFeedBacks.Where(f => !f.isAnswered);
            UnreviewedComplaintsGrid.ItemsSource = unreviewedComplaints;
            SearchReports.Visibility = Visibility.Collapsed;
            restaurant.Visibility = Visibility.Collapsed;
            SearchRes.Visibility = Visibility.Collapsed;
            AllReports.Visibility = Visibility.Collapsed;
            Respond.Visibility = Visibility.Visible;
            Unreviewed.Visibility = Visibility.Collapsed;
        }
        private void ViewAllComplaintsButton_Click(object sender, RoutedEventArgs e)
        {
            AllComplaintsGrid.ItemsSource = adm.AllFeedBacks;
            SearchReports.Visibility = Visibility.Collapsed;
            restaurant.Visibility = Visibility.Collapsed;
            SearchRes.Visibility = Visibility.Collapsed;
            AllReports.Visibility = Visibility.Visible;
            Respond.Visibility = Visibility.Collapsed;
            Unreviewed.Visibility = Visibility.Collapsed;
        }
        public bool isDeli;
        public bool isdin;
        public void DelCheck(object sender, RoutedEventArgs e)
        {
            isDeli = true;
        }
        public void DelNotCheck(object sender, RoutedEventArgs e)
        {
            isDeli = false;
        }
        public void DinCheck(object sender, RoutedEventArgs e)
        {
            isdin = true;
        }
        public void DinNotCheck(object sender, RoutedEventArgs e)
        {
            isdin=false;
        }

        private void AddRes_Click(object sender, RoutedEventArgs e)
        {
            string UserName = Username.Text;
            string Nameee=Namee.Text;
            string city = City.Text;
            string addresss=Address.Text;
            string charss = "QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm0123456789";
            StringBuilder sb = new StringBuilder(10);
            Random r2= new Random();
            for(int i = 0; i < 10; i++)
            {
                sb.Append(charss[r2.Next(charss.Length)]);
            }

            string Password = sb.ToString();
            if(signup.CheckUsername1(UserName)==false)
            {
                MessageBox.Show("username already exists");
                return;
            }
            if(signup.CheckName(Nameee)&&signup.Checkusername2(UserName))
            {
                new Restaurant(Nameee, UserName, Password, "", "", city, isdin, isDeli, addresss);
                MessageBox.Show($"username: {UserName}\npassword:{Password}");
            }
            else
            {
                MessageBox.Show("invalid inputs");
            }
            //Adding new Restaurant.
        }
        private void SearchByCity_Click(object sender, RoutedEventArgs e)
        {
            string city = CitySearchTextBox.Text;
            var restaurantsInCity = Restaurant.AllRestaurants.Where(r => r.city == city);
            SearchResultsGrid.ItemsSource = restaurantsInCity;
            SearchResultsGrid.Visibility = Visibility.Visible;
        }

        private void SearchByName_Click(object sender, RoutedEventArgs e)
        {
            string name = NameSearchTextBox.Text;
            var restaurantsByName = Restaurant.AllRestaurants.Where(r => r.Name.Contains(name));
            SearchResultsGrid.ItemsSource = restaurantsByName;
            SearchResultsGrid.Visibility = Visibility.Visible;
        }

        private void SearchByRating_Click(object sender, RoutedEventArgs e)
        {
            float minRating=0;
            try
            {
                minRating = float.Parse(MinRatingTextBox.Text);
            }
            catch
            {
                MessageBox.Show("invalid input");
                return;
            }
            var restaurantsWithMinScore = Restaurant.AllRestaurants.Where(r => r.restaurantScore >= minRating);
            SearchResultsGrid.ItemsSource = restaurantsWithMinScore;
            SearchResultsGrid.Visibility = Visibility.Visible;
        }
        private void SearchUnreviewedFeedbacks_Click(object sender, RoutedEventArgs e)
        {
            var usersWithUnreviewedFeedbacks = RegularUser.AllRegularUsers.Where(u => u.feedBacks.Any(f => !f.isAnswered));
            SearchResultsGrid.ItemsSource = usersWithUnreviewedFeedbacks;
            SearchResultsGrid.Visibility = Visibility.Visible;
        }

        private void SearchReviewedFeedbacks_Click(object sender, RoutedEventArgs e)
        {
            var usersWithReviewedFeedbacks = RegularUser.AllRegularUsers.Where(u => u.feedBacks.Any(f => f.isAnswered));
            SearchResultsGrid.ItemsSource = usersWithReviewedFeedbacks;
            SearchResultsGrid.Visibility = Visibility.Visible;
        }
        private void SearchComplaints_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameSearchTextBox.Text;
            string complaintTitle = ComplaintTitleSearchTextBox.Text;
            string complainantName = ComplainantNameSearchTextBox.Text;
            string restaurantName = RestaurantNameSearchTextBox.Text;
            string reviewedStatus = ((ComboBoxItem)ReviewedStatusComboBox.SelectedItem).Content.ToString();

            var complaints = adm.AllFeedBacks.Where(c =>
                (string.IsNullOrEmpty(username) || c.Feedbackuser.username.Contains(username)) &&
                (string.IsNullOrEmpty(complaintTitle) || c.title.Contains(complaintTitle)) &&
                (string.IsNullOrEmpty(complainantName) || (c.Feedbackuser.firstName + " " + c.Feedbackuser.lastName).Contains(complainantName)) && 
                (string.IsNullOrEmpty(restaurantName) || c.restaurant.Name.Contains(restaurantName)) &&
                (reviewedStatus == "All" || (reviewedStatus == "Reviewed" && c.isAnswered) || (reviewedStatus == "Unreviewed" && !c.isAnswered))
            );

            SearchResultsGrid.ItemsSource = complaints;
            SearchResultsGrid.Visibility = Visibility.Visible;
        }
        
        private void SubmitResponse_Click(object sender, RoutedEventArgs e)
        {
            FeedBack selectedComplaint = (FeedBack)UnreviewedComplaintsGrid.SelectedItem;
            if (selectedComplaint != null)
            {
                string response = ResponseTextBox.Text;
                selectedComplaint.answerFeedback(response);
                UnreviewedComplaintsGrid.Items.Refresh();
                ResponseTextBox.Clear();
                MessageBox.Show("Response submitted successfully!");
            }
            else
            {
                MessageBox.Show("Please select a complaint to respond to.");
            }
        }
    }
}