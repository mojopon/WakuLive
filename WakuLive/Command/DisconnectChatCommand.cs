using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WakuLive.Controller;

namespace WakuLive.Command
{
    public class DisconnectChatCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        private WakuLiveClientController _controller;

        public DisconnectChatCommand(WakuLiveClientController controller) 
        {
            _controller = controller;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            string id = (string)parameter;

            if (!string.IsNullOrEmpty(id))
            {
                _controller.CloseChannel(id);
            }
            else 
            {
                Debug.Print("CloseTabContentCommand: Id is null.");
            }
        }
    }
}
