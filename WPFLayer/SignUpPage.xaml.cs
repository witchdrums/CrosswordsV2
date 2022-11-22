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
using Security;
using Email;

namespace WPFLayer
{

    public partial class SignUpPage : Page, ServicesImplementation.IUsersManagerCallback
    {
        public SignUpPage()
        {
            InitializeComponent();
            HideErrorLabels();
        }

        private void HideErrorLabels()
        { 
            this.Label_EmailInvalid.Visibility = Visibility.Hidden;
            this.Label_EmailAlreadyInDatabase.Visibility = Visibility.Hidden;
            this.Label_PasswordInvalid.Visibility = Visibility.Hidden;
            this.Label_PasswordsDontMatch.Visibility = Visibility.Hidden;
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
            try
            {
                if (ValidateEmailFormat()
                    && ValidateEmailIsNew()
                    && ValidatePasswordFormat()
                    && ValidateMatchingPasswords())
                {
                    RegisterUser();
                }
            }
            catch (EndpointNotFoundException)
            {
                
                MessageBox.Show(Properties.Resources.Exception_ServerFailure);
            }


        }

        private void RegisterUser()
        {
            InstanceContext context = new InstanceContext(this);

            ServicesImplementation.UsersManagerClient client = new ServicesImplementation.UsersManagerClient(context);

            ServicesImplementation.Users newUser = new ServicesImplementation.Users();
            newUser.email = this.TextBox_Email.Text;
            IEncryptionService encryptionService = new EncryptionService();
            newUser.password = encryptionService.StringToSHA512(this.PasswordBox_Password.Password);
            newUser.username = this.TextBox_Email.Text;

            if (client.AddUser(newUser))
            {
                WelcomeNewUserViaEmail(newUser.email);
                MessageBox.Show(Properties.Resources.Message_SignUpSuccess);
            }

            clearFields();
        }

        private void WelcomeNewUserViaEmail(String userEmail)
        {
            IEmailService emailService = new EmailService();
            emailService.SendEmailToUser
            (
                userEmail,
                Properties.Resources.Email_SignUpWelcome_Subject,
                Properties.Resources.Email_SignUpWelcome_Header,
                Properties.Resources.Email_SignUpWelcome_Body
            );
        }

        private void clearFields()
        {
            this.TextBox_Email.Text = "";
            this.PasswordBox_Password.Password = "";
            this.PasswordBox_PasswordConfirmation.Password = "";
        }

        private bool ValidateEmailIsNew()
        {
            
            InstanceContext context = new InstanceContext(this);
            ServicesImplementation.UsersManagerClient client = new ServicesImplementation.UsersManagerClient(context);
            string emailInput = this.TextBox_Email.Text;

            bool emailIsNew = client.FindUserByEmail(emailInput);
            if (!emailIsNew)
            {
                this.TextBox_Email.BorderBrush = System.Windows.Media.Brushes.Red;
                this.Label_EmailAlreadyInDatabase.Visibility = Visibility.Visible;
            }
            return emailIsNew;
        }
        
        private bool ValidateEmailFormat()
        {
            
            string email = this.TextBox_Email.Text.ToString();
            IUsersValidationService usersValidationService = new UsersValidationService();
            bool emailFormatIsValid = usersValidationService.ValidateEmailFormat(email);
            if (!emailFormatIsValid)
            {
                this.TextBox_Email.BorderBrush = System.Windows.Media.Brushes.Red;
                this.Label_EmailInvalid.Visibility = Visibility.Visible;
            }
            return emailFormatIsValid;
        }

        private bool ValidatePasswordFormat()
        {
            
            string password = this.PasswordBox_Password.Password.ToString();
            IUsersValidationService usersValidationService = new UsersValidationService();
            bool passwordFormatIsValid = usersValidationService.ValidatePasswordFormat(password);
            if (!passwordFormatIsValid)
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
            if (!passwordsMAtch)
            {
                this.PasswordBox_Password.BorderBrush = System.Windows.Media.Brushes.Red;
                this.PasswordBox_PasswordConfirmation.BorderBrush = System.Windows.Media.Brushes.Red;
                this.Label_PasswordsDontMatch.Visibility = Visibility.Visible;
            }
            return passwordsMAtch;
        }

        public void Response(bool response1)
        {
            if (response1)
            {
                MessageBox.Show("User has been added!");
            }
            
        }

        private void Button_Return_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }
    }
}
