using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace twitchChatter.Reply
{
    public class Kappa : ReplyBase
    {
        public Kappa(TwitchClient client)
        {
            Client = client;
            Replies = new List<string>
            {
                "Kappa",
                "Keepo",
                "KappaPride",
                "KappaRoss",
            };
        }
    }
}
