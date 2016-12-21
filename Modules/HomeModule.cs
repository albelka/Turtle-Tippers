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
            Deck.BuildPlayerDeck(player1);
            Deck.BuildPlayerDeck(player2);
            for(int i = 0; i<5; i++)
            {
              Deck.DrawCard(player1);
              Deck.DrawCard(player2);
            }

            Dictionary<string, object> model = new Dictionary<string, object>();
            model.Add("player1", player1);
            model.Add("player2", player2);
            model.Add("currentPlayerId", Arena.CurrentPlayerId);
            model.Add("deck", Deck.GetPlayerHand(currentPlayer));
            model.Add("p1InPlay", Deck.GetCardsInPlay(player1));
            model.Add("p2InPlay", Deck.GetCardsInPlay(player2));
            return View["index.cshtml", model];
          };

          Post["/playCard"] = _ => {
            string[] splitInput = Request.Form["handCard"].ToString().Split(',');
            foreach(string input in splitInput)
            {
              Deck selectedDeck = Deck.Find(int.Parse(input));
              selectedDeck.PlayCard();
            }
            Player player1 = Player.Find(Arena.Player1Id);
            Player player2 = Player.Find(Arena.Player2Id);
            Arena.SwitchTurn();
            Player currentPlayer = Player.Find(Arena.CurrentPlayerId);
            Dictionary<string, object> model = new Dictionary<string, object>();
            model.Add("player1", player1);
            model.Add("player2", player2);
            model.Add("currentPlayerId", Arena.CurrentPlayerId);
            model.Add("deck", Deck.GetPlayerHand(currentPlayer));
            model.Add("p1InPlay", Deck.GetCardsInPlay(player1));
            model.Add("p2InPlay", Deck.GetCardsInPlay(player2));

            return View["index.cshtml", model];
          };

        }
    }
}
