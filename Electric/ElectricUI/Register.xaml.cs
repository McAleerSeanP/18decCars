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
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        electricEntities db = new electricEntities("metadata=res://*/ElectricModel.csdl|res://*/ElectricModel.ssdl|res://*/ElectricModel.msl;provider=System.Data.SqlClient;provider connection string='data source=smcaleer-hp-355;initial catalog=electric;persist security info=True;user id=electric;password=electricPassw0rd!;MultipleActiveResultSets=True;App=EntityFramework'");

        public Register()
        {
            InitializeComponent();
        }

        private void btxRegisterCancel_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        //Validate Thta test boxes are not empty
        private void TbxRegisterUserName_Leave(object sender, System.EventArgs e)
        {
            //validate username
            lblLoginErrorMessage.Content = "A User Name Is Reguired";
        }


        private void BtnRegisterRegister_Click(object sender, RoutedEventArgs e)
        {
            // Click Register
            User user = new User();
            // UserId is Automatically generated
            user.UserName = tbxRegisterUserName.Text.Trim();
            user.Forename = tbxRegisterForename.Text.Trim();
            user.Surname = tbxRegisterSurname.Text.Trim();
            user.Phone = tbxRegisterPhone.Text.Trim();
            user.EMail = tbxRegisterEMail.Text.Trim();
            user.CarReg = tbxRegisterCarReg.Text.Trim();
            user.Password = tbxRegisterPassword.Password.Trim();
            user.AccessLevel = 1;
            user.Active = false;
            int saveSuccess = SaveUser(user);
            if (saveSuccess == 1)
            {
                MessageBox.Show("An admin will contact you.", "Registration Succesful", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Problem With Registration.", "Registration Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        public int SaveUser(User user)
        {
            db.Entry(user).State = System.Data.Entity.EntityState.Added;
            int saveSuccess = db.SaveChanges();
            return saveSuccess;
        }


    }
}
