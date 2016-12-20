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

        public static Deck Find(int searchId)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM decks WHERE id = @DeckId;", conn);
            cmd.Parameters.AddWithValue("@DeckId", searchId);

            SqlDataReader rdr = cmd.ExecuteReader();

            int deckId = 0;
            int deckCardId = 0;
            int deckPlayerId = 0;
            bool deckInHand = false;
            bool deckInPlay = false;
            bool deckDiscard = false;
            int deckHP = 0;
            while(rdr.Read())
            {
                deckId = rdr.GetInt32(0);
                deckCardId = rdr.GetInt32(1);
                deckPlayerId = rdr.GetInt32(2);
                deckInHand = rdr.GetBoolean(3);
                deckInPlay = rdr.GetBoolean(4);
                deckDiscard = rdr.GetBoolean(5);
                deckHP = rdr.GetInt32(6);
            }
            Deck newDeck = new Deck(deckCardId, deckPlayerId, deckHP, deckInHand, deckInPlay, deckDiscard, deckId);

            if(rdr != null)
            {
                rdr.Close();
            }
            if(conn != null)
            {
                conn.Close();
            }
            return newDeck;
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
                int randomCardId = cardIds[rand1.Next(cardIds.Count)];
                Console.WriteLine(cardIds.Count + " "+ randomCardId);
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

            SqlCommand cmd = new SqlCommand("SELECT * FROM decks WHERE player_id = @PlayerId AND in_hand <> 1 AND in_play <> 1 AND discard <> 1;", conn);

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
                SqlCommand cmd = new SqlCommand("UPDATE decks SET in_hand = 1 WHERE id = @DeckId AND player_id = @PlayerId;", conn);

                cmd.Parameters.AddWithValue("@DeckId", nextDeckCard.Id);
                cmd.Parameters.AddWithValue("@PlayerId", player.Id);

                cmd.ExecuteNonQuery();

                if(conn != null)
                {
                    conn.Close();
                }
            }
        }

        public static List<Deck> GetPlayerHand(Player player)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM decks WHERE player_id = @PlayerId AND in_hand = 1;", conn);
            cmd.Parameters.AddWithValue("@PlayerId", player.Id);

            SqlDataReader rdr = cmd.ExecuteReader();

            List<Deck> playerHands = new List<Deck> {};
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
                playerHands.Add(newDeck);
            }
            if(rdr != null)
            {
                rdr.Close();
            }
            if(conn != null)
            {
                conn.Close();
            }
            return playerHands;
        }

        public static void PlayCard(Player player, int deckId)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            Deck selectedDeck = Deck.Find(deckId);

            if(selectedDeck.InHand == true)
            {
                SqlCommand cmd = new SqlCommand("UPDATE decks SET in_hand = 0, in_play = 1 WHERE id = @DeckId;", conn);
                cmd.Parameters.AddWithValue("@DeckId", deckId);
                cmd.ExecuteNonQuery();
            }
            else
            {
                Console.WriteLine("Attempted to play a card that isn't in the player's hand.");
            }

            if(conn != null)
            {
                conn.Close();
            }
        }

        public static List<Deck> GetCardsInPlay(Player player)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM decks WHERE player_id = @PlayerId AND in_play = 1;", conn);
            cmd.Parameters.AddWithValue("@PlayerId", player.Id);

            SqlDataReader rdr = cmd.ExecuteReader();

            List<Deck> playerPlays = new List<Deck> {};
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
                playerPlays.Add(newDeck);
            }
            if(rdr != null)
            {
                rdr.Close();
            }
            if(conn != null)
            {
                conn.Close();
            }
            return playerPlays;
        }

        public static void DiscardCard(Player player, int deckId)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("UPDATE decks SET in_hand = 0, in_play = 0, discard = 1 WHERE id = @DeckId;", conn);
            cmd.Parameters.AddWithValue("@DeckId", deckId);

            cmd.ExecuteNonQuery();

            if(conn != null)
            {
                conn.Close();
            }
        }

        public Card GetCard()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM cards WHERE id = @CardId;", conn);
            cmd.Parameters.AddWithValue("@CardId", this.CardId);

            SqlDataReader rdr = cmd.ExecuteReader();

            int cardId = 0;
            string cardName = null;
            string cardImage = null;
            string cardFlavor = null;
            int cardAttack = 0;
            int cardDefense = 0;
            int cardRevive = 0;
            int cardTier = 0;
            while(rdr.Read())
            {
                cardId = rdr.GetInt32(0);
                cardName = rdr.GetString(1);
                cardImage = rdr.GetString(2);
                cardFlavor = rdr.GetString(3);
                cardAttack = rdr.GetInt32(4);
                cardDefense = rdr.GetInt32(5);
                cardRevive = rdr.GetInt32(6);
                cardTier = rdr.GetInt32(7);
            }
            Card foundCard = new Card(cardName, cardImage, cardFlavor, cardAttack, cardDefense, cardRevive, cardTier, cardId);

            if(rdr != null)
            {
                rdr.Close();
            }
            if(conn != null)
            {
                conn.Close();
            }
            return foundCard;
        }
    }
}