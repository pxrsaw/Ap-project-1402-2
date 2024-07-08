using System.Windows;

namespace UserManagementSystem
{
    public partial class CustomerMainWindow : Window
    {
        public CustomerMainWindow()
        {
            InitializeComponent();
        }

        private void ProfileButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayContent("Profile Page");
        }

        private void SearchRestaurantButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayContent("Searching Page");
        }

        private void OrderHistoryButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayContent("Order History Page");
        }

        private void FeedbacksButton_Click(object sender, RoutedEventArgs e)
        {
            DisplayContent("Feedbacks Page");
        }

        private void DisplayContent(string content)
        {
            ContentText.Text = content;
        }
    }
}