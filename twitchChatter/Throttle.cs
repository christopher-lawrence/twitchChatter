using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace twitchChatter
{
    public class Throttle
    {
        private DateTime[] _sentMessages;

        private readonly int _MaxMessages = 20;
        private readonly int _MaxSeconds = 30;

        public Throttle()
        {
            _sentMessages = new DateTime[20];
        }

        public bool CanSend(DateTime time)
        {
            if (_sentMessages.Count(m => m != default(DateTime)) < _MaxMessages)
            {
                _sentMessages[_sentMessages.Count(m => m != default(DateTime))] = time;
                return true;
            }

            if ((time - _sentMessages[0]).TotalSeconds > _MaxSeconds)
            {
                Rotate(time);
                return true;
            }

            return false;
        }

        public void Rotate(DateTime time)
        {
            var j = 1;
            for(int i=0; i<_MaxMessages-1;i++, j++)
                _sentMessages[i] = _sentMessages[j];

            _sentMessages[19] = time;
        }
    }
}
