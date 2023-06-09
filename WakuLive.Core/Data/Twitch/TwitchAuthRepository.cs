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
        private static TwitchAccessTokenDataStore _dataStore = null;
        public TwitchAuthRepository() { }

        public IObservable<string> GetAccessToken()
        {
            if (_dataStore != null) 
            {
                _dataStore.Dispose();
            }

            _dataStore = new TwitchAccessTokenDataStore();
            return _dataStore.GetAccessTokenObservable();
        }
    }
}
