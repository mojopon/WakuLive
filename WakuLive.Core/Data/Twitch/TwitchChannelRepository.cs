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
    public class TwitchChannelRepository : ITwitchChannelRepository
    {
        // チャンネル情報更新のインターバル
        private readonly int _getInterval = 10;

        private ITwitchChannelDataStore _dataStore;
        private Dictionary<string, TwitchChannelEntity> _entityDic = new Dictionary<string, TwitchChannelEntity>();

        public TwitchChannelRepository(ITwitchChannelDataStore dataStore) 
        {
            _dataStore = dataStore;
        }

        public TwitchChannelEntity ConnectChannel(string channelName, string accessToken)
        {
            var id = TwitchEntityId.Create(channelName);
            if (!_entityDic.ContainsKey(id))
            {
                var entity = _dataStore.GetTwitchChannel(id, channelName, accessToken);
                _entityDic.Add(id, entity);
                return entity;
            }
            else
            {
                return null;
            }
        }

        public void DisconnectChannel(string id)
        {
            if (_entityDic.ContainsKey(id))
            {
                _entityDic[id].Dispose();
                _entityDic.Remove(id);
            }
        }
    }
    
}
