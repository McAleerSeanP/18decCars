﻿using DBLibrary;
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
    /// Interaction logic for Admin.xaml
    /// </summary>
    public partial class Admin : Page
    {
        electricEntities db = new electricEntities("metadata=res://*/ElectricModel.csdl|res://*/ElectricModel.ssdl|res://*/ElectricModel.msl;provider=System.Data.SqlClient;provider connection string='data source=smcaleer-hp-355;initial catalog=electric;persist security info=True;user id=electric;password=electricPassw0rd!;MultipleActiveResultSets=True;App=EntityFramework'");
        List<User> allUsersList = new List<User>();

        private void RefreshList()
        {
            allUsersList.Clear();
            foreach (var user in db.Users)
            {
                allUsersList.Add(user);
            }
            lstAllUsers.ItemsSource = allUsersList;
            lstAllUsers.Items.Refresh();
        }

        public Admin()
        {
            InitializeComponent();
        }

        

        private void UserControl_loaded(object sender, RoutedEventArgs e)
        {
            RefreshList();
        }
    }
}
