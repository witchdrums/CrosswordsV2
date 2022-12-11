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

        public void SaveUser(int idUser, OperationContext operationContext)
        {
            if(usersMap.ContainsKey(idUser))
            {
                usersMap[idUser] = operationContext;
            }else
            {
                usersMap.Add(idUser, operationContext);
            } 
        }
        public OperationContext GetOperationContextForId(int idUser)
        {
            return usersMap[idUser];
        }

        public void DeteleUserForId(int userId)
        {
            usersMap.Remove(userId);
        }
    }
}
