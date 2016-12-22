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

            SqlCommand cmd1 = new SqlCommand("SELECT id FROM cards WHERE tier = 1;", conn);

            SqlDataReader rdr1 = cmd1.ExecuteReader();

            List<int> tier1Ids = new List<int>{};
            while(rdr1.Read())
            {
                int newId = rdr1.GetInt32(0);
                tier1Ids.Add(newId);
            }
            // Console.WriteLine(tier1Ids.ToString());
            if(rdr1 != null)
            {
                rdr1.Close();
            }

            SqlCommand cmd2 = new SqlCommand("SELECT id FROM cards WHERE tier = 2;", conn);

            SqlDataReader rdr2 = cmd2.ExecuteReader();

            List<int> tier2Ids = new List<int>{};
            while(rdr2.Read())
            {
                int newId = rdr2.GetInt32(0);
                tier2Ids.Add(newId);
            }
            // Console.WriteLine(tier1Ids.ToString());
            if(rdr2 != null)
            {
                rdr2.Close();
            }

            Random rand1 = new Random();

            List<int> cardIds = new List<int>{};

            for(int i = 0; i < deckSize; i++)
            {
                int tier = rand1.Next(100);
                if (tier <=  87)
                {
                    cardIds.Add(tier2Ids[(rand1.Next(tier2Ids.Count))]);
                }
                else
                {
                    cardIds.Add(tier1Ids[(rand1.Next(tier1Ids.Count))]);
                }
            }

            for(int i = 0; i < cardIds.Count; i++)
            {
                Card selectedCard = Card.Find(cardIds[i]);
                Deck newDeck = new Deck(selectedCard.Id, player.Id, selectedCard.Defense);
                SqlCommand cmd3 = new SqlCommand("INSERT INTO decks (card_id, player_id, in_hand, in_play, discard, HP) VALUES (@CardId, @PlayerId, @InHand, @InPlay, @Discard, @HP);", conn);

                cmd3.Parameters.AddWithValue("@CardId", newDeck.CardId);
                cmd3.Parameters.AddWithValue("@PlayerId", newDeck.PlayerId);
                cmd3.Parameters.AddWithValue("@InHand", newDeck.InHand);
                cmd3.Parameters.AddWithValue("@InPlay", newDeck.InPlay);
                cmd3.Parameters.AddWithValue("@Discard", newDeck.Discard);
                cmd3.Parameters.AddWithValue("@HP", newDeck.HP);

                cmd3.ExecuteNonQuery();
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
            if(Deck.GetPlayerHand(player).Count < 5 && Deck.GetPlayerDeck(player).Count > 0)
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

        public void PlayCard()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            Player selectedPlayer = Player.Find(this.PlayerId);
            if(this.InHand == true && (this.GetCard().Attack > 0 || this.GetCard().Defense > 0))
            {
                if(selectedPlayer.Turtles < selectedPlayer.MaxTurtles && this.CardId == 4)
                {
                    this.DiscardCard();
                    selectedPlayer.TurtleUnflip(this.GetCard().Revive);
                }
                else if(GetCardsInPlay(selectedPlayer).Count < 6)
                {
                    SqlCommand cmd = new SqlCommand("UPDATE decks SET in_hand = 0, in_play = 1 WHERE id = @DeckId;", conn);
                    cmd.Parameters.AddWithValue("@DeckId", this.Id);
                    cmd.ExecuteNonQuery();
                }
            }
            else if(this.InHand == true)
            {
                this.DiscardCard();
                selectedPlayer.TurtleUnflip(this.GetCard().Revive);
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

        public void DiscardCard()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("UPDATE decks SET in_hand = 0, in_play = 0, discard = 1 WHERE id = @DeckId;", conn);
            cmd.Parameters.AddWithValue("@DeckId", this.Id);

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

        // Reduce hp of the deck card in database, maybe also update the object
        public void TakeDamage(int damageAmount)
        {
            this.HP -= damageAmount;
            if(this.HP < 0)
            {
                this.HP = 0;
            }

            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("UPDATE decks SET HP = @DeckHP WHERE id = @DeckId;", conn);
            cmd.Parameters.AddWithValue("@DeckHP", this.HP);
            cmd.Parameters.AddWithValue("@DeckId", this.Id);

            cmd.ExecuteNonQuery();

            if(conn != null)
            {
                conn.Close();
            }
        }

        public static List<Deck> GetPlayerAnimals(Player player)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT decks.* FROM decks JOIN cards ON (decks.card_id = cards.id) WHERE decks.player_id = @PlayerId AND cards.Revive < 1;", conn);
            cmd.Parameters.AddWithValue("@PlayerId", player.Id);

            SqlDataReader rdr = cmd.ExecuteReader();

            List<Deck> playerAnimals = new List<Deck> {};
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
                playerAnimals.Add(newDeck);
            }
            if(rdr != null)
            {
                rdr.Close();
            }
            if(conn != null)
            {
                conn.Close();
            }
            return playerAnimals;
        }

        public static List<Deck> GetPlayerFruits(Player player)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT decks.* FROM decks JOIN cards ON (decks.card_id = cards.id) WHERE decks.player_id = @PlayerId AND cards.Revive > 0;", conn);
            cmd.Parameters.AddWithValue("@PlayerId", player.Id);

            SqlDataReader rdr = cmd.ExecuteReader();

            List<Deck> playerFruits = new List<Deck> {};
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
                playerFruits.Add(newDeck);
            }
            if(rdr != null)
            {
                rdr.Close();
            }
            if(conn != null)
            {
                conn.Close();
            }
            return playerFruits;
        }
    }
}
