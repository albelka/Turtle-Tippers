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

        public static List<Deck> GetDecks()
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
    }
}
