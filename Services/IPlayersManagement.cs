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
        Players RegisterPlayer(Users user);

        [OperationContract]
        Players GetPlayerFor(Users user);


    }
}
