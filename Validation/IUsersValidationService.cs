using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validation
{
    public interface IUsersValidationService
    {
        bool ValidateEmailFormat(String email);
        bool ValidateEmailIsNew(String email);
        bool ValidatePasswordFormat(String password);
        bool ValidateMatchingPasswords(String password, String passwordConfirmation);
        bool ValidateUserNameIsNotEmpty(String userName);
        bool ValidatePasswordIsNotEmpty(String password);
    }
}
