using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using System.ServiceModel;

namespace Services
{
    public class ConnectionMap
    {
        private readonly Dictionary <int, OperationContext> usersMap = new Dictionary <int, OperationContext> ();

        public ConnectionMap() { }

        public ConnectionMap getInstance()
        {
        return this;
        }
        public void SaveUser(int idUser, OperationContext operationContext)
        {
            Console.WriteLine(idUser);
            usersMap.Add(idUser, operationContext);
        }
        //To do : Define Override <3
       /* public override OperationContext GetOperationContextForId(Users user)
        {
            return usersMap[user.idUser];
        }*/
        public OperationContext GetOperationContextForId(int idUser)
        {
            return usersMap[idUser];
        }

        public void DeteleUserForId(int userId)
        {
            usersMap.Remove(userId);
        }

        public Dictionary<int,OperationContext> GetUsersMap()
        {
            return this.usersMap;

        }
    }
}
