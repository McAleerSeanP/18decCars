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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ElectricUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        electricEntities db = new electricEntities("metadata=res://*/ElectricModel.csdl|res://*/ElectricModel.ssdl|res://*/ElectricModel.msl;provider=System.Data.SqlClient;provider connection string='data source=smcaleer-hp-355;initial catalog=electric;persist security info=True;user id=electric;password=electricPassw0rd!;MultipleActiveResultSets=True;App=EntityFramework'" );
   
        public MainWindow()
        {
            InitializeComponent();
        
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string currentUser = tbxLoginUserName.Text;
            string currentPassword = tbxLoginPassword.Password;
            foreach (var user in db.Users)
            {
                if (user.UserName == currentUser && user.Password == currentPassword)
                {
                    Dashboard dashboard = new Dashboard();
                    dashboard.user = user;
                    App.Current.MainWindow = dashboard;
                    dashboard.ShowDialog();
                    this.Hide();
                }
                else
                {
                    lblLoginErrorMessage.Content = "Please check your username and password.";
                }
                
            }
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            Register RegisterWindow = new Register();
            RegisterWindow.Show();
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            //Shutdown the application
            Application.Current.Shutdown();
        }
    }
}
