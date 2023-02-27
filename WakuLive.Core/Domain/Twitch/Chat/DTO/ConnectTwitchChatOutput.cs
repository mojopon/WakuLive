using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WakuLive.Core.Domain
{
    public class ConnectTwitchChatOutput
    {
        public ChatModel ChatModel { get; private set; }

        public ConnectTwitchChatOutput(ChatModel chatModel)
        {
            ChatModel = chatModel;
        }
    }
}
