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
    /// Interaction logic for Admin.xaml
    /// </summary>
    public partial class Admin : Page
    {
        electricEntities db = new electricEntities("metadata=res://*/ElectricModel.csdl|res://*/ElectricModel.ssdl|res://*/ElectricModel.msl;provider=System.Data.SqlClient;provider connection string='data source=smcaleer-hp-355;initial catalog=electric;persist security info=True;user id=electric;password=electricPassw0rd!;MultipleActiveResultSets=True;App=EntityFramework'");
        List<User> allUsersList = new List<User>();
        User selectedUser = new User();

        enum DBOperation
        {
            Add,
            Modify,
            Delete
        }

        DBOperation dbOperation = new DBOperation();

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

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshUserList();
        }

        private void UserControl_loaded(object sender, RoutedEventArgs e)
        {
            RefreshList();
        }

        private void SubmenuAddNewUser_Click(object sender, RoutedEventArgs e)
        {
            stkUserPanel.Visibility = Visibility.Visible;
            ClearUserDetails();
            dbOperation = DBOperation.Add;
        }

        private void SubmenuModifySelectedUser_Click(object sender, RoutedEventArgs e)
        {
            stkUserPanel.Visibility = Visibility.Visible;
            dbOperation = DBOperation.Modify;
        }

        private void SubmenuDeleteSelectedUser_Click(object sender, RoutedEventArgs e)
        {
            db.Users.RemoveRange(db.Users.Where(t => t.UserId == selectedUser.UserId));
            int saveSuccess = db.SaveChanges();
            if (saveSuccess == 1)
            {
                MessageBox.Show("User deleted.", "Delete From Database", MessageBoxButton.OK, MessageBoxImage.Information);
                //RefreshUserList();
                RefreshList();
                ClearUserDetails();
                stkUserPanel.Visibility = Visibility.Collapsed;
            }
            else
            {
                MessageBox.Show("Problem Deleting Record.", "Delete From Database", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void BtnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (dbOperation == DBOperation.Add)
            {
                //stkUserPanel.Visibility = Visibility.Collapsed;
                User user = new User();
                user.UserName = tbxUserName.Text.Trim();
                user.Forename = tbxForeName.Text.Trim();
                user.Surname = tbxSurname.Text.Trim();
                user.Phone = tbxPhone.Text.Trim();
                user.EMail = tbxEMail.Text.Trim();
                user.CarReg = tbxCarReg.Text.Trim();
                user.Password = tbxPassword.Text.Trim();
                user.AccessLevel = cboAccessLevel.SelectedIndex;
                // Bool user.Active = cboActive.SelectedIndex;
                user.Active = true;
                int saveSuccess = SaveUser(user);
                if (saveSuccess == 1)
                {
                    MessageBox.Show("User saved.", "Save To Database", MessageBoxButton.OK, MessageBoxImage.Information);
                    //RefreshUserList();
                    RefreshList();
                    ClearUserDetails();
                }
                else
                {
                    MessageBox.Show("Problem Saving Record.", "Save To Database", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }

            if (dbOperation == DBOperation.Modify)
            {
                foreach (var user in db.Users.Where(t => t.UserId == selectedUser.UserId))
                {
                    user.UserName = tbxUserName.Text.Trim();
                    user.Forename = tbxForeName.Text.Trim();
                    user.Surname = tbxSurname.Text.Trim();
                    user.Phone = tbxPhone.Text.Trim();
                    user.EMail = tbxEMail.Text.Trim();
                    user.CarReg = tbxCarReg.Text.Trim();
                    user.Password = tbxPassword.Text.Trim();
                    user.AccessLevel = cboAccessLevel.SelectedIndex;
                    user.Active = true;
                }
                int saveSuccess = db.SaveChanges();
                if (saveSuccess == 1)
                {
                    MessageBox.Show("User modified.", "Save To Database", MessageBoxButton.OK, MessageBoxImage.Information);
                    //RefreshUserList();
                    RefreshList();
                    ClearUserDetails();
                    stkUserPanel.Visibility = Visibility.Collapsed;
                }
                else
                {
                    MessageBox.Show("Problem Saving Record.", "Save To Database", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

            }

        
        }

        public int SaveUser(User user)
        {
            db.Entry(user).State = System.Data.Entity.EntityState.Added;
            int saveSuccess = db.SaveChanges();
            return saveSuccess;
        }

        private void RefreshUserList()
        {
            lstAllUsers.ItemsSource = allUsersList;
            allUsersList.Clear();
            foreach (var user in db.Users)
            {
               allUsersList.Add(user);
            }
        }

        private void ClearUserDetails()
        {
            tbxUserName.Text = "";
            tbxForeName.Text = "";
            tbxSurname.Text = "";
            tbxPhone.Text = "";
            tbxEMail.Text = "";
            tbxCarReg.Text = "";
            tbxPassword.Text = "";
            cboAccessLevel.SelectedIndex = 0;
            cboActive.SelectedIndex = 0;
        }

        private void LstAllUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstAllUsers.SelectedIndex > 0)
            {
                selectedUser = allUsersList.ElementAt(lstAllUsers.SelectedIndex);
          
                    submenuModifySelectedUser.IsEnabled = true;
                    tbxUserName.Text = selectedUser.UserName;
                    tbxForeName.Text = selectedUser.Forename;
                    tbxSurname.Text = selectedUser.Surname;
                    tbxPhone.Text = selectedUser.Phone;
                    tbxEMail.Text = selectedUser.EMail;
                    tbxCarReg.Text = selectedUser.CarReg;
                    tbxPassword.Text = selectedUser.Password;
                    cboAccessLevel.SelectedIndex = selectedUser.AccessLevel;
                    //Convert.ToInt32(selectedUser.Active);
                    cboActive.SelectedIndex = Convert.ToInt32(selectedUser.Active);
            
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            ClearUserDetails();
            stkUserPanel.Visibility = Visibility.Collapsed;
        }
    }
}
