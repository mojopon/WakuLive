using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WakuLive.Core.Domain;

namespace WakuLive.Core.Data
{
    public class TwitchAuthRepository : ITwitchAuthRepository
    {
        public TwitchAuthRepository() { }

        public IObservable<string> GetAccessToken()
        {
            var accessTokenDataStore = new TwitchAccessTokenDataStore();
            return accessTokenDataStore.GetAccessTokenObservable();
        }
    }
}
