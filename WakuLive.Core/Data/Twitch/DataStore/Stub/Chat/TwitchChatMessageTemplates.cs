using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WakuLive.Core.Domain;

namespace WakuLive.Core.Data.Stub
{
    public static class TwitchChatMessageTemplates
    {
        public static List<TwitchChatMessageEntityData> ChatMessageTemplateList = new List<TwitchChatMessageEntityData>()
        {
            new TwitchChatMessageEntityData()
            {
                DisplayName = "いぬ",
                UserName = "Dog",
                Message = "こんにちわん",
            },
            new TwitchChatMessageEntityData()
            {
                DisplayName = "ねこ",
                UserName = "cat",
                Message = "にゃ～",
            },
            new TwitchChatMessageEntityData()
            {
                DisplayName = "",
                UserName = "pigeon",
                Message = "ﾎｰﾎｰ ﾎﾎｰ ﾎｰﾎｰ ﾎﾎｰ ﾎｰﾎｰ ﾎﾎｰ ﾎｰﾎｰ ﾎﾎｰ ﾎｰﾎｰ ﾎﾎｰ ﾎ…",
            },
            new TwitchChatMessageEntityData()
            {
                DisplayName = "かものはし",
                Emotes = new List<TwitchChatEmoteEntity>()
                {
                    new TwitchChatEmoteEntity("425618", 4, 6, "LUL", "https://static-cdn.jtvnw.net/emoticons/v1/425618/1.0"),
                    new TwitchChatEmoteEntity("305954156", 12, 19, "PogChamp", "https://static-cdn.jtvnw.net/emoticons/v1/305954156/1.0"),
                },
                UserName = "kamonohasi",
                Message = "ハロー LUL ハロー PogChamp こんにちは",
            },
            new TwitchChatMessageEntityData()
            {
                DisplayName = "おだやかなライオン",
                UserName = "Lion",
                Message = "こんにちは！今日も来ました！",
            },
            new TwitchChatMessageEntityData()
            {
                DisplayName = "狼",
                UserName = "wolf",
                Message = "グルル……",
            },
            new TwitchChatMessageEntityData()
            {
                DisplayName = "",
                UserName = "Giraffe",
                Message = "どうも！首を長くして待ってました",
            },
            new TwitchChatMessageEntityData()
            {
                DisplayName = "話の長い象",
                UserName = "elephant",
                Message = "こんにちは。今日も配信をしてくれてありがとうございます。今日はいい天気ですね。私の鼻もぴんぴん動いて良い気持ちです。日差しがとっても気持ちがいいです。",
            },
            new TwitchChatMessageEntityData()
            {
                DisplayName = "コツメカワウソ",
                UserName = "otter",
                Message = "キュルル！",
            },
            new TwitchChatMessageEntityData()
            {
                DisplayName = "しまうま",
                UserName = "shimauma",
                Message = "しまうまです",
            },
        };
    }
}
