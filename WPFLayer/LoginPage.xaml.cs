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

namespace WPFLayer
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
            Label_UsernameInvalid.Visibility = Visibility.Hidden;
            Label_PasswordInvalid.Visibility = Visibility.Hidden;
        }

        private void buttonLogin_Click(object sender, RoutedEventArgs e)
        {
            checkFieldsUserNameAndPasswordFilled();

        }


        private void checkFieldsUserNameAndPasswordFilled()
        {
            Label_UsernameInvalid.Visibility = Visibility.Hidden;
            Label_PasswordInvalid.Visibility = Visibility.Hidden;
            if (String.IsNullOrEmpty(TextBox_Username.Text))
            {
                Label_UsernameInvalid.Visibility = Visibility.Visible;
            }

            if (String.IsNullOrEmpty(PasswordBox_Password.Password))
            {
                Label_PasswordInvalid.Visibility = Visibility.Visible;
            }
        }

        private void Button_SignUp_Click_(object sender, RoutedEventArgs e)
        {
            SignUpPage signUpPage = new SignUpPage();
            this.NavigationService.Navigate(signUpPage);
            //this.Close();
        }
    }
}
