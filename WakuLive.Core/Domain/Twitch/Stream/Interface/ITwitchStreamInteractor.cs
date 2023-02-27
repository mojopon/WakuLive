using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WakuLive.Core.Domain
{
    public interface ITwitchStreamInteractor
    {
        ConnectTwitchStreamOutput ConnectTwitchStream(ConnectTwitchStreamInput input);
        DisconnectTwitchStreamOutput DisconnectTwitchStream(DisconnectTwitchStreamInput input);
    }
}
