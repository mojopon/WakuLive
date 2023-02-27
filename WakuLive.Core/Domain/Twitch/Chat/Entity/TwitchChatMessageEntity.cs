using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WakuLive.Core.Domain
{
    public class TwitchChatMessageEntity
    {
        public List<KeyValuePair<string, string>> BadgeInfo { get; set; } = new List<KeyValuePair<string, string>>();
        public int Bits { get; set; }
        public string Channel { get; set; }
        public Color Color { get; private set; }
        public string DisplayName { get; private set; }
        public List<TwitchChatEmoteEntity> Emotes { get; private set; } = new List<TwitchChatEmoteEntity>();
        public bool IsFirstMessage { get; set; }
        public bool IsHighlighted { get; set; }
        public string Message { get; private set; }
        public string RoomId { get; private set; }
        public string UserId { get; private set; }
        public string UserName { get; private set; }
        public UserType UserType { get; private set; }

        public TwitchChatMessageEntity(TwitchChatMessageEntityData data)
        {
            BadgeInfo = data.BadgeInfo;
            Bits = data.Bits;
            Channel = data.Channel;
            Color = data.Color;
            DisplayName = data.DisplayName;
            Emotes = data.Emotes;
            IsFirstMessage = data.IsFirstMessage;
            IsHighlighted = data.IsHighlighted;
            Message = data.Message;
            UserId = data.UserId;
            UserName = data.UserName;
        }

        public override string ToString()
        {
            var name = string.IsNullOrEmpty(DisplayName) ? UserName : string.Format("{0}({1})", DisplayName, UserName);
            return string.Format("Name:{0}, Message:{1}", name, Message);
        }
    }
}
