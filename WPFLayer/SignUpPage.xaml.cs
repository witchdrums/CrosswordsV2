using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Text.RegularExpressions;
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
using Validation;

namespace WPFLayer
{
    /* by witchdrums
     * TO DO
        [   ] Turn into PAGE
        [   ] Connect this with chavi's login window
     */
    public partial class SignUpPage : Window, ServicesImplementation.IUsersManagerCallback
    {
        public SignUpPage()
        {
            InitializeComponent();
        }
        private void TextBox_Email_TextChanged(object sender, TextChangedEventArgs e)
        {
            int lengthInTextBox = this.TextBox_Email.Text.Length;
            this.Label_EmailCharacterCount.Content = lengthInTextBox;
            this.Label_EmailInvalid.Visibility = Visibility.Hidden;
            this.TextBox_Email.BorderBrush = System.Windows.Media.Brushes.Gray;
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            this.Label_PasswordInvalid.Visibility = Visibility.Hidden;
            this.Label_PasswordsDontMatch.Visibility = Visibility.Hidden;
            this.PasswordBox_Password.BorderBrush = System.Windows.Media.Brushes.Gray;
            this.PasswordBox_PasswordConfirmation.BorderBrush = System.Windows.Media.Brushes.Gray;
        }

        private void Button_SignUp_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateEmail()
                && ValidatePassword()
                && ValidateMatchingPasswords())
            {
                InstanceContext context = new InstanceContext(this);

                ServicesImplementation.UsersManagerClient client = new ServicesImplementation.UsersManagerClient(context);

                ServicesImplementation.Users newUser = new ServicesImplementation.Users();
                newUser.email = this.TextBox_Email.Text;
                newUser.password = this.PasswordBox_Password.Password;
                newUser.username = this.TextBox_Email.Text;
                newUser.idUserType = 1;


                client.AddUser(newUser);
                MessageBox.Show("User has been added");
                clearFields();
            }

        }

        private void clearFields()
        {
            this.TextBox_Email.Text = "";
            this.PasswordBox_Password.Password = "";
            this.PasswordBox_PasswordConfirmation.Password = "";
        }
        
        private bool ValidateEmail()
        {
            
            string email = this.TextBox_Email.Text.ToString();
            IUsersValidationService usersValidationService = new UsersValidationService();
            bool emailFormatIsValid = usersValidationService.ValidateEmailFormat(email);
            if (emailFormatIsValid == false)
            {
                this.TextBox_Email.BorderBrush = System.Windows.Media.Brushes.Red;
                this.Label_EmailInvalid.Visibility = Visibility.Visible;
            }
            return emailFormatIsValid;
        }

        private bool ValidatePassword()
        {
            
            string password = this.PasswordBox_Password.Password.ToString();
            IUsersValidationService usersValidationService = new UsersValidationService();
            bool passwordFormatIsValid = usersValidationService.ValidatePasswordFormat(password);
            if (passwordFormatIsValid == false)
            {
                this.PasswordBox_Password.BorderBrush = System.Windows.Media.Brushes.Red;
                this.Label_PasswordInvalid.Visibility = Visibility.Visible;
            }
            return passwordFormatIsValid;
        }

        private bool ValidateMatchingPasswords()
        {
            string password = this.PasswordBox_Password.Password.ToString();
            string passwordConfirmation = this.PasswordBox_PasswordConfirmation.Password.ToString();
            IUsersValidationService usersValidationService = new UsersValidationService();
            bool passwordsMAtch = usersValidationService.ValidateMatchingPasswords(password, passwordConfirmation);
            if (passwordsMAtch == false)
            {
                this.PasswordBox_Password.BorderBrush = System.Windows.Media.Brushes.Red;
                this.PasswordBox_PasswordConfirmation.BorderBrush = System.Windows.Media.Brushes.Red;
                this.Label_PasswordsDontMatch.Visibility = Visibility.Visible;
            }
            return passwordsMAtch;
        }

        public void Response(string response)
        {
            MessageBox.Show(response);
        }
    }
}
