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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace UserManagementSystem
{
    /// <summary>
    /// Interaction logic for Food.xaml
    /// </summary>
    public partial class Food3 : Window
    {
        
        private Restaurant rs4;
        RestaurantMenuWindow rmw;
        public Food3( Restaurant rs5,RestaurantMenuWindow restaurantMenuWindow)
        {
            InitializeComponent();
            //od1= food;
            rs4 = rs5;
            rmw= restaurantMenuWindow;
        }
        public void AddBut(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(nameTextBox.Text))
            {
                MessageBox.Show("invalid input(s)");
                return;
            }
            if (string.IsNullOrEmpty(typeTextBox.Text))
            {
                MessageBox.Show("invalid input(s)");
                return;
            }
            if (string.IsNullOrEmpty(priceTextBox.Text))
            {
                MessageBox.Show("invalid input(s)");
                return;
            }
            if (string.IsNullOrEmpty(matTextBox.Text))
            {
                MessageBox.Show("invalid input(s)");
                return;
            }
            if (string.IsNullOrEmpty(invTextBox.Text))
            {
                MessageBox.Show("invalid input(s)");
                return;
            }
            string tptx=typeTextBox.Text.ToLower();
            if(tptx!="food"&&tptx!="drink" && tptx!="appetizer"&&tptx!="dessert")
            {
                MessageBox.Show("invalid type");
                return;
            }
            float _price;
            int _invt;
            try
            {
                _price = float.Parse(priceTextBox.Text);
                _invt = int.Parse(invTextBox.Text);
            }
            catch
            {
                MessageBox.Show("invalid input(s)");
                return;
            }


            var nfd = new Food(nameTextBox.Text, matTextBox.Text, typeTextBox.Text, _invt, _price);
            rs4.Menu.Add(nfd);
            rmw.refreshh();
            Close();

        }

    }

}
