using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WakuLive.Core.Domain
{
    public class TwitchStreamInformationEntityData
    {
        public int ViewerCount { get; set; }
        public string BroadcasterName { get; set; }
        public string BroadcasterId { get; set; }
        public string GameId { get; set; }
        public string GameName { get; set; }
        public string Title { get; set; }
        public string ThumbnailUrl { get; set; }
        public bool IsStreaming { get; set; }
    }
}
