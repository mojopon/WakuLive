using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TwitchLib.Api.ThirdParty.UsernameChange;
using WakuLive.Controller;

namespace WakuLive.Command
{
    public class ConnectChatCommand : ICommand
    {
        private WakuLiveClientController _wakuLiveClientController;
        public ConnectChatCommand(WakuLiveClientController wakuLiveClientController)
        {
            _wakuLiveClientController = wakuLiveClientController;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            throw new NotImplementedException();
        }

        public void Execute(object? parameter)
        {
            var parameters = (ConnectChatCommandParameters)parameter;
            if (parameters != null) 
            {
                _wakuLiveClientController.OpenChannel(parameters.UserName, parameters.ChannelName, parameters.AccessToken);
            }
        }

        public class ConnectChatCommandParameters 
        {
            public string UserName { get; set; }
            public string ChannelName { get; set; }
            public string AccessToken { get; set; }

            public ConnectChatCommandParameters(string userName, string channelName, string accessToken) 
            {
                UserName = userName;
                ChannelName = channelName;
                AccessToken = accessToken;
            }
        }
    }
}
