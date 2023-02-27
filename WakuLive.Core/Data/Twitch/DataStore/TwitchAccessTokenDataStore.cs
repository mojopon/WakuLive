using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;
using WakuLive.Core.Infrastructure;

namespace WakuLive.Core.Data
{
    public class TwitchAccessTokenDataStore
    {
        private CompositeDisposable _disposables;
        private AccessTokenReceiver _receiver;
        private Subject<string> _subject;

        public TwitchAccessTokenDataStore() { }

        /// <summary>
        /// AccessTokenを取得するIObservableを生成する。二度目以降のリクエストは、前回のリクエストに関する処理を全てCancel(Dispose)してから行う。
        /// </summary>
        /// <returns></returns>
        public IObservable<string> GetAccessTokenObservable()
        {
            _disposables?.Dispose();

            _disposables = new CompositeDisposable();
            _receiver = new AccessTokenReceiver();
            _subject = new Subject<string>();

            _disposables.Add(_receiver);
            _disposables.Add(_subject);
            _disposables.Add(_receiver.AccessTokenObservable.Subscribe(OnGetAccessToken));

            _receiver.Start();
            Debug.Print("Start receiving an access token.");

            return _subject;
        }

        private void OnGetAccessToken(string accessToken)
        {
            // 取得したTwitchLiveStreamServiceTokenをストリームに流す。
            _subject.OnNext(accessToken);
            _subject.OnCompleted();

            _disposables?.Dispose();
            _disposables = null;
        }
    }
}
