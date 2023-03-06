using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WakuLive.Core.Domain;

namespace WakuLive.Core.Data.Twitch.Interface
{
    public interface ITwitchChannelDataStore
    {
        TwitchChannelEntity GetTwitchChannel(string id, string channelName, string accessToken);
    }
}
