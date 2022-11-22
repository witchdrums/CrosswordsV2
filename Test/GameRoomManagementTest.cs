using BusinessServices;
using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Test
{
    [TestClass]
    public class GameRoomManagementTest
    {
        [TestMethod]
        public void GetBoardById_Test_Success()
        {
            GameRoomManagement gameRoomManagement = new GameRoomManagement();
            int expectedResult = 18;
            int actualResult = gameRoomManagement.GetBoardById(1).WordsBoards.Count;
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void GetBoardById_Test_Failure1()
        {
            GameRoomManagement gameRoomManagement = new GameRoomManagement();
            int expectedResult = 0;
            int actualResult = gameRoomManagement.GetBoardById(2).WordsBoards.Count;
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestMethod]
        public void GetBoardById_Test_Failure2()
        {
            GameRoomManagement gameRoomManagement = new GameRoomManagement();
            int expectedResult = 0;
            int actualResult = gameRoomManagement.GetBoardById(0).WordsBoards.Count;
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
