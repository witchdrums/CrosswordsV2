﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BusinessServices;
using Domain;

namespace Test
{
    [TestClass]
    public class PlayerManagementTest
    {
        [TestMethod]
        public void GetPlayersFilteredName_Test_Success()
        {
            BusinessServices.PlayersManagement playersManagement = new BusinessServices.PlayersManagement();
            String testName = "Rob";
            List<Domain.Players> playersTest = playersManagement.GetPlayersFilteredName(testName);

            int expected = 2;
            int result = playersTest.Count;

            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void GetPlayersFilteredName_Test_Success_2()
        {
            BusinessServices.PlayersManagement playersManagement = new BusinessServices.PlayersManagement();
            String testName = "";
            List<Domain.Players> playersTest = playersManagement.GetPlayersFilteredName(testName);

            int expected = 0;
            int result = playersTest.Count;

            Assert.AreNotEqual(expected, result);
        }

        [TestMethod]
        public void GetPlayersFilteredName_Test_Failture()
        {
            BusinessServices.PlayersManagement playersManagement = new BusinessServices.PlayersManagement();
            String testName = "ElDani";
            List<Domain.Players> playersTest = playersManagement.GetPlayersFilteredName(testName);

            int expected = 1;
            int result = playersTest.Count;

            Assert.AreNotEqual(expected, result);
        }

        [TestMethod]
        public void GetFriendsPlayersOfPlayerByUser_Test_Success()
        {
            BusinessServices.PlayersManagement playersManagement = new BusinessServices.PlayersManagement();

            Domain.Players playerTest = new Domain.Players();
            playerTest.idPlayer = 8010;

            List<Domain.Players> playersTest = playersManagement.GetFriendsPlayersOfPlayerByUser(playerTest);


            int expected = 2;
            int result = playersTest.Count;

            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void GetFriendsPlayersOfPlayerByUser_Test_Success_2()
        {
            BusinessServices.PlayersManagement playersManagement = new BusinessServices.PlayersManagement();

            Domain.Players playerTest = new Domain.Players();
            playerTest.idPlayer = 8090;

            List<Domain.Players> playersTest = playersManagement.GetFriendsPlayersOfPlayerByUser(playerTest);


            int expected = 0;
            int result = playersTest.Count;

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void IsFriend_Test_Success()
        {
            BusinessServices.PlayersManagement playersManagement = new BusinessServices.PlayersManagement();

            Domain.Players playerOrigin = new Domain.Players();
            Domain.Players playerTarget = new Domain.Players();

            playerOrigin.idPlayer = 8010;
            playerTarget.idPlayer = 8080;

            bool expected = true;
            bool result = playersManagement.IsFriend(playerOrigin, playerTarget);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void IsFriend_Test_Failure()
        {
            BusinessServices.PlayersManagement playersManagement = new BusinessServices.PlayersManagement();

            Domain.Players playerOrigin = new Domain.Players();
            Domain.Players playerTarget = new Domain.Players();

            playerOrigin.idPlayer = 1340;
            playerTarget.idPlayer = 8080;

            bool expected = true;
            bool result = playersManagement.IsFriend(playerOrigin, playerTarget);

            Assert.AreNotEqual(expected, result);
        }

        //Los test de inserción solo se ejecutan al momento de la demostración para evitar conflictos
        //[TestMethod]
        public void AddFriendPlayer_Test_Success()
        {
            BusinessServices.PlayersManagement playersManagement = new BusinessServices.PlayersManagement();

            Domain.Players playerOrigin = new Domain.Players();
            Domain.Players playerTarget = new Domain.Players();

            playerOrigin.idPlayer = 8080;
            playerTarget.idPlayer = 8010;

            bool expected = true;
            bool result = playersManagement.AddFriendPlayer(playerOrigin, playerTarget);


            Assert.AreEqual(expected, result);
        }

        //[TestMethod]
        public void DeleteFriendPlayer_Test_Success()
        {
            BusinessServices.PlayersManagement playersManagement = new BusinessServices.PlayersManagement();

            Domain.Players playerOrigin = new Domain.Players();
            Domain.Players playerTarget = new Domain.Players();

            playerOrigin.idPlayer = 1;
            playerTarget.idPlayer = 1001;

            bool expected = true;
            bool result = playersManagement.DeleteFriendPlayer(playerOrigin, playerTarget);


            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void RegisterGamesPlayers_Test_Success1()
        {
            GamesPlayers gamePlayer = new GamesPlayers();
            gamePlayer.idPlayer = 1;
            gamePlayer.idGame = 5;
            gamePlayer.gameRank = 1;
            gamePlayer.gameScore = 1;

            BusinessServices.PlayersManagement playersManagement = new BusinessServices.PlayersManagement();
            Assert.IsTrue(playersManagement.RegisterGamesPlayer(gamePlayer));
        }

        [TestMethod]
        public void RegisterGamesPlayers_Test_Success2()
        {
            GamesPlayers gamePlayer = new GamesPlayers();
            gamePlayer.idPlayer = 1;
            gamePlayer.idGame = 5;
            gamePlayer.gameRank = 1;
            gamePlayer.gameScore = 0;

            BusinessServices.PlayersManagement playersManagement = new BusinessServices.PlayersManagement();
            Assert.IsTrue(playersManagement.RegisterGamesPlayer(gamePlayer));
        }

        [TestMethod]
        public void RegisterGamesPlayers_Test_Failure1()
        {
            BusinessServices.PlayersManagement playersManagement = new BusinessServices.PlayersManagement();
            Assert.IsFalse(playersManagement.RegisterGamesPlayer(null));
        }

        [TestMethod]
        public void RegisterGamesPlayers_Test_Failure2()
        {
            GamesPlayers gamePlayer = new GamesPlayers();
            gamePlayer.idPlayer = 0;
            gamePlayer.idGame = 5;
            gamePlayer.gameRank = 1;
            gamePlayer.gameScore = 1;

            BusinessServices.PlayersManagement playersManagement = new BusinessServices.PlayersManagement();
            Assert.IsFalse(playersManagement.RegisterGamesPlayer(gamePlayer));
        }

        [TestMethod]
        public void RegisterGamesPlayers_Test_Failure3()
        {
            GamesPlayers gamePlayer = new GamesPlayers();
            gamePlayer.idPlayer = 1;
            gamePlayer.idGame = 0;
            gamePlayer.gameRank = 1;
            gamePlayer.gameScore = 1;

            BusinessServices.PlayersManagement playersManagement = new BusinessServices.PlayersManagement();
            Assert.IsFalse(playersManagement.RegisterGamesPlayer(gamePlayer));
        }

        [TestMethod]
        public void RegisterGamesPlayers_Test_Failure4()
        {
            GamesPlayers gamePlayer = new GamesPlayers();
            gamePlayer.idPlayer = 1;
            gamePlayer.idGame = 5;
            gamePlayer.gameRank = 0;
            gamePlayer.gameScore = 1;

            BusinessServices.PlayersManagement playersManagement = new BusinessServices.PlayersManagement();
            Assert.IsFalse(playersManagement.RegisterGamesPlayer(gamePlayer));
        }

        [TestMethod]
        public void RegisterGamesPlayers_Test_Failure5()
        {
            GamesPlayers gamePlayer = new GamesPlayers();
            gamePlayer.idPlayer = 1;
            gamePlayer.idGame = 5;
            gamePlayer.gameRank = 5;
            gamePlayer.gameScore = 1;

            BusinessServices.PlayersManagement playersManagement = new BusinessServices.PlayersManagement();
            Assert.IsFalse(playersManagement.RegisterGamesPlayer(gamePlayer));
        }

        [TestMethod]
        public void RegisterGamesPlayers_Test_Failure6()
        {
            GamesPlayers gamePlayer = new GamesPlayers();
            gamePlayer.idPlayer = 1;
            gamePlayer.idGame = 5;
            gamePlayer.gameRank = 1;
            gamePlayer.gameScore = -1;

            BusinessServices.PlayersManagement playersManagement = new BusinessServices.PlayersManagement();
            Assert.IsFalse(playersManagement.RegisterGamesPlayer(gamePlayer));
        }

        [TestMethod]
        public void UpdatePlayerPoints_Test_Success()
        {
            GamesPlayers gamePlayer = new GamesPlayers();
            gamePlayer.idPlayer = 1;
            gamePlayer.idGame = 5;
            gamePlayer.gameRank = 1;
            gamePlayer.gameScore = 1;
            gamePlayer.idPlayer = 2;

            BusinessServices.PlayersManagement playersManagement = new BusinessServices.PlayersManagement();
            Assert.IsTrue(playersManagement.UpdatePlayerPoints(gamePlayer));
        }

        [TestMethod]
        public void UpdatePlayerPoints_Test_Failure1()
        {
            BusinessServices.PlayersManagement playersManagement = new BusinessServices.PlayersManagement();
            Assert.IsFalse(playersManagement.UpdatePlayerPoints(null));
        }

        [TestMethod]
        public void UpdatePlayerPoints_Test_Failure2()
        {
            GamesPlayers gamePlayer = new GamesPlayers();
            gamePlayer.idPlayer = 1;
            gamePlayer.idGame = 5;
            gamePlayer.gameRank = 1;
            gamePlayer.gameScore = 1;
            gamePlayer.idPlayer = 0;

            BusinessServices.PlayersManagement playersManagement = new BusinessServices.PlayersManagement();
            Assert.IsFalse(playersManagement.UpdatePlayerPoints(gamePlayer));
        }
    }
}
