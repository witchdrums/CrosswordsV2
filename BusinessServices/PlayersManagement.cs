﻿using BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using System.ComponentModel.Design;
using System.Data.SqlClient;
using System.Runtime.Remoting.Contexts;

namespace BusinessServices
{
    public class PlayersManagement
    {
        public Players GetPlayerFor(Users user)
        {
            Player businessLogicPlayer;
            using (var context = new CrosswordsContext())
            {
                int idUser = user.idUser;

                businessLogicPlayer = (from player in context.Players
                                        where player.idUser == idUser
                                        select player)
                                        .AsEnumerable()
                                        .ElementAt(0);

            }
            Players foundPlayer = ParseToDomain(businessLogicPlayer);
            foundPlayer.User = user;
            return foundPlayer;
        }

        private Domain.Players ParseToDomain(BusinessLogic.Player businessLogicPlayer)
        {
            Players domainPlayer = new Players();
            domainPlayer.idPlayer = businessLogicPlayer.idPlayer;
            domainPlayer.playerName = businessLogicPlayer.playerName;
            return domainPlayer;
        }

        public BusinessLogic.Player ParseToBusiness(Domain.Players domainPlayer)
        {
            Player businessLogicPlayer = new Player();
            businessLogicPlayer.idPlayer = domainPlayer.idPlayer;
            businessLogicPlayer.playerName = domainPlayer.playerName;
            return businessLogicPlayer;
        }

       

        public List<Domain.Players> GetPlayersFilteredName(String nameFilter)
        {
            List<Domain.Players> playersFiltered = new List<Domain.Players>();

            using (var context = new CrosswordsContext())
            {
                UserManagement userManagement = new UserManagement();
                List<BusinessLogic.Player> businessLogicPlayer = context.Players.ToList();
                foreach (BusinessLogic.Player player in businessLogicPlayer)
                {
                    if(player.playerName.ToLower().IndexOf(nameFilter.ToLower())>-1)
                    {
                        Domain.Players filteredPlayer = new Domain.Players();
                        filteredPlayer = this.ParseToDomain(player);
                        filteredPlayer.User = userManagement.ParseToDomain(player.User);
                        playersFiltered.Add(filteredPlayer);
                    }
                }
            }
            return playersFiltered;
        }

        public List<Domain.Players> GetFriendsPlayersOfPlayerByUser(Domain.Players player)
        {
            List<Domain.Players> friendListDomain = new List<Domain.Players>();
            using (var context = new CrosswordsContext())
            {
                UserManagement userManagement = new UserManagement();
                BusinessLogic.Player playerBussinessService;
                var query = (from players in context.Players
                                where players.idPlayer == player.idPlayer select players);
                if(query.Count() > 0)
                {
                    playerBussinessService = query.ToList().ElementAt(0);
                    foreach (Player playerQuery in playerBussinessService.Players1)
                    {
                        Domain.Players parsePlayer = new Domain.Players();
                        parsePlayer = this.ParseToDomain(playerQuery);
                        parsePlayer.User = userManagement.ParseToDomain(playerQuery.User);
                        friendListDomain.Add(parsePlayer);
                    }
                }    
            }
            return friendListDomain;
        }

        public Boolean IsFriend(Domain.Players playerSource, Domain.Players playerTarget)
        {
            Boolean response = false;
            using (var context = new CrosswordsContext())
            {
                UserManagement userManagement = new UserManagement();

                var query= (from players in context.Players
                        where players.idPlayer == playerSource.idPlayer
                        select players);
            if(query.Count() > 0)
            {
                    BusinessLogic.Player playerBussinessService = query.ToList().ElementAt(0);
                    foreach (BusinessLogic.Player playerQuery in playerBussinessService.Players1)
                    {
                        if (playerQuery.idPlayer == playerTarget.idPlayer)
                        {
                            response = true;
                        }
                    }
                }
            }
            return response;
        }

        public Boolean AddFriendPlayer(Domain.Players playerSource, Domain.Players playerTarget)
        {
            Boolean response = false;
            using (var context = new CrosswordsContext())
            {
                response = context.Database.ExecuteSqlCommand(
                "Insert into Friends(idPlayer,idPlayerFriend) Values(@idPlayer, @idPlayerFriend)",
                new SqlParameter("idPlayer", playerSource.idPlayer),
                new SqlParameter("idPlayerFriend", playerTarget.idPlayer)
                )!=0;
            }
            return response;
        }

        public Boolean DeleteFriendPlayer(Domain.Players playerSource, Domain.Players playerTarget)
        {
            Boolean response = false;
            using (var context = new CrosswordsContext())
            {
                response = context.Database.ExecuteSqlCommand(
                "DELETE FROM Friends WHERE idPlayer = @idPlayer AND idPlayerFriend = @idPlayerFriend;",
                new SqlParameter("idPlayer", playerSource.idPlayer),
                new SqlParameter("idPlayerFriend", playerTarget.idPlayer)
                ) != 0;
            }
            return response;
        }
    }
}
