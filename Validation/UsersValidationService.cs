using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Validation
{
    public class UsersValidationService : IUsersValidationService
    {

        public bool ValidateEmailFormat(string email)
        {
            Regex validEmailRegex = new Regex(
                "^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\." +
                "[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:" +
                "[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+" +
                "[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$"
            );
            bool emailIsValid = validEmailRegex.IsMatch(email);
            return emailIsValid;
        }

        public bool ValidateEmailIsNew(string email)
        {
            throw new NotImplementedException();
        }

        public bool ValidateMatchingPasswords(string password, string passwordConfirmation)
        {
            bool passwordsMatch = password.Equals(passwordConfirmation);
            return passwordsMatch;
        }

        public bool ValidatePasswordFormat(string password)
        {
            Regex validPasswordRegex = new Regex("^.*(?=.{10,})" +
            "(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!*@#$%^&+=]).*$");
            bool passwordIsValid = validPasswordRegex.IsMatch(password);
            return passwordIsValid;
        }


        public bool ValidateUserNameIsNotEmpty(String userName)
        {
            bool validUserName = false;
            if (!String.IsNullOrWhiteSpace(userName))
            {
                validUserName = true;
            }
            return validUserName;
        }

        public bool ValidatePasswordIsNotEmpty(String password)
        {
            bool validPassword = false;
            if (!String.IsNullOrWhiteSpace(password))
            {
                validPassword = true;
            }
            return validPassword;
        }
    }
}
