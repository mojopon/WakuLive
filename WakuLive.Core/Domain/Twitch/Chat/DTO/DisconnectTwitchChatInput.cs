using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WakuLive.Core.Domain
{
    public class DisconnectTwitchChatInput
    {
        public string Id { get; private set; }

        public DisconnectTwitchChatInput(string id)
        {
            Id = id;
        }
    }
}
