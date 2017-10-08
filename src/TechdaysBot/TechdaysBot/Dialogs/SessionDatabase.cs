using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechdaysBot.Dialogs
{
    public class Session
    {
        public string Title { get; set; }
        public string Speaker { get; set; }
        public string Image { get; set; }

        public string[] Topics { get; set; }
    }

    public static class SessionDatabase
    {
        public static readonly List<Session> Sessions = new List<Session>
        {
            new Session { Title = "A shared hololens experience", Topics = new [] { "hololens" }, Speaker = "Dave Smits", Image = "https://pbs.twimg.com/profile_images/841023733997408257/_-9XG4Y3_400x400.jpg" },
            new Session { Title = "Connected Hololens apps", Topics = new [] { "hololens" }, Speaker = "Joost van Schaik", Image = "https://pbs.twimg.com/profile_images/697353461571186688/OQezHRIt_400x400.jpg" },

            new Session { Title = "VSTS Bot: A bots journey by the Visual Studio ALM Rangers", Topics = new [] { "bots" }, Speaker = "Jeffrey Opdam", Image = "https://pbs.twimg.com/profile_images/783727614393540612/I_aGUyEo_400x400.jpg" },
        };
    }
}