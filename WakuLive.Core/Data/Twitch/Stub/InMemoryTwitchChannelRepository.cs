using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using WakuLive.Core.Domain;

namespace WakuLive.Core.Data.Stub
{
    public class InMemoryTwitchChannelRepository : ITwitchChannelRepository
    {
        public IObservable<TwitchChannelEntity> GetChannelInformation(string channelName, string accessToken)
        {
            var observable = Observable.Timer(TimeSpan.FromSeconds(1))
                                       .Select(x => CreateChannelInformation());

            return observable;
        }

        private TwitchChannelEntity CreateChannelInformation()
        {
            var data = new TwitchChannelEntityData()
            {
                Title = "Test Channel",
            };

            var entity = new TwitchChannelEntity(data);
            return entity;
        }
    }
}
