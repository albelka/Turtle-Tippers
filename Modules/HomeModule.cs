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
            Player player1 = new Player(5, "player1");
            player1.Save();
            Player player2 = new Player(5, "player2");
            player2.Save();
            Arena newArena = new Arena(player1.Id, player2.Id);
            Arena.SetCurrentPlayer();
            Player currentPlayer = Player.Find(Arena.CurrentPlayerId);
            Player otherPlayer = Player.Find(Arena.OtherPlayerId);
            Deck.BuildPlayerDeck(player1);
            Deck.BuildPlayerDeck(player2);
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
            return View["index.cshtml", model];
          };

          Post["/playCard"] = _ => {
            Arena.PartialTurnCount += 1;
            Arena.TurnStarter();
            if(Request.Form["handCard"].ToString() != "Nancy.DynamicDictionaryValue" )
            {
              string[] splitInput = Request.Form["handCard"].ToString().Split(',');
              foreach(string input in splitInput)
              {
                Deck selectedDeck = Deck.Find(int.Parse(input));
                selectedDeck.PlayCard();
                Deck.DrawCard(Player.Find(selectedDeck.PlayerId));
              }
            }
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

            return View["index.cshtml", model];
          };

          Post["/combat"] = _ => {
            Arena.PartialTurnCount += 1;
            Arena.TurnStarter();
            Arena.CompareCards(Deck.Find(int.Parse(Request.Form["p1-combat-card"])), Deck.Find(int.Parse(Request.Form["p2-combat-card"])));
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
            return View["index.cshtml", model];
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
            return View["index.cshtml", model];
          };
          Get["/turtle"] = _ => {
            Player currentPlayer = Player.Find(Arena.CurrentPlayerId);
            Player otherPlayer = Player.Find(Arena.OtherPlayerId);
            int cardsInPlay = Deck.GetCardsInPlay(currentPlayer).Count;
            for(int i=0; i<cardsInPlay; i++)
            {
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
            return View["index.cshtml", model];
          };
        }
    }
}
