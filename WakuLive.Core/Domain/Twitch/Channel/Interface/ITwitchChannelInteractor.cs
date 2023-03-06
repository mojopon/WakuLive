using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WakuLive.Core.Domain
{
    public interface ITwitchChannelInteractor
    {
        ConnectTwitchChannelOutput ConnectTwitchChannel(ConnectTwitchChannelInput input);
        DisconnectTwitchChannelOutput DisconnectTwitchChannel(DisconnectTwitchChannelInput input);
    }
}
