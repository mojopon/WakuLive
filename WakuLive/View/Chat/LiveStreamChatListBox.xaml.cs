using Livet;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using WakuLive.VM;

namespace WakuLive.View
{
    /// <summary>
    /// Interaction logic for LiveStreamChatLog.xaml
    /// </summary>
    public partial class LiveStreamChatListBox : UserControl
    {
        public LiveStreamChatListBox()
        {
            InitializeComponent();

            this.DataContextChanged += (s, e) =>
            {
                if (e.OldValue is LiveStreamChatListBoxViewModel)
                {
                    var vm = e.OldValue as LiveStreamChatListBoxViewModel;
                    vm.ScrollToBottom = null;
                }
                if (e.NewValue is LiveStreamChatListBoxViewModel)
                {
                    var vm = e.NewValue as LiveStreamChatListBoxViewModel;
                    vm.ScrollToBottom = ScrollToBottom;
                }
            };
        }

        private void ScrollToBottom() 
        {
            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.ContextIdle, () =>
            {
                var peer = ItemsControlAutomationPeer.CreatePeerForElement(ListBox);
                var scrollProvider = peer.GetPattern(PatternInterface.Scroll) as IScrollProvider;
                if (scrollProvider != null && scrollProvider.VerticallyScrollable)
                {
                    scrollProvider.SetScrollPercent(scrollProvider.HorizontalScrollPercent, 100);
                }
            });
        }
    }
}
