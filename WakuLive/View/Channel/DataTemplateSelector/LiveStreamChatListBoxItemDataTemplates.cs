using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WakuLive.VM;

namespace WakuLive.View.Channel.DataTemplateSelector
{
    public static class LiveStreamChatListBoxItemDataTemplates
    {
        private static readonly Double FontSize = 13.0;
        private static readonly Double IconSize = 22.0;

        public static DataTemplate GetDataTemplateForUserComment(LiveStreamChatListBoxItemViewModel vm) 
        {
            var dataTemplate = new DataTemplate();

            var textBlock = new FrameworkElementFactory(typeof(TextBlock));
            textBlock.SetValue(TextBlock.TextWrappingProperty, TextWrapping.Wrap);

            var name = new FrameworkElementFactory(typeof(Run));
            name.SetValue(Run.TextProperty, vm.Name);
            name.SetValue(Run.ForegroundProperty, Brushes.DarkGreen);
            name.SetValue(Run.FontWeightProperty, FontWeights.DemiBold);
            name.SetValue(Run.FontSizeProperty, FontSize);

            var separator = new FrameworkElementFactory(typeof(Run));
            separator.SetValue(Run.TextProperty, ": ");
            separator.SetValue(Run.FontSizeProperty, FontSize);

            textBlock.AppendChild(name);
            textBlock.AppendChild(separator);

            var position = 0;
            var str = vm.Comment;

            // たまにsubstringに失敗する（原因は不明）なので念のためtry-catchしておく
            // 原因を究明してtry-catchしなくていいようにする
            try
            {
                if (vm.Emotes.Count == 0)
                {
                    var run = CreateRun(str);
                    textBlock.AppendChild(run);
                }
                else
                {
                    for (int i = 0; i < vm.Emotes.Count; i++)
                    {
                        var emote = vm.Emotes[i];

                        // エモートのStartIndex以前の文字を切り出して挿入
                        if (position != emote.StartIndex)
                        {
                            var text = str.Substring(position, emote.StartIndex - position);
                            var run = CreateRun(text);
                            textBlock.AppendChild(run);
                            position = emote.StartIndex;
                        }

                        // エモートの画像挿入
                        var inlineUiContainer = new FrameworkElementFactory(typeof(InlineUIContainer));
                        var image = new FrameworkElementFactory(typeof(Image));
                        var fullFilePath = emote.ImageUrl;
                        if (!string.IsNullOrEmpty(fullFilePath))
                        {
                            image.SetValue(Image.SourceProperty, new BitmapImage(new Uri(fullFilePath, UriKind.Absolute)));
                            image.SetValue(Image.WidthProperty, IconSize);
                            image.SetValue(Image.HeightProperty, IconSize);
                            inlineUiContainer.AppendChild(image);
                            textBlock.AppendChild(inlineUiContainer);
                        }

                        // EmoteのIdの文字数だけポジションを進める
                        position = position + emote.Name.Length;

                        // 最後のエモート以降の文字を切り出して挿入
                        if (i + 1 == vm.Emotes.Count)
                        {
                            var text = str.Substring(position, str.Length - position);
                            var run = CreateRun(text);
                            textBlock.AppendChild(run);
                        }
                    }
                }
            }
            catch (Exception ex) 
            {
                Debug.Print("Error, Name" + vm.Name + ", Comment:" + vm.Comment);
            }

            dataTemplate.VisualTree = textBlock;
            return dataTemplate;
        }

        private static FrameworkElementFactory CreateRun(string text) 
        {
            var run = new FrameworkElementFactory(typeof(Run));
            run.SetValue(Run.TextProperty, text);
            run.SetValue(Run.FontSizeProperty, FontSize);
            return run;
        }
    }
}
