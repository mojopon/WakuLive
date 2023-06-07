using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WakuLive.Controller;

namespace WakuLive.Command
{
    public class ToggleAutoScrollCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        private string _chatId;
        private LiveStreamController _controller;
        public ToggleAutoScrollCommand(string chatId, LiveStreamController controller)
        {
            _chatId = chatId;
            _controller = controller;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            var flag = (bool)parameter;

            _controller.ToggleAutoScroll(_chatId, flag);
        }
    }
}
