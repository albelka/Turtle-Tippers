using System.Data;
using System;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace TurtleTippers.Objects
{
    public class Deck
    {
        public int Id { get; set; }
        public int CardId { get; set; }
        public int PlayerId { get; set; }
        public bool InHand { get; set; }
        public bool InPlay { get; set; }
        public bool Discard { get; set; }
        public int HP { get; set; }

        public Deck(int DeckCardId, int DeckPlayerId, int DeckHP = 1, bool DeckInHand = false, bool DeckInPlay = false, bool DeckDiscard = false, int DeckId = 0)
        {
            this.Id = DeckId;
            this.CardId = DeckCardId;
            this.PlayerId = DeckPlayerId;
            this.InHand = DeckInHand;
            this.InPlay = DeckInPlay;
            this.Discard = DeckDiscard;
            this.HP = DeckHP;
        }


        public override bool Equals(System.Object otherDeck)
        {
            if(!(otherDeck is Deck))
            {
                return false;
            }
            else
            {
                Deck newDeck = (Deck) otherDeck;
                bool idEquality = this.Id == newDeck.Id;
                bool cardIdEquality = this.CardId == newDeck.CardId;
                bool playerIdEquality = this.PlayerId == newDeck.PlayerId;
                bool inHandEquality = this.InHand == newDeck.InHand;
                bool inPlayEquality = this.InPlay == newDeck.InPlay;
                bool discardEquality = this.Discard == newDeck.Discard;
                bool HPEquality = this.HP == newDeck.HP;
                return (idEquality && cardIdEquality && playerIdEquality && inHandEquality && inPlayEquality && inPlayEquality && discardEquality && HPEquality);
            }
        }

        public static void DeleteAll()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("DELETE FROM decks;", conn);

            cmd.ExecuteNonQuery();

            if(conn != null)
            {
                conn.Close();
            }
        }

        public static List<Deck> GetAll()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM decks;", conn);

            SqlDataReader rdr = cmd.ExecuteReader();

            List<Deck> wholeDecks = new List<Deck> {};
            while(rdr.Read())
            {
                int deckId = rdr.GetInt32(0);
                int deckCardId = rdr.GetInt32(1);
                int deckPlayerId = rdr.GetInt32(2);
                bool deckInHand = rdr.GetBoolean(3);
                bool deckInPlay = rdr.GetBoolean(4);
                bool deckDiscard = rdr.GetBoolean(5);
                int deckHP = rdr.GetInt32(6);

                Deck newDeck = new Deck(deckCardId, deckPlayerId, deckHP, deckInHand, deckInPlay, deckDiscard, deckId);
                wholeDecks.Add(newDeck);
            }
            if(rdr != null)
            {
                rdr.Close();
            }
            if(conn != null)
            {
                conn.Close();
            }
            return wholeDecks;
        }

        public static void BuildPlayerDeck(Player player, int deckSize = 30)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT id FROM cards;", conn);

            SqlDataReader rdr = cmd.ExecuteReader();

            List<int> cardIds = new List<int> {};
            while(rdr.Read())
            {
                int newId = rdr.GetInt32(0);
                cardIds.Add(newId);
            }
            if(rdr != null)
            {
                rdr.Close();
            }

            Random rand1 = new Random();

            for(int i = 0; i < deckSize; i++)
            {
                int randomCardId = cardIds[rand1.Next(cardIds.Count-1)];
                Card selectedCard = Card.Find(randomCardId);
                Deck newDeck = new Deck(selectedCard.Id, player.Id, selectedCard.Defense);
                SqlCommand cmd2 = new SqlCommand("INSERT INTO decks (card_id, player_id, in_hand, in_play, discard, HP) VALUES (@CardId, @PlayerId, @InHand, @InPlay, @Discard, @HP);", conn);

                cmd2.Parameters.AddWithValue("@CardId", newDeck.CardId);
                cmd2.Parameters.AddWithValue("@PlayerId", newDeck.PlayerId);
                cmd2.Parameters.AddWithValue("@InHand", newDeck.InHand);
                cmd2.Parameters.AddWithValue("@InPlay", newDeck.InPlay);
                cmd2.Parameters.AddWithValue("@Discard", newDeck.Discard);
                cmd2.Parameters.AddWithValue("@HP", newDeck.HP);

                cmd2.ExecuteNonQuery();
            }

            if(conn != null)
            {
                conn.Close();
            }
        }

        public static List<Deck> GetPlayerDeck(Player player)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM decks WHERE player_id = @PlayerId AND in_hand <> true AND in_play <> true AND discard <> true;", conn);

            cmd.Parameters.AddWithValue("@PlayerId", player.Id);

            SqlDataReader rdr = cmd.ExecuteReader();

            List<Deck> playerDecks = new List<Deck> {};
            while(rdr.Read())
            {
                int deckId = rdr.GetInt32(0);
                int deckCardId = rdr.GetInt32(1);
                int deckPlayerId = rdr.GetInt32(2);
                bool deckInHand = rdr.GetBoolean(3);
                bool deckInPlay = rdr.GetBoolean(4);
                bool deckDiscard = rdr.GetBoolean(5);
                int deckHP = rdr.GetInt32(6);

                Deck newDeck = new Deck(deckCardId, deckPlayerId, deckHP, deckInHand, deckInPlay, deckDiscard, deckId);
                playerDecks.Add(newDeck);
            }
            if(rdr != null)
            {
                rdr.Close();
            }
            if(conn != null)
            {
                conn.Close();
            }
            return playerDecks;
        }

        public static void DrawCard(Player player)
        {
            if(Deck.GetPlayerHand(player).Count < 5)
            {
                SqlConnection conn = DB.Connection();
                conn.Open();

                List<Deck> playerDeck = Deck.GetPlayerDeck(player);
                Deck nextDeckCard = playerDeck[0];
                SqlCommand cmd = new SqlCommand("UPDATE decks in_hand = true WHERE id = @DeckId AND player_id = @PlayerId;", conn);

                cmd.Parameters.AddWithValue("@DeckId", nextDeckCard.Id);
                cmd.Parameters.AddWithValue("@PlayerId", player.Id);

                cmd.ExecuteNonQuery();

                if(conn != null)
                {
                    conn.Close();
                }
            }
        }

        public static List<Card> GetPlayerHand(Player player)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT cards.* FROM cards JOIN decks ON (cards.id = decks.card_id) WHERE cards.player_id = @PlayerId AND deck.in_hand = true;", conn);
            cmd.Parameters.AddWithValue("@PlayerId", player.Id);

            List<Card> handCards = new List<Card> {};
            while(rdr.Read())
            {
                int cardId = rdr.GetInt32(0);
                string cardName = rdr.GetString(1);
                string cardImage = rdr.GetString(2);
                string cardFlavor = rdr.GetString(3);
                int cardAttack = rdr.GetInt32(4);
                int cardDefense = rdr.GetInt32(5);
                int cardRevive = rdr.GetInt32(6);

                Card newCard = new Card(cardName, cardImage, cardFlavor, cardAttack, cardDefense, cardRevive, cardId);
                handCards.Add(newCard);
            }
            if(rdr != null)
            {
                rdr.Close();
            }
            if(conn != null)
            {
                conn.Close();
            }
            return handCards;
        }

    }
}
