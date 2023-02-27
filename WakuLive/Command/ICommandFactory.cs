using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WakuLive.Command
{
    public interface ICommandFactory
    {
        DisconnectChatCommand GetDisconnectChatCommand();
        ConnectChatCommand GetConnectChatCommand();
        OpenConfigurationWindowCommand GetOpenConfigurationWindowCommand();
        LinkTwitchAccountCommand GetLinkTwitchAccountCommand();
        UnlinkTwitchAccountCommand GetUnlinkTwitchAccountCommand();
    }
}
