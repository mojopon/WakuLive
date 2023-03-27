using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media.Imaging;
using WakuLive.VM;

namespace WakuLive.View.Channel.DataTemplateSelector
{
    public static class LiveStreamChatListBoxItemDataTemplates
    {
        public static DataTemplate GetDataTemplateForUserComment(LiveStreamChatListBoxItemViewModel vm) 
        {
            var dataTemplate = new DataTemplate();

            var textBlock = new FrameworkElementFactory(typeof(TextBlock));
            textBlock.SetValue(TextBlock.TextWrappingProperty, TextWrapping.Wrap);

            var name = new FrameworkElementFactory(typeof(Run));
            name.SetValue(Run.TextProperty, vm.Name);

            var separator = new FrameworkElementFactory(typeof(Run));
            separator.SetValue(Run.TextProperty, ": ");

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
                    var run = new FrameworkElementFactory(typeof(Run));
                    run.SetValue(Run.TextProperty, str);
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
                            var run = new FrameworkElementFactory(typeof(Run));
                            var text = str.Substring(position, emote.StartIndex - position);
                            run.SetValue(Run.TextProperty, text);
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
                            image.SetValue(Image.WidthProperty, 20.0);
                            image.SetValue(Image.HeightProperty, 20.0);
                            inlineUiContainer.AppendChild(image);
                            textBlock.AppendChild(inlineUiContainer);
                        }

                        // EmoteのIdの文字数だけポジションを進める
                        position = position + emote.Name.Length;

                        // 最後のエモート以降の文字を切り出して挿入
                        if (i + 1 == vm.Emotes.Count)
                        {
                            var run = new FrameworkElementFactory(typeof(Run));
                            var text = str.Substring(position, str.Length - position);
                            run.SetValue(Run.TextProperty, text);
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
    }
}
