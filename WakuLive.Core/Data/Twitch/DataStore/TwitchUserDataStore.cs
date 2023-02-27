using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Reactive.Threading.Tasks;
using System.Text;
using System.Threading.Tasks;
using TwitchLib.Api;
using TwitchLib.Api.Helix.Models.Users.GetUsers;
using WakuLive.Core.Domain;

namespace WakuLive.Core.Data
{
    public class TwitchUserDataStore
    {
        private TwitchAPI api = new TwitchAPI();
        public TwitchUserDataStore() 
        {
            api.Settings.ClientId = "4wimbgsvg8axlcriswl3g7cv8120vc";
        }

        public IObservable<TwitchUserEntity> GetUser(string userName, string accessToken)
        {
            IObservable<GetUsersResponse> sourceObservable = api.Helix.Users.GetUsersAsync(null, new List<string>() { userName }, accessToken).ToObservable();
            IObservable<TwitchUserEntity> observable = sourceObservable.Where(x => (x.Users != null && x.Users.Length > 0))
                                                                       .Select(x =>
                                                                       {
                                                                           var user = x.Users.First();
                                                                           var data = new TwitchUserEntityData()
                                                                           {
                                                                               BroadcasterId = user.Id,
                                                                               UserName = user.Login,
                                                                               DisplayName = user.DisplayName,
                                                                           };
                                                                           return new TwitchUserEntity(data);
                                                                       });

            return observable;
        }
    }
}
