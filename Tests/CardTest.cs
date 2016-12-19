using Xunit;
using TurtleTippers.Objects;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace TurtleTippers
{
    public class CardTest : IDisposable
    {
        public CardTest()
        {
            DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog = turtle_tippers_test;Integrated Security=SSPI;";
        }

        public void Dispose()
        {
            Card.DeleteAll();
        }

        [Fact]
        public void Test_CardsEmptyAtFirst()
        {
            int result = Card.GetAll().Count;

            Assert.Equal(0, result);
        }

        [Fact]
        public void Test_Equal_ReturnsTrueForSameData()
        {
            Card card1 = new Card("Squirrel", "Content/img/squirrel.jpg", "A vicious, agile squirrel.", 1, 1, 0);
            Card card2 = new Card("Squirrel", "Content/img/squirrel.jpg", "A vicious, agile squirrel.", 1, 1, 0);

            Assert.Equal(card1, card2);
        }

        // [Fact]
        // public void Test_Save_SavesCardToDatabase()
        // {
        //     Card testCard = new Card("Some Card Name", "Some Card Description");
        //     testCard.Save();
        //
        //     List<Card> result = Card.GetAll();
        //     List<Card> testList = new List<Card> {testCard};
        //
        //     Assert.Equal(testList, result);
        // }
        //
        // [Fact]
        // public void Test_Save_AssignsIdToSavedObject()
        // {
        //     Card testCard = new Card("Some Card Name", "Some Card Description");
        //     testCard.Save();
        //
        //     Card savedCard = Card.GetAll()[0];
        //
        //     int result = savedCard.Id;
        //     int expected = testCard.Id;
        //
        //     Assert.Equal(expected, result);
        // }
        //
        // [Fact]
        // public void Test_Find_ReturnsSpecificCardFromDatabase()
        // {
        //     Card testCard = new Card("Some Card Name", "Some Card Description");
        //     testCard.Save();
        //
        //     Card result = Card.Find(testCard.Id);
        //
        //     Assert.Equal(result, testCard);
        // }
    }
}
