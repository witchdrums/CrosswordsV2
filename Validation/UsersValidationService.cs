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
        public static readonly Regex ValidEmailRegex = new Regex
        (
            "^[A-Za-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\." +
            "[A-Za-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:" +
            "[A-Za-z0-9](?:[A-Za-z0-9-]*[A-Za-z0-9])?\\.)+" +
            "[A-Za-z0-9](?:[A-Za-z0-9-]*[A-Za-z0-9])?$"
        );

        public static readonly Regex ValidPasswordRegex = new Regex
        (
            "^.*(?=.{10,})(?=.*\\d)(?=.*[a-z])" +
            "(?=.*[A-Z])(?=.*[!*@#$%^&+=]).*$"
        );

        public bool ValidateEmailFormat(string email)
        {
            bool emailIsValid = ValidEmailRegex.IsMatch(email);
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
            bool passwordIsValid = ValidPasswordRegex.IsMatch(password);
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
