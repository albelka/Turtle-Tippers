using System.Data;
using System;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace TurtleTippers.Objects
{
    public class Arena
    {
      public int Id {get; set;}
      public int CurrentPlayerId {get; set;}
      public int Player1Id {get; set;}
      public int Player2Id {get; set;}


    public Arena(int arenaPlayer1Id, int arenaPlayer2Id, int currentPlayerId = 0, int id = 0)
    {
      this.Id = id;
      this.CurrentPlayerId = currentPlayerId;
      this.Player1Id = arenaPlayer1Id;
      this.Player2Id = arenaPlayer2Id;
    }

    public void SetCurrentPlayer()
    {
      Random rand1 = new Random();
      int flip = rand1.Next(2);

      if(flip == 0)
      {
          this.CurrentPlayerId = this.Player1Id;
      }
      else
      {
          this.CurrentPlayerId = this.Player2Id;
      }
    }

    // Method for when cards fight each other, probably also evaluate if a card dies
    public void CompareCards(Deck deck1, Deck deck2)
    {
        Card card1 = deck1.GetCard();
        Card card2 = deck2.GetCard();

        deck1.TakeDamage(card2.Attack);
        deck2.TakeDamage(card1.Attack);

        if(deck1.HP <= 0)
        {
            deck1.DiscardCard();
        }
        else if(deck2.HP <= 0)
        {
            deck2.DiscardCard();
        }
    }
  }
}
