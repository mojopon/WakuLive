using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchLib.Client.Models;
using WakuLive.Core.Domain.Twitch.Utility;

namespace WakuLive.Core.Test.Data
{
    public class TwitchLibUtilitiesTest
    {
        [Test]
        public void ConvertEmotesTest() 
        {
            var emotesSource = new Emote[]
            {
                new Emote("425618", "LUL", 9, 11),
                new Emote("305954156", "PogChamp", 17, 24),
            };
            var emoteSet = new EmoteSet(emotesSource, "emotes");
            var emotes = TwitchLibUtilities.ConvertEmotes(emoteSet);

            for(int i = 0; i < emotes.Count; i++)
            {
                Assert.That(emotes[i].Id, Is.EqualTo(emoteSet.Emotes[i].Id));
                Assert.That(emotes[i].Name, Is.EqualTo(emoteSet.Emotes[i].Name));
                Assert.That(emotes[i].StartIndex, Is.EqualTo(emoteSet.Emotes[i].StartIndex));
                Assert.That(emotes[i].EndIndex, Is.EqualTo(emoteSet.Emotes[i].EndIndex));
            }
        }
    }
}
