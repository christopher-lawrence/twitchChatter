using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace twitchChatter
{
    class Program
    {
        public static string ClientId = "CLIENT_ID";
        public static string ClientSecret = "CLIENT_SECRET";
        public static string OAuthToken = "OAUTH_TOKEN";
        public static string RedirectUri = "http://localhost";

        public static string TwitchChatOAuthToken = "IRC_OAUTH_TOKEN";

        public static string host = "irc.chat.twitch.tv";
        public static int port = 80;
        public static string username = "USERNAME";

        public static string channel = "CHANNEL_TO_JOIN";

        static void Main(string[] args)
        {
            var client = new TwitchClient(host, port);
            Task.WhenAll(client.Login(username, TwitchChatOAuthToken));
            Thread.Sleep(5);
            Task.WhenAll(client.Join(channel));

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
