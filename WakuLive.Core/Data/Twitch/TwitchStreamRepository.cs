using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WakuLive.Core.Data.DataStore;
using WakuLive.Core.Domain;
using WakuLive.Core.Domain.Twitch.Utility;

namespace WakuLive.Core.Data
{
    public class TwitchStreamRepository : ITwitchStreamRepository
    {
        private Dictionary<string, TwitchStreamDataStore> _dataStoreDic = new Dictionary<string, TwitchStreamDataStore>();

        public TwitchStreamEntity ConnectStream(string channelName, string accessToken)
        {
            var id = CreateId(channelName);
            if (!_dataStoreDic.ContainsKey(id))
            {
                var dataStore = new TwitchStreamDataStore();
                _dataStoreDic.Add(id, dataStore);
                var entity = dataStore.ConnectStream(id, channelName, accessToken);
                return entity;
            }
            else
            {
                return null;
            }
        }

        public void DisconnectStream(string id)
        {
            if (_dataStoreDic.ContainsKey(id))
            {
                _dataStoreDic[id].Dispose();
                _dataStoreDic.Remove(id);
            }
        }

        private string CreateId(string channelName) 
        {
            return TwitchEntityId.Create(channelName);
        }
    }
    
}
