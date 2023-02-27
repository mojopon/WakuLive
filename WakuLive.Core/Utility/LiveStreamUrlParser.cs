using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WakuLive.Core.Utility;

namespace WakuLive.Utility
{
    public static class LiveStreamUrlParser
    {
        public static ParseResult ParseLiveStreamUrl(string url) 
        {
            if (IsTwitchUrl(url)) 
            {
                return new ParseResult(LiveStreamServiceType.Twitch, GetTwitchUserNameFromUrl(url));
            }

            return new ParseResult(LiveStreamServiceType.None, "");
        }

        public static bool IsTwitchUrl(string url) 
        {
            bool isTwitchUrl = false;
            isTwitchUrl = url.Contains("https://www.twitch.tv/") ||
                          url.Contains("www.twitch.tv/") ||
                          url.Contains("twitch.tv/");
            return isTwitchUrl; 
        }

        public static string GetTwitchUserNameFromUrl(string url) 
        {
            string id = url;
            id = id.Replace("https://www.twitch.tv/", "");
            id = id.Replace("www.twitch.tv/", "");
            id = id.Replace("twitch.tv/", "");
            return id;
        }
    }
}
