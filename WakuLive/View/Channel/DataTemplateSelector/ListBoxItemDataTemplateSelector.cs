using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WakuLive.View.Channel.DataTemplateSelector;
using WakuLive.VM;

namespace WakuLive.View
{
    public class ListBoxItemDataTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var vm = (LiveStreamChatListBoxItemViewModel)item;
            var dataTemplate = LiveStreamChatListBoxItemDataTemplates.GetDataTemplateForUserComment(vm);
            return dataTemplate;
        }
    }
}
