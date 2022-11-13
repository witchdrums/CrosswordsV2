using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic;
using Domain;


namespace BusinessServices
{
    public class UserManagement
    {
        CrosswordsContext context = new CrosswordsContext();
        
        public Boolean RegisterUser(Users user){
            Boolean result = false;
            BusinessLogic.User businessLogicUser = new BusinessLogic.User();
            businessLogicUser.password = user.password;
            businessLogicUser.email = user.email;
            businessLogicUser.username = user.username;
            businessLogicUser.idUserType = 1;
            using(context)
            {
                context.Users.Add(businessLogicUser);
                result = context.SaveChanges() > 0;
            }
            return result;
        }

        public Boolean FindUserByEmail(String userEmail)
        {
            Boolean result = false;
            using (context)
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
                    player.user = user;
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
                                  where players.idUser == player.user.idUser 
                                  select players).ToList();
                if (foundPlayers.Count > 0)
                {
                    player.idPlayer = foundPlayers[0].idPlayer;
                    player.playerName = foundPlayers[0].playerName;
                    player.playerDescription = foundPlayers[0].playerDescription;
                    player.playerLevel = foundPlayers[0].playerLevel;
                    player.playerRank = foundPlayers[0].playerRank;
                }
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

    }
}
