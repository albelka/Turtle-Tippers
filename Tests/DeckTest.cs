using Xunit;
using TurtleTippers.Objects;
using System.Collections.Generic;
using System;

namespace TurtleTippers
{
    public class DeckTest : IDisposable
    {
        public DeckTest()
        {
            DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog = turtle_tippers_test;Integrated Security=SSPI;";
        }

        public void Dispose()
        {
            Deck.DeleteAll();
        }

        [Fact]
        public void Test_DecksEmptyAtFirst()
        {
            int result = Deck.GetDecks().Count;

            Assert.Equal(0, result);
        }

        [Fact]
        public void Test_Equal_ReturnsTrueForSameData()
        {
            Deck deck1 = new Deck(1, 1);
            Deck deck2 = new Deck(1, 1);

            Assert.Equal(deck1, deck2);
        }

        // [Fact]
        // public void Test_Save_SavesDeckToDatabase()
        // {
        //     Deck testDeck = new Deck("Squirrel", "Content/img/squirrel.jpg", "A vicious, agile squirrel.", 1, 1, 0);
        //     testDeck.Save();
        //
        //     List<Deck> result = Deck.GetAll();
        //     List<Deck> testList = new List<Deck> {testDeck};
        //
        //     Assert.Equal(testList, result);
        // }
        //
        // [Fact]
        // public void Test_Save_AssignsIdToSavedObject()
        // {
        //     Deck testDeck = new Deck("Squirrel", "Content/img/squirrel.jpg", "A vicious, agile squirrel.", 1, 1, 0);
        //     testDeck.Save();
        //
        //     Deck savedDeck = Deck.GetAll()[0];
        //
        //     int result = savedDeck.Id;
        //     int expected = testDeck.Id;
        //
        //     Assert.Equal(expected, result);
        // }
        //
        // [Fact]
        // public void Test_Find_ReturnsSpecificDeckFromDatabase()
        // {
        //     Deck testDeck = new Deck("Squirrel", "Content/img/squirrel.jpg", "A vicious, agile squirrel.", 1, 1, 0);
        //     testDeck.Save();
        //
        //     Deck result = Deck.Find(testDeck.Id);
        //
        //     Assert.Equal(result, testDeck);
        // }
    }
}
