using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WakuLive.Core.Domain.Twitch.Chat.Model;

namespace WakuLive.VM
{
    public class LiveStreamChatListBoxItemViewModel
    {
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public Color Color { get; set; }
        public string Comment { get; set; }
        public List<ChatEmoteModel> Emotes { get; set; }
    }
}
