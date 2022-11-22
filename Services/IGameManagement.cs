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
        void PassTurn(Queue<GamesPlayers> gamePlayers, int currentTurns);

        [OperationContract(IsOneWay = true)]
        void EndGame(int idRoom, List<GamesPlayers> playerRanks);

        [OperationContract(IsOneWay = true)]
        void RemovePlayer(Players leavingPlayer, Queue<GamesPlayers> gamePlayers);

        [OperationContract(IsOneWay = true)]
        void RemoveHost(GamesPlayers host, Queue<GamesPlayers> gamePlayers);
    }

    [ServiceContract]
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
        [OperationContract(IsOneWay = true)]
        void ShowPlayerRanks(List<GamesPlayers> playerRanks);

        [OperationContract(IsOneWay = true)]
        void RemoveLeavingUser(Players leavingPlayer, Queue<GamesPlayers> updatedQueue);

        [OperationContract(IsOneWay = true)]
        void SendLeavingUserToMainMenu();

        [OperationContract(IsOneWay = true)]
        void StopGame();
    }
}
