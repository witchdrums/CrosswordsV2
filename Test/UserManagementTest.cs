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
    }

}

