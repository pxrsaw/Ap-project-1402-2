using System.Collections.ObjectModel;
using System.Security.Cryptography;
using System.Windows;

namespace UserManagementSystem;

public partial class FoodComment : Window
{
    public ObservableCollection<string> Comments { get; set; }
    //public ObservableCollection<string> ReplyComments { get; set; }
    RegularUser ru8;
    Food food;
    public FoodComment(Food food1, RegularUser ru7)
    {
        food=food1;
        InitializeComponent();
        ru8 = ru7;
        // Initialize the collection
        List<string> comments2 = new List<string>();
        //List<string> comments3 = new List<string>();
        foreach (var it2 in food.Comments)
        {
            comments2.Add(it2.cm);
            foreach(var it3 in it2.answerComments)
            {
                comments2.Add("        "+it3);

            }
        }
        Comments = new ObservableCollection<string>(comments2);

        CommentsListBox.ItemsSource = Comments;
        //this.customer = customer;
    }

    //private void RepBut(object sender, System.EventArgs e)
    //{
    //    string newComment2 = NewCommentTextBox.Text;
    //    string newComment = $"             {ru8.username}: {newComment2}";
    //    if (!string.IsNullOrWhiteSpace(newComment))
    //    {
    //        Comments.Add(newComment);
    //        var cm12 = new Comment(newComment, ru8, food, null);
    //        foreach (var it3 in ru8.ScoredFoods)
    //        {
    //            if (it3.Key == food)
    //            {
    //                cm12.userScore = it3.Value;
    //                break;
    //            }
    //        }

    //        NewCommentTextBox.Text = string.Empty;
    //    }
    //    else
    //    {
    //        MessageBox.Show("Please enter a comment before adding.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
    //    }
    //}
    private void AddCommentButton_Click(object sender, RoutedEventArgs e)
    {
        
        string newComment2 = NewCommentTextBox.Text;
        string newComment = $"{ru8.username}: {newComment2}";
        if (!string.IsNullOrWhiteSpace(newComment))
        {
            Comments.Add(newComment);
            var cm12=new Comment(newComment,ru8,food,null);
            foreach(var it3 in ru8.ScoredFoods)
            {
                if(it3.Key==food)
                {
                    cm12.userScore = it3.Value;
                    break;
                }
            }
            CommentsListBox.ItemsSource = Comments;
            NewCommentTextBox.Text = string.Empty;
        }
        else
        {
            MessageBox.Show("Please enter a comment before adding.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
    
}