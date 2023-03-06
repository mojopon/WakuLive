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
    public class TwitchChannelDataStoreForTest : ITwitchChannelDataStore
    {
        private TwitchChannelInformationEntity _prevInformation;
        private Dictionary<string, Subject<TwitchChannelInformationEntity>> subjectDict = new Dictionary<string, Subject<TwitchChannelInformationEntity>>();

        public TwitchChannelEntity GetTwitchChannel(string id, string channelName, string accessToken)
        {
            var subject = new Subject<TwitchChannelInformationEntity>();
            subjectDict.Add(id, subject);
            var disposable = Disposable.Create(() =>
            {
                subject.Dispose();
                subjectDict.Remove(id);
            });
            return new TwitchChannelEntity(id, subject, disposable);
        }

        public void SendChannelInformation(string id, TwitchChannelInformationEntityData data) 
        {
            var entity = new TwitchChannelInformationEntity(data);
            subjectDict[id].OnNext(entity);
        }
    }
}
