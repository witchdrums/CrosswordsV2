using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using Domain;


namespace Services
{
    [ServiceContract(CallbackContract = typeof(IGameRoomManagementCallback))]
    public interface IGameRoomManagement
    {
        [OperationContract]
        void SendInvitationToRoom(int idRoom, Users userTarget);
        [OperationContract]
        void JoinToRoom(int idRoom,Users user);

        [OperationContract]
        int CreateRoom(Users user);
        
    }
    
    [ServiceContract]
    public interface IGameRoomManagementCallback
    {
        [OperationContract]
        void ReciveInvitationToRoom(int idRoom);
        [OperationContract]
        void JoinToRoom(int idRoom,List<Users> usersInRoom);
    }
  
}
