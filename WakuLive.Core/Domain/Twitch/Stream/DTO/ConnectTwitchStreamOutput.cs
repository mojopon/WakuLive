using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WakuLive.Core.Domain
{
    public class ConnectTwitchStreamOutput
    {
        public ChannelModel ChannelModel { get; private set; }

        public ConnectTwitchStreamOutput(ChannelModel channelModel) 
        {
            ChannelModel = channelModel;
        }
    }
}
