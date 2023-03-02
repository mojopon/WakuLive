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
    public class TwitchChatDataStore : ITwitchChatDataStore
    {
        public TwitchChatClientEntity GetTwitchChatClient(string id, string userName, string channelName, string accessToken)
        {
            ConnectionCredentials credentials = new ConnectionCredentials(userName, accessToken);
            var clientOptions = new ClientOptions
            {
                MessagesAllowedInPeriod = 750,
                ThrottlingPeriod = TimeSpan.FromSeconds(30),
            };
            WebSocketClient customClient = new WebSocketClient(clientOptions);
            var client = new TwitchClient(customClient);
            client.Initialize(credentials, channelName);

            var entity = CreateEntity(client, id, channelName);

            client.Connect();
            return entity;
        }

        private TwitchChatClientEntity CreateEntity(TwitchClient client, string id, string channelName) 
        {
            var subject = new Subject<TwitchChatMessageEntity>();
            var messageReceivedHandler = CreateMessageReceivedHandler(subject);

            var disposable = Disposable.Create(() =>
                                       {
                                           client.OnMessageReceived -= messageReceivedHandler;
                                           subject.Dispose();
                                       });

            var entity = new TwitchChatClientEntity(id, subject, disposable);

            client.OnMessageReceived += messageReceivedHandler;

            return entity;
        }

        private EventHandler<OnMessageReceivedArgs> CreateMessageReceivedHandler(Subject<TwitchChatMessageEntity> subject) 
        {
            var action = (object sender, OnMessageReceivedArgs e) =>
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
                subject.OnNext(message);
            };

            EventHandler<OnMessageReceivedArgs> handler = (s, e) => action(s, e);
            return handler;
        }
    }
}
