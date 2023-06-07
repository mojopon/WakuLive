using Livet;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WakuLive.VM
{
    public class LiveStreamChatOptionBarViewModel : ViewModel
    {
        private string _chatId;
        private bool _enableSpeechChat = true;
        private bool _enableScrollChatToBottom = true;

        public bool EnableSpeechChat 
        { 
            get { return _enableSpeechChat; }
            set 
            {
                _enableSpeechChat = value;
                Debug.Print("Enable Speech Chat:" + _enableSpeechChat);
                RaisePropertyChanged();
            }
        }

        public bool EnableScrollChatToBottom 
        {
            get { return _enableScrollChatToBottom; }
            set 
            {
                _enableScrollChatToBottom = value;
                Debug.Print("Enable Scroll Chat:" + _enableScrollChatToBottom);
                RaisePropertyChanged();
            }
        }

        public void SetChatId(string id) 
        {
            _chatId = id;
        }
    }
}
