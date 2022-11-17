using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    [ServiceContract]
    public interface IPlayersManagement
    {
        [OperationContract]
        List<Players> GetFriendList(Players player);

        [OperationContract]
        Players RegisterPlayer(Users user);

        [OperationContract]
        Players GetPlayerFor(Users user);

        [OperationContract]
        List<Players> GetFilteredPlayers(String nameFilter);
    }
}
