using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UserManagementSystem;

namespace UserManagementSystem
{
    /// <summary>
    /// Interaction logic for RestaurantPage.xaml
    /// </summary>
    public partial class RestaurantPage : Window
    {
        private Restaurant restaurant;
        private RegularUser ru10;
        List<Food> AllFoods = new List<Food>();
        List<Food> AllDrinks = new List<Food>();
        List<Food> AllAppetizers = new List<Food>();
        List<Food> AllDesserts = new List<Food>();
        public bool isOnline = false;
        public static Dictionary<Food,int> OrderDic= new Dictionary<Food,int>();
       // public RegularUser ru8;
        public RestaurantPage(Restaurant restaurantt,RegularUser ru9)
        {
            restaurant = restaurantt;
            ru10 = ru9;
            //MessageBox.Show(restaurant.Name);
            InitializeComponent();

            foreach (var fd1 in restaurant.Menu)
            {
                if (fd1.Type == "Food")
                {
                    AllFoods.Add(fd1);
                }
                if (fd1.Type == "Drink")
                {
                    AllDrinks.Add(fd1);
                }
                if (fd1.Type == "Appetizer")
                {
                    AllAppetizers.Add(fd1);
                }
                if (fd1.Type == "Dessert")
                {
                    AllDesserts.Add(fd1);
                }
            }
            foreach (var item in restaurant.Menu)
            {
                var stackpanel = new StackPanel();
                stackpanel.Orientation = Orientation.Horizontal;
                //item.
            }
            string[] Foods = AllFoods.Select(r => r.Name).ToArray();
            ComplaintsListView.ItemsSource = AllFoods;
            ComplaintsListView3.ItemsSource = AllDrinks;
            ComplaintsListView4.ItemsSource = AllAppetizers;
            ComplaintsListView5.ItemsSource = AllDesserts;

        }
        public RestaurantPage(Restaurant restaurantt,int numbb,Food fdd, RegularUser ru9)
        {
            restaurant = restaurantt;
            ru10 = ru9;
            int ch1 = 0;
            foreach (var item in OrderDic)
            {
                if(item.Key==fdd)
                {
                    MessageBox.Show("already choosed this food");
                    fdd.Inventory += numbb;
                    ch1 = 1;
                    break;
                }
            }
            if(ch1 == 0)
            {
                OrderDic.Add(fdd, numbb);
            }
            //MessageBox.Show(restaurant.Name);
            InitializeComponent();
            
            foreach (var fd1 in restaurant.Menu)
            {
                if (fd1.Type == "Food")
                {
                    AllFoods.Add(fd1);
                }
                if (fd1.Type == "Drink")
                {
                    AllDrinks.Add(fd1);
                }
                if (fd1.Type == "Appetizer")
                {
                    AllAppetizers.Add(fd1);
                }
                if (fd1.Type == "Dessert")
                {
                    AllDesserts.Add(fd1);
                }
            }
            foreach (var item in restaurant.Menu)
            {
                var stackpanel = new StackPanel();
                stackpanel.Orientation = Orientation.Horizontal;
                //item.
            }
            string[] Foods = AllFoods.Select(r => r.Name).ToArray();
            ComplaintsListView.ItemsSource = AllFoods;
            ComplaintsListView3.ItemsSource = AllDrinks;
            ComplaintsListView4.ItemsSource = AllAppetizers;
            ComplaintsListView5.ItemsSource = AllDesserts;

        }

        private void Reserve_Click(object sender, RoutedEventArgs e)
        {
            Menu.Visibility = Visibility.Visible;
        }

        private void Order_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Score_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ComplaintsListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Food selectedFood = (Food)ComplaintsListView.SelectedItem;
            //int i=AllFoods.Count;
            //int i2=AllDrinks.Count;
            //int i3=AllAppetizers.Count;
            //int i4=AllDesserts.Count;
            Food selectedFood = AllFoods[ComplaintsListView.SelectedIndex];
            if (selectedFood.Inventory == 0)
            {
                MessageBox.Show("item not available");
                return;
            }
            UserManagementSystem.Food2 Selectedfood = new UserManagementSystem.Food2(selectedFood,restaurant,ru10);

            Selectedfood.Show();
            Close();
        }
        private void ComplaintsListView3_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Food selectedFood = (Food)ComplaintsListView.SelectedItem;
            //int i=AllFoods.Count;
            //MessageBox.Show("hi");
            Food selectedFood = AllDrinks[ComplaintsListView3.SelectedIndex];
            if (selectedFood.Inventory == 0)
            {
                MessageBox.Show("item not available");
                return;
            }
            UserManagementSystem.Food2 Selectedfood = new UserManagementSystem.Food2(selectedFood, restaurant,ru10);

            Selectedfood.Show();
            Close();
        }
        private void ComplaintsListView4_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Food selectedFood = (Food)ComplaintsListView.SelectedItem;
            //int i=AllFoods.Count;
            Food selectedFood = AllAppetizers[ComplaintsListView4.SelectedIndex];
            if (selectedFood.Inventory == 0)
            {
                MessageBox.Show("item not available");
                return;
            }
            UserManagementSystem.Food2 Selectedfood = new UserManagementSystem.Food2(selectedFood, restaurant, ru10);

            Selectedfood.Show();
            Close();
        }
        public void OnlCheck(object sender, RoutedEventArgs e)
        {
            isOnline = true;
        }
        private void ComplaintsListView5_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Food selectedFood = (Food)ComplaintsListView.SelectedItem;
            //int i=AllFoods.Count;
            Food selectedFood = AllDesserts[ComplaintsListView5.SelectedIndex];
            if(selectedFood.Inventory==0)
            {
                MessageBox.Show("item not available");
                return;
            }
            UserManagementSystem.Food2 Selectedfood = new UserManagementSystem.Food2(selectedFood, restaurant, ru10);

            Selectedfood.Show();
            Close();
        }
        private void AddToCart(object sender, RoutedEventArgs e)
        {
            string? sOrder=null;
            double pricee=0;
            foreach(var v2 in OrderDic)
            {
                sOrder += $"{v2.Key.Name}:   {v2.Value}\n";
                pricee += (v2.Value * (v2.Key.Price));

            }
            
            sOrder += $"Sum Price: {pricee}";
            MessageBox.Show(sOrder);
            if(isOnline)
            {
                try
                {
                    MailMessage mail = new MailMessage();
                    mail.From = new MailAddress("restaurant.managementpx@gmail.com");
                    mail.To.Add(ru10.email);
                    mail.Subject = "your order reciept";
                    sOrder += $"\nrestaurant: {restaurant.Name}";
                    mail.Body = sOrder;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.gmail.com";
                    smtp.UseDefaultCredentials = false;
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    smtp.Credentials = new NetworkCredential("restaurant.managementpx@gmail.com", "jrgf purp tskt zzwg");
                    smtp.Send(mail);
                }
                catch
                {
                    MessageBox.Show("could not send email");
                }
            }
            ru10.orderFood(OrderDic, restaurant, !isOnline);
            OrderDic.Clear();
            //var cst=new Customer()

        }

    }
    
}
