using Xunit;
using TurtleTippers.Objects;
using System.Collections.Generic;
using System;

namespace TurtleTippers
{
    public class PlayerTest : IDisposable
    {
        public PlayerTest()
        {
            DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog = turtle_tippers_test;Integrated Security=SSPI;";
        }

        public void Dispose()
        {
            Player.DeleteAll();
        }

        [Fact]
        public void Test_PlayersEmptyAtFirst()
        {
            int result = Player.GetAll().Count;

            Assert.Equal(0, result);
        }

        [Fact]
        public void Test_Equal_ReturnsTrueForSameData()
        {
            Player player1 = new Player(10, "Tom");
            Player player2 = new Player(10, "Tom");

            Assert.Equal(player1, player2);
        }

        [Fact]
        public void Test_Save_SavesPlayerToDatabase()
        {
            Player testPlayer = new Player(10, "Tom");
            testPlayer.Save();

            List<Player> result = Player.GetAll();
            List<Player> testList = new List<Player> {testPlayer};

            Assert.Equal(testList, result);
        }

        [Fact]
        public void Test_Save_AssignsIdToSavedObject()
        {
            Player testPlayer = new Player(10, "Tom");
            testPlayer.Save();

            Player savedPlayer = Player.GetAll()[0];

            int result = savedPlayer.Id;
            int expected = testPlayer.Id;

            Assert.Equal(expected, result);
        }
    }
}
