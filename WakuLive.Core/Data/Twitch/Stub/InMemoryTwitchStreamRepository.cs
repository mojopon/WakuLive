using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using WakuLive.Core.Domain;

namespace WakuLive.Core.Data.Stub
{
    public class InMemoryTwitchStreamRepository : ITwitchStreamRepository
    {
        private Dictionary<string, IDisposable> _disposablesDic = new Dictionary<string, IDisposable>();

        public TwitchStreamEntity ConnectStream(string channelName, string accessToken)
        {
            var id = CreateId(channelName);
            if (!_disposablesDic.ContainsKey(id))
            {
                var compositeDisposable = new CompositeDisposable();
                var subject = new Subject<TwitchStreamInformationEntity>();
                var intervalDisposable = Observable.Interval(TimeSpan.FromSeconds(1))
                                                   .Subscribe(x =>
                                                   {
                                                       subject.OnNext(CreateChannelInformation());
                                                   });

                var completeOnDispose = Disposable.Create(() => 
                                                     {
                                                         subject.OnCompleted();
                                                     });

                compositeDisposable.Add(completeOnDispose);
                compositeDisposable.Add(intervalDisposable);
                compositeDisposable.Add(subject);

                _disposablesDic.Add(id, compositeDisposable);

                var entity = new TwitchStreamEntity(id, subject);
                return entity;
            }
            else
            {
                return null;
            }
        }

        public void DisconnectStream(string id)
        {
            if (_disposablesDic.ContainsKey(id)) 
            {
                _disposablesDic[id].Dispose();
                _disposablesDic.Remove(id);
            }
        }

        private string CreateId(string channelName) 
        {
            return "InMemory-" + channelName;
        }

        public IObservable<TwitchStreamInformationEntity> GetStreamInformation(string channelName, string accessToken)
        {
            var observable = Observable.Timer(TimeSpan.FromSeconds(1))
                                       .Select(x => CreateChannelInformation());

            return observable;
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
