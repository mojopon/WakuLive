using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WakuLive.Core.Domain;

namespace WakuLive.Core.Data.Twitch.Interface
{
    public interface ITwitchStreamDataStore
    {
        TwitchStreamEntity GetStream(string id, string channelName, string accessToken);
        IObservable<TwitchStreamInformationEntity> GetStreamInformation(string channelName, string accessToken, Action<Exception> onError);
    }
}
