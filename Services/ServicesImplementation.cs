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
using System.Runtime.Serialization;
using System.Data.Entity;

namespace Services
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant)]

    public partial class ServicesImplementation : IUsersManager
    {
        private static readonly ConnectionMap connectionMap = new ConnectionMap();
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

        public List<Categories> GetReportCategories()
        {
            BusinessServices.UserManagement userManagement = new BusinessServices.UserManagement();
            return userManagement.GetReportCategories();
        }

        public Users GetUserByPlayer(Players player)
        {
            BusinessServices.UserManagement userManagement = new BusinessServices.UserManagement();
            return userManagement.GetUserInformationForPlayer(player);
        }

        public Players Login(Users user)
        {
            Players playerLogin = new Players();
            BusinessServices.UserManagement userManagement = new BusinessServices.UserManagement();
            playerLogin = userManagement.FindUserByUserNameAndPassword(user);
            if (playerLogin.User.credential)
            {
                connectionMap.SaveUser(playerLogin.User.idUser, OperationContext.Current);
            }
            return playerLogin;
        }

        public bool RecoverPassword(Users user)
        {
            BusinessServices.UserManagement userManagement = new BusinessServices.UserManagement();
            bool userFound = userManagement.FindUserByEmailAndUsername(user);
            return userFound;
        }

        public bool RegisterRecoveredPassword(Users user)
        {
            BusinessServices.UserManagement userManagement = new BusinessServices.UserManagement();
            bool passwordRegistered = userManagement.RegisterRecoveredPasswordUser(user);
            return passwordRegistered;
        }

        public bool RegisterReport(Reports report)
        {
            BusinessServices.UserManagement userManagement = new BusinessServices.UserManagement();
            return userManagement.RegisterReport(report);
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
        private static readonly RoomMap roomMap = new RoomMap();
        private static readonly ConnectionMap connectionMapRoomManagement = new ConnectionMap();

        public bool CheckRoomAvailability(int idRoom)
        {
            bool response = true;
            if (!(roomMap.ExistRoom(idRoom)))
            {
                response = false;
            }
            else
            {
                if (roomMap.IsFullRoom(idRoom))
                {
                    response = false;
                }
            }

            return response;
        }

        public void ConnectGameRoomManagement(Users users)
        {
            connectionMapRoomManagement.SaveUser(users.idUser, OperationContext.Current);
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

        public void JoinToRoom(int idRoom, Users newUser)
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

        public void LaunchGamePage(GameConfiguration gameConfiguration, int idRoom)
        {
            List<Users> usersInRoom = roomMap.GetUsersInRoom(idRoom);
            foreach (Users user in usersInRoom)
            {
                connectionMapRoomManagement.GetOperationContextForId(user.idUser).GetCallbackChannel<IGameRoomManagementCallback>().EnterGame(gameConfiguration);
            }


        }

        public Boards GetBoardById(int idBoard)
        {
            GameRoomManagement gameRoomManagement = new GameRoomManagement();
            return gameRoomManagement.GetBoardById(idBoard);
        }

    }

    public partial class ServicesImplementation : IGameManagement
    {
        private static readonly ConnectionMap connectionMapGameManagement = new ConnectionMap();

        public void JoinGame(GamesPlayers gamePlayer)
        {
            int idUser = gamePlayer.Player.User.idUser;
            connectionMapGameManagement.SaveUser(idUser, OperationContext.Current);
        }

        public void SendSolvedWordsBoard(Queue<GamesPlayers> gamePlayersQueue, GamesPlayers solver, Domain.WordsBoard solvedWordsBoard)
        {

            foreach (GamesPlayers gamePlayer in gamePlayersQueue)
            {
                int idUser = gamePlayer.Player.User.idUser;
                OperationContext userContext = connectionMapGameManagement.GetOperationContextForId(idUser);
                userContext.GetCallbackChannel<IGameManagementCallback>().ReceiveSolvedWordsBoard(solver, solvedWordsBoard);
            }
        }

        public void PassTurn(Queue<GamesPlayers> gamePlayersQueue, int currentTurns)
        {

            OperationContext userContext;

            currentTurns -= 1;
            gamePlayersQueue.Enqueue(gamePlayersQueue.Dequeue());
            foreach (GamesPlayers gamePlayer in gamePlayersQueue)
            {
                int idUser = gamePlayer.Player.User.idUser;
                userContext = connectionMapGameManagement.GetOperationContextForId(idUser);
                userContext.GetCallbackChannel<IGameManagementCallback>().UpdateGamePlayersQueue(gamePlayersQueue, currentTurns);
            }
            int nextIdUser = gamePlayersQueue.Peek().Player.User.idUser;
            userContext = connectionMapGameManagement.GetOperationContextForId(nextIdUser);
            userContext.GetCallbackChannel<IGameManagementCallback>().ReceiveTurn();
        }

        public void EndGame(int idRoom, List<GamesPlayers> playerRanks)
        {
            roomMap.DeleteRoom(idRoom);
            OperationContext userContext;
            foreach (GamesPlayers player in playerRanks)
            {
                int idUser = player.Player.User.idUser;
                userContext = connectionMapGameManagement.GetOperationContextForId(idUser);
                userContext.GetCallbackChannel<IGameManagementCallback>().ShowPlayerRanks(playerRanks);
            }
        }


        public void RemovePlayer(Players leavingPlayer, Queue<GamesPlayers> gamePlayers)
        {
            OperationContext userContext;
            userContext = connectionMapGameManagement.GetOperationContextForId(leavingPlayer.User.idUser);
            userContext.GetCallbackChannel<IGameManagementCallback>().SendLeavingUserToMainMenu();

            connectionMapGameManagement.DeteleUserForId(leavingPlayer.User.idUser);
            int playerCount = gamePlayers.Count;

            for (int playerIndex = 0; playerIndex < playerCount; playerIndex++)
            {
                if (gamePlayers.Peek().idPlayer != leavingPlayer.idPlayer)
                {
                    gamePlayers.Enqueue(gamePlayers.Dequeue());
                }
                else
                {
                    gamePlayers.Dequeue();
                }
            }
            
            foreach (GamesPlayers player in gamePlayers)
            {
                int idUser = player.Player.User.idUser;
                userContext = connectionMapGameManagement.GetOperationContextForId(idUser);
                userContext.GetCallbackChannel<IGameManagementCallback>().RemoveLeavingUser(leavingPlayer, gamePlayers);
            }
        }

        public void RemoveHost(GamesPlayers host, Queue<GamesPlayers> gamePlayers)
        {
            roomMap.DeleteRoom(host.Player.User.idUser);
            OperationContext userContext;
            foreach (GamesPlayers player in gamePlayers)
            {
                int idUser = player.Player.User.idUser;
                if (idUser != host.Player.User.idUser)
                {
                    userContext = connectionMapGameManagement.GetOperationContextForId(idUser);
                    userContext.GetCallbackChannel<IGameManagementCallback>().StopGame();
                    connectionMapGameManagement.DeteleUserForId(idUser);
                }
            }
        }


    }

    public partial class ServicesImplementation : IPlayersManagement
    {
        public List<Players> GetFilteredPlayers(string nameFilter)
        {
            BusinessServices.PlayersManagement playersManagement = new PlayersManagement();
            return playersManagement.GetPlayersFilteredName(nameFilter);
        }

        public Players GetPlayerFor(Users user)
        {
            throw new NotImplementedException();
        }

        public Players RegisterPlayer(Users user)
        {
            throw new NotImplementedException();
        }

        public List<Players> GetFriendList(Players player)
        {
            BusinessServices.PlayersManagement playersManagement = new BusinessServices.PlayersManagement();
            List<Players> friendList = playersManagement.GetFriendsPlayersOfPlayerByUser(player);
            return friendList;
        }

        public bool AddFriend(Players playerOrigin, Players playerTarget)
        {
            bool result = false;
            BusinessServices.PlayersManagement playersManagement = new BusinessServices.PlayersManagement();

            if (playerOrigin.idPlayer != playerTarget.idPlayer)
            {
                if (!playersManagement.IsFriend(playerOrigin, playerTarget))
                {
                    playersManagement.AddFriendPlayer(playerOrigin, playerTarget);
                    result = true;
                }
            }
            return result;
        }
    }

}
