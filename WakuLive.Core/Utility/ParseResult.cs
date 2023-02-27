using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WakuLive.Utility;

namespace WakuLive.Core.Utility
{
    public class ParseResult
    {
        public LiveStreamServiceType LiveStreamServiceType { get; set; }
        public string Id { get; set; }

        public ParseResult(LiveStreamServiceType liveStreamServiceType, string id)
        {
            LiveStreamServiceType = liveStreamServiceType;
            Id = id;
        }
    }
}
