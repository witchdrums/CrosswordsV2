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
        private bool currentEmailIsValid;
        private bool currentPasswordIsValid;
        private bool currentPasswordsMatch;
        public SignUpPage()
        {
            InitializeComponent();
            HideErrorLabels();
        }

        private void EnableButton()
        {
            this.Button_SignUp.IsEnabled =
                currentEmailIsValid && currentPasswordIsValid && currentPasswordsMatch;
        }

        private void HideErrorLabels()
        {
            Visibility visibility = GetVisibility(true);
            this.Label_EmailInvalid.Visibility = visibility;
            this.Label_PasswordInvalid.Visibility = visibility;
            this.Label_PasswordsDontMatch.Visibility = visibility;
        }

        private Visibility GetVisibility(bool match)
        {
            Visibility visibility = Visibility.Hidden;
            if (!match)
            {
                visibility = Visibility.Visible;
            }
            return visibility;
        }

        private void TextBox_Email_TextChanged(object sender, TextChangedEventArgs e)
        {
            String currentEmail = this.TextBox_Email.Text;
            currentEmailIsValid = UsersValidationService.ValidEmailRegex.IsMatch(currentEmail);
            this.Label_EmailInvalid.Visibility = GetVisibility(currentEmailIsValid);
            EnableButton();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs eventArguments)
        {
            String currentPassword = this.PasswordBox_Password.Password;
            String currentPasswordMatch = this.PasswordBox_PasswordConfirmation.Password;
            currentPasswordIsValid = UsersValidationService.ValidPasswordRegex.IsMatch(currentPassword);
            currentPasswordsMatch = currentPassword.Equals(currentPasswordMatch);
            this.Label_PasswordInvalid.Visibility = GetVisibility(currentPasswordIsValid);
            this.Label_PasswordsDontMatch.Visibility = GetVisibility(currentPasswordsMatch);
            EnableButton();

        }

        private void TextBox_PreviewExecuted(object sender, ExecutedRoutedEventArgs eventArguments)
        {
            if (eventArguments.Command == ApplicationCommands.Copy ||
                eventArguments.Command == ApplicationCommands.Cut ||
                eventArguments.Command == ApplicationCommands.Paste)
            {
                eventArguments.Handled = true;
            }
        }

        private void Button_SignUp_Click(object sender, RoutedEventArgs eventArguments)
        {
            try
            {
                ValidateEmailIsNew();
                RegisterUser();
            }
            catch (ArgumentException argumentException)
            {
                MessageBox.Show
                (
                    argumentException.Message,
                    "", 
                    MessageBoxButton.OK, 
                    MessageBoxImage.Warning
                );
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

            ClearFields();
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

        private void ClearFields()
        {
            this.TextBox_Email.Text = "";
            this.PasswordBox_Password.Password = "";
            this.PasswordBox_PasswordConfirmation.Password = "";
        }

        private void ValidateEmailIsNew()
        {

            InstanceContext context = new InstanceContext(this);
            ServicesImplementation.UsersManagerClient client = 
                new ServicesImplementation.UsersManagerClient(context);
            string emailInput = this.TextBox_Email.Text;

            bool emailIsNew = client.FindUserByEmail(emailInput);
            if (!emailIsNew)
            {
                throw new ArgumentException(Properties.Resources.General_Label_EmailAlreadyExists);
            }
        }


        public void Response(bool response1)
        {
            if (response1)
            {
                MessageBox.Show("User has been added!");
            }

        }

        private void Button_Return_Click(object sender, RoutedEventArgs eventArguments)
        {
            this.NavigationService.GoBack();
        }
    }
}
