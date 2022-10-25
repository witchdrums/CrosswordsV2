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
    public interface IUsersManager
    {
        [OperationContract]
        bool AddUser(Users user);

        [OperationContract]
        void FindUserByEmail(String userEmail);
    }

    [ServiceContract]
    public interface IUsersManagerCallback
    {
        [OperationContract]
        void Response(bool response);
    }

}
