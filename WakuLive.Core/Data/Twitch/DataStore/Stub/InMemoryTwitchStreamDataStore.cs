using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using WakuLive.Core.Data.Twitch.Interface;
using WakuLive.Core.Domain;

namespace WakuLive.Core.Data
{
    public class InMemoryTwitchStreamDataStore : ITwitchStreamDataStore
    {
        public TwitchStreamEntity GetTwitchStream(string id, string channelName, string accessToken)
        {
            var disposables = new CompositeDisposable();
            var subject = new Subject<TwitchStreamInformationEntity>();
            var intervalDiposable = Observable.Interval(TimeSpan.FromSeconds(10))
                                              .StartWith(0)
                                              .Select(x => GetStreamInformation(channelName, accessToken, ex =>
                                              {
                                                  if (!subject.IsDisposed)
                                                  {
                                                      subject.OnError(ex);
                                                  }
                                              }))
                                              .Merge()
                                              .Subscribe(x =>
                                              {
                                                  subject.OnNext(x);
                                              });

            var completeOnDispose = Disposable.Create(() =>
            {
                subject.OnCompleted();
            });

            disposables.Add(intervalDiposable);
            disposables.Add(completeOnDispose);
            disposables.Add(subject);

            var entity = new TwitchStreamEntity(id, subject, disposables);
            return entity;
        }

        public IObservable<TwitchStreamInformationEntity> GetStreamInformation(string channelName, string accessToken, Action<Exception> onError)
        {
            return Observable.Timer(TimeSpan.FromMilliseconds(100))
                             .Select(x => CreateChannelInformation());
        }

        private int viewerCount = 114;
        private TwitchStreamInformationEntity CreateChannelInformation()
        {
            var data = new TwitchStreamInformationEntityData()
            {
                BroadcasterName = "Test User",
                ViewerCount = viewerCount++,
                Title = "Test Channel",
            };

            Debug.Print("InMemoryTwitchStreamRepository: CreateChannelInformation");

            var entity = new TwitchStreamInformationEntity(data);
            return entity;
        }
    }
}
