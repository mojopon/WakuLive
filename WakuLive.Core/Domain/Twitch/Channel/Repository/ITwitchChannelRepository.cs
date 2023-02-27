using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WakuLive.Core.Domain
{
    public interface ITwitchChannelRepository
    {
        IObservable<TwitchChannelEntity> GetChannelInformation(string channelId, string accessToken);
    }
}
