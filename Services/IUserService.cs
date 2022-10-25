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

    [ServiceContract(CallbackContract = typeof(IUserServiceCallback))]
    public interface IUserService
    {
        [OperationContract]
        bool AddUser(Users user);

        [OperationContract]
        void FindUserByEmail(String userEmail);
    }

    [ServiceContract]
    public interface IUserServiceCallback
    {
        [OperationContract]
        void Response(bool response);
    }

}
