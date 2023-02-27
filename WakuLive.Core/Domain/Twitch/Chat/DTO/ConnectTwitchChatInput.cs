using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WakuLive.Core.Domain
{
    public class ConnectTwitchChatInput
    {
        public string UserName { get; private set; }
        public string ChannelName { get; private set; }
        public string AccessToken { get; private set; }

        public ConnectTwitchChatInput(string userName, string channelName, string accessToken) 
        {
            UserName = userName;
            ChannelName = channelName;
            AccessToken = accessToken;
        }
    }
}
