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
            int result = Deck.GetAll().Count;

            Assert.Equal(0, result);
        }

        [Fact]
        public void Test_Equal_ReturnsTrueForSameData()
        {
            Deck deck1 = new Deck(1, 1);
            Deck deck2 = new Deck(1, 1);

            Assert.Equal(deck1, deck2);
        }

        [Fact]
        public void Test_BuildPlayerDeck_CreatesRandomDeckOf30InDatabase()
        {
            Player newPlayer = new Player(10, "Tom");
            newPlayer.Save();
            // Build a deck for player 1.
            Deck.BuildPlayerDeck(newPlayer.Id);

            int result = Deck.GetAll().Count;
            int expected = 30;

            foreach(Deck deck in Deck.GetAll())
            {
                Console.WriteLine("id: " + deck.Id + ", card_id: " + deck.CardId + ", player_id: " + deck.PlayerId);
            }
            Assert.Equal(expected, result);
        }
    }
}
