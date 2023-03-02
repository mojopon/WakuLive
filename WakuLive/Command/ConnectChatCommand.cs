using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TwitchLib.Api.ThirdParty.UsernameChange;
using WakuLive.Controller;
using WakuLive.Utility;

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
            var url = (string)parameter;
            if (!string.IsNullOrEmpty(url)) 
            {
                var parseResult = LiveStreamUrlParser.ParseLiveStreamUrl(url);
                _wakuLiveClientController.OpenChannel(parseResult.LiveStreamServiceType, parseResult.ChannelName);
            }
        }
    }
}
