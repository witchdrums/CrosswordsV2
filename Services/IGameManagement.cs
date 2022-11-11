using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Domain;

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

        [OperationContract]
        Domain.Games GetGames();

    }

    public interface IGameManagementCallback
    {
        [OperationContract(IsOneWay = true)]
        void ReceiveSolvedWordsBoard
        (
            Users usersOrigin,
            WordsBoard solvedWordsBoard
        );
    }
}
