using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace twitchChatter.Reply
{
    public class Kreygasm : ReplyBase
    {
        public Kreygasm(TwitchClient client)
        {
            Client = client;
            Replies = new List<string>
            {
                "it was good for me too",

            };
        }
    }
}
