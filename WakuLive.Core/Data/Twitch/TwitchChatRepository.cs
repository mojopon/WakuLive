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
        private Dictionary<string, TwitchChatDataStore> _dataStoreDic = new Dictionary<string, TwitchChatDataStore>();

        public TwitchChatClientEntity ConnectChat(string userName, string channelName, string accessToken)
        {
            var id = TwitchEntityId.Create(channelName);
            if (!_dataStoreDic.ContainsKey(id))
            {
                var dataStore = new TwitchChatDataStore();
                _dataStoreDic.Add(id, dataStore);
                var entity = dataStore.ConnectChat(id, userName, channelName, accessToken);
                return entity;
            }
            else 
            {
                return null;
            }
        }

        public void DisconnectChat(string id)
        {
            if (_dataStoreDic.ContainsKey(id))
            {
                _dataStoreDic[id].Dispose();
                _dataStoreDic.Remove(id);
            }
        }
    }
}
