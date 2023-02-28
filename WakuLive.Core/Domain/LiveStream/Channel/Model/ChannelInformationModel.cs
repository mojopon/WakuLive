using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WakuLive.Core.Domain
{
    public class ChannelInformationModel
    {
        public string BroadcasterName { get; private set; }
        public int ViewerCount { get; private set; }
        public string Title { get; private set; }
        public bool IsStreaming { get; private set; }

        public ChannelInformationModel(ChannelInformationModelData data) 
        {
            BroadcasterName= data.BroadcasterName;
            ViewerCount = data.ViewerCount;
            Title = data.Title;
            IsStreaming = data.IsStreaming;
        }
    }
}
