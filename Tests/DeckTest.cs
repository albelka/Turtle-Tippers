using Xunit;
using TurtleTippers.Objects;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
            Deck.BuildPlayerDeck(newPlayer, 100);

            int result = Deck.GetAll().Count;
            int expected = 30;

            List<int> tier1Ids = new List<int>{};
            SqlConnection conn = DB.Connection();
            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT card_id FROM decks WHERE card_id = 3 OR card_id = 5 OR card_id = 10 OR card_id = 15 OR card_id=16", conn);
            SqlDataReader rdr = cmd.ExecuteReader();
            while(rdr.Read())
            {
              tier1Ids.Add(rdr.GetInt32(0));
            }
            if (rdr != null) rdr.Close();
            if (conn != null) conn.Close();

              Console.WriteLine(tier1Ids.Count);              


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
            playerHand[0].PlayCard();
            playerHand[1].PlayCard();

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

            Deck.GetPlayerDeck(newPlayer)[1].PlayCard();

            int playResult = Deck.GetCardsInPlay(newPlayer).Count;
            int playExpected = 0;

            int deckResult = Deck.GetPlayerDeck(newPlayer).Count;
            int deckExpected = 30;

            Assert.Equal(playExpected, playResult);
            Assert.Equal(deckExpected, deckResult);
        }

        [Fact]
        public void Test_DiscardCard_UpdatesDatabaseToDiscardSelectedCard()
        {
            Player newPlayer = new Player(10, "Tom");
            newPlayer.Save();
            Deck.BuildPlayerDeck(newPlayer);

            Deck.DrawCard(newPlayer);
            Deck.DrawCard(newPlayer);

            Deck.GetPlayerHand(newPlayer)[0].DiscardCard();

            int handResult = Deck.GetPlayerHand(newPlayer).Count;
            int handExpected = 1;

            int deckResult = Deck.GetPlayerDeck(newPlayer).Count;
            int deckExpected = 28;

            Assert.Equal(handExpected, handResult);
            Assert.Equal(deckExpected, deckResult);
        }

        [Fact]
        public void Test_TakeDamage_UpdateHPInObjectAndDB()
        {
            Player newPlayer = new Player(10, "Tom");
            newPlayer.Save();
            Deck.BuildPlayerDeck(newPlayer);

            Deck playerCard = Deck.GetPlayerAnimals(newPlayer)[0];
            int startingHP = playerCard.HP;
            playerCard.TakeDamage(4);

            int damagedResult = playerCard.HP;
            int testHP = 0;

            Deck databaseCard = Deck.Find(playerCard.Id);

            int databaseResult = databaseCard.HP;
            int databaseExpected = 0;

            Assert.Equal(true, startingHP > 0);
            Assert.Equal(testHP, damagedResult);
            Assert.Equal(databaseExpected, databaseResult);
        }
    }
}
