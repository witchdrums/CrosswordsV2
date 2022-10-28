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
        [OperationContract]
        void SendChatMessage(List<Users> room,Users userOrigin, string message);
        [OperationContract]
        void SendPrivateMessage(Users userOrigin,Users userDestination, string message);

    }

    [ServiceContract]
    public interface IMessagesCallback
    {
        [OperationContract]
        void ReciveChatMessage(Users userOrigin,string message);
    }
}
