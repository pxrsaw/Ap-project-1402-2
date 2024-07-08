using System.Windows;
using System.Windows.Controls;

namespace UserManagementSystem
{
    public partial class RestaurantMenuWindow : Window
    {
        Restaurant restaurant;
        public RestaurantMenuWindow(Restaurant restaurant)
        {
            InitializeComponent();
            this.restaurant = restaurant;
        }

        private void ChangeMenuButton_Click(object sender, RoutedEventArgs e)
        {
            Order.Visibility = Visibility.Collapsed;
            Reservation.Visibility = Visibility.Collapsed;
            Available.Visibility = Visibility.Collapsed;
            Change.Visibility = Visibility.Visible;
        }

        private void ChangeAvailableFoodsButton_Click(object sender, RoutedEventArgs e)
        {
            Order.Visibility = Visibility.Collapsed;
            Reservation.Visibility = Visibility.Collapsed;
            Available.Visibility = Visibility.Visible;
            Change.Visibility = Visibility.Collapsed;
        }

        private void ActivateReservationButton_Click(object sender, RoutedEventArgs e)
        {
            Order.Visibility = Visibility.Collapsed;
            Reservation.Visibility = Visibility.Visible;
            Available.Visibility = Visibility.Collapsed;
            Change.Visibility = Visibility.Collapsed;
            if(restaurant.IsReservable==true)
            {
                MessageBox.Show("reservation is already activated");
            }
            else
            {
                if(restaurant.restaurantScore>=4.5)
                {
                    MessageBox.Show("Reservation service activated");
                    restaurant.IsReservable = true;
                }
                else
                {
                    MessageBox.Show("you don't have enough score for reservation");
                }
            }
        }

        private void OrderHistoryButton_Click(object sender, RoutedEventArgs e)
        {
            Order.Visibility = Visibility.Visible;
            Reservation.Visibility = Visibility.Collapsed;
            Available.Visibility = Visibility.Collapsed;
            Change.Visibility = Visibility.Collapsed;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            FoodDataGrid.ItemsSource = (System.Collections.IEnumerable)restaurant;//bejaye in restaurant list food roo bezar.
            FoodDataGrid.Visibility = Visibility.Visible;
            Food.Visibility = Visibility.Visible;
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            FoodDataGrid.ItemsSource = (System.Collections.IEnumerable)restaurant;//bejaye in restaurant list food roo bezar.
            FoodDataGrid.Visibility = Visibility.Visible;
        }

        private void ChangeInventory_Click(object sender, RoutedEventArgs e)
        {
            List<Food> Menuu = new List<Food>();
            foreach(var v1 in restaurant.Menu)
            {
                foreach(var v2 in v1.Value)
                {
                    Menuu.Add(v2);
                }
            }

            FoodGrid.ItemsSource = (System.Collections.IEnumerable)Menuu;//bejaye in restaurant list food roo bezar.
            FoodGrid.Visibility = Visibility.Visible;
            txtNumber.Visibility = Visibility.Visible;
        }
        private void txtNumber_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = !int.TryParse(e.Text, out int result);
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            DatePicker dp = null;

            if (btn.Tag.ToString() == "Day")
            {
                dp = btn.DataContext as DatePicker;
                dp.SelectedDateFormat = DatePickerFormat.Short;
            }
            else if (btn.Tag.ToString() == "Month")
            {
                dp = btn.DataContext as DatePicker;
                //dp.SelectedDateFormat = DatePickerFormat.//Medium;//check bokon bebin DatePickerFormat.Medium behet error mide ya na.
            }
            else if (btn.Tag.ToString() == "Year")
            {
                dp = btn.DataContext as DatePicker;
                dp.SelectedDateFormat = DatePickerFormat.Long;
            }
        }

        //private void Days_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        //{
        //    // Get the value entered in the TextBox
        //    //string inputText = Days.Text;

        //    // Convert the entered value to an integer
        //    if (int.TryParse(inputText, out int day))
        //    {
        //        // Check if the entered number is between 1 and 31
        //        if (day >= 1 && day <= 31)
        //        {
        //            // Perform the desired operation with the day value
        //            MessageBox.Show($"Day: {day}");
        //        }
        //        else
        //        {
        //            MessageBox.Show("Please enter a number between 1 and 31.");
        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show("Invalid input. Please enter a number.");
        //    }
        //}

        //private void Years_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        //{
        //    // Get the value entered in the TextBox
        //    string inputText = Years.Text;

        //    // Convert the entered value to an integer
        //    if (int.TryParse(inputText, out int number))
        //    {
        //        // Check if the length of the entered number is 4 digits
        //        if (inputText.Length == 4)
        //        {
        //            // Perform the desired operation with the number value
        //            MessageBox.Show($"Number: {number}");
        //        }
        //        else
        //        {
        //            MessageBox.Show("Please enter a 4-digit number.");
        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show("Invalid input. Please enter a number.");
        //    }
        //}

        //private void Years2_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        //{
        //    // Get the value entered in the TextBox
        //    string inputText = Years2.Text;

        //    // Convert the entered value to an integer
        //    if (int.TryParse(inputText, out int number))
        //    {
        //        // Check if the length of the entered number is 4 digits
        //        if (inputText.Length == 4)
        //        {
        //            // Perform the desired operation with the number value
        //            MessageBox.Show($"Number: {number}");
        //        }
        //        else
        //        {
        //            MessageBox.Show("Please enter a 4-digit number.");
        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show("Invalid input. Please enter a number.");
        //    }
        //}

        //private void Days2_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        //{
        //    // Get the value entered in the TextBox
        //    string inputText = Days2.Text;

        //    // Convert the entered value to an integer
        //    if (int.TryParse(inputText, out int day))
        //    {
        //        // Check if the entered number is between 1 and 31
        //        if (day >= 1 && day <= 31)
        //        {
        //            // Perform the desired operation with the day value
        //            MessageBox.Show($"Day: {day}");
        //        }
        //        else
        //        {
        //            MessageBox.Show("Please enter a number between 1 and 31.");
        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show("Invalid input. Please enter a number.");
        //    }
        //}
        private void SearchOrders_Click(object sender, RoutedEventArgs e)
        {
            OrdersListView.Visibility = Visibility.Visible;
            // Get the date values entered by the user
            int year1 = int.Parse(Date1Year.Text);
            int month1 = Date1Month.SelectedIndex + 1;
            int day1 = int.Parse(Date1Day.Text);
            int year2 = int.Parse(Year2.Text);
            int month2 = Date2Month.SelectedIndex + 1;
            int day2 = int.Parse(Date2Day.Text);

            // Create two dates from the entered values
            DateTime startDate = new DateTime(year1, month1, day1);
            DateTime endDate = new DateTime(year2, month2, day2);

            // Search for orders between the two dates
            var ordersInRange = restaurant.orders.Where(o => o.orderDate >= startDate && o.orderDate <= endDate);

            // Display the orders in the ListView on the right side of the page
            OrdersListView.ItemsSource = ordersInRange;
        }

    }
}
