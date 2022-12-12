using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using BusinessServices;
using Domain;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using BusinessLogic;

namespace Test
{
    [TestClass]
    public class UserManagementTest
    {
        [TestMethod]
        public void GetPlayerInformation_Test_Success()
        {
            BusinessServices.UserManagement userManagement = new BusinessServices.UserManagement();
            Domain.Users user = new Users();
            user.idUser = 1063;
            Domain.Players actualPlayer = new Players();
            actualPlayer.User = user;
            actualPlayer = userManagement.GetPlayerInformation(actualPlayer);

            Domain.Players expectedPlayer = new Players();
            expectedPlayer.User = user;
            expectedPlayer.playerName = "vito";
            expectedPlayer.playerDescription = "asdf";
            expectedPlayer.playerLevel = 1;
            expectedPlayer.playerRank = "asdf";

            Assert.AreEqual(expectedPlayer.playerName, actualPlayer.playerName);
        }

        [TestMethod]
        public void GetPlayerInformation_Test_Failure1()
        {
            BusinessServices.UserManagement userManagement = new BusinessServices.UserManagement();
            Domain.Users user = new Users();
            user.idUser = 1060;
            Domain.Players actualPlayer = new Players();
            actualPlayer.User = user;
            actualPlayer = userManagement.GetPlayerInformation(actualPlayer);

            Domain.Players expectedPlayer = new Players();
            expectedPlayer.User = user;
            expectedPlayer.playerName = "vito";
            expectedPlayer.playerDescription = "asdf";
            expectedPlayer.playerLevel = 1;
            expectedPlayer.playerRank = "asdf";

            Assert.IsNull(actualPlayer.playerName);
        }

        [TestMethod]
        public void GetPlayerInformation_Test_Failure2()
        {
            BusinessServices.UserManagement userManagement = new BusinessServices.UserManagement();
            Domain.Users user = new Users();
            user.idUser = 0;
            Domain.Players actualPlayer = new Players();
            actualPlayer.User = user;
            actualPlayer = userManagement.GetPlayerInformation(actualPlayer);

            Domain.Players expectedPlayer = new Players();
            expectedPlayer.User = user;
            expectedPlayer.playerName = "vito";
            expectedPlayer.playerDescription = "asdf";
            expectedPlayer.playerLevel = 1;
            expectedPlayer.playerRank = "asdf";

            Assert.IsNull(actualPlayer.playerName);
        }

        [TestMethod]
        public void GetReportCategories_Test_Success()
        {
            BusinessServices.UserManagement userManagement = new BusinessServices.UserManagement();

            List<Categories> categories = userManagement.GetReportCategories();

            Assert.AreEqual(categories.Count, 4);
        }

