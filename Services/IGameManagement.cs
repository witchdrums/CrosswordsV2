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
        Domain.Board GetBoardById(int idBoard);

        [OperationContract(IsOneWay = true)]
        void SendSolvedWordsBoard
        (
            List<Users> room,
            Users usersOrigin,
            Domain.WordsBoard solvedWordsBoard
        );

        [OperationContract(IsOneWay = true)]
        void JoinGame(Users user);

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
