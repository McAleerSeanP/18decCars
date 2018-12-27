using DBLibrary;
using System.Text.RegularExpressions;
using System.Windows;



//verify that email address format is correct
public static class ValidatorExtensions
{
    public static bool IsValidEmailAddress(this string s)
    {
        Regex regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
        return regex.IsMatch(s);
    }
}


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

        /// <summary>
        /// Checks that the chosen username does not already exist
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TbxRegisterUserName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (tbxRegisterUserName.Text == "")
            {
                lblLoginErrorMessage.Content = "A username is required.";
            }
            //Setup strings to use in db check
            string currentUserName = tbxRegisterUserName.Text;
            foreach (var user in db.Users)
                if (user.UserName == currentUserName)
                {
                    lblLoginErrorMessage.Content = "Sorry that user name exists.";
                }

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

        private void TbxRegisterForename_LostFocus(object sender, RoutedEventArgs e)
        {
            lblLoginErrorMessage.Content = "";
            if (tbxRegisterForename.Text.Length == 0 || tbxRegisterForename.Text.Length > 30)
            {
                lblLoginErrorMessage.Content = "First name cannot be blank and must be less than 30 characters.";
            }
        }

        private void TbxRegisterSurname_LostFocus(object sender, RoutedEventArgs e)
        {
            lblLoginErrorMessage.Content = "";
            if (tbxRegisterSurname.Text.Length == 0 || tbxRegisterSurname.Text.Length > 30)
            {
                lblLoginErrorMessage.Content = "Last name cannot be blank and must be less than 30 characters.";
            }
        }

        private void TbxRegisterEMail_LostFocus(object sender, RoutedEventArgs e)
        {
            lblLoginErrorMessage.Content = "";
            bool result = ValidatorExtensions.IsValidEmailAddress(tbxRegisterEMail.Text);
            //MessageBox.Show(result.ToString(), Title);
            if (result == false)
            {
                lblLoginErrorMessage.Content = "Please check your email and the format of your email.";
            }
        }

        private void TbxRegisterPhone_LostFocus(object sender, RoutedEventArgs e)
        {
            lblLoginErrorMessage.Content = "";
            if (tbxRegisterPhone.Text.Length == 0 || tbxRegisterPhone.Text.Length > 20)
            {
                lblLoginErrorMessage.Content = "Phone Number cannot be blank and must be less than 20 characters.";
            }
        }

        private void TbxRegisterCarReg_LostFocus(object sender, RoutedEventArgs e)
        {
            lblLoginErrorMessage.Content = "";
            if (tbxRegisterCarReg.Text.Length == 0 || tbxRegisterCarReg.Text.Length > 20)
            {
                lblLoginErrorMessage.Content = "Number plate cannot be blank and must be less than 20 characters.";
            }
        }

        private void TbxRegisterPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            lblLoginErrorMessage.Content = "";
            if (tbxRegisterPassword.Password.Length == 0 || tbxRegisterPassword.Password.Length > 30)
            {
                lblLoginErrorMessage.Content = "Password cannot be blank and must be less than 30 characters.";
            }
        }

        private void TbxRegisterPasswordRpt_LostFocus(object sender, RoutedEventArgs e)
        {
            lblLoginErrorMessage.Content = "";
            if (tbxRegisterPassword.Password.Length == 0 || tbxRegisterPasswordRpt.Password != tbxRegisterPasswordRpt.Password)
            {
                lblLoginErrorMessage.Content = "Password cannot be blank and must be the same as the password field above it.";
            }
        }
    }
}

