using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WakuLive.Core.Domain.Twitch.Chat.Model;

namespace WakuLive.Core.Domain
{
    public class ChatMessageModel
    {
        public string DisplayName { get; private set; }
        public string Message { get; private set; }
        public string UserId { get; private set; }
        public string UserName { get; private set; }
        public Color Color { get; private set; }

        public ChatMessageModel(ChatMessageModelData data) 
        {
            UserName = data.UserName;
            DisplayName = data.DisplayName;
            Message = data.Message;
            Color = data.Color;
        }

        public override string ToString()
        {
            var name = string.IsNullOrEmpty(DisplayName) ? UserName : string.Format("{0}({1})", DisplayName, UserName);
            return string.Format("Name:{0}, Message:{1}", name, Message);
        }
    }
}
