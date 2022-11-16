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

        public List<Categories> GetReportCategories()
        {
            BusinessServices.UserManagement userManagement = new BusinessServices.UserManagement();
            return userManagement.GetReportCategories();
        }

        public Players Login(Users user)
        {
            Players playerLogin = new Players();
            BusinessServices.UserManagement userManagement = new BusinessServices.UserManagement();
            playerLogin = userManagement.FindUserByUserNameAndPassword(user);
            if (playerLogin.user.credential)
            {
                connectionMap.SaveUser(playerLogin.user.idUser, OperationContext.Current);
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

        public void LaunchGamePage(GameConfiguration gameConfiguration)
        {

            foreach (Users user in gameConfiguration.UsersRoom)
            {
                Console.WriteLine(user.idUser);
                connectionMapRoomManagement.GetOperationContextForId(user.idUser)
                    .GetCallbackChannel<IGameRoomManagementCallback>().EnterGame(gameConfiguration);
            }
        }

        public Domain.Boards GetBoardById(int idBoard)
        {

            Domain.Boards domainBoard = new Domain.Boards();
            using (var context = new CrosswordsContext())
            {
                BusinessLogic.Board foundBoard = new BusinessLogic.Board();
                foundBoard = (from board in context.Boards
                              where board.idBoard == idBoard
                              select board)
                                .ToList()
                                .ElementAt(0);


                domainBoard.idBoard = foundBoard.idBoard;
                domainBoard.boardMatrix = foundBoard.boardMatrix;

                foreach (BusinessLogic.WordsBoard businessWordBoard in foundBoard.WordsBoards)
                {
                    Domain.WordsBoard domainWordBoard = new Domain.WordsBoard();
                    domainWordBoard.idBoard = idBoard;
                    domainWordBoard.xStart = businessWordBoard.xStart;
                    domainWordBoard.xEnd = businessWordBoard.xEnd;
                    domainWordBoard.yStart = businessWordBoard.yStart;
                    domainWordBoard.yEnd = businessWordBoard.yEnd;

                    Domain.Word domainWord = new Domain.Word();
                    domainWord.term = businessWordBoard.Word.word1;
                    domainWord.clue = businessWordBoard.Word.clue;
                    domainWordBoard.Word = domainWord;

                    domainBoard.WordsBoards.Add(domainWordBoard);
                }
            }



            return domainBoard;

        }

    }

    public partial class ServicesImplementation : IGameManagement
    {
        private static readonly ConnectionMap connectionMapGameManagement = new ConnectionMap();

        public void JoinGame(GamesPlayers gamePlayer)
        {
            int idUser = gamePlayer.Player.user.idUser;
            connectionMapGameManagement.SaveUser(idUser, OperationContext.Current);
        }

        public void SendSolvedWordsBoard(Queue<GamesPlayers> gamePlayersQueue, GamesPlayers solver, Domain.WordsBoard solvedWordsBoard)
        {

            foreach (GamesPlayers gamePlayer in gamePlayersQueue)
            {
                int idUser = gamePlayer.Player.user.idUser;
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
                int idUser = gamePlayer.Player.user.idUser;
                userContext = connectionMapGameManagement.GetOperationContextForId(idUser);
                userContext.GetCallbackChannel<IGameManagementCallback>().UpdateGamePlayersQueue(gamePlayersQueue, currentTurns);
            }
            int nextIdUser = gamePlayersQueue.Peek().Player.user.idUser;
            userContext = connectionMapGameManagement.GetOperationContextForId(nextIdUser);
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
