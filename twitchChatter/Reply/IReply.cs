using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace twitchChatter.Reply
{
    public interface IReply
    {
        List<string> Replies { get; set; }
        TwitchClient Client { get; set; }
        
        Task Reply(string message);

        Task Reply(ChatMessage message);
    }

    public class ReplyBase : IReply
    {
        private Random _random { get; set; }
        public List<string> Replies { get; set; }
        public TwitchClient Client { get; set; }

        public ReplyBase()
        {
            _random = new Random((int)DateTime.Now.Ticks);
        }
        
        public Task Reply(ChatMessage message)
        {
            var reply = string.Format("@{0} {1}", message.Username, getRandomReply());
            return Reply(reply);
        }

        public Task Reply(string message)
        {
            return Client.SendChatMessage(message);
        }

        private string getRandomReply()
        {
            return Replies[_random.Next(Replies.Count())];
        }
    }
}
