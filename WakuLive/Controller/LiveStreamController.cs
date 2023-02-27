using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WakuLive.Core.Domain;
using WakuLive.Presenter;

namespace WakuLive.Controller
{
    public class LiveStreamController
    {
        private ITwitchChatInteractor    _twitchChatInteractor;
        private ITwitchStreamInteractor  _twitchStreamInteractor;
        private ILiveStreamPresenter _liveStreamPresenter;
        private TextToSpeechController _textToSpeechController;
        public LiveStreamController(ITwitchChatInteractor    twitchChatInteractor,
                                    ITwitchStreamInteractor  twitchStreamInteractor,
                                    ILiveStreamPresenter liveStreamPresenter,
                                    TextToSpeechController textToSpeechController) 
        {
            _twitchChatInteractor    = twitchChatInteractor;
            _twitchStreamInteractor  = twitchStreamInteractor;
            _liveStreamPresenter = liveStreamPresenter;
            _textToSpeechController = textToSpeechController;
        }

        public void OpenChannel(string userName, string channelName, string accessToken)
        {
            var connectTwitchChatInput = new ConnectTwitchChatInput(userName, channelName, accessToken);
            var connectTwitchChatOutput = _twitchChatInteractor.ConnectTwitchChat(connectTwitchChatInput);
            var chatModel = connectTwitchChatOutput.ChatModel;

            var connectTwitchStreamInput = new ConnectTwitchStreamInput(channelName, accessToken);
            var connectTwitchStreamOutput = _twitchStreamInteractor.ConnectTwitchStream(connectTwitchStreamInput);
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

            var disconnectTwitchChannelInput = new DisconnectTwitchStreamInput(id);
            _twitchStreamInteractor.DisconnectTwitchStream(disconnectTwitchChannelInput);

            _liveStreamPresenter.DeleteModels(id);
            _textToSpeechController.StopSpeech(id);
        }
    }
}
