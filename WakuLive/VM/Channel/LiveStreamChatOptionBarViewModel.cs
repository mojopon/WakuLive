using Livet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WakuLive.VM
{
    public class LiveStreamChatOptionBarViewModel : ViewModel
    {
        private string _chatId;
        private bool _enableSpeechChat;
        private bool _enableScrollChatToBottom;

        public bool EnableSpeechChat 
        { 
            get { return _enableSpeechChat; }
            set 
            {
                _enableSpeechChat = value;
                RaisePropertyChanged();
            }
        }

        public bool EnableScrollChatToBottom 
        {
            get { return _enableScrollChatToBottom; }
            set 
            {
                _enableScrollChatToBottom = value;
                RaisePropertyChanged();
            }
        }

        public void SetChatId(string id) 
        {
            _chatId = id;
        }
    }
}
