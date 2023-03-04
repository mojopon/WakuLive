using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using WakuLive.Core.Data.DataStore;
using WakuLive.Core.Data.Twitch.Interface;
using WakuLive.Core.Domain;
using WakuLive.Core.Domain.Twitch.Utility;

namespace WakuLive.Core.Data
{
    public class TwitchStreamRepository : ITwitchStreamRepository
    {
        // チャンネル情報更新のインターバル
        private readonly int _getInterval = 10;

        private ITwitchStreamDataStore _dataStore;
        private Dictionary<string, TwitchStreamEntity> _entityDic = new Dictionary<string, TwitchStreamEntity>();

        public TwitchStreamRepository(ITwitchStreamDataStore dataStore) 
        {
            _dataStore = dataStore;
        }

        public TwitchStreamEntity ConnectStream(string channelName, string accessToken)
        {
            var id = CreateId(channelName);
            if (!_entityDic.ContainsKey(id))
            {
                var disposables = new CompositeDisposable();
                var subject = new Subject<TwitchStreamInformationEntity>();
                var intervalDiposable = Observable.Interval(TimeSpan.FromSeconds(_getInterval))
                                                  .StartWith(0)
                                                  .Select(x => _dataStore.GetStreamInformation(channelName, accessToken, ex =>
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
                _entityDic.Add(id, entity);

                return entity;
            }
            else
            {
                return null;
            }
        }

        public void DisconnectStream(string id)
        {
            if (_entityDic.ContainsKey(id))
            {
                _entityDic[id].Dispose();
                _entityDic.Remove(id);
            }
        }

        private string CreateId(string channelName) 
        {
            return TwitchEntityId.Create(channelName);
        }
    }
    
}
