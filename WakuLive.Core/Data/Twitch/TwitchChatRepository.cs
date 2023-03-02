using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using TwitchLib.Client;
using TwitchLib.Client.Events;
using TwitchLib.Client.Models;
using TwitchLib.Communication.Clients;
using TwitchLib.Communication.Interfaces;
using TwitchLib.Communication.Models;
using TwitchLib.EventSub.Websockets.Client;
using WakuLive.Core.Data;
using WakuLive.Core.Domain;
using WakuLive.Core.Domain.Twitch.Utility;

namespace WakuLive.Core
{
    public class TwitchChatRepository : ITwitchChatRepository
    {
        private ITwitchChatDataStore _dataStore;

        private Dictionary<string, TwitchChatClientEntity> _entityDic = new Dictionary<string, TwitchChatClientEntity>();

        public TwitchChatRepository(ITwitchChatDataStore dataStore) 
        {
            _dataStore = dataStore;
        }

        public TwitchChatClientEntity ConnectChat(string userName, string channelName, string accessToken)
        {
            var id = TwitchEntityId.Create(channelName);
            if (!_entityDic.ContainsKey(id))
            {
                var entity = _dataStore.GetTwitchChatClient(id, userName, channelName, accessToken);
                _entityDic.Add(id, entity);
                return entity;
            }
            else 
            {
                return null;
            }
        }

        public void DisconnectChat(string id)
        {
            if (_entityDic.ContainsKey(id))
            {
                _entityDic[id].Dispose();
                _entityDic.Remove(id);
            }
        }
    }
}
