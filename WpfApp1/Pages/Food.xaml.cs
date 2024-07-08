﻿using System;
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
            if(numbb>food1.Inventory)
            {
                MessageBox.Show("unavailable amount!");
                return;
            }
            food1.Inventory -= numbb;
            var rs = new RestaurantPage(rs4,numbb,food1,ru12);
            Close();

        }
    }
    
}
