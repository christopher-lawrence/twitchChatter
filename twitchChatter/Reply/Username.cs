using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace twitchChatter.Reply
{
    public class Username : ReplyBase
    {
        public Username(TwitchClient client)
        {
            Client = client;
            Replies = new List<string>
            {
                "no",
                "I am evolved",
                "you wish you were me PJSalt",
            };
        }
    }
}
