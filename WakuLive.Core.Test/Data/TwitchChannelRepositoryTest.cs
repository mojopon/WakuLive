using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchLib.PubSub.Models.Responses.Messages;
using WakuLive.Core.Data;
using WakuLive.Core.Data.DataStore;
using WakuLive.Core.Domain;

namespace WakuLive.Core.Test.Data
{
    public class TwitchChannelRepositoryTest
    {
        private ITwitchChannelRepository _twitchChannelRepository;
        private TwitchChannelDataStoreForTest _twitchChannelDataStore;

        [SetUp]
        public void Setup()
        {
            _twitchChannelDataStore = new TwitchChannelDataStoreForTest();
            _twitchChannelRepository = new TwitchChannelRepository(_twitchChannelDataStore);
        }

        [Test]
        public void ConnectChannelTest()
        {
            var channelName = "testchannel";
            var entity = _twitchChannelRepository.ConnectChannel(channelName, "");
            Assert.IsNotNull(entity);

            TwitchChannelInformationEntity informationEntity = null;
            entity.ChannelInformationObservable.Subscribe(x => informationEntity = x);

            TwitchChannelInformationEntityData data = new TwitchChannelInformationEntityData()
            {
                BroadcasterId = "123",
                BroadcasterName = channelName,
                GameId = "456",
                GameName = "testgame",
                IsStreaming = true,
                ThumbnailUrl = "thumbnailurl",
                Title = "testchannel title",
                ViewerCount = 100,
            };
            _twitchChannelDataStore.SendChannelInformation(entity.Id, data);
            Assert.IsNotNull(informationEntity);
            Assert.That(informationEntity.BroadcasterId, Is.EqualTo(data.BroadcasterId));
            Assert.That(informationEntity.BroadcasterName, Is.EqualTo(data.BroadcasterName));
            Assert.That(informationEntity.GameId, Is.EqualTo(data.GameId));
            Assert.That(informationEntity.GameName, Is.EqualTo(data.GameName));
            Assert.That(informationEntity.IsStreaming, Is.EqualTo(data.IsStreaming));
            Assert.That(informationEntity.ThumbnailUrl, Is.EqualTo(data.ThumbnailUrl));
            Assert.That(informationEntity.Title, Is.EqualTo(data.Title));
            Assert.That(informationEntity.ViewerCount, Is.EqualTo(data.ViewerCount));
        }

        /// <summary>
        /// 同じチャンネルに2回以上接続を試みた場合、2度目以降はNullとなる事を確認するテスト
        /// </summary>
        [Test]
        public void ConnectChannelTwiceTest()
        {
            var channelName = "testchannel";
            var entity = _twitchChannelRepository.ConnectChannel(channelName, "");
            Assert.IsNotNull(entity);

            entity = _twitchChannelRepository.ConnectChannel(channelName, "");
            Assert.IsNull(entity);
        }

        [Test]
        public void DisconnectChannelTest()
        {
            var channelName = "testchannel";
            var entity = _twitchChannelRepository.ConnectChannel(channelName, "");
            Assert.IsNotNull(entity);
            Assert.IsFalse(entity.IsDisposed);

            _twitchChannelRepository.DisconnectChannel(entity.Id);
            Assert.IsTrue(entity.IsDisposed);

            // 切断後はもう一度接続可能
            entity = _twitchChannelRepository.ConnectChannel(channelName, "");
            Assert.IsNotNull(entity);
            Assert.IsFalse(entity.IsDisposed);
        }
    }
}
