using System;
using System.Collections.Generic;
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
        private TextToSpeechController _textToSpeechController;
        public LiveStreamController(IWakuLiveConfiguration wakuLiveConfiguration,
                                    ITwitchChatInteractor    twitchChatInteractor,
                                    ITwitchChannelInteractor  twitchStreamInteractor,
                                    ILiveStreamPresenter liveStreamPresenter,
                                    TextToSpeechController textToSpeechController) 
        {
            _wakuLiveConfiguration = wakuLiveConfiguration;
            _twitchChatInteractor    = twitchChatInteractor;
            _twitchStreamInteractor  = twitchStreamInteractor;
            _liveStreamPresenter = liveStreamPresenter;
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

            _liveStreamPresenter.AddModels(id, chatModel, channelModel);
            _textToSpeechController.StartSpeech(id, chatModel);
        }

        public void CloseChannel(string id) 
        {
            var disconnectTwitchChatInput = new DisconnectTwitchChatInput(id);
            _twitchChatInteractor.DisconnectTwitchChat(disconnectTwitchChatInput);

            var disconnectTwitchChannelInput = new DisconnectTwitchChannelInput(id);
            _twitchStreamInteractor.DisconnectTwitchChannel(disconnectTwitchChannelInput);

            _liveStreamPresenter.DeleteModels(id);
            _textToSpeechController.StopSpeech(id);
        }
    }
}
