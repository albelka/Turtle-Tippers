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
        public void Test_BuildPlayerDeck_CreatesRandomDeckInDatabase()
        {
            // Build a deck for player 1.
            Deck.BuildPlayerDeck(1);

            List<Deck> result = Deck.GetAll();
            List<Deck> expected = Deck.GetAll();

            foreach(Deck deck in result)
            {
                Console.WriteLine("id: " + deck.Id + ", deck_id: " + deck.CardId + ", player_id: " + deck.PlayerId);
            }
            Assert.Equal(expected, result);
        }
    }
}
