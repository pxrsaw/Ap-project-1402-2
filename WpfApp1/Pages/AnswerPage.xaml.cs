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
using System.Windows.Shapes;

namespace UserManagementSystem
{
    /// <summary>
    /// Interaction logic for AnswerPage.xaml
    /// </summary>
    public partial class AnswerPage : Window
    {
        public AdminPanelWindow apw;
        FeedBack feedback;

        public AnswerPage(AdminPanelWindow apw2,FeedBack fb5)
        {
            InitializeComponent();
            this.apw = apw2;
            feedback = fb5;
        }
        public void SubBu(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrWhiteSpace(AnswerBox.Text))
            {
                feedback.answerFeedback(AnswerBox.Text);
                apw.Refreshh();
                Close();
            }
            else
            {
                MessageBox.Show("invalid entry");
            }
        }
    }
}
