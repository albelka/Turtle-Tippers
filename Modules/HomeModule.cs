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
            return View["index.cshtml"];
          };
          Post["/test"] = _ => {
            Console.WriteLine(Request.Form["test"].ToString());
            return View["index.cshtml"];
          };

        }
    }
}
