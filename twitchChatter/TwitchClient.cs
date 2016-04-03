using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace twitchChatter
{
    public class TwitchClient
    {
        private TcpClient _tcpClient;
        private StreamReader _streamReader;
        private StreamWriter _streamWriter;
        private string _username;
        private string _channel;
        private Throttle _throttle;
        

        public TwitchClient(string host, int port)
        {
            _tcpClient = new TcpClient(host, port);
            
            _streamReader = new StreamReader(_tcpClient.GetStream());

            _streamWriter = new StreamWriter(_tcpClient.GetStream());

            _throttle = new Throttle();
        }

        public async Task Login(string username, string password)
        {
            await _streamWriter.WriteLineAsync(string.Format("PASS {0}", password));
            await _streamWriter.WriteLineAsync(string.Format("NICK {0}", username));
            await _streamWriter.WriteLineAsync(string.Format("USER {0} 8 * :{0}", username));
            await _streamWriter.FlushAsync();
        }

        public async Task Join(string channel)
        {
            _channel = channel;
            await _streamWriter.WriteLineAsync(string.Format("JOIN #{0}", channel));
            await _streamWriter.FlushAsync();
        }

        public Task SendChatMessage(string message, params object[] args)
        {
            if (!_throttle.CanSend(DateTime.Now))
            {
                Console.WriteLine("[{0}] - Throttled.", DateTime.Now);
                return Task.FromResult(true);
            }
            Console.WriteLine(string.Format("[{0}] - {1}", DateTime.Now, string.Format(message, args)));
            return sendIrcMessage(string.Format(":{0}!{0}@{0}.tmi.twich.tv PRIVMSG #{1} :{2}",
                _username, _channel, string.Format(message, args)));
        }

        public Task<string> ReadChatMessage()
        {
            return _streamReader.ReadLineAsync();
        }

        public async Task<ChatMessage> WaitForMessage(string message, params object[] args)
        {
            var found = false;
            string chat = null;

            var formattedMessage = string.Format(message, args);

            while (!found)
            {
                chat = await ReadChatMessage();
                if (chat.ToLower().Contains(formattedMessage.ToLower()))
                    found = true;
            }

            return ParseChatMessage(chat);
        }

        public ChatMessage ParseChatMessage(string message)
        {
            try
            {
                if (message == null || message.Length < 2 || !message.Contains("!"))
                    return null;
                var username = message.Substring(1, message.IndexOf('!') - 1);
                var msg = message.Substring(message.LastIndexOf(':'));

                if (username.Contains("derpy_derper"))
                    return null;

                return new ChatMessage { Message = msg, Username = username };
            }
            catch (Exception e)
            {
                Console.WriteLine(string.Format("[{0}] - {1}", DateTime.Now, e.Message));
            }

            return null;
        }

        private async Task sendIrcMessage(string message)
        {
            Thread.Sleep(1);
            await _streamWriter.WriteLineAsync(message);
            await _streamWriter.FlushAsync();
        }
    }
}
