using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using BusinessServices;
using Domain;

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
            actualPlayer.user = user;
            actualPlayer = userManagement.GetPlayerInformation(actualPlayer);

            Domain.Players expectedPlayer = new Players();
            expectedPlayer.user = user;
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
            actualPlayer.user = user;
            actualPlayer = userManagement.GetPlayerInformation(actualPlayer);

            Domain.Players expectedPlayer = new Players();
            expectedPlayer.user = user;
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
            actualPlayer.user = user;
            actualPlayer = userManagement.GetPlayerInformation(actualPlayer);

            Domain.Players expectedPlayer = new Players();
            expectedPlayer.user = user;
            expectedPlayer.playerName = "vito";
            expectedPlayer.playerDescription = "asdf";
            expectedPlayer.playerLevel = 1;
            expectedPlayer.playerRank = "asdf";

            Assert.IsNull(actualPlayer.playerName);
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
    }
}
