using System;
using System.Collections.Generic;
using System.Linq;
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

namespace WPFLayer
{
    /* by witchdrums
     * TO DO
        [   ] Turn into PAGE
        [   ] Connect this with chavi's login window
     */
    public partial class SignUpPage : Window
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
            ValidateEmail();
            ValidatePassword();
            ValidateMatchingPasswords();
        }

        private void ValidateEmail()
        {
            Regex validEmailRegex = new Regex(
                "^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\." +
                "[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:" +
                "[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+" +
                "[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$"
            );
            string email = this.TextBox_Email.Text.ToString();
            if (validEmailRegex.IsMatch(email) == false)
            {
                this.TextBox_Email.BorderBrush = System.Windows.Media.Brushes.Red;
                this.Label_EmailInvalid.Visibility = Visibility.Visible;
            }
        }

        private void ValidatePassword()
        {
            Regex validPasswordRegex = new Regex("^.*(?=.{10,})" +
            "(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!*@#$%^&+=]).*$");
            string password = this.PasswordBox_Password.Password.ToString();
            if (validPasswordRegex.IsMatch(password) == false)
            {
                this.PasswordBox_Password.BorderBrush = System.Windows.Media.Brushes.Red;
                this.Label_PasswordInvalid.Visibility = Visibility.Visible;
            }
        }

        private void ValidateMatchingPasswords()
        {
            string password = this.PasswordBox_Password.Password.ToString();
            string passwordConfirmation = this.PasswordBox_PasswordConfirmation.Password.ToString();
            if (password.Equals(passwordConfirmation) == false)
            {
                this.PasswordBox_Password.BorderBrush = System.Windows.Media.Brushes.Red;
                this.PasswordBox_PasswordConfirmation.BorderBrush = System.Windows.Media.Brushes.Red;
                this.Label_PasswordsDontMatch.Visibility = Visibility.Visible;
            }
        }
    }
}
