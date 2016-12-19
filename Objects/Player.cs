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
    }
}
