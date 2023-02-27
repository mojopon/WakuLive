using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WakuLive.Core.Domain
{
    public class TwitchChatMessageEntityData
    {
        public List<KeyValuePair<string, string>> BadgeInfo { get; set; } = new List<KeyValuePair<string, string>>();
        public int Bits { get; set; }
        public string Channel { get; set; }
        public Color Color { get; set; }
        public string DisplayName { get; set; }
        public List<TwitchChatEmoteEntity> Emotes { get; set; } = new List<TwitchChatEmoteEntity>();
        public bool IsFirstMessage { get; set; }
        public bool IsHighlighted { get; set; }
        public string Message { get; set; }
        public string RoomId { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public UserType UserType { get; set; }
    }
}
