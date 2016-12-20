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


    public Arena(int currentPlayerId=0, int id = 0)
    {
      this.Id = id;
      this.CurrentPlayerId = currentPlayerId;
    }

    public void SetCurrentPlayer()
    {
      Random rand1 = new Random();
      int flip = rand1.Next(2);

      List<Player> allPlayers = Player.GetAll();

      this.CurrentPlayerId = allPlayers[flip].Id;
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
