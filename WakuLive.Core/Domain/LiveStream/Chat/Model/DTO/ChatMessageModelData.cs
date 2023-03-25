using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WakuLive.Core.Domain.Twitch.Chat.Model;

namespace WakuLive.Core.Domain
{
    public class ChatMessageModelData
    {
        public string DisplayName { get; set; }
        public List<ChatEmoteModel> Emotes { get; set; }
        public string Message { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public UserType UserType { get; set; }
        public Color Color { get; set; }
    }
}
