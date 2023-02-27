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
    public class InMemoryTwitchAuthRepository : ITwitchAuthRepository
    {
        public readonly static string ValidUserToken = "your token";

        public IObservable<string> GetAccessToken()
        {
            var subject = new Subject<string>();
            Observable.Timer(TimeSpan.FromSeconds(1))
                      .Subscribe(x =>
                      {
                          subject.OnNext(ValidUserToken);
                          subject.OnCompleted();
                      });
            

            return subject;
        }
    }
}
