using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WakuLive.Core.Data;
using WakuLive.Core.Domain;
using WakuLive.Core.Domain.Twitch.Utility;

namespace WakuLive.Core.Test.Data
{
    public class TwitchChatRepositoryTest
    {
        private ITwitchChatRepository _twitchChatRepository;
        private TwitchChatDataStoreForTest _twitchChatDataStore;

        [SetUp]
        public void Setup()
        {
            _twitchChatDataStore= new TwitchChatDataStoreForTest();
            _twitchChatRepository = new TwitchChatRepository(_twitchChatDataStore);
        }

        /// <summary>
        /// Chat接続時の動作テスト
        /// </summary>
        [Test]
        public void ConnectChatTest()
        {
            var userName = "testuser";
            var channelName = "testchannel";
            var entity = _twitchChatRepository.ConnectChat(userName, channelName, "");
            Assert.IsNotNull(entity);

            TwitchChatMessageEntity messageEntity = null;
            entity.ChatMessageObservable.Subscribe(x => messageEntity = x);

            var messageData = new TwitchChatMessageEntityData()
            {
                Bits = 100,
                Color = new System.Drawing.Color(),
                DisplayName = userName,
                Emotes = new List<TwitchChatEmoteEntity>()
                {
                    new TwitchChatEmoteEntity("425618", 9, 11, "LUL", "http://emotes.com/id/425618"),
                    new TwitchChatEmoteEntity("305954156", 17, 24, "PogChamp", "http://emotes.com/id/305954156"),
                },
                UserName = userName,
                Channel = channelName,
                Message = "testMessage",
                UserId = "123",
                RoomId = "456",
                UserType = UserType.Viewer,
            };
            _twitchChatDataStore.SendChatMessage(entity.Id, messageData);
            Assert.IsNotNull(messageEntity);
            Assert.That(messageEntity.Bits, Is.EqualTo(messageData.Bits));
            Assert.That(messageEntity.Color, Is.EqualTo(messageData.Color));
            Assert.That(messageEntity.DisplayName, Is.EqualTo(messageData.DisplayName));
            Assert.That(messageEntity.Emotes[0].Id, Is.EqualTo(messageData.Emotes[0].Id));
            Assert.That(messageEntity.Emotes[0].StartIndex, Is.EqualTo(messageData.Emotes[0].StartIndex));
            Assert.That(messageEntity.Emotes[0].EndIndex, Is.EqualTo(messageData.Emotes[0].EndIndex));
            Assert.That(messageEntity.Emotes[0].Name, Is.EqualTo(messageData.Emotes[0].Name));
            Assert.That(messageEntity.Emotes[0].ImageUrl, Is.EqualTo(messageData.Emotes[0].ImageUrl));
            Assert.That(messageEntity.Emotes[1].Id, Is.EqualTo(messageData.Emotes[1].Id));
            Assert.That(messageEntity.Emotes[1].StartIndex, Is.EqualTo(messageData.Emotes[1].StartIndex));
            Assert.That(messageEntity.Emotes[1].EndIndex, Is.EqualTo(messageData.Emotes[1].EndIndex));
            Assert.That(messageEntity.Emotes[1].Name, Is.EqualTo(messageData.Emotes[1].Name));
            Assert.That(messageEntity.Emotes[1].ImageUrl, Is.EqualTo(messageData.Emotes[1].ImageUrl));
            Assert.That(messageEntity.UserName, Is.EqualTo(messageData.UserName));
            Assert.That(messageEntity.Message, Is.EqualTo(messageData.Message));
            Assert.That(messageEntity.UserId, Is.EqualTo(messageData.UserId));
            Assert.That(messageEntity.RoomId, Is.EqualTo(messageData.RoomId));
            Assert.That(messageEntity.UserType, Is.EqualTo(messageData.UserType));
        }

        /// <summary>
        /// 同じチャンネルに2回以上接続を試みた場合、2度目以降はNullとなる事を確認するテスト
        /// </summary>
        [Test]
        public void ConnectChatTwiceTest() 
        {
            var userName = "testuser";
            var channelName = "testchannel";
            var entity = _twitchChatRepository.ConnectChat(userName, channelName, "");
            Assert.IsNotNull(entity);
            
            entity = _twitchChatRepository.ConnectChat(userName, channelName, "");
            Assert.IsNull(entity);
        }

        /// <summary>
        /// Chat切断時の動作テスト
        /// </summary>
        [Test]
        public void DisconnectChatTest() 
        {
            var userName = "testuser";
            var channelName = "testchannel";
            var entity = _twitchChatRepository.ConnectChat(userName, channelName, "");
            Assert.IsNotNull(entity);
            Assert.IsFalse(entity.IsDisposed);

            _twitchChatRepository.DisconnectChat(entity.Id);
            Assert.IsTrue(entity.IsDisposed);

            // 切断後はまた接続できるのでその確認
            entity = _twitchChatRepository.ConnectChat(userName, channelName, "");
            Assert.IsNotNull(entity);
        }
    }
}
