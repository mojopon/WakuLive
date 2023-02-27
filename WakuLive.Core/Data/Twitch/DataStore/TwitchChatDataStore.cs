using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using TwitchLib.Client.Models;
using TwitchLib.Client;
using TwitchLib.Communication.Models;
using WakuLive.Core.Domain;
using TwitchLib.Communication.Clients;
using TwitchLib.Client.Events;
using System.Reactive.Disposables;
using WakuLive.Core.Domain.Twitch.Utility;

namespace WakuLive.Core.Data
{
    public class TwitchChatDataStore : IDisposable
    {
        private CompositeDisposable _disposables = new CompositeDisposable();

        private TwitchClient _client;

        private Subject<TwitchChatMessageEntity> _twitchChatMessageSubject = new Subject<TwitchChatMessageEntity>();

        public TwitchChatDataStore()
        {
            _disposables.Add(_twitchChatMessageSubject);
        }

        public TwitchChatClientEntity ConnectChat(string id, string userName, string channelName, string accessToken)
        {
            ConnectionCredentials credentials = new ConnectionCredentials(userName, accessToken);
            var clientOptions = new ClientOptions
            {
                MessagesAllowedInPeriod = 750,
                ThrottlingPeriod = TimeSpan.FromSeconds(30),
            };
            WebSocketClient customClient = new WebSocketClient(clientOptions);
            _client = new TwitchClient(customClient);
            _client.Initialize(credentials, channelName);

            _client.OnMessageReceived += Client_OnMessageReceived;
            _client.Connect();

            var entity = CreateEntity(id, channelName);
            return entity;
        }

        private TwitchChatClientEntity CreateEntity(string id, string channelName) 
        {
            var entity = new TwitchChatClientEntity(id, _twitchChatMessageSubject);

            return entity;
        }

        private void Client_OnMessageReceived(object sender, OnMessageReceivedArgs e)
        {
            var data = new TwitchChatMessageEntityData()
            {
                Bits = e.ChatMessage.Bits,
                Channel = e.ChatMessage.Channel,
                Color = e.ChatMessage.Color,
                DisplayName = e.ChatMessage.DisplayName,
                Message = e.ChatMessage.Message,
                RoomId = e.ChatMessage.RoomId,
                UserId = e.ChatMessage.UserId,
                UserName = e.ChatMessage.Username,
                UserType = TwitchLibUtilities.ConvertUserType(e.ChatMessage.UserType),
            };
            var message = new TwitchChatMessageEntity(data);
            _twitchChatMessageSubject.OnNext(message);
        }

        public void Dispose()
        {
            _client.Disconnect();
            _disposables.Dispose();
        }
    }
}
