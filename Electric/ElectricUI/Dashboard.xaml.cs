using DBLibrary;
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

namespace ElectricUI
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Window
    {
 
        public User user = new User();
        public Dashboard()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Click Menu Items
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuReports_Click(object sender, RoutedEventArgs e)
        {

        }

        private void mnuAdmin_Click(object sender, RoutedEventArgs e)
        {

        }

        private void mnuStations_Click(object sender, RoutedEventArgs e)
        {

        }

        private void mnuExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Check user access level and determine menu visibility
        /// </summary>
        /// <param name="user"></param>
        private void CheckUserAccess(User user)
        {
            if (user.AccessLevel > 1)
            {
                mnuReportsMenu.Visibility = Visibility.Visible;
            }

            if (user.AccessLevel > 2)
            {
                mnuAdminMenu.Visibility = Visibility.Visible;
                mnuStationsMenu.Visibility = Visibility.Visible;
            }

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            CheckUserAccess(user);
        }

        private void mnuSearch_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
