using BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace BusinessServices
{
    public class PlayersManagement
    {
        public Players GetPlayerFor(Users user)
        {
            Players foundPlayer = new Players();
            BusinessLogic.Player businessLogicPlayer = new BusinessLogic.Player();
            using (var context = new CrosswordsContext())
            {
                int idUser = user.idUser;

                businessLogicPlayer = (from player in context.Players
                                       where player.idUser == idUser
                                       select player)
                                       .ToList()
                                       .ElementAt(0);
                foundPlayer = ParseToDomain(businessLogicPlayer);
                foundPlayer.user = user;
            }
            return foundPlayer;
        }

        private Domain.Players ParseToDomain(BusinessLogic.Player businessLogicPlayer)
        {
            Players domainPlayer = new Players();
            domainPlayer.idPlayer = businessLogicPlayer.idPlayer;
            domainPlayer.playerName = businessLogicPlayer.playerName;
            domainPlayer.playerRank = businessLogicPlayer.playerRank;
            return domainPlayer;
        }
    }
}
