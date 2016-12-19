using System.Data;
using System;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace TurtleTippers.Objects
{
    public class Player
    {
        public int Id { get; set; }
        public int Turtle { get; set; }
        public string Name { get; set; }

        public Player(int PlayerTurtle, string PlayerName, int PlayerId = 0)
        {
            this.Id = PlayerId;
            this.Turtle = PlayerTurtle;
            this.Name = PlayerName;
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
                bool turtleEquality = this.Turtle == newCard.Turtle;
                bool nameEquality = this.Name == newCard.Name;
                return(idEquality && turtleEquality && nameEquality);
            }
        }
    }
}
