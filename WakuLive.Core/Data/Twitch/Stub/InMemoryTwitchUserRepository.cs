using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using WakuLive.Core.Domain;

namespace WakuLive.Core.Data.Stub
{
    public class InMemoryTwitchUserRepository : ITwitchUserRepository
    {
        public IObservable<TwitchUserEntity> GetUser(string userName, string accessToken)
        {
            var observable = Observable.Timer(TimeSpan.FromSeconds(1))
                                       .Select(x => ReturnUser());

            return observable;
        }

        private TwitchUserEntity ReturnUser()
        {
            var data = new TwitchUserEntityData()
            {
                BroadcasterId = "1234",
                UserName = "testuser",
                DisplayName = "テスト",
            };
            return new TwitchUserEntity(data);
        }
    }
}
