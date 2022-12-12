using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Test
{
    [TestClass]
    public class GameManagementTest
    {
        [TestMethod]
        public void RegisterGame_Test_Success()
        {
            Games game = new Games();
            game.gameDate = DateTime.Now;
            BusinessServices.GameManagement gameManagement = new BusinessServices.GameManagement();

            Assert.IsTrue(gameManagement.RegisterGame(game) > 0);
        }
        [TestMethod]
        public void RegisterGame_Test_Failure1()
        {
            BusinessServices.GameManagement gameManagement = new BusinessServices.GameManagement();

            Assert.IsTrue(gameManagement.RegisterGame(null) == 0);
        }

        [TestMethod]
        public void RegisterGame_Test_Failure2()
        {
            Games game = new Games();
            game.gameDate = DateTime.Now.AddDays(1);
            BusinessServices.GameManagement gameManagement = new BusinessServices.GameManagement();

            Assert.IsTrue(gameManagement.RegisterGame(game) == 0);
        }
    }
}
