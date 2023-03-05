using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using WakuLive.Core.Data.DataStore;
using WakuLive.Core.Data.Twitch.Interface;
using WakuLive.Core.Domain;
using WakuLive.Core.Domain.Twitch.Utility;

namespace WakuLive.Core.Data
{
    public class TwitchStreamRepository : ITwitchStreamRepository
    {
        // チャンネル情報更新のインターバル
        private readonly int _getInterval = 10;

        private ITwitchStreamDataStore _dataStore;
        private Dictionary<string, TwitchStreamEntity> _entityDic = new Dictionary<string, TwitchStreamEntity>();

        public TwitchStreamRepository(ITwitchStreamDataStore dataStore) 
        {
            _dataStore = dataStore;
        }

        public TwitchStreamEntity ConnectStream(string channelName, string accessToken)
        {
            var id = TwitchEntityId.Create(channelName);
            if (!_entityDic.ContainsKey(id))
            {
                var entity = _dataStore.GetTwitchStream(id, channelName, accessToken);
                _entityDic.Add(id, entity);
                return entity;
            }
            else
            {
                return null;
            }
        }

        public void DisconnectStream(string id)
        {
            if (_entityDic.ContainsKey(id))
            {
                _entityDic[id].Dispose();
                _entityDic.Remove(id);
            }
        }
    }
    
}
