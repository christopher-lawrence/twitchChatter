using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using twitchChatter.Reply;

namespace twitchChatter
{
    public class AutoReply
    {
        private TwitchClient _client;
        private Kappa _kappa;
        private BibleThump _bibleThump;
        private Kreygasm _kreygasm;
        private Username _username;

        public AutoReply(TwitchClient client)
        {
            _client = client;

            _kappa = new Kappa(client);
            _bibleThump = new BibleThump(client);
            _kreygasm = new Kreygasm(client);
            _username = new Username(client);
        }

        public List<string> lookup = new List<string>
        {
            "kappa",
            "derpy_derper",
            "biblethump",
            "kreygasm"
        };

        public async Task Run()
        {
            while (true)
            {
                var chat = await _client.ReadChatMessage();
                var found = lookup.FirstOrDefault(l => chat.ToLower().Contains(l));

                if (found != null)
                {
                    try
                    {
                        var parsedChat = _client.ParseChatMessage(chat);
                        if (parsedChat == null)
                            continue;
                        switch (found)
                        {
                            //case "kappa":
                            //    if (parsedChat.Username.ToLower().Contains("kappa"))
                            //        break;
                            //    await _kappa.Reply(parsedChat);
                            //    break;
                            case "derpy_derper":
                                await _username.Reply(parsedChat);
                                break;
                            case "biblethump":
                                await _bibleThump.Reply(parsedChat);
                                break;
                            case "kreygasm":
                                await _kreygasm.Reply(parsedChat);
                                break;
                            default:
                                break;
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(string.Format("[{0}] - {1}", DateTime.Now, e.Message));
                    }
                }

            }
        }
    }
}
