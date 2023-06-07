using Livet;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WakuLive.Command;

namespace WakuLive.VM
{
    public class LiveStreamChatOptionBarViewModel : ViewModel
    {
        private string _chatId;
        private bool _enableSpeechChat = true;
        private bool _enableScrollChatToBottom = true;
        private ToggleSpeechCommand _toggleSpeechCommand;
        private ToggleAutoScrollCommand _toggleAutoScrollCommand;

        public bool EnableSpeechChat 
        { 
            get { return _enableSpeechChat; }
            set 
            {
                _enableSpeechChat = value;
                _toggleSpeechCommand?.Execute(_enableSpeechChat);
                RaisePropertyChanged();
            }
        }

        public bool EnableScrollChatToBottom 
        {
            get { return _enableScrollChatToBottom; }
            set 
            {
                _enableScrollChatToBottom = value;
                _toggleAutoScrollCommand?.Execute(_enableScrollChatToBottom);
                RaisePropertyChanged();
            }
        }

        public void SetChatId(string id) 
        {
            _chatId = id;
        }

        public void SetToggleSpeechCommand(ToggleSpeechCommand toggleSpeechCommand) 
        {
            _toggleSpeechCommand = toggleSpeechCommand;
        }

        public void SetToggleAutoScrollCommand(ToggleAutoScrollCommand toggleAutoScrollCommand) 
        {
            _toggleAutoScrollCommand = toggleAutoScrollCommand;
        }
    }
}
