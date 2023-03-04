using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using WakuLive.Core.Data.Twitch.Interface;
using WakuLive.Core.Domain;

namespace WakuLive.Core.Data
{
    public class InMemoryTwitchStreamDataStore : ITwitchStreamDataStore
    {
        public IObservable<TwitchStreamInformationEntity> GetStreamInformation(string channelName, string accessToken, Action<Exception> onError)
        {
            return Observable.Timer(TimeSpan.FromSeconds(1))
                             .Select(x => CreateChannelInformation());
        }

        private int viewerCount = 114;
        private TwitchStreamInformationEntity CreateChannelInformation()
        {
            var data = new TwitchStreamInformationEntityData()
            {
                BroadcasterName = "Test User",
                ViewerCount = viewerCount++,
                Title = "Test Channel",
            };

            Debug.Print("InMemoryTwitchStreamRepository: CreateChannelInformation");

            var entity = new TwitchStreamInformationEntity(data);
            return entity;
        }
    }
}
