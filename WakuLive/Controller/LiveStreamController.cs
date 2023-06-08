using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WakuLive.Configuration;
using WakuLive.Core.Domain;
using WakuLive.Presenter;
using WakuLive.Utility;

namespace WakuLive.Controller
{
    public class LiveStreamController
    {
        private IWakuLiveConfiguration _wakuLiveConfiguration;
        private ITwitchChatInteractor    _twitchChatInteractor;
        private ITwitchChannelInteractor  _twitchStreamInteractor;
        private ILiveStreamPresenter _liveStreamPresenter;
        private LiveStreamChatListBoxesPresenter _liveStreamChatListBoxesPresenter;
        private StatusBarController _statusBarController;
        private TextToSpeechController _textToSpeechController;
        public LiveStreamController(IWakuLiveConfiguration wakuLiveConfiguration,
                                    ITwitchChatInteractor    twitchChatInteractor,
                                    ITwitchChannelInteractor  twitchStreamInteractor,
                                    ILiveStreamPresenter liveStreamPresenter,
                                    LiveStreamChatListBoxesPresenter liveStreamChatListBoxesPresenter,
                                    StatusBarController statusBarController,
                                    TextToSpeechController textToSpeechController) 
        {
            _wakuLiveConfiguration = wakuLiveConfiguration;
            _twitchChatInteractor    = twitchChatInteractor;
            _twitchStreamInteractor  = twitchStreamInteractor;
            _liveStreamPresenter = liveStreamPresenter;
            _liveStreamChatListBoxesPresenter = liveStreamChatListBoxesPresenter;
            _statusBarController = statusBarController;
            _textToSpeechController = textToSpeechController;
        }

        public void OpenChannel(LiveStreamServiceType serviceType, string channelName)
        {
            var userName = "user";
            var accessToken = "";

            switch (serviceType) 
            {
                case LiveStreamServiceType.None: 
                    {
                        return;
                    }
                case LiveStreamServiceType.Twitch: 
                    {
                        accessToken = _wakuLiveConfiguration.Account.TwitchAccessToken.Value;
                        break;
                    }
            }

            if (string.IsNullOrEmpty(accessToken)) 
            {
                _statusBarController.OnError("認証がされてません。Twitch配信を開くために設定画面から認証を行ってください。");
                return;
            }

            var connectTwitchChatInput = new ConnectTwitchChatInput(userName, channelName, accessToken);
            var connectTwitchChatOutput = _twitchChatInteractor.ConnectTwitchChat(connectTwitchChatInput);
            var chatModel = connectTwitchChatOutput.ChatModel;

            var connectTwitchStreamInput = new ConnectTwitchChannelInput(channelName, accessToken);
            var connectTwitchStreamOutput = _twitchStreamInteractor.ConnectTwitchChannel(connectTwitchStreamInput);
            var channelModel = connectTwitchStreamOutput.ChannelModel;

            if (chatModel == null || channelModel == null)
            {
                return;
            }

            var id = chatModel.Id;

            _liveStreamPresenter.SetModels(id, chatModel, channelModel);
            _statusBarController.AddChannelModel(id, channelModel);
            _textToSpeechController.StartSpeech(id, chatModel);
        }

        public void CloseChannel(string id) 
        {
            var disconnectTwitchChatInput = new DisconnectTwitchChatInput(id);
            _twitchChatInteractor.DisconnectTwitchChat(disconnectTwitchChatInput);

            var disconnectTwitchChannelInput = new DisconnectTwitchChannelInput(id);
            _twitchStreamInteractor.DisconnectTwitchChannel(disconnectTwitchChannelInput);

            _liveStreamPresenter.DeleteModels(id);
            _statusBarController.DeleteChannelModel(id);
            _textToSpeechController.StopSpeech(id);
        }

        public void ToggleAutoScroll(string id, bool flag) 
        {
            _liveStreamChatListBoxesPresenter.ToggleAutoScroll(id, flag);
        }
    }
}
