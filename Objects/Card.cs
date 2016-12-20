using System.Data;
using System;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace TurtleTippers.Objects
{
    public class Card
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string FlavorText { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int Revive { get; set; }
        public int Tier { get; set; }

        public Card(string cardName, string cardImage, string cardFlavor, int cardAttack, int cardDefense, int cardRevive, int cardTier, int cardId = 0)
        {
            this.Id = cardId;
            this.Name = cardName;
            this.Image = cardImage;
            this.FlavorText = cardFlavor;
            this.Attack = cardAttack;
            this.Defense = cardDefense;
            this.Revive = cardRevive;
            this.Tier = cardTier;
        }

        public override bool Equals(System.Object otherCard)
        {
            if(!(otherCard is Card))
            {
                return false;
            }
            else
            {
                Card newCard = (Card) otherCard;
                bool idEquality = this.Id == newCard.Id;
                bool nameEquality = this.Name == newCard.Name;
                bool imageEquality = this.Image == newCard.Image;
                bool flavorEquality = this.FlavorText == newCard.FlavorText;
                bool attackEquality = this.Attack == newCard.Attack;
                bool defenseEquality = this.Defense == newCard.Defense;
                bool reviveEquality = this.Revive == newCard.Revive;
                bool tierEquality = this.Tier == newCard.Tier;
                return(idEquality && nameEquality && imageEquality && flavorEquality && attackEquality && defenseEquality && reviveEquality && tierEquality);
            }
        }

        public static void DeleteAll()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("DELETE FROM cards;", conn);

            cmd.ExecuteNonQuery();

            if(conn != null)
            {
                conn.Close();
            }
        }

        public static List<Card> GetAll()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM cards;", conn);

            SqlDataReader rdr = cmd.ExecuteReader();

            List<Card> allCards = new List<Card> {};
            while(rdr.Read())
            {
                int cardId = rdr.GetInt32(0);
                string cardName = rdr.GetString(1);
                string cardImage = rdr.GetString(2);
                string cardFlavor = rdr.GetString(3);
                int cardAttack = rdr.GetInt32(4);
                int cardDefense = rdr.GetInt32(5);
                int cardRevive = rdr.GetInt32(6);
                int cardTier = rdr.GetInt32(7);

                Card newCard = new Card(cardName, cardImage, cardFlavor, cardAttack, cardDefense, cardRevive, cardId);
                allCards.Add(newCard);
            }
            if(rdr != null)
            {
                rdr.Close();
            }
            if(conn != null)
            {
                conn.Close();
            }
            return allCards;
        }

        public void Save()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO cards (name, image, flavor_text, attack, defense, revive, tier) OUTPUT INSERTED.id VALUES (@CardName, @CardImage, @CardFlavor, @CardAttack, @CardDefense, @CardRevive, @CardTier);", conn);

            cmd.Parameters.AddWithValue("@CardName", this.Name);
            cmd.Parameters.AddWithValue("@CardImage", this.Image);
            cmd.Parameters.AddWithValue("@CardFlavor", this.FlavorText);
            cmd.Parameters.AddWithValue("@CardAttack", this.Attack);
            cmd.Parameters.AddWithValue("@CardDefense", this.Defense);
            cmd.Parameters.AddWithValue("@CardRevive", this.Revive);
            cmd.Parameters.AddWithValue("@CardTier", this.Tier);

            SqlDataReader rdr = cmd.ExecuteReader();

            while(rdr.Read())
            {
                this.Id = rdr.GetInt32(0);
            }
            if(rdr != null)
            {
                rdr.Close();
            }
            if(conn != null)
            {
                conn.Close();
            }
        }

        public static Card Find(int searchId)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM cards WHERE id = @CardId;", conn);
            cmd.Parameters.AddWithValue("@CardId", searchId);

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
