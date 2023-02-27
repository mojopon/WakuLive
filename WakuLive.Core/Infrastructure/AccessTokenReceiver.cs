using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Reactive.Disposables;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace WakuLive.Core.Infrastructure
{
    /// <summary>
    /// ブラウザからlocalhost経由でアクセストークンの取得を行うためのサーバーの起動・管理および取得したアクセストークンの通知を行うクラス
    /// </summary>
    public class AccessTokenReceiver : IDisposable
    {
        private readonly int _port = 59237;

        private CompositeDisposable _compositeDisposable = new CompositeDisposable();
        private HttpListener _listener = new HttpListener();
        private Subject<string> _subject = new Subject<string>();

        public IObservable<string> AccessTokenObservable => _subject;

        public AccessTokenReceiver()
        {
            _compositeDisposable.Add(_listener);
            _compositeDisposable.Add(_subject);
        }

        public void Start()
        {
            _listener.Prefixes.Add("http://localhost:" + _port.ToString() + "/");
            _listener.Start();
            Receive();
        }

        public void Stop()
        {
            _listener.Stop();
        }

        private void Receive()
        {
            if (_listener.IsListening)
            {
                _listener.BeginGetContext(new AsyncCallback(ListenerCallback), _listener);
            }
        }

        private void ListenerCallback(IAsyncResult result)
        {
            if (_listener.IsListening)
            {
                var context = _listener.EndGetContext(result);

                var response = context.Response;
                response.StatusCode = (int)HttpStatusCode.OK;
                response.ContentType = "text/html";

                byte[] buffer = Encoding.UTF8.GetBytes(CreateResponseHtml());
                response.OutputStream.Write(buffer, 0, buffer.Length);
                response.OutputStream.Close();

                OnGetContext(context);

                Receive();
            }
        }

        private void OnGetContext(HttpListenerContext context)
        {
            // RequestにAccessTokenのクエリが含まれてないか調べる。
            var request = context.Request;
            Debug.Print($"{request.HttpMethod} {request.Url}");

            if (!string.IsNullOrEmpty(request.QueryString["access_token"]))
            {
                OnGetAccessToken(request.QueryString["access_token"]);
            }
        }

        private void OnGetAccessToken(string accessToken)
        {
            _subject.OnNext(accessToken);
        }

        private string CreateResponseHtml()
        {
            return $@"
                <!DOCTYPE html><html><head></head><body></body>
                  <script>
                    var hash = location.hash;
                    if(hash.length > 0) 
                    {{
                      var params = hash.split('&');     
                      if(params.length > 0 && params[0].match(/#access_token/)) 
                      {{
                        var token = params[0].split('=')[1];
                        window.location.replace(`http://localhost:{_port}/?access_token=` + token);
                      }}
                    }}
                    else 
                  {{
                    window.location.replace(`about:blank`);
                  }}
                  </script>
                </html>
            ";
        }

        public void Dispose()
        {
            Stop();
            _compositeDisposable.Dispose();
        }
    }
}
