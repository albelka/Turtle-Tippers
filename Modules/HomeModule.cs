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
            newArena.SetCurrentPlayer();
            Deck.BuildPlayerDeck(player1);
            Deck.BuildPlayerDeck(player2);
            Deck.DrawCard(player1);
            Deck.DrawCard(player1);
            Deck.DrawCard(player1);
            Deck.DrawCard(player1);
            Deck.DrawCard(player1);
            Deck.DrawCard(player2);

            Dictionary<string, object> model = new Dictionary<string, object>();
            model.Add("player1", player1);
            model.Add("player2", player2);
            model.Add("arena", newArena);
            model.Add("deck", Deck.GetPlayerHand(player1));
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
            List<Player> players = Player.GetAll();
            Player player1 = players[0];
            Player player2 = players[1];
            Arena newArena = new Arena(player1.Id, player2.Id);
            Dictionary<string, object> model = new Dictionary<string, object>();
            model.Add("player1", player1);
            model.Add("player2", player2);
            model.Add("arena", newArena);
            model.Add("deck", Deck.GetPlayerHand(player1));
            model.Add("p1InPlay", Deck.GetCardsInPlay(player1));
            model.Add("p2InPlay", Deck.GetCardsInPlay(player2));
            return View["index.cshtml", model];
          };

        }
    }
}
