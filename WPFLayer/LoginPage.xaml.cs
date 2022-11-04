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
using System.ServiceModel;
using Validation;
using Security;

namespace WPFLayer
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page, ServicesImplementation.IUsersManagerCallback
    {
        public LoginPage()
        {
            InitializeComponent();
            Label_UsernameInvalid.Visibility = Visibility.Hidden;
            Label_PasswordInvalid.Visibility = Visibility.Hidden;
        }

        private void buttonLogin_Click(object sender, RoutedEventArgs e)
        {
            bool validUserName = checkFieldUserName();
            bool validPassword = checkFieldPassword();
            if (validUserName && validPassword)
            {
                UserValidation();
            }

        }

        private bool checkFieldUserName()
        {
            Label_UsernameInvalid.Visibility = Visibility.Hidden;
            this.TextBox_Username.BorderBrush = System.Windows.Media.Brushes.Gray;
            String userName = this.TextBox_Username.Text;
            IUsersValidationService usersValidationService = new UsersValidationService();
            Boolean validUserName = usersValidationService.ValidateUserNameIsNotEmpty(userName);
            if (!validUserName)
            {
                this.TextBox_Username.BorderBrush = System.Windows.Media.Brushes.Red;
                this.Label_UsernameInvalid.Visibility = Visibility.Visible;
            }
            return validUserName;
        }

        private bool checkFieldPassword()
        {
            Label_PasswordInvalid.Visibility = Visibility.Hidden;
            this.PasswordBox_Password.BorderBrush = System.Windows.Media.Brushes.Gray;
            String password = this.PasswordBox_Password.Password;
            IUsersValidationService usersValidationService = new UsersValidationService();
            Boolean validPassword = usersValidationService.ValidatePasswordIsNotEmpty(password);
            if (!validPassword)
            {
                this.PasswordBox_Password.BorderBrush = System.Windows.Media.Brushes.Red;
                this.Label_PasswordInvalid.Visibility = Visibility.Visible;
            }
            return validPassword;
        }

        private void UserValidation()
        {
            InstanceContext context = new InstanceContext(this);
            ServicesImplementation.UsersManagerClient client = new ServicesImplementation.UsersManagerClient(context);
            ServicesImplementation.Players playerLogin = new ServicesImplementation.Players();
            ServicesImplementation.Users userLogin = new ServicesImplementation.Users();
            userLogin.username = this.TextBox_Username.Text;
            EncryptionService encriptador = new EncryptionService();
            //userLogin.credential = false;
            userLogin.password = encriptador.StringToSHA512(this.PasswordBox_Password.Password);
            playerLogin.user = userLogin;
            playerLogin = client.Login(playerLogin.user);
            if (playerLogin.user.credential)
            {
                //Singleton
                MessageBox.Show(playerLogin.user.email);
            }
            else
            {
                MessageBox.Show("Usuario no encontrado, verifique sus credenciales o registrese",
                                "Usuario no encontrado", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Button_SignUp_Click_(object sender, RoutedEventArgs e)
        {
            SignUpPage signUpPage = new SignUpPage();
            this.NavigationService.Navigate(signUpPage);
            //this.Close();
        }

        public void Response([MessageParameter(Name = "response")] bool response1)
        {
            throw new NotImplementedException();
        }
    }
}
