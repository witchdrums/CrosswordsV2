using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Services;

namespace Services
{
    [ServiceContract(CallbackContract = typeof(IPingCallback))]
    public interface IPing
    {
        [OperationContract]
        void ConnectPingManagement(Users user);
        
    }

    [ServiceContract]
    public interface IPingCallback
    {
        [OperationContract]
        void Alive();
        [OperationContract]
        void BackMenu();
    }

}
