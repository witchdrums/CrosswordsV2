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

        public Players GetPlayerInformation(Players player)
        {
            BusinessServices.UserManagement userManagement = new BusinessServices.UserManagement();
            Players foundPlayer = userManagement.GetPlayerInformation(player);
            return foundPlayer;
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
            connectionMapMessages.GetOperationContextForId(userDestination.idUser).GetCallbackChannel<IMessagesCallback>().ReciveChatMessage(userOrigin, "[Private] " + message);
        }

    }
    public partial class ServicesImplementation : IGameRoomManagement
    {
        private static readonly RoomMap roomMap = new RoomMap();
        private static readonly ConnectionMap connectionMapRoomManagement = new ConnectionMap();

        public bool CheckRoomAvailability(int idRoom)
        {
            bool response = true;
            if(!(roomMap.ExistRoom(idRoom)))
            {
                response = false;
            }else
            {
                if(roomMap.IsFullRoom(idRoom))
                {
                    response = false;
                }
            }
           
            return response;
        }

        public void ConnectGameRoomManagement(Users users)
        {
            connectionMapRoomManagement.SaveUser(users.idUser,OperationContext.Current);
        }

        public int CreateRoom(Users user)
        {
            roomMap.NewRoom(user.idUser);
            return user.idUser;
        }

        public void DeleteRoom(int idRoom)
        {
            List<Users> usersInRoom = roomMap.GetUsersInRoom(idRoom);
            roomMap.DeleteRoom(idRoom);
            foreach (Users user in usersInRoom)
            {
                connectionMapRoomManagement.GetOperationContextForId(user.idUser).GetCallbackChannel<IGameRoomManagementCallback>().ForceExitToRoom();
            }
        }

        public void ExitToRoom(int idRoom, Users user)
        {
            roomMap.RemoveUserToRoom(idRoom, user);
            List<Users> updatedUserList = roomMap.GetUsersInRoom(idRoom);
            foreach (Users userUpdated in updatedUserList)
            {
                connectionMapRoomManagement.GetOperationContextForId(userUpdated.idUser).GetCallbackChannel<IGameRoomManagementCallback>().UpdateRoom(updatedUserList);

            }
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

        public void StartGame(List<Users> usersRoom)
        {
            foreach (Users user in usersRoom)
            {
                connectionMapRoomManagement.GetOperationContextForId(user.idUser).GetCallbackChannel<IGameRoomManagementCallback>().EnterGame();
            }
        }
    }

    public partial class ServicesImplementation : IGameManagement
    {
        private static readonly ConnectionMap connectionMapGameManagement = new ConnectionMap();
        private static readonly GamePlayerQueue gamePlayerQueue = new GamePlayerQueue();

        public void JoinGame(Users user)
        {
            connectionMapGameManagement.SaveUser(user.idUser, OperationContext.Current);
        }

        public Domain.Boards GetBoardById(int idBoard)
        {
            Domain.Boards foundBoard = new Domain.Boards();
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

        public GamePlayerQueue GetQueue()
        {
            return new GamePlayerQueue();
        }

        public void PassTurn(List<GamesPlayers> gamePlayers, int currentPlayerIndex)
        {
            int playerIndexLimit = gamePlayers.Count - 1;
            if (currentPlayerIndex == playerIndexLimit)
            {
                currentPlayerIndex = 0;
            }
            else if (playerIndexLimit > 0)
            {
                currentPlayerIndex += 1;
            }
            int nextIdUser = gamePlayers.ElementAt(currentPlayerIndex).Player.user.idUser;

            OperationContext userContext = connectionMapGameManagement.GetOperationContextForId(nextIdUser);
            userContext.GetCallbackChannel<IGameManagementCallback>().ReceiveTurn();

            foreach (GamesPlayers gamePlayer in gamePlayers)
            {
                int idUser = gamePlayer.Player.user.idUser;
                userContext = connectionMapGameManagement.GetOperationContextForId(idUser);
                userContext.GetCallbackChannel<IGameManagementCallback>().SetCurrentPlayerIndex(currentPlayerIndex);
            }
            Console.WriteLine(currentPlayerIndex);
        }

        public void InitializeGamePlayerQueue(List<GamesPlayers> room)
        {
            gamePlayerQueue.Initialize(room);
        }

        public GamesPlayers GetGamesPlayers()
        {
            throw new NotImplementedException();
        }

        public void GetFirstTurn(List<GamesPlayers> gamePlayers)
        {
            int firstIdUser = gamePlayers.ElementAt(0).Player.user.idUser;
            OperationContext userContext = connectionMapGameManagement.GetOperationContextForId(firstIdUser);
            userContext.GetCallbackChannel<IGameManagementCallback>().ReceiveTurn();
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
