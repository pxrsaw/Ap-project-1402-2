using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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
            List<Restaurant> filteredRestaurants = new List<Restaurant>(user.apdt.AllRestaurants1);
            if (!string.IsNullOrWhiteSpace(txtRestaurantName.Text))
            {
                filteredRestaurants = RegularUser.SearchByName(filteredRestaurants, txtRestaurantName.Text);
            }
            if (!string.IsNullOrWhiteSpace(txtrest.Text))
            {
                filteredRestaurants = RegularUser.SearchByAddress(filteredRestaurants, txtrest.Text);
            }
            //MessageBox.Show(cbCity.SelectedValue?.ToString());
            if (!string.IsNullOrEmpty(cbCity.SelectedValue?.ToString()))
            {
                //filteredRestaurants = RegularUser.SearchByCity(filteredRestaurants,cbCity.SelectedValue?.ToString());
                //MessageBox.Show(cbCity.sele);
                if (cbCity.SelectedIndex == 1)
                {
                    filteredRestaurants = RegularUser.SearchByCity(filteredRestaurants, "tehran");

                }
                if (cbCity.SelectedIndex == 2)
                {
                    filteredRestaurants = RegularUser.SearchByCity(filteredRestaurants, "shiraz");
                }
                if (cbCity.SelectedIndex == 3)
                {
                    filteredRestaurants = RegularUser.SearchByCity(filteredRestaurants, "mashhad");
                }
                if (cbCity.SelectedIndex == 4)
                {
                    filteredRestaurants = RegularUser.SearchByCity(filteredRestaurants, "esfehan");
                }
            }
            if (!string.IsNullOrEmpty(cbAcceptType.SelectedValue?.ToString()))
            {
                //MessageBox.Show(cbAcceptType.SelectedValue.ToString());
                if (cbAcceptType.SelectedIndex == 0)
                {
                    filteredRestaurants = RegularUser.SearchByDelivery(filteredRestaurants);
                    //MessageBox.Show("hi");
                }
                if (cbAcceptType.SelectedIndex == 1)
                {
                    filteredRestaurants = RegularUser.SearchByDineIn(filteredRestaurants);
                }
                if (cbAcceptType.SelectedIndex == 3)
                {
                    filteredRestaurants = RegularUser.SearchByDineIn(filteredRestaurants);
                    filteredRestaurants = RegularUser.SearchByDelivery(filteredRestaurants);
                }
            }
            if(isCom==true)
            {
                filteredRestaurants=RegularUser.SearchByHavingComplaint(filteredRestaurants);
            }
            filteredRestaurants = RegularUser.SearchByScore(sliderMinScore.Value, filteredRestaurants);
            //MessageBox.Show(Restaurant.AllRestaurants[0].restaurantScore.ToString());
            lvRestaurants.ItemsSource = filteredRestaurants;
        }
        private void RegisterRestaurantButton_Click(object sender, RoutedEventArgs e)
        {
            restaurant.Visibility = Visibility.Visible;
            SearchRes.Visibility = Visibility.Collapsed;
            SearchReports.Visibility = Visibility.Collapsed;
            Unreviewed.Visibility = Visibility.Collapsed;
            AllReports.Visibility = Visibility.Collapsed;
            //Respond.Visibility = Visibility.Collapsed;

        }
        private void lvRestaurants_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //lvOrders.SelectedItem = regularUser.AllUserOrders;
            //lvRestaurants.SelectedItem=Restaurant.AllRestaurants;
        }
        private void lvRestaurants_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            
        }
        private void lvComplaints(object sender, MouseButtonEventArgs e)
        {
            FeedBack fb4 = (FeedBack)UnreviewedFeedbacksGrid.SelectedItem;
            var ap2=new AnswerPage(this, fb4);
            ap2.Show();
        }
        public void Refreshh()
        {
            var unreviewedFeedbacks =user.apdt.AllFeedBacks1.Where(x => x.isAnswered == false);

            UnreviewedFeedbacksGrid.ItemsSource= unreviewedFeedbacks;
        }
        private void NavigateToRestaurantPage(Restaurant restaurant)
        {
            // Create a new instance of the Restaurant page and pass the selected restaurant
            //UserManagementSystem.RestaurantPage restaurantPage = new UserManagementSystem.RestaurantPage(restaurant, regularUser);
            ////MessageBox.Show("hi");
            //restaurantPage.Show();
            //RestaurantPage.OrderDic.Clear();
        }
        private void SearchRestaurantButton_Click(object sender, RoutedEventArgs e)
        {
            restaurant.Visibility = Visibility.Collapsed;
            SearchReports.Visibility = Visibility.Collapsed;
            SearchRes.Visibility = Visibility.Visible;
            Unreviewed.Visibility= Visibility.Collapsed;
            AllReports.Visibility = Visibility.Collapsed;
            //Respond.Visibility = Visibility.Collapsed;
        }
        private void LogOut_Click(object sender, RoutedEventArgs e)
        {
            var MainWindow = new MainWindow();
            MainWindow.Show();
            Close();
        }
        private void UnreviewedReportsButton_Click(object sender, RoutedEventArgs e)
        {
            //var unreviewedFeedbacks = adm.AllFeedBacks.Where(f => !f.isAnswered).OrderByDescending(f => f.Feedbackuser.AllUserOrders.LastOrDefault()?.orderDate ?? DateTime.MinValue);
            var unreviewedFeedbacks =user.apdt.AllFeedBacks1.Where(x=>x.isAnswered==false);
            UnreviewedFeedbacksGrid.ItemsSource = unreviewedFeedbacks;
            Unreviewed.Visibility = Visibility.Visible;
            restaurant.Visibility= Visibility.Collapsed;
            SearchRes.Visibility= Visibility.Collapsed;
            SearchReports.Visibility= Visibility.Collapsed;
            AllReports.Visibility = Visibility.Collapsed;
            //Respond.Visibility = Visibility.Collapsed;
        }
        private void SearchComplaintsButton_Click(object sender, RoutedEventArgs e)
        {
            SearchReports.Visibility = Visibility.Visible;
            restaurant.Visibility = Visibility.Collapsed;
            SearchRes.Visibility = Visibility.Collapsed;
            AllReports.Visibility = Visibility.Collapsed;
            //Respond.Visibility = Visibility.Collapsed;
            Unreviewed.Visibility = Visibility.Collapsed;
        }
        private void RespondToComplaintsButton_Click(object sender, RoutedEventArgs e)
        {
            var unreviewedComplaints = user.apdt.AllFeedBacks1.Where(f => !f.isAnswered);
            //UnreviewedComplaintsGrid.ItemsSource = unreviewedComplaints;
            SearchReports.Visibility = Visibility.Collapsed;
            restaurant.Visibility = Visibility.Collapsed;
            SearchRes.Visibility = Visibility.Collapsed;
            AllReports.Visibility = Visibility.Collapsed;
            //Respond.Visibility = Visibility.Visible;
            Unreviewed.Visibility = Visibility.Collapsed;
        }
        private void ViewAllComplaintsButton_Click(object sender, RoutedEventArgs e)
        {
            AllComplaintsGrid.ItemsSource = user.apdt.AllFeedBacks1;
            SearchReports.Visibility = Visibility.Collapsed;
            restaurant.Visibility = Visibility.Collapsed;
            SearchRes.Visibility = Visibility.Collapsed;
            AllReports.Visibility = Visibility.Visible;
           // Respond.Visibility = Visibility.Collapsed;
            Unreviewed.Visibility = Visibility.Collapsed;
        }
        public bool isDeli;
        public bool isdin;
        public bool isCom=false;
        public void DelCheck(object sender, RoutedEventArgs e)
        {
            isDeli = true;
        }
        public void ComCheck(object sender, RoutedEventArgs e)
        {
            isCom = true;
        }
        public void ComNotCheck(object sender, RoutedEventArgs e)
        {
            isCom=false;
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
        //private void SearchByCity_Click(object sender, RoutedEventArgs e)
        //{
        //    string city = CitySearchTextBox.Text;
        //    var restaurantsInCity = Restaurant.AllRestaurants.Where(r => r.city == city);
        //    SearchResultsGrid.ItemsSource = restaurantsInCity;
        //    SearchResultsGrid.Visibility = Visibility.Visible;
        //}

        //private void SearchByName_Click(object sender, RoutedEventArgs e)
        //{
        //    string name = NameSearchTextBox.Text;
        //    var restaurantsByName = Restaurant.AllRestaurants.Where(r => r.Name.Contains(name));
        //    SearchResultsGrid.ItemsSource = restaurantsByName;
        //    SearchResultsGrid.Visibility = Visibility.Visible;
        //}

        //private void SearchByRating_Click(object sender, RoutedEventArgs e)
        //{
        //    float minRating=0;
        //    try
        //    {
        //        minRating = float.Parse(MinRatingTextBox.Text);
        //    }
        //    catch
        //    {
        //        MessageBox.Show("invalid input");
        //        return;
        //    }
        //    var restaurantsWithMinScore = Restaurant.AllRestaurants.Where(r => r.restaurantScore >= minRating);
        //    SearchResultsGrid.ItemsSource = restaurantsWithMinScore;
        //    SearchResultsGrid.Visibility = Visibility.Visible;
        //}
        //private void SearchUnreviewedFeedbacks_Click(object sender, RoutedEventArgs e)
        //{
        //    var usersWithUnreviewedFeedbacks = RegularUser.AllRegularUsers.Where(u => u.feedBacks.Any(f => !f.isAnswered));
        //    SearchResultsGrid.ItemsSource = usersWithUnreviewedFeedbacks;
        //    SearchResultsGrid.Visibility = Visibility.Visible;
        //}

        //private void SearchReviewedFeedbacks_Click(object sender, RoutedEventArgs e)
        //{
        //    var usersWithReviewedFeedbacks = RegularUser.AllRegularUsers.Where(u => u.feedBacks.Any(f => f.isAnswered));
        //    SearchResultsGrid.ItemsSource = usersWithReviewedFeedbacks;
        //    SearchResultsGrid.Visibility = Visibility.Visible;
        //}
        private void SearchComplaints_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameSearchTextBox.Text;
            string complaintTitle = ComplaintTitleSearchTextBox.Text;
            string complainantName = ComplainantNameSearchTextBox.Text;
            string restaurantName = RestaurantNameSearchTextBox.Text;
            int reviewedStatus = ReviewedStatusComboBox.SelectedIndex;

            //var complaints = adm.AllFeedBacks.Where(c =>
            //    (string.IsNullOrEmpty(username) || c.Feedbackuser.username.Contains(username)) &&
            //    (string.IsNullOrEmpty(complaintTitle) || c.title.Contains(complaintTitle)) &&
            //    (string.IsNullOrEmpty(complainantName) || (c.Feedbackuser.firstName + " " + c.Feedbackuser.lastName).Contains(complainantName)) &&
            //    (string.IsNullOrEmpty(restaurantName) || c.restaurant.Name.Contains(restaurantName)) &&
            //    (reviewedStatus == 0 || (reviewedStatus == 1 && c.isAnswered) || (reviewedStatus == 2 && !c.isAnswered))
            //);
            var complaints = new List<FeedBack>(user.apdt.AllFeedBacks1);
            if(!string.IsNullOrEmpty(UsernameSearchTextBox.Text))
            {
                foreach(var item in complaints.ToList())
                {
                    if(item.Feedbackuser.username.Contains(username)==false)
                    {
                        complaints.Remove(item);
                    }
                }
            }
            if (!string.IsNullOrEmpty(ComplaintTitleSearchTextBox.Text))
            {
                foreach (var item in complaints.ToList())
                {
                    if (item.title.Contains(complaintTitle) == false)
                    {
                        complaints.Remove(item);
                    }
                }
            }
            if (!string.IsNullOrEmpty(ComplainantNameSearchTextBox.Text))
            {
                foreach (var item in complaints.ToList())
                {
                    if (item.user_Name.Contains(complainantName) == false)
                    {
                        complaints.Remove(item);
                    }
                }
            }
            if (!string.IsNullOrEmpty(RestaurantNameSearchTextBox.Text))
            {
                foreach (var item in complaints.ToList())
                {
                    if (item.restaurant.Name.Contains(restaurantName) == false)
                    {
                        complaints.Remove(item);
                    }
                }
            }
            if(!string.IsNullOrEmpty(ReviewedStatusComboBox.Text))
            {
                //MessageBox.Show(ReviewedStatusComboBox.Text);
                if(ReviewedStatusComboBox.Text== "Reviewed")
                {
                    complaints=complaints.Where(x=>x.isAnswered==true).ToList();
                }
                if(ReviewedStatusComboBox.Text== "Unreviewed")
                {
                    complaints = complaints.Where(x => x.isAnswered == false).ToList();
                }
            }
            
            //SearchResultsGrid.ItemsSource = complaints;
            //SearchResultsGrid.Visibility = Visibility.Visible;
            uun.ItemsSource = complaints;
            uun.Items.Refresh();
            //MessageBox.Show(complaints.ToList().Count.ToString());


        }

        //private void SubmitResponse_Click(object sender, RoutedEventArgs e)
        //{
        //    FeedBack selectedComplaint = (FeedBack)UnreviewedComplaintsGrid.SelectedItem;
        //    if (selectedComplaint != null)
        //    {
        //        string response = ResponseTextBox.Text;
        //        selectedComplaint.answerFeedback(response);
        //        UnreviewedComplaintsGrid.Items.Refresh();
        //        ResponseTextBox.Clear();
        //        MessageBox.Show("Response submitted successfully!");
        //    }
        //    else
        //    {
        //        MessageBox.Show("Please select a complaint to respond to.");
        //    }
        //}
    }
}