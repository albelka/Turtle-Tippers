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
            Player player1 = new Player(3, "player1");
            player1.Save();
            Player player2 = new Player(5, "player2");
            player2.Save();
            Arena newArena = new Arena();
            newArena.SetCurrentPlayer();
            Deck.BuildPlayerDeck(player1);
            Deck.BuildPlayerDeck(player2);
            Deck.DrawCard(player1);
            Deck.DrawCard(player1);
            Deck.DrawCard(player1);
            Deck.DrawCard(player1);
            Deck.DrawCard(player1);
            Deck.DrawCard(player2);

            int played1 = Deck.GetPlayerHand(player1)[0].Id;
            Deck.PlayCard(player1, played1);
            int played2 = Deck.GetPlayerHand(player2)[0].Id;
            Deck.PlayCard(player2, played2);

            Deck.DrawCard(player1);
            
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

            return View["index.cshtml"];
          };

        }
    }
}
