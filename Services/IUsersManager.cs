using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using Domain;

namespace Services
{ 

    [ServiceContract(CallbackContract = typeof(IUsersManagerCallback))]
    interface IUsersManager
    {
        [OperationContract]
        bool AddUser(Users user);

        [OperationContract]
        void FindUserByEmail(String userEmail);
    }

    [ServiceContract]
    interface IUsersManagerCallback
    {
        [OperationContract]
        void Response(String response);
    }

}
