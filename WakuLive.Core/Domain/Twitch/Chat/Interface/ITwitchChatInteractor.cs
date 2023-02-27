using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WakuLive.Core.Domain
{
    public interface ITwitchChatInteractor
    {
        ConnectTwitchChatOutput ConnectTwitchChat(ConnectTwitchChatInput input);
        DisconnectTwitchChatOutput DisconnectTwitchChat(DisconnectTwitchChatInput input);
    }
}
