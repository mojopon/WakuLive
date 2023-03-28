using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using WakuLive.VM;

namespace WakuLive.View
{
    /// <summary>
    /// Interaction logic for LiveStreamChatTabUserControl.xaml
    /// </summary>
    public partial class LiveStreamChannelTab : UserControl
    {
        public LiveStreamChannelTab()
        {
            InitializeComponent();

            this.DataContextChanged += (s, e) =>
            {
                if (e.OldValue is LiveStreamChannelTabViewModel)
                {
                    var vm = e.OldValue as LiveStreamChannelTabViewModel;
                    vm.ShowLatestTab = null;
                }
                if (e.NewValue is LiveStreamChannelTabViewModel)
                {
                    var vm = e.NewValue as LiveStreamChannelTabViewModel;
                    vm.ShowLatestTab = ShowLatestTab;
                }
            };
        }

        private void ShowLatestTab() 
        {
            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.ContextIdle, () =>
            {
                ChannelTab.SelectedIndex = ChannelTab.Items.Count - 1;
            });
        }
    }
}
