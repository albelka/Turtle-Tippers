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
      public static int OtherPlayerId {get; set;}
      public static int Player1Id {get; set;}
      public static int Player2Id {get; set;}
      public static int AttackingDeckId {get; set;}
      public static string TurnPhase {get; set;}
      public static int PartialTurnCount {get; set;}
      public static int WholeTurnCount {get; set;}
      public static List<int> HaveAttackedDeckIds1 {get; set;}
      public static List<int> HaveAttackedDeckIds2 {get; set;}
      public static int StartingCombatCards1 {get; set;}
      public static int StartingCombatCards2 {get; set;}
      public static int DeckSize {get; set;}
      public static int DrawLimit {get; set;}


    public Arena(int arenaPlayer1Id, int arenaPlayer2Id, int currentPlayerId = 0, int attackingDeckId = 0, int id = 0)
    {
      this.Id = id;
      CurrentPlayerId = currentPlayerId;
      OtherPlayerId = 0;
      Player1Id = arenaPlayer1Id;
      Player2Id = arenaPlayer2Id;
      AttackingDeckId = attackingDeckId;
      TurnPhase = "play";
      PartialTurnCount = 0;
      WholeTurnCount = 0;
      HaveAttackedDeckIds1 = new List<int> {};
      HaveAttackedDeckIds2 = new List<int> {};
      StartingCombatCards1 = 0;
      StartingCombatCards2 = 0;
      DeckSize = 30;
      DrawLimit = 3;
    }

    public static void SetCurrentPlayer()
    {
      Random rand1 = new Random();
      int flip = rand1.Next(2);

      if(flip == 0)
      {
          CurrentPlayerId = Player1Id;
          OtherPlayerId = Player2Id;
      }
      else
      {
          CurrentPlayerId = Player2Id;
          OtherPlayerId = Player1Id;
      }
    }

    // Method for when cards fight each other, probably also evaluate if a card dies
    public static void CompareCards(Deck deck1, Deck deck2)
    {
        if(CurrentPlayerId == Player1Id)
        {
          HaveAttackedDeckIds1.Add(deck1.Id);
        }
        else if(CurrentPlayerId == Player2Id)
        {
          HaveAttackedDeckIds2.Add(deck2.Id);
        }

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
            Player player2 = Player.Find(Player2Id);
            StartingCombatCards2 = Deck.GetCardsInPlay(player2).Count;
            HaveAttackedDeckIds1.Clear();
            HaveAttackedDeckIds2.Clear();
            CurrentPlayerId = Player2Id;
            OtherPlayerId = Player1Id;
        }
        else
        {
            Player player1 = Player.Find(Player1Id);
            StartingCombatCards1 = Deck.GetCardsInPlay(player1).Count;
            HaveAttackedDeckIds1.Clear();
            HaveAttackedDeckIds2.Clear();
            CurrentPlayerId = Player1Id;
            OtherPlayerId = Player2Id;
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
        Player player1 = Player.Find(Player1Id);
        Player player2 = Player.Find(Player2Id);

        if(player1.Turtles <= 0 || Deck.GetPlayerDeck(player1).Count + Deck.GetPlayerHand(player1).Count <= 0)
        {
            TurnPhase = "over2";
        }
        else if (Deck.GetPlayerDeck(player2).Count + Deck.GetPlayerHand(player2).Count <= 0 || player2.Turtles <= 0)
        {
          TurnPhase = "over1";
        }
        else
        {
            SwitchCurrentPlayer();

            if(PartialTurnCount % 2 == 0)
            {
              SwitchPhase();
            }

            if(PartialTurnCount % 4 == 0)
            {
              WholeTurnCount += 1;
              SwitchCurrentPlayer();
            }
        }
    }
  }
}
