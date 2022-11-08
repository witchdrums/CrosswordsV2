using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace Services
{
    [ServiceContract(CallbackContract = typeof(IMessagesCallback))]
    public interface IMessages
    {
        [OperationContract(IsOneWay = true)]
        void SendChatMessage(List<Users> room, Users userOrigin, string message);
        [OperationContract(IsOneWay = true)]
        void SendPrivateMessage(Users userOrigin, Users userDestination, string message);
        [OperationContract]
        void ConnectMessages(Users user);


    }

    [ServiceContract]
    public interface IMessagesCallback
    {
        [OperationContract]
        void ReciveChatMessage(Users userOrigin, string message);
    }
}
