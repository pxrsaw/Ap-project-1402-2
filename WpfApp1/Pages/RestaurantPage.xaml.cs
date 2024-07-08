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
        public RestaurantPage(Restaurant restaurantt)
        {
            restaurant = restaurantt;
            //MessageBox.Show(restaurant.Name);
            InitializeComponent();
            foreach(var item in restaurant.Menu)
            {
                var stackpanel = new StackPanel();
                stackpanel.Orientation = Orientation.Horizontal;
                //item.
            }
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
    }
}
