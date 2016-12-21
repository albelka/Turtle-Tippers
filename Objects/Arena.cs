using System.Data;
using System;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace TurtleTippers.Objects
{
    public class Arena
    {
      public int Id {get; set;}
      public static int CurrentPlayerId {get; set;}
      public static int Player1Id {get; set;}
      public static int Player2Id {get; set;}
      public static int AttackingDeckId {get; set;}
      public static string TurnPhase {get; set;}
      public static int PartialTurnCount {get; set;}
      public static int WholeTurnCount {get; set;}


    public Arena(int arenaPlayer1Id, int arenaPlayer2Id, int currentPlayerId = 0, int attackingDeckId = 0, int id = 0)
    {
      this.Id = id;
      CurrentPlayerId = currentPlayerId;
      Player1Id = arenaPlayer1Id;
      Player2Id = arenaPlayer2Id;
      AttackingDeckId = attackingDeckId;
      TurnPhase = "play";
      PartialTurnCount = 0;
      WholeTurnCount = 0;
    }

    public static void SetCurrentPlayer()
    {
      Random rand1 = new Random();
      int flip = rand1.Next(2);

      if(flip == 0)
      {
          CurrentPlayerId = Player1Id;
      }
      else
      {
          CurrentPlayerId = Player2Id;
      }
    }

    // Method for when cards fight each other, probably also evaluate if a card dies
    public static void CompareCards(Deck deck1, Deck deck2)
    {
        Card card1 = deck1.GetCard();
        Card card2 = deck2.GetCard();

        deck1.TakeDamage(card2.Attack);
        deck2.TakeDamage(card1.Attack);

        if(deck1.HP <= 0)
        {
            deck1.DiscardCard();
        }
        if(deck2.HP <= 0)
        {
            deck2.DiscardCard();
        }
    }

    public static void SwitchCurrentPlayer()
    {
        if(CurrentPlayerId == Player1Id)
        {
            CurrentPlayerId = Player2Id;
        }
        else
        {
            CurrentPlayerId = Player1Id;
        }
    }

    public static void SwitchPhase()
    {
        if(TurnPhase == "play")
        {
            TurnPhase = "combat";
        }
        else if(TurnPhase == "combat")
        {
            TurnPhase = "play";
        }
    }

    public static void TurnStarter()
    {
        SwitchCurrentPlayer();

        if(PartialTurnCount % 2)
        {
            SwitchPhase();
        }

        if(PartialTurnCount % 4)
        {
            WholeTurnCount += 1;
            SwitchCurrentPlayer();
        }
    }
  }
}
