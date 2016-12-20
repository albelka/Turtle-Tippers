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
            Player.DeleteAll();
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
            Deck.BuildPlayerDeck(newPlayer);

            int result = Deck.GetAll().Count;
            int expected = 30;

            // foreach(Deck deck in Deck.GetAll())
            // {
            //     Console.WriteLine("id: " + deck.Id + ", card_id: " + deck.CardId + ", player_id: " + deck.PlayerId);
            // }
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Test_GetPlayerDeck_ReturnsDeckOf30InDatabaseForPlayerOnly()
        {
            Player newPlayer = new Player(10, "Tom");
            Player otherPlayer = new Player(10, "Mary");
            newPlayer.Save();
            otherPlayer.Save();
            // Build a deck for player 1.
            Deck.BuildPlayerDeck(newPlayer);
            Deck.BuildPlayerDeck(otherPlayer, 20);

            int result = Deck.GetPlayerDeck(newPlayer).Count;
            int expected = 30;
            // foreach(Deck deck in Deck.GetPlayerDeck(newPlayer))
            // {
            //     Console.WriteLine("id: " + deck.Id + ", card_id: " + deck.CardId + ", player_id: " + deck.PlayerId);
            // }
            // foreach(Deck deck in Deck.GetPlayerDeck(otherPlayer))
            // {
            //     Console.WriteLine("id: " + deck.Id + ", card_id: " + deck.CardId + ", player_id: " + deck.PlayerId);
            // }

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Test_GetPlayerHand_Returns5CardsForPlayer()
        {
            Player newPlayer = new Player(10, "Tom");
            newPlayer.Save();
            Deck.BuildPlayerDeck(newPlayer);

            Deck.DrawCard(newPlayer);
            Deck.DrawCard(newPlayer);
            Deck.DrawCard(newPlayer);
            Deck.DrawCard(newPlayer);
            Deck.DrawCard(newPlayer);
            Deck.DrawCard(newPlayer);
            Deck.DrawCard(newPlayer);

            int result = Deck.GetPlayerHand(newPlayer).Count;
            int expected = 5;

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Test_DrawCard_UpdatesDatabaseForPlayerToMoveOneCardToHand()
        {
            Player newPlayer = new Player(10, "Tom");
            newPlayer.Save();
            Deck.BuildPlayerDeck(newPlayer);

            Deck.DrawCard(newPlayer);

            int handResult = Deck.GetPlayerHand(newPlayer).Count;
            int handExpected = 1;

            int deckResult = Deck.GetPlayerDeck(newPlayer).Count;
            int deckExpected = 29;

            Assert.Equal(handExpected, handResult);
            Assert.Equal(deckExpected, deckResult);
        }

        [Fact]
        public void Test_PlayCard_UpdatesDatabaseForPlayerToMoveCardFromHandToPlay()
        {
            Player newPlayer = new Player(10, "Tom");
            newPlayer.Save();
            Deck.BuildPlayerDeck(newPlayer);

            Deck.DrawCard(newPlayer);
            Deck.DrawCard(newPlayer);
            Deck.DrawCard(newPlayer);
            Deck.DrawCard(newPlayer);
            Deck.DrawCard(newPlayer);

            List<Deck> playerHand = Deck.GetPlayerHand(newPlayer);
            Deck.PlayCard(newPlayer, playerHand[0].Id);
            Deck.PlayCard(newPlayer, playerHand[1].Id);

            int handResult = Deck.GetPlayerHand(newPlayer).Count;
            int handExpected = 3;

            int deckResult = Deck.GetPlayerDeck(newPlayer).Count;
            int deckExpected = 25;

            int playResult = Deck.GetCardsInPlay(newPlayer).Count;
            int playExpected = 2;

            Assert.Equal(handExpected, handResult);
            Assert.Equal(deckExpected, deckResult);
            Assert.Equal(playResult, playExpected);
        }

        [Fact]
        public void Test_PlayCard_DoesNotMoveCardToPlayIfNotInHand()
        {
            Player newPlayer = new Player(10, "Tom");
            newPlayer.Save();
            Deck.BuildPlayerDeck(newPlayer);

            Deck.PlayCard(newPlayer, Deck.GetPlayerDeck(newPlayer)[1].Id);

            int playResult = Deck.GetCardsInPlay(newPlayer).Count;
            int playExpected = 0;

            int deckResult = Deck.GetPlayerDeck(newPlayer).Count;
            int deckExpected = 30;

            Assert.Equal(playExpected, playResult);
            Assert.Equal(deckExpected, deckResult);
        }
    }
}
