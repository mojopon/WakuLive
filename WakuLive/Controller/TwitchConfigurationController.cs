using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WakuLive.Core.Data;
using WakuLive.Core.Domain;
using WakuLive.Interface;
using WakuLive.Presenter;
using WakuLive.View;

namespace WakuLive.Controller
{
    public class TwitchConfigurationController
    {
        private IWakuLiveClient _wakuLiveClient;
        private ITwitchAuthInteractor _twitchAuthInteractor;
        private TwitchConfigurationTabItemPresenter _twitchConfigurationTabItemPresenter;

        public TwitchConfigurationController(IWakuLiveClient wakuLiveClient,
                                              ITwitchAuthInteractor twitchAuthInteractor,
                                              TwitchConfigurationTabItemPresenter twitchConfigurationTabItemPresenter)
        {
            _wakuLiveClient = wakuLiveClient;
            _twitchAuthInteractor = twitchAuthInteractor;
            _twitchConfigurationTabItemPresenter = twitchConfigurationTabItemPresenter;
        }

        private IDisposable _twitchAccessTokenDisposable;
        public void LinkTwitchAccount() 
        {
            _twitchAccessTokenDisposable?.Dispose();
            var output = _twitchAuthInteractor.GetTwitchAccessToken(new GetTwitchAccessTokenInput());
            _twitchAccessTokenDisposable = output.AccessTokenObservable
                                                 .Subscribe(x =>
                                                 {
                                                     _wakuLiveClient.AddTwitchAccessToken(x);
                                                 });

            UrlOpener.Open("https://id.twitch.tv/oauth2/authorize?client_id=4wimbgsvg8axlcriswl3g7cv8120vc&redirect_uri=http://localhost:59237&response_type=token&scope=channel:manage:broadcast%20channel:moderate%20chat:read%20chat:edit");
        }

        public void UnlinkTwitchAccount() 
        {
            _wakuLiveClient.RemoveTwitchAccessToken();
        }
    }
}
