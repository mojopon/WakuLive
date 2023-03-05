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
    public class TwitchStreamRepositoryTest
    {
        private ITwitchStreamRepository _twitchStreamRepository;
        private TwitchStreamDataStoreForTest _twitchStreamDataStore;

        [SetUp]
        public void Setup()
        {
            _twitchStreamDataStore= new TwitchStreamDataStoreForTest();
            _twitchStreamRepository = new TwitchStreamRepository(_twitchStreamDataStore);
        }

        [Test]
        public void ConnectStreamTest()
        {
            var channelName = "testchannel";
            var entity = _twitchStreamRepository.ConnectStream(channelName, "");
            Assert.IsNotNull(entity);

            TwitchStreamInformationEntity informationEntity = null;
            entity.StreamInformationObservable.Subscribe(x => informationEntity = x);

            TwitchStreamInformationEntityData data = new TwitchStreamInformationEntityData()
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
            _twitchStreamDataStore.SendStreamInformation(entity.Id, data);
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
    }
}
