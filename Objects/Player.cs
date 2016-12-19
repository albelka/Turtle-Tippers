using System.Data;
using System;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace TurtleTippers.Objects
{
    public class Player
    {
        public int Id { get; set; }
        public int Turtles { get; set; }
        public string Name { get; set; }

        public Player(int PlayerTurtles, string PlayerName, int PlayerId = 0)
        {
            this.Id = PlayerId;
            this.Turtles = PlayerTurtles;
            this.Name = PlayerName;
        }

        public override bool Equals(System.Object otherPlayer)
        {
            if(!(otherPlayer is Player))
            {
                return false;
            }
            else
            {
                Player newPlayer = (Player) otherPlayer;
                bool idEquality = this.Id == newPlayer.Id;
                bool turtleEquality = this.Turtles == newPlayer.Turtles;
                bool nameEquality = this.Name == newPlayer.Name;
                return(idEquality && turtleEquality && nameEquality);
            }
        }

        public static void DeleteAll()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("DELETE FROM players;", conn);

            cmd.ExecuteNonQuery();

            if(conn != null)
            {
                conn.Close();
            }
        }

        public static List<Player> GetAll()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM players;", conn);

            SqlDataReader rdr = cmd.ExecuteReader();

            List<Player> allPlayers = new List<Player> {};
            while(rdr.Read())
            {
                int playerId = rdr.GetInt32(0);
                int playerTurtles = rdr.GetInt32(1);
                string playerName = rdr.GetString(2);

                Player newPlayer = new Player(playerTurtles, playerName, playerId);
                allPlayers.Add(newPlayer);
            }
            if(rdr != null)
            {
                rdr.Close();
            }
            if(conn != null)
            {
                conn.Close();
            }
            return allPlayers;
        }

        public void Save()
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("INSERT INTO players (turtles, name) OUTPUT INSERTED.id VALUES (@PlayerTurtles, @PlayerName);", conn);

            cmd.Parameters.AddWithValue("@PlayerTurtles", this.Turtles);
            cmd.Parameters.AddWithValue("@PlayerName", this.Name);

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

        public static Player Find(int searchId)
        {
            SqlConnection conn = DB.Connection();
            conn.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM players WHERE id = @PlayerId;", conn);
            cmd.Parameters.AddWithValue("@PlayerId", searchId);

            SqlDataReader rdr = cmd.ExecuteReader();

            int playerId = 0;
            int playerTurtles = 0;
            string playerName = null;
            while(rdr.Read())
            {
                playerId = rdr.GetInt32(0);
                playerTurtles = rdr.GetInt32(1);
                playerName = rdr.GetString(2);

            }
            Player foundPlayer = new Player(playerTurtles, playerName, playerId);

            if(rdr != null)
            {
                rdr.Close();
            }
            if(conn != null)
            {
                conn.Close();
            }
            return foundPlayer;
        }
    }
}
