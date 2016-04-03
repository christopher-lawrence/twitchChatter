using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace twitchChatter.Reply
{
    public class BibleThump : ReplyBase
    {
        public BibleThump (TwitchClient client)
        {
            Client = client;
            Replies = new List<string>
            {
                "get over it PogChamp",
                "FeelsBadMan",
            };
        }
    }
}
