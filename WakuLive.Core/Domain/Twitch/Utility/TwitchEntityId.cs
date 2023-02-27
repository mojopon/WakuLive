using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WakuLive.Core.Domain.Twitch.Utility
{
    internal static class TwitchEntityId
    {
        public static string Create(string channelName) 
        {
            return "Twitch-" + channelName;
        }
    }
}
