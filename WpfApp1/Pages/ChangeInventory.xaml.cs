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
    /// Interaction logic for ChangeInventory.xaml
    /// </summary>
    public partial class ChangeInventory : Window
    {
        public Food food;
        public RestaurantMenuWindow menuWindow;
        public ChangeInventory(Food fd2,RestaurantMenuWindow ruw)
        {
            food = fd2;
            menuWindow = ruw;
            InitializeComponent();

        }
        public void SubButnnn(object sender, RoutedEventArgs e)
        {
            int i1;
            try
            {
                i1 = int.Parse(AmountBox.Text);
            }
            catch 
            {
                MessageBox.Show("invalid number");
                return;
            }
            if(i1 <0)
            {
                MessageBox.Show("invalid number");
                return;
            }
            food.Inventory = i1;
            menuWindow.refreshh();
            Close();
        }
    }
}
