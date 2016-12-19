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
    }
}
