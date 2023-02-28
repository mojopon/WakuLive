using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WakuLive.Core.Domain
{
    public class ChannelInformationModelData
    {
        public string BroadcasterName { get; set; }
        public int ViewerCount { get; set; }
        public string Title { get; set; }
        public bool IsStreaming { get; set; }
    }
}
