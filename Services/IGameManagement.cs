using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Services;

namespace Services
{
    [ServiceContract(CallbackContract = typeof(IGameManagementCallback))]
    public interface IGameManagement
    {
        [OperationContract]
        Domain.Boards GetBoardById(int idBoard);

        [OperationContract(IsOneWay = true)]
        void SendSolvedWordsBoard
        (
            List<Users> room,
            Users usersOrigin,
            Domain.WordsBoard solvedWordsBoard
        );

        [OperationContract(IsOneWay = true)]
        void JoinGame(Users user);

        [OperationContract(IsOneWay = true)]
        void PassTurn(List<GamesPlayers>gamePlayers,int currentPlayerIndex);


        [OperationContract(IsOneWay = true)]
        void InitializeGamePlayerQueue(List<GamesPlayers> room);

        [OperationContract(IsOneWay = true)]
        void GetFirstTurn(List<GamesPlayers> gamePlayers);

        [OperationContract]
        GamePlayerQueue GetQueue();

        [OperationContract]
        GamesPlayers GetGamesPlayers();



    }

    public interface IGameManagementCallback
    {
        [OperationContract(IsOneWay = true)]
        void ReceiveSolvedWordsBoard
        (
            Users usersOrigin,
            WordsBoard solvedWordsBoard
        );

        [OperationContract(IsOneWay = true)]
        void ReceiveTurn();

        [OperationContract(IsOneWay = true)]
        void SetCurrentPlayerIndex(int currentPlayerIndex);
    }
}
