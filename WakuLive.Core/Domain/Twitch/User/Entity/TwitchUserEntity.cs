using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WakuLive.Core.Domain
{
    public class TwitchUserEntity
    {
        public string BroadcasterId { get; private set; }
        public string UserName { get; private set; }
        public string DisplayName { get; private set; }

        public TwitchUserEntity(TwitchUserEntityData data)
        {
            BroadcasterId = data.BroadcasterId;
            UserName = data.UserName;
            DisplayName = data.DisplayName;
        }
    }
}
