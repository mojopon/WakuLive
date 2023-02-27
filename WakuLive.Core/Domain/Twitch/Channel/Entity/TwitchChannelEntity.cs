using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WakuLive.Core.Domain
{
    public class TwitchChannelEntity
    {
        public string BroadcasterId { get; private set; }
        public string GameId { get; private set; }
        public string GameName { get; private set; }
        public string Title { get; private set; }
        public int Delay { get; private set; }

        public TwitchChannelEntity(TwitchChannelEntityData data) 
        {
            BroadcasterId = data.BroadcasterId;
            GameId = data.GameId;
            GameName = data.GameName;
            Title = data.Title;
            Delay = data.Delay;
        }
    }
}
