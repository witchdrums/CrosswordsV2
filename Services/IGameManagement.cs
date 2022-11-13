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
        [OperationContract(IsOneWay = true)]
        void SendSolvedWordsBoard
        (
            Queue<GamesPlayers> gamePlayersQueue,
            GamesPlayers solver,
            WordsBoard solvedWordsBoard
        );

        [OperationContract(IsOneWay = true)]
        void JoinGame(GamesPlayers gamePlayer);

        [OperationContract(IsOneWay = true)]
        void PassTurn(Queue<GamesPlayers>gamePlayers, int currentTurns);



    }

    public interface IGameManagementCallback
    {
        [OperationContract(IsOneWay = true)]
        void ReceiveSolvedWordsBoard
        (
            GamesPlayers solver,
            WordsBoard solvedWordsBoard
        );

        [OperationContract(IsOneWay = true)]
        void ReceiveTurn();
        [OperationContract(IsOneWay = true)]
        void UpdateGamePlayersQueue(Queue<GamesPlayers> gamePlayers, int remainingTurns);

    }
}
