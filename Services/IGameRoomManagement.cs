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
    interface IGameRoomManagement
    {
        [OperationContract(IsOneWay = true)]
        void SendInvitationToRoom(int idRoom, Users userTarget);
        [OperationContract(IsOneWay = true)]
        void JoinToRoom(int idRoom, Users newUser);

        [OperationContract]
        int CreateRoom(Users user);
        [OperationContract]
        void ConnectGameRoomManagement(Users users);
        
    }
    
    [ServiceContract]
    interface IGameRoomManagementCallback
    {
        [OperationContract]
        void ReciveInvitationToRoom(int idRoom);
        [OperationContract]
        void UpdateRoom(List<Users> usersInRoom);

    }
  
}
