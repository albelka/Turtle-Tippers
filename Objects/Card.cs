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

        public Card(string cardName, string cardImage, string cardFlavor, int cardAttack, int cardDefense, int cardRevive, int cardId = 0)
        {
            this.Id = cardId;
            this.Name = cardName;
            this.Image = cardImage;
            this.FlavorText = cardFlavor;
            this.Attack = cardAttack;
            this.Defense = cardDefense;
            this.Revive = cardRevive;
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
                return(idEquality && nameEquality && imageEquality && flavorEquality && attackEquality && defenseEquality && reviveEquality);
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

            SqlCommand cmd = new SqlCommand("INSERT INTO cards (name, image, flavor_text, attack, defense, revive) OUTPUT INSERTED.id VALUES (@CardName, @CardImage, @CardFlavor, @CardAttack, @CardDefense, @CardRevive);", conn);

            SqlParameter nameParameter = new SqlParameter();
            nameParameter.ParameterName = "@CardName";
            nameParameter.Value = this.Name;

            SqlParameter imageParameter = new SqlParameter();
            imageParameter.ParameterName = "@CardImage";
            imageParameter.Value = this.Image;

            SqlParameter flavorParameter = new SqlParameter();
            flavorParameter.ParameterName = "@CardFlavor";
            flavorParameter.Value = this.FlavorText;

            SqlParameter attackParameter = new SqlParameter();
            attackParameter.ParameterName = "@CardAttack";
            attackParameter.Value = this.Attack;

            SqlParameter defenseParameter = new SqlParameter();
            defenseParameter.ParameterName = "@CardDefense";
            defenseParameter.Value = this.Defense;

            SqlParameter reviveParameter = new SqlParameter();
            reviveParameter.ParameterName = "@CardRevive";
            reviveParameter.Value = this.Revive;

            cmd.Parameters.Add(nameParameter);
            cmd.Parameters.Add(imageParameter);
            cmd.Parameters.Add(flavorParameter);
            cmd.Parameters.Add(attackParameter);
            cmd.Parameters.Add(defenseParameter);
            cmd.Parameters.Add(reviveParameter);

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
    }
}
