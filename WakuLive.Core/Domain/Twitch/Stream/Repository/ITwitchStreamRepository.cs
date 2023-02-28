using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WakuLive.Core.Domain
{
    public interface ITwitchStreamRepository
    {
        TwitchStreamEntity ConnectStream(string channelName, string accessToken);
        void DisconnectStream(string id);
    }
}
