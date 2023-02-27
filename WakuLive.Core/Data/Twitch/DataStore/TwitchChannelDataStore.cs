using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Text;
using System.Threading.Tasks;
using TwitchLib.Api;
using TwitchLib.Api.Helix.Models.Channels.GetChannelInformation;
using TwitchLib.Api.Helix.Models.Users.GetUsers;
using WakuLive.Core.Domain;

namespace WakuLive.Core.Data.DataStore
{
    public class TwitchChannelDataStore
    {
        private TwitchAPI api = new TwitchAPI();
        public TwitchChannelDataStore()
        {
            api.Settings.ClientId = "4wimbgsvg8axlcriswl3g7cv8120vc";
        }

        public IObservable<TwitchChannelEntity> GetChannelInformation(string channelId, string accessToken) 
        {
            IObservable<GetChannelInformationResponse> sourceObservable = api.Helix.Channels.GetChannelInformationAsync(channelId, accessToken).ToObservable();
            IObservable<TwitchChannelEntity> observable = sourceObservable.Where(x => (x.Data != null && x.Data.Length > 0))
                                                                                     .Select(x =>
                                                                                     {
                                                                                         var information = x.Data.First();
                                                                                         var data = new TwitchChannelEntityData()
                                                                                         {
                                                                                             BroadcasterId = information.BroadcasterId,
                                                                                             Title = information.Title,
                                                                                             GameId = information.GameId,
                                                                                             GameName = information.GameName,
                                                                                             Delay = information.Delay,
                                                                                         };
                                                                                         return new TwitchChannelEntity(data);
                                                                                     });

            return observable;
        }
    }
}
