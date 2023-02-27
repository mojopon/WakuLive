using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WakuLive.Core.Domain;

namespace WakuLive.Core.Data
{
    public class TwitchUserRepository : ITwitchUserRepository
    {
        public TwitchUserRepository(){ }

        public IObservable<TwitchUserEntity> GetUser(string userName, string accessToken)
        {
            var dataStore = new TwitchUserDataStore();
            return dataStore.GetUser(userName, accessToken);
        }
    }
}
