using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WakuLive.Core.Domain.Twitch.Chat.Model
{
    public class ChatEmoteModel
    {

        public string Id { get; private set; }
        public int StartIndex { get; private set; }
        public int EndIndex { get; private set; }
        public string Name { get; private set; }
        public string ImageUrl { get; private set; }

        public ChatEmoteModel(string id, int startIndex, int endIndex, string name, string imageUrl)
        {
            Id = id;
            StartIndex = startIndex;
            EndIndex = endIndex;
            Name = name;
            ImageUrl = imageUrl;
        }
    }
}
