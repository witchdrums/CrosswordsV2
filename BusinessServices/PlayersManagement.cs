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
using System.Data.Entity.Migrations;

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
                        Domain.Players filteredPlayer = this.ParseToDomain(player);
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
                        Domain.Players parsePlayer = this.ParseToDomain(playerQuery);
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

        public bool RegisterGamesPlayer(GamesPlayers gamePlayer)
        {
            bool updateWasSuccessful = false;
            if (gamePlayer != null 
                && gamePlayer.idPlayer > 0
                && gamePlayer.idGame > 0 
                && gamePlayer.gameRank > 0
                && gamePlayer.gameRank < 5
                && gamePlayer.gameScore >= 0)
            {
                using (var context = new CrosswordsContext())
                {
                    GamesPlayer businessGamesPlayer = new GamesPlayer();
                    businessGamesPlayer.idPlayer = gamePlayer.idPlayer;
                    businessGamesPlayer.idGame = gamePlayer.idGame;
                    businessGamesPlayer.gameRank = gamePlayer.gameRank;
                    businessGamesPlayer.gameScore = gamePlayer.gameScore;
                    UpdatePlayerPoints(gamePlayer);
                    context.GamesPlayers.Add(businessGamesPlayer);
                    updateWasSuccessful = context.SaveChanges() == 1;
                }
            }
            return updateWasSuccessful;
        }

        public bool UpdatePlayerPoints(GamesPlayers gamePlayer)
        {
            bool updateWasSuccessful = false;
            if (gamePlayer != null
                && gamePlayer.idPlayer > 0)
            {
                using (var context = new CrosswordsContext())
                {
                    Player businessPlayer = context.Players.Find(gamePlayer.idPlayer);
                    if (businessPlayer != null)
                    {
                        businessPlayer.playerLevelPoints += 1;
                        businessPlayer.playerLevel += 1;
                        if (gamePlayer.gameRank == 1)
                        {
                            businessPlayer.idRank = GetNextRank(businessPlayer);
                        }
                        context.Players.AddOrUpdate(businessPlayer);
                        updateWasSuccessful = context.SaveChanges() == 1;
                    }
                }
            }
            return updateWasSuccessful;
        }

        public int GetNextRank(Player player)
        {
            int currentIdRank = 1;
            if (player != null
                && player.idRank > 0
                && player.idRank < 7)
            {
                currentIdRank = player.idRank;
                if (currentIdRank < 3)
                {
                    currentIdRank += 1;
                }
                else
                {
                    currentIdRank = 6;
                }
            }
            return currentIdRank;
        }

        public bool UpdatePlayerProfileInformation(Players playerToUpdate)
        {
            bool updatePlayerSuccessful = false;
            if (playerToUpdate.idPlayer > 0)
            {
                using (var context = new CrosswordsContext())
                {
                    Player businessPlayer = context.Players.Find(playerToUpdate.idPlayer);
                    businessPlayer.idProfileImage = playerToUpdate.idProfileImage;
                    businessPlayer.playerName = playerToUpdate.playerName;
                    businessPlayer.playerDescription = playerToUpdate.playerDescription;
                    context.Players.AddOrUpdate(businessPlayer);
                    updatePlayerSuccessful = context.SaveChanges() == 1;
                }
            }
            return updatePlayerSuccessful;
        }
    }
}
