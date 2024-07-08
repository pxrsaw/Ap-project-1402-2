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
    /// Interaction logic for Food.xaml
    /// </summary>
    public partial class Food2 : Window
    {
        private Food food1;
        public Food2(Food food)
        {
            food1 = food;
            InitializeComponent();
        }
    }
}
