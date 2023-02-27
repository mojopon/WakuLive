using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using WakuLive.Core.Data.DataStore;
using WakuLive.Core.Domain;

namespace WakuLive.Core.Data
{
    public class TwitchChannelRepository : ITwitchChannelRepository
    {
        public IObservable<TwitchChannelEntity> GetChannelInformation(string channelId, string accessToken)
        {
            var dataStore = new TwitchChannelDataStore();
            return dataStore.GetChannelInformation(channelId, accessToken);
        }
    }
}
