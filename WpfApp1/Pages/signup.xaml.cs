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
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Net;
namespace UserManagementSystem
{
    /// <summary>
    /// Interaction logic for signup.xaml
    /// </summary>
    public partial class signup : Window
    {
        public signup()
        {
            InitializeComponent();
        }
        public static bool CheckName(string name)
        {
            string pattern = @"[a-zA-Z]{3,32}$";
            bool isValid=Regex.IsMatch(name,pattern);
            return isValid;
        }
        public bool CheckEmail(string email)
        {

            string pattern2 = @"^[^@\s]{3,32}@[^\s@]{3,32}\.[a-zA-Z]{2,3}";
            bool isValid= Regex.IsMatch(email, pattern2);
            return isValid;
        }
        public bool CheckPhoneNumber(string phoneNumber)
        {
            string pattern3 = @"^09\d{9}$";
            bool isValid = Regex.IsMatch(phoneNumber, pattern3);
            return isValid;
        }
        public bool CheckPhoneNumber2(string phoneNumber)
        {
            foreach (user ru6 in user.AllUsers)
            {
                if (ru6.phoneNumber == phoneNumber)
                {
                    return false;
                }
            }
            return true;
        }
        public static bool CheckUsername1(string usernamee)
        {
            foreach(user ru6 in user.AllUsers)
            {
                if(ru6.username==usernamee)
                {
                    return false;
                }
            }
            return true;
        }
        public static bool Checkusername2(string usernamee)
        {
            if(usernamee.Length<3)
            {
                return false;
            }
            return true;
        }
        public void Button_Click_11(object sender, RoutedEventArgs e)
        {
            string username_=UsernameTextBox.Text;
            string name_=NameBox.Text;
            string lastname_ = LastNameBox.Text;
            string email_=EmailBox.Text;
            string phonenumber_=PhoneBox.Text;
            if (CheckUsername1(username_) == false)
            {
                MessageBox.Show("username already exists");
                return;

            }
            if(CheckPhoneNumber2(phonenumber_)==false)
            {
                MessageBox.Show("phone number already exists");
                return;
            }
            if (CheckEmail(email_) && CheckName(name_) && CheckName(lastname_) && CheckPhoneNumber(phonenumber_) && Checkusername2(username_))
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("restaurant.managementpx@gmail.com");
                mail.To.Add(email_);
                mail.Subject = "verification code";
                Random r = new Random();
                int rn = r.Next(10000, 100000);
                mail.Body = rn.ToString();
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.UseDefaultCredentials = false;
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.Credentials = new NetworkCredential("restaurant.managementpx@gmail.com", "jrgf purp tskt zzwg");
                smtp.Send(mail);
                MessageBox.Show("email sent");
                var sust = new signupsubmit(username_, name_, lastname_, email_, phonenumber_, rn.ToString());
                sust.Show();
                Close();
            }
            else
            {
                MessageBox.Show("invalid inputs");
            }

        }
    }
    
}
