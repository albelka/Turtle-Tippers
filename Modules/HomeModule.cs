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
            Player player1 = new Player(5, "player1");
            player1.Save();
            Arena newArena = new Arena();
            newArena.SetCurrentPlayer();
            Deck.BuildPlayerDeck(player1);
            Deck.DrawCard(player1);
            Deck.DrawCard(player1);
            Deck.DrawCard(player1);
            Deck.DrawCard(player1);
            Deck.DrawCard(player1);

            Dictionary<string, object> model = new Dictionary<string, object>();
            model.Add("arena", newArena);
            model.Add("deck", Deck.GetPlayerHand(player1));
            return View["index.cshtml", model];
          };
          Post["/test"] = _ => {
            Console.WriteLine(Request.Form["test"].ToString());
            return View["index.cshtml"];
          };

        }
    }
}
