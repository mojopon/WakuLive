using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using WakuLive.Core.Domain;

namespace WakuLive.Core.Test.Data
{
    public class TwitchChannelEntityTranslatorTest
    {
        string id = "twitch-test";
        TwitchChannelEntity entity = null;
        Subject<TwitchChannelInformationEntity> subject = null;
        IDisposable disposable = null;
        // TwitchChannelEntityのセットアップ
        [SetUp] 
        public void SetUp() 
        {
            subject = new Subject<TwitchChannelInformationEntity>();
            disposable = Disposable.Create(() =>
            {
                subject.OnCompleted();
            });

            entity = new TwitchChannelEntity(id, subject, disposable);
        }

        [Test]
        public void TranslateTwitchChannelEntityTest() 
        {
            var translator = new TwitchChannelEntityTranslator();
            var model = translator.Translate(entity);
            Assert.IsNotNull(model);

            ChannelInformationModel channelInformationModel = null;
            Exception error = null;
            bool isCompleted = false;
            model.ChannelInformationObservable.Subscribe(x => channelInformationModel = x, ex => error = ex, () => isCompleted = true);

            Assert.IsNull(channelInformationModel);
            Assert.IsFalse(isCompleted);
            Assert.IsNull(error);

            // ChannelInformationEntityが正しく通知去れるかどうかのテスト
            var data = new TwitchChannelInformationEntityData()
            {
                BroadcasterId = "test",
                GameId = "test gameid",
                GameName = "test game",
                BroadcasterName = "test broadcaster",
                IsStreaming = true,
                Title = "test title",
                ThumbnailUrl = "test thumbnail url",
                ViewerCount = 1234,
            };
            var channelInformationEntity = new TwitchChannelInformationEntity(data);
            subject.OnNext(channelInformationEntity);

            Assert.IsNotNull(channelInformationModel);
            Assert.AreEqual(data.BroadcasterName, channelInformationModel.BroadcasterName);
            Assert.AreEqual(data.Title, channelInformationModel.Title);
            Assert.AreEqual(data.IsStreaming, channelInformationModel.IsStreaming);
            Assert.AreEqual(data.ThumbnailUrl, channelInformationModel.ThumbnailUrl);
            Assert.AreEqual(data.ViewerCount, channelInformationModel.ViewerCount);


            // OnCompleteが正しく通知されるかテスト
            Assert.IsFalse(isCompleted);
            subject.OnCompleted();
            Assert.IsTrue(isCompleted);
        }

        [Test]
        public void TranslateTwitchChannelEntityErrorTest() 
        {
            var translator = new TwitchChannelEntityTranslator();
            var model = translator.Translate(entity);
            Assert.IsNotNull(model);

            Exception error = null;
            model.ChannelInformationObservable.Subscribe(x => { }, ex => error = ex, () => { });

            // エラーが正しく通知されるかテスト
            Assert.IsNull(error);
            var errorMessage = "An error has occured";
            subject.OnError(new Exception(errorMessage));

            Assert.IsNotNull(error);
            Assert.AreEqual(errorMessage, error.Message);
        }
    }
}
