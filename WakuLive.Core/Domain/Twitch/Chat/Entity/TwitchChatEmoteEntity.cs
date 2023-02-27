using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WakuLive.Core.Domain
{
    public class TwitchChatEmoteEntity
    {
        public string Id { get; private set; }
        public int StartIndex { get; private set; }
        public int EndIndex { get; private set; }
        public string Name { get; private set; }
        public string ImageUrl { get; private set; }

        public TwitchChatEmoteEntity(string id, int startIndex, int endIndex, string name, string imageUrl) 
        {
            Id = id;
            StartIndex = startIndex;
            EndIndex = endIndex;
            Name = name;
            ImageUrl = imageUrl;
        }
    }
}
