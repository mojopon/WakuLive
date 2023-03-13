using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WakuLive.VM;

namespace WakuLive.View.Channel.DataTemplateSelector
{
    public static class LiveStreamChatListBoxItemDataTemplates
    {
        public static DataTemplate GetDataTemplateForUserComment(LiveStreamChatListBoxItemViewModel vm) 
        {
            var dataTemplate = new DataTemplate();

            var wrapPanelFactory = new FrameworkElementFactory(typeof(WrapPanel));

            var nameTextBlock = new FrameworkElementFactory(typeof(TextBlock));
            nameTextBlock.SetValue(TextBlock.TextProperty, vm.Name);

            var separatorTextBlock = new FrameworkElementFactory(typeof(TextBlock));
            separatorTextBlock.SetValue(TextBlock.TextProperty, ": ");

            var commentTextBlock = new FrameworkElementFactory(typeof(TextBlock));
            commentTextBlock.SetValue(TextBlock.TextProperty, vm.Comment);

            wrapPanelFactory.AppendChild(nameTextBlock);
            wrapPanelFactory.AppendChild(separatorTextBlock);
            wrapPanelFactory.AppendChild(commentTextBlock);

            dataTemplate.VisualTree = wrapPanelFactory;
            return dataTemplate;
        }
    }
}
