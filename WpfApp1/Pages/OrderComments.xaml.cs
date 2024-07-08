using System.Collections.ObjectModel;
using System.Security.Cryptography;
using System.Windows;

namespace UserManagementSystem;

public partial class OrderComments : Window
{
    // ObservableCollection to hold the list of comments
    public ObservableCollection<string> Comments { get; set; }
    RegularUser ru8;
    Order ord2;
    Customer customer;
    public OrderComments(Order order,RegularUser ru7, Customer customerrr)
    {
        
        customer = customerrr;
        InitializeComponent();
        if (order.Score != null)
        {
            pomn.Visibility = Visibility.Collapsed;
            ScoreBox.Visibility = Visibility.Collapsed;
        }
        ru8 = ru7;
        ord2 = order;
        // Initialize the collection
        List<string> comments2 = new List<string>();
        foreach (var it2 in order.comments)
        {
            comments2.Add(it2.cm);
        }
        Comments = new ObservableCollection<string>(comments2);

        // Bind the collection to the ListBox
        CommentsListBox.ItemsSource = Comments;
        //this.customer = customer;
    }

    // Event handler for the "Add Comment" button click
    private void AddCommentButton_Click(object sender, RoutedEventArgs e)
    {
        // Get the new comment text
        string newComment = NewCommentTextBox.Text;

        // Check if the new comment is not empty or whitespace
        if (!string.IsNullOrWhiteSpace(newComment))
        {
            // Add the new comment to the collection
            Comments.Add(newComment);
            new Comment(newComment, ru8, ord2);

            // Clear the TextBox for the next comment
            NewCommentTextBox.Text = string.Empty;
        }
        else
        {
            // Optionally, you can show a message to the user if the comment is empty
            MessageBox.Show("Please enter a comment before adding.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
    private void AddScore(object sender, RoutedEventArgs e)
    {
        if (ord2.Score != null)
        {
            MessageBox.Show("already rated this order");
            return;
        }
        float f1;
        try
        {
            f1 = float.Parse(ScoreBox.Text);
        }
        catch
        {
            MessageBox.Show("invalid number");
            return;
        }
        if (f1 < 0 || f1 > 5)
        {
            MessageBox.Show("invalid number");
            return;
        }
        //ru12.ScoredFoods.Add(food1, f1);
        
            ord2.Score= f1;

        //ord2.res.UpdateScore(f1);
        ord2.res.UpdateScore(f1);
        //var muyt = new Customer(ru8);
        customer.Ref4();
        //new RestaurantPage(rs4, ru12);
        Close();
        MessageBox.Show("succesfully rated");
    }
}