        [TestMethod]
        public void FindUserByEmail_Test_Success()
        {
            BusinessServices.UserManagement userManagement = new BusinessServices.UserManagement();

            bool expectedResult = false;
            bool actualResult = userManagement.FindUserByEmail("vitocfdz@gmail.com");

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void FindUserByEmail_Test_Success2()
        {
            BusinessServices.UserManagement userManagement = new BusinessServices.UserManagement();

            bool expectedResult = true;
            bool actualResult = userManagement.FindUserByEmail(" ");

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void FindUserByEmail_Test_Success3()
        {
            BusinessServices.UserManagement userManagement = new BusinessServices.UserManagement();

            bool expectedResult = true;
            bool actualResult = userManagement.FindUserByEmail("");

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void FindUserByEmail_Test_Failure()
        {
            BusinessServices.UserManagement userManagement = new BusinessServices.UserManagement();

            bool expectedResult = true;
            bool actualResult = userManagement.FindUserByEmail("4321");

            Assert.AreEqual(expectedResult, actualResult);
        }


        [TestMethod]
        public void RegisterUser_Test_Success()
        {
            BusinessServices.UserManagement userManagement = new BusinessServices.UserManagement();

            Users user = new Users();
            user.password = "password";
            user.username = "username";
            user.email = "email@email.com";

            bool expectedResult = true;
            bool actualResult = userManagement.RegisterUser(user);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void RegisterUser_Test_Failure()
        {
            BusinessServices.UserManagement userManagement = new BusinessServices.UserManagement();

            Users user = new Users();
            user.password = "password";
            user.username = "username2";


            Assert.ThrowsException<DbEntityValidationException>(() => userManagement.RegisterUser(user));
        }

        [TestMethod]
        public void RegisterUser_Test_Failure2()
        {
            BusinessServices.UserManagement userManagement = new BusinessServices.UserManagement();

            Users user = new Users();
            user.password = "password";
            user.email = "email@email.com";


            Assert.ThrowsException<DbEntityValidationException>(() => userManagement.RegisterUser(user));
        }

        [TestMethod]
        public void RegisterUser_Test_Failure3()
        {
            BusinessServices.UserManagement userManagement = new BusinessServices.UserManagement();

            Users user = new Users();
            user.username = "username2";
            user.email = "email@email.com";


            Assert.ThrowsException<DbEntityValidationException>(() => userManagement.RegisterUser(user));
        }

        [TestMethod]
        public void RegisterReport_Test_Success()
        {
            BusinessServices.UserManagement userManagement = new BusinessServices.UserManagement();

            Reports report = new Reports();
            report.idUser = 1063;
            report.idCategory = 1;
            report.chatLog = "";


            bool expectedResult = true;
            bool actualResult = userManagement.RegisterReport(report);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void RegisterReport_Test_Failure1()
        {
            BusinessServices.UserManagement userManagement = new BusinessServices.UserManagement();

            Reports report = new Reports();
            //report.idUser = 1063;
            report.idCategory = 1;
            report.chatLog = "";


            Assert.ThrowsException<System.Data.Entity.Infrastructure.DbUpdateException>(() => userManagement.RegisterReport(report));
        }

        [TestMethod]
        public void RegisterReport_Test_Failure2()
        {
            BusinessServices.UserManagement userManagement = new BusinessServices.UserManagement();

            Reports report = new Reports();
            report.idUser = 1063;
            //report.idCategory = 1;
            report.chatLog = "";


            Assert.ThrowsException<System.Data.Entity.Infrastructure.DbUpdateException>(() => userManagement.RegisterReport(report));
        }

        [TestMethod]
        public void RegisterReport_Test_Failure3()
        {
            BusinessServices.UserManagement userManagement = new BusinessServices.UserManagement();

            Reports report = new Reports();
            report.idUser = 1063;
            report.idCategory = 0;
            report.chatLog = "";


            Assert.ThrowsException<System.Data.Entity.Infrastructure.DbUpdateException>(() => userManagement.RegisterReport(report));
        }

        [TestMethod]
        public void RegisterReport_Test_Failure4()
        {
            BusinessServices.UserManagement userManagement = new BusinessServices.UserManagement();

            Reports report = new Reports();
            report.idUser = 1063;
            report.idCategory = 5;
            report.chatLog = "";


            Assert.ThrowsException<System.Data.Entity.Infrastructure.DbUpdateException>(() => userManagement.RegisterReport(report));
        }

        [TestMethod]
        public void RegisterReport_Test_Failure5()
        {
            BusinessServices.UserManagement userManagement = new BusinessServices.UserManagement();

            Reports report = new Reports();
            report.idUser = 0;
            report.idCategory = 1;
            report.chatLog = "";


            Assert.ThrowsException<System.Data.Entity.Infrastructure.DbUpdateException>(() => userManagement.RegisterReport(report));
        }

        [TestMethod]
        public void RegisterReport_Test_Failure6()
        {
            BusinessServices.UserManagement userManagement = new BusinessServices.UserManagement();

            Reports report = new Reports();
            report.idUser = 1070;
            report.idCategory = 1;
            report.chatLog = "";


            Assert.ThrowsException<System.Data.Entity.Infrastructure.DbUpdateException>(() => userManagement.RegisterReport(report));
        }

        [TestMethod]
        public void GetUserInformationForPlayer_Test_Success()
        {
            BusinessServices.UserManagement userManagement = new BusinessServices.UserManagement();
            Domain.Players playerQuery = new Players();
            playerQuery.idPlayer = 8080;
            Domain.Users getItUser = new Users();
            getItUser = userManagement.GetUserInformationForPlayer(playerQuery);

            Assert.AreEqual(getItUser.idUser, 30);
        }


        [TestMethod]
        public void GetReportedPlayers_Test_Success()
        {
            BusinessServices.UserManagement userManagement = new BusinessServices.UserManagement();
            int expectedResult = 2;
            int actualResult = userManagement.GetReportedUsers().Count;
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void UpdateBanStatus_Test_Success_Ban()
        {
            Users userToBan = new Users();
            userToBan.idUser = 1043;
            userToBan.isBanned = true;
            BusinessServices.UserManagement userManagement = new BusinessServices.UserManagement();
            bool expectedResult = true;
            bool actualResult = userManagement.UpdateUserBanStatus(userToBan);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void UpdateBanStatus_Test_Success_Unban()
        {
            Users userToBan = new Users();
            userToBan.idUser = 1043;
            userToBan.banDate = DateTime.Now.AddDays(2);
            userToBan.isBanned = false;
            BusinessServices.UserManagement userManagement = new BusinessServices.UserManagement();
            bool expectedResult = true;
            bool actualResult = userManagement.UpdateUserBanStatus(userToBan);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void UpdateBanStatus_Test_Failure1()
        {
            Users userToBan = new Users();
            userToBan.idUser = 0;
            userToBan.banDate = DateTime.Now.AddDays(2);
            userToBan.isBanned = false;
            BusinessServices.UserManagement userManagement = new BusinessServices.UserManagement();
            bool expectedResult = false;
            bool actualResult = userManagement.UpdateUserBanStatus(userToBan);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void UpdateBanStatus_Test_Failure2()
        {
            Users userToBan = new Users();
            userToBan.idUser = 99999;
            userToBan.banDate = DateTime.Now.AddDays(2);
            userToBan.isBanned = false;
            BusinessServices.UserManagement userManagement = new BusinessServices.UserManagement();
            bool expectedResult = false;
            bool actualResult = userManagement.UpdateUserBanStatus(userToBan);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void UpdateBanStatus_Test_Failure3()
        {
            Users userToBan = new Users();
            userToBan.idUser = -1;
            userToBan.banDate = DateTime.Now.AddDays(2);
            userToBan.isBanned = false;
            BusinessServices.UserManagement userManagement = new BusinessServices.UserManagement();
            bool expectedResult = false;
            bool actualResult = userManagement.UpdateUserBanStatus(userToBan);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void UpdateBanStatus_Test_Failure4()
        {
            Users userToBan = null;
            BusinessServices.UserManagement userManagement = new BusinessServices.UserManagement();
            bool expectedResult = false;
            bool actualResult = userManagement.UpdateUserBanStatus(userToBan);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void LiftBan_Test_Success()
        {
            Users userToBan = new Users();
            userToBan.idUser = 1043;
            userToBan.banDate = DateTime.Now;
            userToBan.isBanned = true;
            BusinessServices.UserManagement userManagement = new BusinessServices.UserManagement();
            userManagement.UpdateUserBanStatus(userToBan);

            User businessUser = new User();
            using (var context = new CrosswordsContext())
            {
                businessUser = context.Users.Find(userToBan.idUser);
            }
            businessUser.banDate = userToBan.banDate;
            businessUser.isBanned = userToBan.isBanned;

            bool expectedResult = true;
            bool actualResult = userManagement.LiftBan(businessUser);
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void LiftBan_Test_Failure1()
        {
            Users userToBan = new Users();
            userToBan.idUser = 1043;
            userToBan.banDate = DateTime.Now.AddDays(5);
            userToBan.isBanned = true;
            BusinessServices.UserManagement userManagement = new BusinessServices.UserManagement();
            userManagement.UpdateUserBanStatus(userToBan);

            User businessUser = new User();
            using (var context = new CrosswordsContext())
            {
                businessUser = context.Users.Find(userToBan.idUser);
            }
            businessUser.banDate = userToBan.banDate;
            businessUser.isBanned = userToBan.isBanned;

            Assert.IsFalse(userManagement.LiftBan(businessUser));
        }

        [TestMethod]
        public void LiftBan_Test_Failure2()
        {
            Users userToBan = new Users();
            userToBan.idUser = 1043;
            userToBan.banDate = DateTime.Now.AddDays(5);
            userToBan.isBanned = true;
            BusinessServices.UserManagement userManagement = new BusinessServices.UserManagement();
            userManagement.UpdateUserBanStatus(userToBan);

            User businessUser = new User();
            using (var context = new CrosswordsContext())
            {
                businessUser = context.Users.Find(userToBan.idUser);
            }
            businessUser.idUser = 0;
            businessUser.banDate = userToBan.banDate;
            businessUser.isBanned = userToBan.isBanned;

            Assert.IsFalse(userManagement.LiftBan(businessUser));
        }

        [TestMethod]
        public void LiftBan_Test_Failure3()
        {
            User businessUser = new User();
            using (var context = new CrosswordsContext())
            {
                businessUser = context.Users.Find(1043);
            }
            BusinessServices.UserManagement userManagement = new BusinessServices.UserManagement();
            Assert.IsFalse(userManagement.LiftBan(businessUser));
        }

        [TestMethod]
        public void FindUserByUsernameAndPassword_Test_Success()
        {
            Domain.Users user = new Domain.Users();
            user.username = "TheChavis";
            user.password = "e83e8535d6f689493e5819bd60aa3e5fdcba940e6d111ab6fb5c34f24f86496bf3726e2bf4ec59d6d2f5a2aeb1e4f103283e7d64e4f49c03b4c4725cb361e773";
            BusinessServices.UserManagement userManagement = new BusinessServices.UserManagement();
            Domain.Players userResult = userManagement.FindUserByUserNameAndPassword(user);
            Domain.Players userExpected = new Domain.Players();
            userExpected.idPlayer = 1001;
            Assert.AreEqual(userExpected.idPlayer, userResult.idPlayer);
        }

        [TestMethod]
        public void FindUserByUserNameAndPassword_Test_Failure_UsernameNotFound()
        {
            Domain.Users user = new Domain.Users();
            user.username = "TheChahshshshsvis";
            user.password = "e83e8535d6f689493e5819bd60aa3e5fdcba940e6d111ab6fb5c34f24f86496bf3726e2bf4ec59d6d2f5a2aeb1e4f103283e7d64e4f49c03b4c4725cb361e773";
            BusinessServices.UserManagement userManagement = new BusinessServices.UserManagement();
            Domain.Players userResult = userManagement.FindUserByUserNameAndPassword(user);
            Domain.Players userExpected = new Domain.Players();
            userExpected.idPlayer = 1001;
            Assert.AreNotEqual(userExpected.idPlayer, userResult.idPlayer);
        }

        [TestMethod]
        public void FindUserByUserNameAndPassword_Test_Failure_PasswordNotFound()
        {
            Domain.Users user = new Domain.Users();
            user.username = "TheChahshshshsvis";
            user.password = "35d6f689493e5819bd60aa3e5fdcba940e6d111ab6fb5c34f24f86496bf3726e2bf4ec59d6d2f5a2aeb1e4f103283e7d64e4f49c03b4c4725cb361e773";
            BusinessServices.UserManagement userManagement = new BusinessServices.UserManagement();
            Domain.Players userResult = userManagement.FindUserByUserNameAndPassword(user);
            Domain.Players userExpected = new Domain.Players();
            userExpected.idPlayer = 1001;
            Assert.AreNotEqual(userExpected.idPlayer, userResult.idPlayer);
        }

        [TestMethod]
        public void FindUserByUserNameAndPassword_Test_Failure_UsernameNotExist()
        {
            Domain.Users user = new Domain.Users();
            user.password = "35d6f689493e5819bd60aa3e5fdcba940e6d111ab6fb5c34f24f86496bf3726e2bf4ec59d6d2f5a2aeb1e4f103283e7d64e4f49c03b4c4725cb361e773";
            BusinessServices.UserManagement userManagement = new BusinessServices.UserManagement();
            Domain.Players userResult = userManagement.FindUserByUserNameAndPassword(user);
            Domain.Players userExpected = new Domain.Players();
            userExpected.idPlayer = 1001;
            Assert.AreNotEqual(userExpected.idPlayer, userResult.idPlayer);
        }

        [TestMethod]
        public void FindUserByUserNameAndPassword_Test_Failure_PasswordNotExist()
        {
            Domain.Users user = new Domain.Users();
            user.username = "TheChahshshshsvis";
            BusinessServices.UserManagement userManagement = new BusinessServices.UserManagement();
            Domain.Players userResult = userManagement.FindUserByUserNameAndPassword(user);
            Domain.Players userExpected = new Domain.Players();
            userExpected.idPlayer = 1001;
            Assert.AreNotEqual(userExpected.idPlayer, userResult.idPlayer);
        }

        [TestMethod]
        public void GetPlayerRank_Test_Success()
        {
            Domain.Players player = new Domain.Players();
            player.idRank = 6;
            Domain.Players playerRankExpected = new Domain.Players();
            playerRankExpected.Rank.idRank = 4;
            playerRankExpected.Rank.rankName = "Maestro";
            Domain.Players playerResult = new Domain.Players();
            BusinessServices.UserManagement userManagement = new BusinessServices.UserManagement();
            playerResult = userManagement.GetPlayerRank(player);
            Assert.AreEqual(playerRankExpected.Rank.rankName, playerResult.Rank.rankName);
        }

        [TestMethod]
        public void GetPlayerRank_Test_Failure_IdPlayerRankNotExist()
        {
            Domain.Players player = new Domain.Players();
            player.idRank = 16;
            Domain.Players playerRankExpected = new Domain.Players();
            playerRankExpected.Rank.idRank = 4;
            playerRankExpected.Rank.rankName = "Maestro";
            Domain.Players playerResult = new Domain.Players();
            BusinessServices.UserManagement userManagement = new BusinessServices.UserManagement();
            playerResult = userManagement.GetPlayerRank(player);
            Assert.AreNotEqual(playerRankExpected.Rank.rankName, playerResult.Rank.rankName);
        }

        [TestMethod]
        public void GetPlayerRank_Test_Failure_InvalidIdPlayerRank()
        {
            Domain.Players player = new Domain.Players();
            Domain.Players playerRankExpected = new Domain.Players();
            playerRankExpected.Rank.idRank = 4;
            playerRankExpected.Rank.rankName = "Maestro";
            Domain.Players playerResult = new Domain.Players();
            BusinessServices.UserManagement userManagement = new BusinessServices.UserManagement();
            playerResult = userManagement.GetPlayerRank(player);
            Assert.AreNotEqual(playerRankExpected.Rank.rankName, playerResult.Rank.rankName);
        }


        [TestMethod]
        public void GetPlayerProfileImage_Test_Success()
        {
            Domain.Players player = new Domain.Players();
            player.idProfileImage = 2;
            Domain.Players playerProfileImageExpected = new Domain.Players();
            playerProfileImageExpected.ProfileImage.idProfileImage = 2;
            playerProfileImageExpected.ProfileImage.profileImageName = "PurpleBoo";
            Domain.Players playerResult = new Domain.Players();
            BusinessServices.UserManagement userManagement = new BusinessServices.UserManagement();
            playerResult = userManagement.GetPlayerProfileImage(player);
            Assert.AreEqual(playerProfileImageExpected.ProfileImage.profileImageName, playerResult.ProfileImage.profileImageName);
        }

        [TestMethod]
        public void GetPlayerProfileImage_Test_Failure_IdProfileImageNotExist()
        {
            Domain.Players player = new Domain.Players();
            player.idProfileImage = 12;
            Domain.Players playerProfileImageExpected = new Domain.Players();
            playerProfileImageExpected.ProfileImage.idProfileImage = 2;
            playerProfileImageExpected.ProfileImage.profileImageName = "PurpleBoo";
            Domain.Players playerResult = new Domain.Players();
            BusinessServices.UserManagement userManagement = new BusinessServices.UserManagement();
            playerResult = userManagement.GetPlayerProfileImage(player);
            Assert.AreNotEqual(playerProfileImageExpected.ProfileImage.profileImageName, playerResult.ProfileImage.profileImageName);
        }

        [TestMethod]
        public void GetPlayerProfileImage_Test_Failure_InvalidIdProfileImage()
        {
            Domain.Players player = new Domain.Players();
            Domain.Players playerProfileImageExpected = new Domain.Players();
            playerProfileImageExpected.ProfileImage.idProfileImage = 2;
            playerProfileImageExpected.ProfileImage.profileImageName = "PurpleBoo";
            Domain.Players playerResult = new Domain.Players();
            BusinessServices.UserManagement userManagement = new BusinessServices.UserManagement();
            playerResult = userManagement.GetPlayerProfileImage(player);
            Assert.AreNotEqual(playerProfileImageExpected.ProfileImage.profileImageName, playerResult.ProfileImage.profileImageName);
        }

        [TestMethod]
        public void GetPlayerGames_Test_Seccess()
        {
            Domain.Players player = new Domain.Players();
            player.idPlayer = 1001;
            Domain.Players playerExpected = new Domain.Players();
            playerExpected.idPlayer = 1001;
            playerExpected.gamesPlayed = 1;
            BusinessServices.UserManagement userManagement = new BusinessServices.UserManagement();
            Domain.Players playerResult = new Domain.Players();
            playerResult = userManagement.GetPlayerGames(player);
            Assert.AreEqual(playerExpected.gamesPlayed, playerResult.gamesPlayed);
        }

        [TestMethod]
        public void GetPlayerGames_Test_Failure_IdPlayerIsNotExist()
        {
            Domain.Players player = new Domain.Players();
            player.idPlayer = 10001;
            Domain.Players playerExpected = new Domain.Players();
            playerExpected.idPlayer = 1001;
            playerExpected.gamesPlayed = 1;
            BusinessServices.UserManagement userManagement = new BusinessServices.UserManagement();
            Domain.Players playerResult = new Domain.Players();
            playerResult = userManagement.GetPlayerGames(player);
            Assert.AreNotEqual(playerExpected.gamesPlayed, playerResult.gamesPlayed);
        }

        [TestMethod]
        public void GetPlayerGames_Test_Failure_InvalidIdPlayer()
        {
            Domain.Players player = new Domain.Players();
            Domain.Players playerExpected = new Domain.Players();
            playerExpected.idPlayer = 1001;
            playerExpected.gamesPlayed = 1;
            BusinessServices.UserManagement userManagement = new BusinessServices.UserManagement();
            Domain.Players playerResult = new Domain.Players();
            playerResult = userManagement.GetPlayerGames(player);
            Assert.AreNotEqual(playerExpected.gamesPlayed, playerResult.gamesPlayed);
        }

        [TestMethod]
        public void GetPlayerGamesWon_Test_Success()
        {
            Domain.Players player = new Domain.Players();
            player.idPlayer = 1001;
            Domain.Players playerExpected = new Domain.Players();
            playerExpected.idPlayer = 1001;
            playerExpected.gamesWon = 1;
            BusinessServices.UserManagement userManagement = new BusinessServices.UserManagement();
            Domain.Players playerResult = new Domain.Players();
            playerResult = userManagement.GetPlayerGamesWon(player);
            Assert.AreEqual(playerExpected.gamesWon, playerResult.gamesWon);
        }


        [TestMethod]
        public void GetPlayerGamesWon_Test_Failure_IdPlayerNotExist()
        {
            Domain.Players player = new Domain.Players();
            player.idPlayer = 10001;
            Domain.Players playerExpected = new Domain.Players();
            playerExpected.idPlayer = 1001;
            playerExpected.gamesWon = 1;
            BusinessServices.UserManagement userManagement = new BusinessServices.UserManagement();
            Domain.Players playerResult = new Domain.Players();
            playerResult = userManagement.GetPlayerGamesWon(player);
            Assert.AreNotEqual(playerExpected.gamesWon, playerResult.gamesWon);
        }

        [TestMethod]
        public void GetPlayerGamesWon_Test_Failure_InvalidIdPlayer()
        {
            Domain.Players player = new Domain.Players();
            Domain.Players playerExpected = new Domain.Players();
            playerExpected.idPlayer = 1001;
            playerExpected.gamesWon = 1;
            BusinessServices.UserManagement userManagement = new BusinessServices.UserManagement();
            Domain.Players playerResult = new Domain.Players();
            playerResult = userManagement.GetPlayerGamesWon(player);
            Assert.AreNotEqual(playerExpected.gamesWon, playerResult.gamesWon);
        }

        [TestMethod]
        public void FindUserByEmailAndUsername_Test_Seccess()
        {
            Domain.Users user = new Domain.Users();
            user.username = "TheChavis";
            user.email = "javierdominguez0070@gmail.com";
            BusinessServices.UserManagement userManagement = new BusinessServices.UserManagement();
            Assert.IsTrue(userManagement.FindUserByEmailAndUsername(user));
        }

        [TestMethod]
        public void FindUserByEmailAndUsername_Test_Failure_UsernameNotExist()
        {
            Domain.Users user = new Domain.Users();
            user.username = "TheCsdsdhavis";
            user.email = "javierdominguez0070@gmail.com";
            BusinessServices.UserManagement userManagement = new BusinessServices.UserManagement();
            Assert.IsFalse(userManagement.FindUserByEmailAndUsername(user));
        }

        [TestMethod]
        public void FindUserByEmailAndUsername_Test_Failure_InvalidUsername()
        {
            Domain.Users user = new Domain.Users();
            user.email = "javierdominguez0070@gmail.com";
            BusinessServices.UserManagement userManagement = new BusinessServices.UserManagement();
            Assert.IsFalse(userManagement.FindUserByEmailAndUsername(user));
        }

        [TestMethod]
        public void FindUserByEmailAndUsername_Test_Failure_EmailNotExist()
        {
            Domain.Users user = new Domain.Users();
            user.username = "TheChavis";
            user.email = "javierdominguez00979997979770@gmail.com";
            BusinessServices.UserManagement userManagement = new BusinessServices.UserManagement();
            Assert.IsFalse(userManagement.FindUserByEmailAndUsername(user));
        }

        [TestMethod]
        public void FindUserByEmailAndUsername_Test_Failure_InvalidEmail()
        {
            Domain.Users user = new Domain.Users();
            user.username = "TheCsdsdhavis";
            BusinessServices.UserManagement userManagement = new BusinessServices.UserManagement();
            Assert.IsFalse(userManagement.FindUserByEmailAndUsername(user));
        }

        [TestMethod]
        public void RegisterRecoveredPasswordUser_Test_Seccess()
        {
            Domain.Users user = new Domain.Users();
            user.username = "TheChavis";
            user.email = "javierdominguez0070@gmail.com";
            user.password = "hola";
            BusinessServices.UserManagement userManagement = new BusinessServices.UserManagement();
            Assert.IsTrue(userManagement.RegisterRecoveredPasswordUser(user));
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "Not users found")]
        public void RegisterRecoveredPasswordUser_Test_Failure_UserNameNotExist()
        {
            Domain.Users user = new Domain.Users();
            user.username = "ywguygqyuge";
            user.email = "javierdominguez0070@gmail.com";
            user.password = "hola";
            BusinessServices.UserManagement userManagement = new BusinessServices.UserManagement();
            userManagement.RegisterRecoveredPasswordUser(user);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "Not users found")]
        public void RegisterRecoveredPasswordUser_Test_Failure_InvalidUserName()
        {
            Domain.Users user = new Domain.Users();
            user.email = "javierdominguez0070@gmail.com";
            user.password = "hola";
            BusinessServices.UserManagement userManagement = new BusinessServices.UserManagement();
            userManagement.RegisterRecoveredPasswordUser(user);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "Not users found")]
        public void RegisterRecoveredPasswordUser_Test_Failure_EmailNotExist()
        {
            Domain.Users user = new Domain.Users();
            user.username = "TheChavis";
            user.email = "javierdbsjkbaominguez0070@gmail.com";
            user.password = "hola";
            BusinessServices.UserManagement userManagement = new BusinessServices.UserManagement();
            userManagement.RegisterRecoveredPasswordUser(user);
        }


        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "Not users found")]
        public void RegisterRecoveredPasswordUser_Test_Failure_InavalidEmailt()
        {
            Domain.Users user = new Domain.Users();
            user.username = "TheChavis";
            user.password = "hola";
            BusinessServices.UserManagement userManagement = new BusinessServices.UserManagement();
            userManagement.RegisterRecoveredPasswordUser(user);
        }

        [TestMethod]
        [ExpectedException(typeof(DbEntityValidationException), "Invalid password")]
        public void RegisterRecoveredPasswordUser_Test_Failure_InvalidPassword()
        {
            Domain.Users user = new Domain.Users();
            user.username = "TheChavis";
            user.email = "javierdominguez0070@gmail.com";
            BusinessServices.UserManagement userManagement = new BusinessServices.UserManagement();
            userManagement.RegisterRecoveredPasswordUser(user);
        }

        [TestMethod]
        public void GetProfileImages_Test_Seccess()
        {
            int profileImagesExpected = 5;
            BusinessServices.UserManagement userManagement = new BusinessServices.UserManagement();
            List<Domain.ProfileImages> profileImages = new List<Domain.ProfileImages>();
            profileImages = userManagement.GetProfileImages();
            Assert.AreEqual(profileImagesExpected, profileImages.Count);
        }
    }
}

