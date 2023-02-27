using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WakuLive.Core.Domain
{
    public class ConnectTwitchStreamInput
    {
        public string ChannelName { get; private set; }
        public string AccessToken { get; private set; }

        public ConnectTwitchStreamInput(string channelName, string accessToken) 
        {
            ChannelName = channelName;
            AccessToken = accessToken;
        }
    }
}
