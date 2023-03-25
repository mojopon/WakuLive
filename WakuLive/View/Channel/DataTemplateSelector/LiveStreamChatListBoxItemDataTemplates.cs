using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
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

            var comment = new FrameworkElementFactory(typeof(Run));
            comment.SetValue(Run.TextProperty, vm.Comment);

            textBlock.AppendChild(name);
            textBlock.AppendChild(separator);
            textBlock.AppendChild(comment);

            dataTemplate.VisualTree = textBlock;
            return dataTemplate;
        }
    }
}
