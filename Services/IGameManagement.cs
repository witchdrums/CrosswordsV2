using System;
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
    }

    public interface IGameManagementCallback
    {

    }
}
