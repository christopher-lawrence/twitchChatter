using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace twitchChatter
{
    class Program
    {
        public static string ClientId = "cszuck6wvbeqwwwnviqolurifs8kv2i";
        public static string ClientSecret = "8nm4gyasr1m54ysq2s3llhd2h6md56k";
        public static string OAuthToken = "s92tass9c4p1y69t2zokmfvvi7bcd2";
        public static string RedirectUri = "http://localhost";

        public static string TwitchChatOAuthToken = "oauth:qyers1ehicn3si4jd52lk11x8cla3j";

        public static string host = "irc.chat.twitch.tv";
        public static int port = 80;
        public static string username = "derpy_derper";

        static void Main(string[] args)
        {
            var client = new TwitchClient(host, port);
            Task.WhenAll(client.Login(username, TwitchChatOAuthToken));
            Thread.Sleep(5);
            Task.WhenAll(client.Join("thijshs"));

            var autoReply = new AutoReply(client);

            client.SendChatMessage("Kappa Kappa Kappa");
            

            Task.Run(() => autoReply.Run()).Wait();

            //while (true)
            //{
            //var message = Task.WhenAll(client.WaitForMessage("kappa")).Result[0];

            //client.SendChatMessage("@{0} Kappa", message.Username);
            //}
        }
    }
}
