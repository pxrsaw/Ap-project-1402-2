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
        List<Food> AllFoods = new List<Food>();
        List<Food> AllDrinks = new List<Food>();
        List<Food> AllAppetizers = new List<Food>();
        List<Food> AllDesserts = new List<Food>();
        public static Dictionary<int,Food> OrderDic= new Dictionary<int,Food>();
        public RestaurantPage(Restaurant restaurantt)
        {
            restaurant = restaurantt;
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
        public RestaurantPage(Restaurant restaurantt,int numbb,Food fdd)
        {
            restaurant = restaurantt;
            OrderDic.Add(numbb, fdd);
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

            UserManagementSystem.Food2 Selectedfood = new UserManagementSystem.Food2(selectedFood,restaurant);

            Selectedfood.Show();
            Close();
        }
        private void ComplaintsListView3_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Food selectedFood = (Food)ComplaintsListView.SelectedItem;
            //int i=AllFoods.Count;
            //MessageBox.Show("hi");
            Food selectedFood = AllDrinks[ComplaintsListView3.SelectedIndex];

            UserManagementSystem.Food2 Selectedfood = new UserManagementSystem.Food2(selectedFood, restaurant);

            Selectedfood.Show();
            Close();
        }
        private void ComplaintsListView4_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Food selectedFood = (Food)ComplaintsListView.SelectedItem;
            //int i=AllFoods.Count;
            Food selectedFood = AllAppetizers[ComplaintsListView4.SelectedIndex];

            UserManagementSystem.Food2 Selectedfood = new UserManagementSystem.Food2(selectedFood, restaurant);

            Selectedfood.Show();
            Close();
        }
        private void ComplaintsListView5_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Food selectedFood = (Food)ComplaintsListView.SelectedItem;
            //int i=AllFoods.Count;
            Food selectedFood = AllDesserts[ComplaintsListView5.SelectedIndex];

            UserManagementSystem.Food2 Selectedfood = new UserManagementSystem.Food2(selectedFood, restaurant);

            Selectedfood.Show();
            Close();
        }

    }
    
}
