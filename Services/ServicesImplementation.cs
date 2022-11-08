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
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant)]

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
                connectionMapMessages.GetOperationContextForId(user.idUser).GetCallbackChannel<IMessagesCallback>().ReciveChatMessage(userOrigin, message);
            }
        }

        public void SendPrivateMessage(Users userOrigin, Users userDestination, string message)
        {
            connectionMapMessages.GetOperationContextForId(userDestination.idUser).GetCallbackChannel<IMessagesCallback>().ReciveChatMessage(userOrigin, "[Private] " + message);
        }

    }
    public partial class ServicesImplementation : IGameRoomManagement
    {
        RoomMap roomMap = new RoomMap();
        public int CreateRoom(Users user)
        {
            roomMap.NewRoom(user.idUser);//Todo Generate Random ID
            return user.idUser;
        }

        public void JoinToRoom(int idRoom, Users newUser)
        {
            roomMap.AddUserToRoom(idRoom, newUser);
            List<Users> usersRoom = roomMap.GetUsersInRoom(idRoom);
            foreach (Users user in usersRoom)
            {
                connectionMapMessages.GetOperationContextForId(user.idUser).GetCallbackChannel<IGameRoomManagementCallback>().UpdateRoom(usersRoom);
            }
        }

        public void SendInvitationToRoom(int idRoom, Users userTarget)
        {
            connectionMapMessages.GetOperationContextForId(userTarget.idUser).GetCallbackChannel<IGameRoomManagementCallback>().ReciveInvitationToRoom(idRoom);
        }
    }

    public partial class ServicesImplementation : IGameManagement
    {

        private static readonly ConnectionMap connectionMapGameManagement = new ConnectionMap();

        public void JoinGame(Users user)
        {
            connectionMapGameManagement.SaveUser(user.idUser, OperationContext.Current);
        }

        public Domain.Board GetBoardById(int idBoard)
        {
            Domain.Board foundBoard = new Domain.Board();
            BusinessServices.GameManagement gameManagement = new BusinessServices.GameManagement();
            foundBoard = gameManagement.GetBoardById(idBoard);


            return foundBoard;

        }

        public void SendSolvedWordsBoard(List<Users> room, Users usersOrigin, Domain.WordsBoard solvedWordsBoard)
        {

            foreach (Users user in room)
            {
                OperationContext userContext = connectionMapGameManagement.GetOperationContextForId(user.idUser);
                userContext.GetCallbackChannel<IGameManagementCallback>().ReceiveSolvedWordsBoard(usersOrigin, solvedWordsBoard);
            }
        }
    }

    public partial class ServicesImplementation : IPlayersManagement
    {
        public Players GetPlayerFor(Users user)
        {
            throw new NotImplementedException();
        }

        public Players RegisterPlayer(Users user)
        {
            throw new NotImplementedException();
        }
    }

}
