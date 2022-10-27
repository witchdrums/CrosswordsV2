using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic;
using System.Runtime.Remoting.Contexts;
using System.Data.Entity.Validation;
using BusinessServices;



namespace Services
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant)]

    public partial class ServicesImplementation : IUsersManager
    {
        
        CrosswordsContext _context = new CrosswordsContext();
        public bool AddUser(Users user)
        {
            BusinessServices.UserManagement userManagement = new BusinessServices.UserManagement();
            bool result = userManagement.RegisterUser(user);
            return result;
        }

        public void FindUserByEmail(string userMEmail)
        {
            //Console.WriteLine(userMEmail);
            OperationContext.Current.GetCallbackChannel<IUsersManagerCallback>().Response(true);
        }

        public Users FindUserByUserNameAndPassword(Users user)
        {
            BusinessLogic.User businessLogicUser = new BusinessLogic.User();
            businessLogicUser.username = user.username;
            businessLogicUser.password = user.password;
            if (_context.Users.Contains(businessLogicUser))
            {
                businessLogicUser = _context.Users.Find(businessLogicUser);
                user.username = businessLogicUser.username;
                user.credential = true;
            }
            else
            {
                user.credential = false;
            }

            return user;
        }

    }

}
