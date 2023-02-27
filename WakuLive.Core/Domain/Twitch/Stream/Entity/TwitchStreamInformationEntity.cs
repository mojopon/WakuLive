using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WakuLive.Core.Domain
{
    public class TwitchStreamInformationEntity
    {
        public int ViewerCount { get; private set; }
        public string BroadcasterName { get; private set; }
        public string BroadcasterId { get; private set; }
        public string GameId { get; private set; }
        public string GameName { get; private set; }
        public string Title { get; private set; }
        public string ThumbnailUrl { get; private set; }

        public TwitchStreamInformationEntity(TwitchStreamInformationEntityData data)
        {
            ViewerCount = data.ViewerCount;
            BroadcasterName = data.BroadcasterName;
            BroadcasterId = data.BroadcasterId;
            GameId = data.GameId;
            GameName = data.GameName;
            Title = data.Title;
            ThumbnailUrl= data.ThumbnailUrl;
        }
    }
}
