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

namespace WakuLive.Core.Test.Data
{
    public class TwitchStreamDataStoreForTest : ITwitchStreamDataStore
    {
        private TwitchStreamInformationEntity _prevInformation;
        private Dictionary<string, Subject<TwitchStreamInformationEntity>> subjectDict = new Dictionary<string, Subject<TwitchStreamInformationEntity>>();

        public TwitchStreamEntity GetTwitchStream(string id, string channelName, string accessToken)
        {
            var subject = new Subject<TwitchStreamInformationEntity>();
            subjectDict.Add(id, subject);
            var disposable = Disposable.Create(() =>
            {
                subject.Dispose();
                subjectDict.Remove(id);
            });
            return new TwitchStreamEntity(id, subject, disposable);
        }

        public void SendStreamInformation(string id, TwitchStreamInformationEntityData data) 
        {
            var entity = new TwitchStreamInformationEntity(data);
            subjectDict[id].OnNext(entity);
        }
    }
}
