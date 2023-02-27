using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WakuLive.Core.Domain
{
    public class TwitchChannelEntityData
    {
        public string BroadcasterId { get; set; }
        public string GameId { get; set; }
        public string GameName { get; set; }
        public string Title { get; set; }
        public int Delay { get; set; }
    }
}
