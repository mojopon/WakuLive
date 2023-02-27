using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Reactive.Threading.Tasks;
using System.Text;
using System.Threading.Tasks;
using TwitchLib.Api;
using TwitchLib.Api.Helix.Models.Channels.GetChannelInformation;
using TwitchLib.Api.Helix.Models.Streams.GetStreams;
using WakuLive.Core.Domain;
using WakuLive.Core.Domain.Twitch.Utility;

namespace WakuLive.Core.Data.DataStore
{
    public class TwitchStreamDataStore : IDisposable
    {
        private readonly int _getInterval = 15; 
        private TwitchAPI _api = new TwitchAPI();
        private CompositeDisposable _disposables = new CompositeDisposable();
        public TwitchStreamDataStore()
        {
            _api.Settings.ClientId = "4wimbgsvg8axlcriswl3g7cv8120vc";
        }

        public TwitchStreamEntity ConnectStream(string id, string channelName, string accessToken) 
        {
            var subject = new Subject<TwitchStreamInformationEntity>();
            var intervalDiposable = Observable.Interval(TimeSpan.FromSeconds(_getInterval))
                                              .StartWith(0)
                                              .Do(x => Debug.Print("Connect Stream"))
                                              .Select(x => GetStreamInformation(channelName, accessToken))
                                              .Merge()
                                              .Subscribe(x => 
                                              {
                                                  subject.OnNext(x);
                                              });

            var completeOnDispose = Disposable.Create(() => 
                                              {
                                                  subject.OnCompleted();
                                              });

            _disposables.Add(intervalDiposable);
            _disposables.Add(completeOnDispose);
            _disposables.Add(subject);

            var entity = new TwitchStreamEntity(id, subject);
            return entity;
        }

        private string CreateId(string channelName) 
        {
            return TwitchEntityId.Create(channelName);
        }

        public IObservable<TwitchStreamInformationEntity> GetStreamInformation(string channelName, string accessToken)
        {
            IObservable<GetStreamsResponse> sourceObservable = _api.Helix.Streams.GetStreamsAsync(null, 20, null, null, null, new List<string> { channelName }, accessToken).ToObservable();
            IObservable<TwitchStreamInformationEntity> observable = sourceObservable.Where(x => (x.Streams != null && x.Streams.Length > 0))
                                                                                    .Select(x =>
                                                                                    {
                                                                                        var information = x.Streams.First();
                                                                                        var data = new TwitchStreamInformationEntityData()
                                                                                        {
                                                                                            ViewerCount = information.ViewerCount,
                                                                                            BroadcasterName = information.UserName,
                                                                                            BroadcasterId = information.UserId,
                                                                                            Title = information.Title,
                                                                                            GameId = information.GameId,
                                                                                            GameName = information.GameName,
                                                                                            ThumbnailUrl = information.ThumbnailUrl,
                                                                                        };
                                                                                        return new TwitchStreamInformationEntity(data);
                                                                                    });

            return observable;
        }

        public void Dispose()
        {
            Debug.Print("TwitchStreamDataStore: Disposed.");
            _disposables?.Dispose();
        }
    }
}
