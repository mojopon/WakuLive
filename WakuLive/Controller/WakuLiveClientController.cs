﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WakuLive.Configuration;
using WakuLive.Interface;
using WakuLive.Presenter;

namespace WakuLive.Controller
{
    public class WakuLiveClientController
    {
        private IWakuLiveClient _wakuLiveClient;
        private IWakuLiveConfiguration _wakuLiveConfiguration;
        private LiveStreamController _liveStreamChatController;
        public WakuLiveClientController(IWakuLiveClient wakuLiveClient,
                                        IWakuLiveConfiguration wakuLiveConfiguration,
                                        LiveStreamController liveStreamChatController) 
        {
            _wakuLiveClient = wakuLiveClient;
            _wakuLiveConfiguration = wakuLiveConfiguration;
            _liveStreamChatController = liveStreamChatController;
        }

        public void OpenChannel(string userName, string channelName, string accessToken) 
        {
            _liveStreamChatController.OpenChannel(userName, channelName, _wakuLiveConfiguration.Account.TwitchAccessToken.Value);
        }

        public void CloseChannel(string id) 
        {
            _liveStreamChatController.CloseChannel(id);
        }

        public void OpenMainWindow() 
        {
            _wakuLiveClient.OpenMainWindow();
        }

        public void OpenConfigurationWindow() 
        {
            _wakuLiveClient.OpenConfigurationWindow();
        }

        public void ExitApplication() 
        {
            _wakuLiveClient.ExitApplication();
        }
    }
}
