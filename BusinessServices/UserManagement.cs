using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic;
using Domain;


namespace BusinessServices
{
    public class UserManagement
    {
        public Boolean RegisterUser(Users user)
        {
            Boolean result = false;
            BusinessLogic.User businessLogicUser = new BusinessLogic.User();
            businessLogicUser.password = user.password;
            businessLogicUser.email = user.email;
            businessLogicUser.username = user.username;
            businessLogicUser.idUserType = 1;

            Player businessLogicPlayer = new Player();
            businessLogicPlayer.playerName = user.email;
            businessLogicPlayer.User = businessLogicUser;
            businessLogicPlayer.idUser = businessLogicUser.idUser;
            businessLogicPlayer.playerLevel = 1;

            using (var context = new CrosswordsContext())
            {
                context.Players.Add(businessLogicPlayer);
                context.Users.Add(businessLogicUser);
                result = context.SaveChanges() > 0;
            }
            return result;
        }

        public Boolean FindUserByEmail(String userEmail)
        {
            Boolean result = false;
            using (var context = new CrosswordsContext())
            {
                var foundUsers = (from user in context.Users
                                  where user.email == userEmail
                                  select user).ToList();
                result = foundUsers.Count == 0;
              
            }
            return result;
        }

        public Players FindUserByUserNameAndPassword(Users user)
        {
            Players player = new Players();
            using (var context = new CrosswordsContext())
            {
                var foundUsers = (from users in context.Users
                                  where users.username == user.username && users.password == user.password
                                  select users).ToList();
                if (foundUsers.Count > 0)
                {
                    user.credential = true;
                    user.idUser = foundUsers[0].idUser;
                    user.idUserType = foundUsers[0].idUserType;
                    user.email = foundUsers[0].email;
                    player.User = user;
                    player = GetPlayerInformation(player);
                }
            }

            return player;
        }

        public Players GetPlayerInformation(Players player)
        {
            using (var context = new CrosswordsContext())
            {
                var foundPlayers = (from players in context.Players
                                    where players.idUser == player.User.idUser
                                    select players).ToList();
                if (foundPlayers.Count > 0)
                {
                    player.idPlayer = foundPlayers[0].idPlayer;
                    player.playerName = foundPlayers[0].playerName;
                    player.playerDescription = foundPlayers[0].playerDescription;
                    player.playerLevel = foundPlayers[0].playerLevel;
                    player.idRank = foundPlayers[0].idRank;
                    player.idProfileImage = foundPlayers[0].idProfileImage;
                    player = GetPlayerRank(player);
                    player = GetPlayerProfileImage(player);
                    player = GetPlayerGames(player);
                }
            }
            return player;
        }

        public Players GetPlayerRank(Players player)
        {
            using (var context = new CrosswordsContext())
            {
                var foundRank = (from ranks in context.Ranks
                                    where ranks.idRank == player.idRank
                                    select ranks).ToList();
                if (foundRank.Count > 0)
                {
                    player.Rank = new Ranks();
                    player.Rank.idRank = foundRank[0].idRank;
                    player.Rank.rankName = foundRank[0].rankName;
                }
            }
            return player;
        }

        public Players GetPlayerProfileImage(Players player)
        {
            using (var context = new CrosswordsContext())
            {
                var foundProfileImage = (from profileImages in context.ProfileImages
                                 where profileImages.idProfileImage == player.idProfileImage
                                 select profileImages).ToList();
                if (foundProfileImage.Count > 0)
                {
                    player.ProfileImage = new ProfileImages();
                    player.ProfileImage.idProfileImage = foundProfileImage[0].idProfileImage;
                    player.ProfileImage.profileImageName = foundProfileImage[0].profileImageName;
                }
            }
            return player;
        }
        public Players GetPlayerGames(Players player)
        {
            using (var context = new CrosswordsContext())
            {
                var foundPlayerGames = (from gamesPlayer in context.GamesPlayers
                                           where gamesPlayer.idPlayer == player.idPlayer
                                           select gamesPlayer).ToList();
                player.gamesPlayed = foundPlayerGames.Count;
                if (foundPlayerGames.Count > 0)
                {
                    player = GetPlayerGamesWon(player);
                }
                else 
                {
                    player.gamesWon = 0;
                }
            }
            return player;
        }

        public Players GetPlayerGamesWon(Players player)
        {
            int primerLugar = 1;
            using (var context = new CrosswordsContext())
            {
                var foundPlayerGamesWon = (from gamesPlayer in context.GamesPlayers
                                           where gamesPlayer.gameRank == primerLugar && gamesPlayer.idPlayer == player.idPlayer
                                           select gamesPlayer).ToList();
                player.gamesWon = foundPlayerGamesWon.Count;
            }
            return player;
        }

        public bool FindUserByEmailAndUsername(Users user)
        {
            bool userFound = false;
            using (var context = new CrosswordsContext())
            {
                var foundUsers= (from users in context.Users
                                    where users.username == user.username && users.email == user.email
                                    select users).ToList();
                if (foundUsers.Count > 0)
                {
                    userFound = true;
                }
            }
            return userFound;
        }

        public bool RegisterRecoveredPasswordUser(Users user)
        {
            bool passwordRegistered = false;
            using (var context = new CrosswordsContext())
            {
                var foundUsers = context.Users.Where(users => users.username == user.username && users.email == user.email).First();
                foundUsers.username = user.username;
                foundUsers.password = user.password;
                passwordRegistered = context.SaveChanges()>=1;
            }
            return passwordRegistered;
        }

        public List<Categories> GetReportCategories()
        {
            List<Categories> categories = new List<Categories>();
            using (var context = new CrosswordsContext())
            {
                List<Category> businessLogicCategories = context.Categories.ToList();
                foreach (Category category in businessLogicCategories)
                {
                    Categories domainCategory = new Categories();
                    domainCategory.idCategory = category.idCategory;
                    domainCategory.description = category.description;
                    categories.Add(domainCategory);
                }
            }
            return categories;
        }

        public bool RegisterReport(Reports report)
        {
            bool confirmation = false;
            using (var context = new CrosswordsContext())
            {
                Report businessLogicReport = new Report();
                businessLogicReport.chatLog = report.chatLog;
                businessLogicReport.idCategory = report.idCategory;
                businessLogicReport.idUser = report.idUser;

                context.Reports.Add(businessLogicReport);
                confirmation = context.SaveChanges() > 0;
            }
            return confirmation;
        }

        public Domain.Users ParseToDomain(BusinessLogic.User businessLogicUser)
        {
            Domain.Users domainUsers = new Domain.Users();
            domainUsers.idUser = businessLogicUser.idUser;
            domainUsers.username = businessLogicUser.username;
            return domainUsers;

        }

        public Domain.Users GetUserInformationForPlayer(Domain.Players player)
        {
            Domain.Users domainUser = new Domain.Users();
            BusinessLogic.User bussinesLogicUser = new BusinessLogic.User();
            BusinessLogic.Player bussinesLogicPlayer = new BusinessLogic.Player();
            int idUser = player.idPlayer;
            using (var context = new CrosswordsContext())
            {
                var query = (from players in context.Players
                                    where players.idPlayer == player.idPlayer
                                    select players);

                if(query.First() != null)
                {
                    bussinesLogicPlayer = query.First();
                    domainUser.idUser = bussinesLogicPlayer.User.idUser;
                    domainUser.idUserType = bussinesLogicPlayer.User.idUserType;
                    domainUser.username = bussinesLogicPlayer.User.username;
                    domainUser.email = bussinesLogicPlayer.User.email;
                }

            }
            return domainUser;
        }

    }
}
