using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for signupsubmit.xaml
    /// </summary>
    public partial class signupsubmit : Window
    {
        public string userName;
        public string Namee;
        public string lastName;
        public string email;
        public string phoneNumber;
        string ActiveCode;

        public signupsubmit(string usn,string nm,string ln,string em,string pn,string avcode)
        {
            InitializeComponent();
            userName = usn;
            Namee = nm;
            lastName = ln;
            email = em;
            phoneNumber = pn;
            ActiveCode= avcode;
        }
        public bool CheckPassword(string password)
        {
            string pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,32}$";
            return Regex.IsMatch(password, pattern);
        }
        public void Button_Click(object sender, RoutedEventArgs e)
        {
            if (ActiveCode != CodeTextBox.Text) 
            {
                MessageBox.Show("invalid code");
                return;
            }
            if(!CheckPassword(Password_TextBox.Text))
            {
                MessageBox.Show("invalid password");
                return;
            }
            RegularUser ru7 = new RegularUser(Namee, lastName, userName, Password_TextBox.Text, email, phoneNumber);
            MessageBox.Show("signed up successfully");
            var window = new Customer(ru7);
            Close();
            window.Show();
        }
    }
}
