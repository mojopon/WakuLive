using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WakuLive.Core.Domain
{
    public class ConnectTwitchChannelInput
    {
        public string ChannelName { get; private set; }
        public string AccessToken { get; private set; }

        public ConnectTwitchChannelInput(string channelName, string accessToken) 
        {
            ChannelName = channelName;
            AccessToken = accessToken;
        }
    }
}
