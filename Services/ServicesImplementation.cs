using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic;
using System.Runtime.Remoting.Contexts;
using System.Data.Entity.Validation;
using BusinessServices;



namespace Services
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple)]

    public partial class ServicesImplementation : IUsersManager
    {
        private static readonly ConnectionMap connectionMap = new ConnectionMap();
        CrosswordsContext context = new CrosswordsContext();
        public bool AddUser(Users user)
        {
            BusinessServices.UserManagement userManagement = new BusinessServices.UserManagement();
            bool result = userManagement.RegisterUser(user);
            return result;
        }

        public bool FindUserByEmail(string userEmail)
        {
            BusinessServices.UserManagement userManagement = new BusinessServices.UserManagement();
            bool result = userManagement.FindUserByEmail(userEmail);
            return result;
        }

        public Players Login(Users user)
        {
            Players playerLogin = new Players();
            BusinessServices.UserManagement userManagement = new BusinessServices.UserManagement();
            playerLogin = userManagement.FindUserByUserNameAndPassword(user);
            if(playerLogin.user.credential)
            {
                connectionMap.SaveUser(playerLogin.user.idUser, OperationContext.Current);
            }
            return playerLogin;
        }
    }

    public partial class ServicesImplementation : IMessages
    {
        private static readonly ConnectionMap connectionMapMessages = new ConnectionMap();
        public void ConnectMessages(Users user)
        {
            connectionMapMessages.SaveUser(user.idUser, OperationContext.Current);
        }

        public void SendChatMessage(List<Users> room, Users userOrigin, string message)
        {
            foreach (Users user in room)
            {
                connectionMapMessages.GetOperationContextForId(user.idUser).GetCallbackChannel<IMessagesCallback>().ReciveChatMessage(userOrigin,message);
            }
        }

        public void SendPrivateMessage(Users userOrigin, Users userDestination, string message)
        {
            connectionMap.GetOperationContextForId(userDestination.idUser).GetCallbackChannel<IMessagesCallback>().ReciveChatMessage(userOrigin,"[Private] "+message);
        }


    }
    public partial class ServicesImplementation : IGameRoomManagement
    {
        private static readonly RoomMap roomMap = new RoomMap();
        private static readonly ConnectionMap connectionMapRoomManagement = new ConnectionMap();

        public void ConnectGameRoomManagement(Users users)
        {
            connectionMapRoomManagement.SaveUser(users.idUser,OperationContext.Current);
        }

        public int CreateRoom(Users user)
        {
            roomMap.NewRoom(user.idUser);
            return user.idUser;
        }

        public void JoinToRoom(int idRoom,Users newUser)
        {
            roomMap.AddUserToRoom(idRoom, newUser);
            List<Users> usersRoom = roomMap.GetUsersInRoom(idRoom);
            foreach (Users user in usersRoom)
            {
                connectionMapRoomManagement.GetOperationContextForId(user.idUser).GetCallbackChannel<IGameRoomManagementCallback>().UpdateRoom(usersRoom);
            }            
        }

        public void SendInvitationToRoom(int idRoom, Users userTarget)
        {
            connectionMap.GetOperationContextForId(userTarget.idUser).GetCallbackChannel<IGameRoomManagementCallback>().ReciveInvitationToRoom(idRoom);
        }
    }

    public partial class ServicesImplementation : IGameManagement
    {
        

        public Domain.Board GetBoardById(int idBoard)
        {
            Domain.Board foundBoard = new Domain.Board();
            BusinessServices.GameManagement gameManagement = new BusinessServices.GameManagement();
            foundBoard = gameManagement.GetBoardById(idBoard);


            return foundBoard;

        }
    }

}
