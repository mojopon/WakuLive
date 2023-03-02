using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WakuLive.Core.Domain
{
    public interface ITwitchChatRepository
    {
        TwitchChatClientEntity ConnectChat(string userName, string channelName, string accessToken);
        void DisconnectChat(string id);
    }
}
