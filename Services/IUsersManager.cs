using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using Domain;
using BusinessLogic;

namespace Services
{ 

    [ServiceContract(CallbackContract = typeof(IUsersManagerCallback))]
    public interface IUsersManager
    {
        [OperationContract]
        bool AddUser(Users user);

        [OperationContract]
        void FindUserByEmail(String userEmail);

        [OperationContract]
        Users FindUserByUserNameAndPassword(Users user);
    }

    [ServiceContract]
    public interface IUsersManagerCallback
    {
        [OperationContract]
        void Response(bool response);
    }

}
