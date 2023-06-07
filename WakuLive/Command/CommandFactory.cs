using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WakuLive.Controller;

namespace WakuLive.Command
{
    public class CommandFactory : ICommandFactory
    {
        private WakuLiveClientController _wakuLiveClientController;
        private TwitchConfigurationController _accountConfigurationController;
        private TextToSpeechController _textToSpeechController;

        public CommandFactory(WakuLiveClientController wakuLiveClientController,
                              TwitchConfigurationController accountConfigurationController,
                              TextToSpeechController textToSpeechController) 
        {
            _wakuLiveClientController = wakuLiveClientController;
            _accountConfigurationController = accountConfigurationController;
            _textToSpeechController = textToSpeechController;
        }

        public DisconnectChatCommand GetDisconnectChatCommand() 
        {
            return new DisconnectChatCommand(_wakuLiveClientController);
        }

        public ConnectChatCommand GetConnectChatCommand() 
        {
            return new ConnectChatCommand(_wakuLiveClientController);
        }

        public OpenConfigurationWindowCommand GetOpenConfigurationWindowCommand()
        {
            return new OpenConfigurationWindowCommand(_wakuLiveClientController);
        }

        public LinkTwitchAccountCommand GetLinkTwitchAccountCommand()
        {
            return new LinkTwitchAccountCommand(_accountConfigurationController);
        }

        public UnlinkTwitchAccountCommand GetUnlinkTwitchAccountCommand()
        {
            return new UnlinkTwitchAccountCommand(_accountConfigurationController);
        }

        public ToggleSpeechCommand GetToggleSpeechCommand(string id) 
        {
            return new ToggleSpeechCommand(id, _textToSpeechController);
        }
    }
}
