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

namespace Services
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant)]

    public partial class ServicesImplementation : IUserService
    {

        CrosswordsContext _context = new CrosswordsContext();
        public bool AddUser(Domain.Users user)
        {
            
            BusinessLogic.User businesLogicUser = new BusinessLogic.User();
            businesLogicUser.password = user.password;
            businesLogicUser.email = user.email;
            businesLogicUser.username = user.username;
            businesLogicUser.idUserType = 1;

            bool result = false;
            using (var _context = new CrosswordsContext())
            {
                Console.WriteLine("before");
                _context.Users.Add(businesLogicUser);
                result = _context.SaveChanges() > 0;
            }
            return result;
        }

        public void FindUserByEmail(string userMEmail)
        {
            //Console.WriteLine(userMEmail);
            OperationContext.Current.GetCallbackChannel<IUserServiceCallback>().Response(true);
        }
    }
}
