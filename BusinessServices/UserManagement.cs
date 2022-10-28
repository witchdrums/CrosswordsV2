using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic;
using Domain;


namespace BusinessServices
{
    public class UserManagement
    {
        CrosswordsContext context = new CrosswordsContext();
        
        public Boolean RegisterUser(Users user){
            Boolean result = false;
            BusinessLogic.User businessLogicUser = new BusinessLogic.User();
            businessLogicUser.password = user.password;
            businessLogicUser.email = user.email;
            businessLogicUser.username = user.username;
            businessLogicUser.idUserType = 1;
            using(context)
            {
                context.Users.Add(businessLogicUser);
                result = context.SaveChanges() > 0;
            }
            return result;
        }

        public Boolean FindUserByEmail(String userEmail)
        {
            Boolean result = false;
            using (context)
            {
                var foundUsers = (from user in context.Users
                                 where user.email == userEmail
                                 select user).ToList();
                result = foundUsers.Count == 0;
                Console.WriteLine(foundUsers.Count);
            }
            return result;
        }
    }
}
