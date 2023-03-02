using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WakuLive.Core.Domain;

namespace WakuLive.Core.Data
{
    public interface ITwitchChatDataStore
    {
        TwitchChatClientEntity GetTwitchChatClient(string id, string userName, string channelName, string accessToken);
    }
}
