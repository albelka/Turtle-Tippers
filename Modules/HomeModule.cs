using Nancy;
using TurtleTippers.Objects;
using System.Collections.Generic;
using System;

namespace TurtleTippers
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {
          Get["/"] = _ => {
            Deck.DeleteAll();
            Player.DeleteAll();
            return View["index.cshtml"];
          };

          Post["/game"] = _ => {
            Player player1 = new Player(5, Request.Form["player-1"]);
            player1.Save();
            Player player2 = new Player(5, Request.Form["player-2"]);
            player2.Save();
            Arena newArena = new Arena(player1.Id, player2.Id);
            Arena.SetCurrentPlayer();
            Arena.DeckSize = int.Parse(Request.Form["deck-size"]);
            Arena.DrawLimit = int.Parse(Request.Form["draw-limit"]);
            Player currentPlayer = Player.Find(Arena.CurrentPlayerId);
            Player otherPlayer = Player.Find(Arena.OtherPlayerId);
            Deck.BuildPlayerDeck(player1, Arena.DeckSize);
            Deck.BuildPlayerDeck(player2, Arena.DeckSize);
            for(int i = 0; i<5; i++)
            {
              Deck.DrawCard(player1);
              Deck.DrawCard(player2);
            }
            Dictionary<string, object> model = new Dictionary<string, object>();
            model.Add("turnPhase", Arena.TurnPhase);
            model.Add("player1", player1);
            model.Add("player2", player2);
            model.Add("currentPlayerId", Arena.CurrentPlayerId);
            model.Add("currentPlayerHand", Deck.GetPlayerHand(currentPlayer));
            model.Add("p1InPlay", Deck.GetCardsInPlay(player1));
            model.Add("p2InPlay", Deck.GetCardsInPlay(player2));
            model.Add("currentInPlay", Deck.GetCardsInPlay(currentPlayer));
            model.Add("otherInPlay", Deck.GetCardsInPlay(otherPlayer));
            model.Add("usedCardsPlayer1", Arena.HaveAttackedDeckIds1);
            model.Add("usedCardsPlayer2", Arena.HaveAttackedDeckIds2);
            model.Add("p1Deck", Deck.GetPlayerDeck(player1).Count);
            model.Add("p2Deck", Deck.GetPlayerDeck(player2).Count);
            model.Add("drawLimit", Arena.DrawLimit);
            return View["game.cshtml", model];
          };

          Post["/playCard"] = _ => {
            if(Request.Form["handCard"].ToString() != "Nancy.DynamicDictionaryValue" )
            {
              string[] splitInput = Request.Form["handCard"].ToString().Split(',');
              foreach(string input in splitInput)
              {
                Deck selectedDeck = Deck.Find(int.Parse(input));
                selectedDeck.PlayCard();
              }
              for (int i = 0; i < Arena.DrawLimit; i++)
              {
                Deck.DrawCard(Player.Find(Arena.CurrentPlayerId));
              }
            }
            Arena.PartialTurnCount += 1;
            Arena.TurnStarter();
            Player player1 = Player.Find(Arena.Player1Id);
            Player player2 = Player.Find(Arena.Player2Id);
            Player currentPlayer = Player.Find(Arena.CurrentPlayerId);
            Player otherPlayer = Player.Find(Arena.OtherPlayerId);
            Dictionary<string, object> model = new Dictionary<string, object>();
            model.Add("turnPhase", Arena.TurnPhase);
            model.Add("player1", player1);
            model.Add("player2", player2);
            model.Add("currentPlayerId", Arena.CurrentPlayerId);
            model.Add("currentPlayerHand", Deck.GetPlayerHand(currentPlayer));
            model.Add("p1InPlay", Deck.GetCardsInPlay(player1));
            model.Add("p2InPlay", Deck.GetCardsInPlay(player2));
            model.Add("currentInPlay", Deck.GetCardsInPlay(currentPlayer));
            model.Add("otherInPlay", Deck.GetCardsInPlay(otherPlayer));
            model.Add("usedCardsPlayer1", Arena.HaveAttackedDeckIds1);
            model.Add("usedCardsPlayer2", Arena.HaveAttackedDeckIds2);
            model.Add("p1Deck", Deck.GetPlayerDeck(player1).Count);
            model.Add("p2Deck", Deck.GetPlayerDeck(player2).Count);
            model.Add("drawLimit", Arena.DrawLimit);
            return View["game.cshtml", model];
          };

          Post["/combat"] = _ => {
            Player player1 = Player.Find(Arena.Player1Id);
            Player player2 = Player.Find(Arena.Player2Id);
            Arena.CompareCards(Deck.Find(int.Parse(Request.Form["p1-combat-card"])), Deck.Find(int.Parse(Request.Form["p2-combat-card"])));
            if((Arena.CurrentPlayerId == Arena.Player1Id && Arena.HaveAttackedDeckIds1.Count == Arena.StartingCombatCards1) || (Arena.CurrentPlayerId == Arena.Player2Id && Arena.HaveAttackedDeckIds2.Count == Arena.StartingCombatCards2))
            {
              Arena.PartialTurnCount += 1;
              Arena.TurnStarter();
            }
            player1 = Player.Find(Arena.Player1Id);
            player2 = Player.Find(Arena.Player2Id);
            Player currentPlayer = Player.Find(Arena.CurrentPlayerId);
            Player otherPlayer = Player.Find(Arena.OtherPlayerId);
            Dictionary<string, object> model = new Dictionary<string, object>();
            model.Add("turnPhase", Arena.TurnPhase);
            model.Add("player1", player1);
            model.Add("player2", player2);
            model.Add("currentPlayerId", Arena.CurrentPlayerId);
            model.Add("currentPlayerHand", Deck.GetPlayerHand(currentPlayer));
            model.Add("p1InPlay", Deck.GetCardsInPlay(player1));
            model.Add("p2InPlay", Deck.GetCardsInPlay(player2));
            model.Add("currentInPlay", Deck.GetCardsInPlay(currentPlayer));
            model.Add("otherInPlay", Deck.GetCardsInPlay(otherPlayer));
            model.Add("usedCardsPlayer1", Arena.HaveAttackedDeckIds1);
            model.Add("usedCardsPlayer2", Arena.HaveAttackedDeckIds2);
            model.Add("p1Deck", Deck.GetPlayerDeck(player1).Count);
            model.Add("p2Deck", Deck.GetPlayerDeck(player2).Count);
            model.Add("drawLimit", Arena.DrawLimit);
            return View["game.cshtml", model];
          };
          Get["/combat"] = _ => {
            Arena.PartialTurnCount += 1;
            Arena.TurnStarter();

            Player player1 = Player.Find(Arena.Player1Id);
            Player player2 = Player.Find(Arena.Player2Id);
            Player currentPlayer = Player.Find(Arena.CurrentPlayerId);
            Player otherPlayer = Player.Find(Arena.OtherPlayerId);
            Dictionary<string, object> model = new Dictionary<string, object>();
            model.Add("turnPhase", Arena.TurnPhase);
            model.Add("player1", player1);
            model.Add("player2", player2);
            model.Add("currentPlayerId", Arena.CurrentPlayerId);
            model.Add("currentPlayerHand", Deck.GetPlayerHand(currentPlayer));
            model.Add("p1InPlay", Deck.GetCardsInPlay(player1));
            model.Add("p2InPlay", Deck.GetCardsInPlay(player2));
            model.Add("currentInPlay", Deck.GetCardsInPlay(currentPlayer));
            model.Add("otherInPlay", Deck.GetCardsInPlay(otherPlayer));
            model.Add("usedCardsPlayer1", Arena.HaveAttackedDeckIds1);
            model.Add("usedCardsPlayer2", Arena.HaveAttackedDeckIds2);
            model.Add("p1Deck", Deck.GetPlayerDeck(player1).Count);
            model.Add("p2Deck", Deck.GetPlayerDeck(player2).Count);
            model.Add("drawLimit", Arena.DrawLimit);
            return View["game.cshtml", model];
          };
          Get["/turtle"] = _ => {
            Player currentPlayer = Player.Find(Arena.CurrentPlayerId);
            Player otherPlayer = Player.Find(Arena.OtherPlayerId);
            List<Deck> cardsInPlay = Deck.GetCardsInPlay(currentPlayer);
            for(int i=0; i < cardsInPlay.Count; i++)
            {
              if(Arena.HaveAttackedDeckIds1.IndexOf(cardsInPlay[i].Id) < 0 && Arena.HaveAttackedDeckIds2.IndexOf(cardsInPlay[i].Id) < 0)
              otherPlayer.TurtleFlip();
            }
            Arena.PartialTurnCount += 1;
            Arena.TurnStarter();
            Player player1 = Player.Find(Arena.Player1Id);
            Player player2 = Player.Find(Arena.Player2Id);
            currentPlayer = Player.Find(Arena.CurrentPlayerId);
            otherPlayer = Player.Find(Arena.OtherPlayerId);
            Dictionary<string, object> model = new Dictionary<string, object>();
            model.Add("turnPhase", Arena.TurnPhase);
            model.Add("player1", player1);
            model.Add("player2", player2);
            model.Add("currentPlayerId", Arena.CurrentPlayerId);
            model.Add("currentPlayerHand", Deck.GetPlayerHand(currentPlayer));
            model.Add("p1InPlay", Deck.GetCardsInPlay(player1));
            model.Add("p2InPlay", Deck.GetCardsInPlay(player2));
            model.Add("currentInPlay", Deck.GetCardsInPlay(currentPlayer));
            model.Add("otherInPlay", Deck.GetCardsInPlay(otherPlayer));
            model.Add("usedCardsPlayer1", Arena.HaveAttackedDeckIds1);
            model.Add("usedCardsPlayer2", Arena.HaveAttackedDeckIds2);
            model.Add("p1Deck", Deck.GetPlayerDeck(player1).Count);
            model.Add("p2Deck", Deck.GetPlayerDeck(player2).Count);
            model.Add("drawLimit", Arena.DrawLimit);
            return View["game.cshtml", model];
          };
        }
    }
}
