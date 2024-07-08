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
    public partial class Food2 : Window
    {
        private Food food1;
        private Restaurant rs4;
        private RegularUser ru12;
        public Food2(Food food,Restaurant r1,RegularUser ru11)
        {
            InitializeComponent();
            qqa.Visibility = Visibility.Collapsed;
            
            qqa3.Visibility = Visibility.Collapsed;

            
            ru12 = ru11;
            food1 = food;
            rs4 = r1;
            nametxt.Text= $"Name: {food1.Name}";
            mattxt.Text = $"Materials: {food1.Materials}";
            typtxt.Text=$"Type: {food1.Type}";
            scotxt.Text = $"Score: {food1.Score.ToString()}";
            invtxt.Text = $"Avalability: {food1.Inventory.ToString()}";
            prctxt.Text=$"Price: {food1.Price}";
        }
        public void subbutn(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("hyy");
            int numbb=0;
            try
            {
                numbb = int.Parse(numbox.Text);
            }
            catch 
            {
                MessageBox.Show("enter number");
                return;
            }
            if(numbb==0)
            {
                new RestaurantPage(rs4,ru12);
                Close();
                return;
            }
            if(numbb>food1.Inventory)
            {
                MessageBox.Show("unavailable amount!");
                return;
            }
            food1.Inventory -= numbb;
            new RestaurantPage(rs4,numbb,food1,ru12);
            //RestaurantPage.OrderDic.Add(food1,numbb);
            Close();

        }
        public void rateBut(object sender, RoutedEventArgs e)
        {
            foreach(var item in ru12.ScoredFoods)
            {
                if(item.Key==food1)
                {
                    MessageBox.Show("you have already rated this food");
                    return;
                }
            }
            qqa.Visibility = Visibility.Visible;
            
            qqa3.Visibility = Visibility.Visible;

        }
        public void AddCm(object sender, RoutedEventArgs e)
        {
            var vfd=new FoodComment(food1,ru12);
            vfd.Show();
        }
        public void subRate(object sender, RoutedEventArgs e)
        {
            float f1;
            try
            {
                f1 = float.Parse(rtBox.Text);
            }
            catch
            {
                MessageBox.Show("invalid number");
                return;
            }
            if(f1<0||f1>5)
            {
                MessageBox.Show("invalid number");
                return;
            }
            ru12.ScoredFoods.Add(food1, f1);
            food1.UpdateScores(f1);
            rs4.UpdateScore(f1);
            new RestaurantPage(rs4,ru12);
            Close();
            MessageBox.Show("succesfully rated");
        }
    }
    
}
